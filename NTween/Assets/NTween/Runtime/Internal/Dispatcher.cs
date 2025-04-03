using UnityEngine;

namespace NTween {

    public static class Dispatcher {


        private static class StorageCache<TValue> {

            private static TweenStorage<TValue> _storage;

            public static TweenStorage<TValue> GetOrCreate() {
                if(_storage == null) {
                    _storage = new TweenStorage<TValue>(TweenManager.TweenTypeCount);
                    TweenManager.Register(_storage);
                }
                return _storage;
            }
        }



        internal static TweenHandle Schedule<TValue>(ref TweenBuilder<TValue> builder) {

            var storage = StorageCache<TValue>.GetOrCreate();
        }

    }
}
