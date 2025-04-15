using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;
using UnityEngine.Profiling;

namespace NTween.Internal {


    /// <summary>
    /// <see cref="Tween"/>�I�u�W�F�N�g�̃f�B�X�p�b�`��S���N���X�D
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

            // PlayerLoop�ɃR�[���o�b�N�o�^
            var loop = PlayerLoop.GetCurrentPlayerLoop();
            {
                var earlyUpdateSystem = new PlayerLoopSystem {
                    updateDelegate = OnUpdate,
                    type = typeof(TweenDispatcher)
                };

                // ���ΏۃC���f�b�N�X���������C�؍\���ɑ}������
                var type = TweenDispatcher.GetTimingType(timing);
                var index = Array.FindIndex(loop.subSystemList, c => c.type == type);
                var list = new List<PlayerLoopSystem>(loop.subSystemList[index].subSystemList);
                list.Add(earlyUpdateSystem);    // ���Ō�ɒǉ�
                loop.subSystemList[index].subSystemList = list.ToArray();
            }
            PlayerLoop.SetPlayerLoop(loop);
        }

        /// <summary>
        /// Callback when the class is destroyed.
        /// </summary>
        private void OnDestroy() {
            var timing = GetTimingType(Timing);

            // PlayerLoop����R�[���o�b�N�o�^����
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
        /// <see cref="Tween"/>��o�^����D
        /// </summary>
        /// <param name="tween">The component to register</param>
        private void RegisterInternal(Tween tween) {
            var index = _tweens.Count;
            _tweens.Add(tween);

            tween.Index = index;
        }

        /// <summary>
        /// <see cref="Tween"/>��o�^��������D
        /// </summary>
        /// <param name="tween">The component to unregister</param>
        /// <param name="removeIndex">The ID of the element to be removed</param>
        private void UnregisterInternal(Tween tween, out int removeIndex) {

            removeIndex = tween.Index;
            if (removeIndex == -1) return;

            // �C���f�b�N�X�X�V
            var lastIndex = _tweens.Count - 1;
            _tweens[removeIndex] = _tweens[lastIndex];
            _tweens.RemoveAt(lastIndex);
            _tweens[removeIndex].Index = removeIndex;
            tween.Index = -1;
        }



        /// ----------------------------------------------------------------------------
        #region Static

        /// <summary>
        /// <see cref="Tween"/>�����s���X�g�ɓo�^����D
        /// </summary>
        /// <param name="tween"></param>
        /// <param name="timing"></param>
        public static void Register(Tween tween, UpdateTiming timing) {
            if (tween.IsRegistered) return;

            var instance = GetInstance(timing);
            instance.RegisterInternal(tween);
        }

        /// <summary>
        /// <see cref="Tween"/>�����s���X�g����o�^��������D
        /// </summary>
        /// <param name="tween"></param>
        /// <param name="timing"></param>
        public static void Unregister(Tween tween, UpdateTiming timing) {
            if (!IsCreated(timing) || !tween.IsRegistered) return;

            var instance = GetInstance(timing);
            instance.UnregisterInternal(tween, out var removedIndex);
        }

        /// <summary>
        /// <see cref="PlayerLoop"/>�̑Ή�����^���擾����D
        /// </summary>
        /// <param name="timing">�X�V�^�C�~���O�D</param>
        /// <returns>�Ή�����^�D</returns>
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
