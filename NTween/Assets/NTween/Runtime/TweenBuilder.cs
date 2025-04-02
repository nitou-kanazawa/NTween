#if DEVELOPMENT_BUILD || UNITY_EDITOR
#define NTWEEN_DEBUG
#endif

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

            _buffer.ResetValues();
            _buffer = null;
        }


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


        /// ----------------------------------------------------------------------------
        // Private Method

        readonly void CheckBuffer() {
            if (_buffer == null || _buffer.version != _version) 
                throw new InvalidOperationException("MotionBuilder is either not initialized or has already run a Build (or Bind).");
        }
    }


    /// <summary>
    /// Tweenのパラメータを格納するクラス．
    /// インスタンスをObject-Poolで管理することで余計なアロケーションを避ける方針．
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    internal sealed class TweenBuilderBuffer<TValue> {

        public ushort version;

        public TValue startValue;
        public TValue endValue;

        public float duration;
        public float delay;
        public Ease ease;

        // バインド用
        public object state0;
        public object state1;
        public object state2;

        // Callback
        public Action onCompleteAction;

#if NTWEEN_DEBUG
        public string debugName;
#endif


        public void ResetValues() {

            version++;              // なぜインクリメントなのか分からない

            startValue = default;
            endValue = default;

            duration = default;
            delay = default;
            ease = default;

            state0 = default;
            state1 = default;
            state2 = default;

#if NTWEEN_DEBUG
            debugName = default;
#endif
        }


        /// ----------------------------------------------------------------------------
        #region Static

        private static readonly ObjectPool<TweenBuilderBuffer<TValue>> _pool =
            new (
                createFunc: () => new TweenBuilderBuffer<TValue>(),
                actionOnRelease: buffer => buffer.ResetValues(),
                collectionCheck: false, // デフォルトではtrueだが、falseにすることで二重解放のチェックを無効化できる
                defaultCapacity: 10,    // 初期容量
                maxSize: 100            // 最大保持数
            );

        public static TweenBuilderBuffer<TValue> Rent() {
            return _pool.Get();
        }

        public static void Return(TweenBuilderBuffer<TValue> buffer) {
            _pool.Release(buffer);
        }
        #endregion
    }
}
