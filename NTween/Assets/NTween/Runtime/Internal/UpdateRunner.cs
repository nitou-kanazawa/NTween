using System;

namespace NTween {

    internal interface IUpdateRunner {
        ITweenStorage Storage { get; }
        public void Update(double time, double unscaledTime, double realTime);
        public void Reset();
    }

    internal sealed class UpdateRunner<TValue> : IUpdateRunner {

        private readonly TweenStorage<TValue> _storage;

        private double prevTime;
        private double prevUnscaledTime;
        private double prevRealtime;


        public TweenStorage<TValue> Storage => _storage;
        ITweenStorage IUpdateRunner.Storage => _storage;




        /// ----------------------------------------------------------------------------
        // Public Method

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
        }

        public void Reset() {
            prevTime = 0;
            prevUnscaledTime = 0;
            prevRealtime = 0;
            _storage.Reset();
        }
    }
}
