using System;
using UnityEngine;
using NTween.Internal;

namespace NTween {

    public class Tween {
        public int Index { get; internal set; }
        public bool IsRegistered {
            get => Index > 0;
        }

        public UpdateTiming Timing { get; protected set; } = UpdateTiming.Update;
        
        // Time
        protected float _duration;
        protected float _elapsedTime;

        // 
        protected bool _isPlaying;


        /// ----------------------------------------------------------------------------

        internal Tween(float duration) {
            _duration = duration;
            _elapsedTime = 0f;
            _isPlaying = true;
        }

        internal virtual void OnUpdate() { }

        // Tweenを破棄
        public void Kill() {
            if (IsRegistered) {
                TweenDispatcher.Unregister(this, Timing);
            }
        }


        /// ----------------------------------------------------------------------------
        #region Static

        // Tweenを生成
        public static FloatTween To(Func<float> getter, Action<float> setter, float endValue, float duration) {
            var tween = new FloatTween(getter, setter, endValue, duration);
            tween._onComplete = t => t.Kill();

            TweenDispatcher.Register(tween, tween.Timing);
            return tween;
        }

        #endregion
    }



    public class FloatTween : Tween {

        // Properity
        private Action<float> _setter;

        // Value
        private float _startValue;
        private float _endValue;

        internal Action<FloatTween> _onComplete;


        /// ----------------------------------------------------------------------------

        internal FloatTween(Func<float> getter, Action<float> setter, float endValue, float duration) 
            : base(duration){
            _setter = setter;
            
            _startValue = getter();
            _endValue = endValue;
        }

        internal override void OnUpdate() {
            if (!_isPlaying) return;

            _elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(_elapsedTime / _duration);

            _setter(Evaluate());

            if (_elapsedTime >= _duration) {
                _isPlaying = false;
                _onComplete?.Invoke(this);
            }

        }

        // 進行状況に応じた補間を計算
        public float Evaluate() {
            _elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(_elapsedTime / _duration); // 進行割合

            // Easeを使って補間値を取得
            float easeValue = EaseUtils.Evaluate(t, Ease.Linear);

            return Mathf.Lerp(_startValue, _endValue, easeValue);
        }
    };

}
