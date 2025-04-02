using UnityEngine;

namespace NTween.Internal {

    internal abstract class UpdateTimingSingletonSO<TSystem> : ScriptableObject
        where TSystem : UpdateTimingSingletonSO<TSystem> {

        /// <summary>
        /// 更新タイミング．
        /// </summary>
        public UpdateTiming Timing { get; private set; }

        /// <summary>
        /// インスタンスが生成されたときの処理．
        /// </summary>
        protected abstract void OnCreate(UpdateTiming timing);

        /// <summary>
        /// アプリが終了する時の処理．
        /// This is to handle EnterPlayMode.
        /// </summary>
        private void OnQuit() {
            Application.quitting -= OnQuit;
            if (Application.isPlaying) {
                Destroy(this);
            } else {
                DestroyImmediate(this);
            }
        }


        /// ----------------------------------------------------------------------------
        #region Static

        /// <summary>
        /// <see cref="UpdateTiming"/>の各タイミングをサポートするためのインスタンス．
        /// </summary>
        private static readonly TSystem[] Instance = new TSystem[3];

        /// <summary>
        /// インスタンスが生成済みか確認する．
        /// </summary>
        public static bool IsCreated(UpdateTiming timing) => Instance[(int)timing] != null;

        /// <summary>
        /// 指定したタイミングのインスタンスを取得する．まだ生成されていない場合は，生成する．
        /// </summary>
        /// <param name="timing">更新タイミング．</param>
        /// <returns>インスタンス．</returns>
        public static TSystem GetInstance(UpdateTiming timing) {
            var index = (int)timing;
            if (IsCreated(timing)) return Instance[index];

            var instance = CreateInstance<TSystem>();
            instance.Timing = timing;
            instance.OnCreate(timing);
            Application.quitting += instance.OnQuit;

            Instance[index] = instance;
            return instance;
        }
        #endregion
    }
}
