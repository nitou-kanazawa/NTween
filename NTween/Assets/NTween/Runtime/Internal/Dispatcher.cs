using UnityEngine;

namespace NTween {

    public static class Dispatcher {


        private static class StorageCache<TValue> {

            private static TweenStorage<TValue> _storage;

            public static TweenStorage<TValue> GetOrCreate() {
                if(_storage == null) {
                    //_storage = new TweenStorage<TValue>(TweenManager.TweenTypeCount);
                    //TweenManager.Register(_storage);
                }
                return _storage;
            }
        }

        private static class RunnerCache<TValue> {

            private static UpdateRunner<TValue> _runner;

            public static UpdateRunner<TValue> GEtOrCreate() {
                if(_runner == null) {

                }
                return _runner;
            }

        }


        internal static TweenHandle Schedule<TValue>(ref TweenBuilder<TValue> builder) {

            var storage = StorageCache<TValue>.GetOrCreate();

            throw new System.NotImplementedException();
        }

    }
}
