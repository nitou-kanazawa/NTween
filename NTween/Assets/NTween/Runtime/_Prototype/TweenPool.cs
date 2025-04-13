using UnityEngine;
using UnityEngine.Pool;

namespace NTween {

    public interface ITweenPool {

    }

    public  class TweenPool<TValue> {

        private static readonly ObjectPool<Tween<TValue>> _pool;


        static TweenPool() {
            _pool = new ObjectPool<Tween<TValue>> (
                createFunc: () => new Tween<TValue>(),
                actionOnRelease: buffer => buffer.Reset(),
                defaultCapacity: 10,    // ‰Šú—e—Ê
                maxSize: 100            // Å‘å•Û”
             );
        }

    }
}
