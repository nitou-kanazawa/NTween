using UnityEngine;

namespace NTween {
    
    /// <summary>
    /// 
    /// </summary>
    public static partial class NTween {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from">�J�n�l�D</param>
        /// <param name="to">�I���l�D</param>
        /// <param name="duration">�p�����ԁD</param>
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
