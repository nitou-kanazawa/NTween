using UnityEngine;

namespace NTween {
    
    /// <summary>
    /// 
    /// </summary>
    public static partial class NTween {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from">開始値．</param>
        /// <param name="to">終了値．</param>
        /// <param name="duration">継続時間．</param>
        /// <returns></returns>
        public static TweenBuilder<float> Create(float from, float to, float duration) => Create(in from, in to, duration);


        public static TweenBuilder<TValue> Create<TValue>(in TValue from, in TValue to, float duration) {
            var buffer = TweenBuilderBuffer<TValue>.Rent();
            buffer.startValue = from;
            buffer.endValue = to;
            buffer.duration = duration;
            return new TweenBuilder<TValue>(buffer);
        }
    }
}
