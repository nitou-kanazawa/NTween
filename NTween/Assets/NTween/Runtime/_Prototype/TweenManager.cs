using System;
using System.Collections.Generic;
using UnityEngine;

namespace NTween {

    internal static class TweenManager {

        private static List<ITweenStorage> _storages;

        public static int TweenTypeCount { get; private set; }

        public static void Register<TValue>(TweenStorage<TValue> storage) {
            _storages.Add(storage);
            TweenTypeCount++;
        }


        public static void Complete(Tween tween) {
            throw new NotImplementedException();
        }
    }
}
