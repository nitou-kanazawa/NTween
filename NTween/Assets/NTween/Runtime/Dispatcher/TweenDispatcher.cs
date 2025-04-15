using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;
using UnityEngine.Profiling;

namespace NTween.Internal {


    /// <summary>
    /// <see cref="Tween"/>オブジェクトのディスパッチを担うクラス．
    /// </summary>
    internal class TweenDispatcher : UpdateTimingSingletonSO<TweenDispatcher> {

        private readonly List<Tween> _tweens = new();


        /// ----------------------------------------------------------------------------
        // Lifecycle Events

        /// <summary>
        /// Callback when the class is created.
        /// </summary>
        /// <param name="timing">The timing</param>
        protected override void OnCreate(UpdateTiming timing) {

            _tweens.Clear();

            // PlayerLoopにコールバック登録
            var loop = PlayerLoop.GetCurrentPlayerLoop();
            {
                var earlyUpdateSystem = new PlayerLoopSystem {
                    updateDelegate = OnUpdate,
                    type = typeof(TweenDispatcher)
                };

                // ※対象インデックスを検索し，木構造に挿入する
                var type = TweenDispatcher.GetTimingType(timing);
                var index = Array.FindIndex(loop.subSystemList, c => c.type == type);
                var list = new List<PlayerLoopSystem>(loop.subSystemList[index].subSystemList);
                list.Add(earlyUpdateSystem);    // ※最後に追加
                loop.subSystemList[index].subSystemList = list.ToArray();
            }
            PlayerLoop.SetPlayerLoop(loop);
        }

        /// <summary>
        /// Callback when the class is destroyed.
        /// </summary>
        private void OnDestroy() {
            var timing = GetTimingType(Timing);

            // PlayerLoopからコールバック登録解除
            var loop = PlayerLoop.GetCurrentPlayerLoop();
            {
                var type = typeof(TweenDispatcher);
                var index = Array.FindIndex(loop.subSystemList, c => c.type == timing);
                var list = new List<PlayerLoopSystem>(loop.subSystemList[index].subSystemList);
                list.RemoveAll(c => c.type == type);
                loop.subSystemList[index].subSystemList = list.ToArray();
            }
            PlayerLoop.SetPlayerLoop(loop);
        }

        /// <summary>
        /// Callback called before the specified timing.
        /// </summary>
        private void OnUpdate() {
            var count = _tweens.Count;
            for (var index = 0; index < count; index++) {
                var tween = _tweens[index];

#if DEBUG
                Profiler.BeginSample(tween.GetType().Name);
#endif
                tween.OnUpdate();
#if DEBUG
                Profiler.EndSample();
#endif

            }
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// <see cref="Tween"/>を登録する．
        /// </summary>
        /// <param name="tween">The component to register</param>
        private void RegisterInternal(Tween tween) {
            var index = _tweens.Count;
            _tweens.Add(tween);

            tween.Index = index;
        }

        /// <summary>
        /// <see cref="Tween"/>を登録解除する．
        /// </summary>
        /// <param name="tween">The component to unregister</param>
        /// <param name="removeIndex">The ID of the element to be removed</param>
        private void UnregisterInternal(Tween tween, out int removeIndex) {

            removeIndex = tween.Index;
            if (removeIndex == -1) return;

            // インデックス更新
            var lastIndex = _tweens.Count - 1;
            _tweens[removeIndex] = _tweens[lastIndex];
            _tweens.RemoveAt(lastIndex);
            _tweens[removeIndex].Index = removeIndex;
            tween.Index = -1;
        }



        /// ----------------------------------------------------------------------------
        #region Static

        /// <summary>
        /// <see cref="Tween"/>を実行リストに登録する．
        /// </summary>
        /// <param name="tween"></param>
        /// <param name="timing"></param>
        public static void Register(Tween tween, UpdateTiming timing) {
            if (tween.IsRegistered) return;

            var instance = GetInstance(timing);
            instance.RegisterInternal(tween);
        }

        /// <summary>
        /// <see cref="Tween"/>を実行リストから登録解除する．
        /// </summary>
        /// <param name="tween"></param>
        /// <param name="timing"></param>
        public static void Unregister(Tween tween, UpdateTiming timing) {
            if (!IsCreated(timing) || !tween.IsRegistered) return;

            var instance = GetInstance(timing);
            instance.UnregisterInternal(tween, out var removedIndex);
        }

        /// <summary>
        /// <see cref="PlayerLoop"/>の対応する型を取得する．
        /// </summary>
        /// <param name="timing">更新タイミング．</param>
        /// <returns>対応する型．</returns>
        private static Type GetTimingType(UpdateTiming timing) {
            return timing switch {
                UpdateTiming.Update => typeof(Update),
                UpdateTiming.FixedUpdate => typeof(FixedUpdate),
                UpdateTiming.LateUpdate => typeof(PostLateUpdate),
                _ => typeof(Update)
            };
        }
        #endregion
    }
}
