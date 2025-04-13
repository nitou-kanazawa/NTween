using System;
using System.Collections.Generic;
using UnityEngine;

namespace NTween {

    internal interface ITweenStorage {
        bool IsActive(TweenHandle handle);
        bool IsPlaying(TweenHandle handle);
        void Reset();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    internal sealed class TweenStorage<TValue> : ITweenStorage {

        private const int INITIAL_CAPACITY = 16;
        private readonly List<Tween> _tweens = new List<Tween>(capacity: INITIAL_CAPACITY);

        public int Id { get; }
        public int Count => _tweens.Count;

        public IEnumerable<Tween> Tweens => _tweens;


        /// ----------------------------------------------------------------------------
        // Public Method

        public TweenStorage(int id) {
            Id = id;
        }

        public TweenHandle Create(ref TweenBuilder<TValue> builder) {

            throw new NotImplementedException();
        }

        public bool IsActive(TweenHandle handle) {
            throw new NotImplementedException();
        }

        public bool IsPlaying(TweenHandle handle) {
            throw new NotImplementedException();
        }

        public void Reset() {
            throw new NotImplementedException();
        }
    }
}
