using System;
using UnityEngine;
using UnityEngine.Pool;

namespace NTween {

    public struct TweenBuilder<TValue> : IDisposable {

        internal ushort _version;
        internal TweenBuilderBuffer<TValue> _buffer;


        /// ----------------------------------------------------------------------------
        // Public Method

        internal TweenBuilder(TweenBuilderBuffer<TValue> buffer) {
            _buffer = buffer;
            _version = buffer.version;
        }

        public void Dispose() {
            if (_buffer == null)
                return;

            TweenBuilderBuffer<TValue>.Return(_buffer);
            _buffer = null;
        }


        #region With

        public readonly TweenBuilder<TValue> WithEase(Ease ease) {
            CheckBuffer();
            _buffer.ease = ease;
            return this;
        }

        public readonly TweenBuilder<TValue> WithDebugName(string debugName) {
#if NTWEEN_DEBUG
            CheckBuffer();
            _buffer.debugName = debugName;
#endif
            return this;
        }
        #endregion


        #region Bind

        public TweenHandle Bind(Action<TValue> action) {
            CheckBuffer();
            SetCallbackData(action);
            return ScheduleTween();
        }

        #endregion


        internal TweenHandle ScheduleTween() {
            TweenHandle handle;

            
        }


        /// ----------------------------------------------------------------------------
        // 

        internal readonly void SetCallbackData(Action<TValue> action) {
            _buffer.stateCount = 0;
            _buffer.updateAction = action;
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        private readonly void CheckBuffer() {
            if (_buffer == null || _buffer.version != _version) 
                throw new InvalidOperationException("MotionBuilder is either not initialized or has already run a Build (or Bind).");
        }
    }


    
}
