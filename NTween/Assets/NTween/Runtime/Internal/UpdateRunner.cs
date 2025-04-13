using System;

namespace NTween {

    internal interface IUpdateRunner {
        ITweenStorage Storage { get; }
        public void Update(double time, double unscaledTime, double realTime);
        public void Reset();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    internal sealed class UpdateRunner<TValue> : IUpdateRunner {

        private readonly TweenStorage<TValue> _storage;

        private double prevTime;
        private double prevUnscaledTime;
        private double prevRealtime;


        public TweenStorage<TValue> Storage => _storage;
        ITweenStorage IUpdateRunner.Storage => _storage;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="time"></param>
        /// <param name="unscaledTime"></param>
        /// <param name="realtime"></param>
        public UpdateRunner(TweenStorage<TValue> storage, 
            double time, double unscaledTime, double realtime) {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            prevTime = time;
            prevUnscaledTime = unscaledTime;
            prevRealtime = realtime;
        }

        public void Update(double time, double unscaledTime, double realtime) {

            // 
            var deltaTime = time - prevTime;
            var unscaledDeltaTime = unscaledTime - prevUnscaledTime;
            var realDeltaTime = realtime - prevRealtime;
            prevTime = time;
            prevUnscaledTime = unscaledTime;
            prevRealtime = realtime;

            // 
            foreach(var tween in _storage.Tweens) {
                tween.OnUpdate();

            }

        }

        public void Reset() {
            prevTime = 0;
            prevUnscaledTime = 0;
            prevRealtime = 0;
            _storage.Reset();
        }
    }
}
