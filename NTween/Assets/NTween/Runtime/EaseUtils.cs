using UnityEngine;
using Unity.Mathematics;

namespace NTween {

    public static class EaseUtils {

        private const float PI = math.PI;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ease"></param>
        /// <returns></returns>
        public static float Evaluate(float t, Ease ease) {
            return ease switch {
                Ease.InSine => InSine(t),
                Ease.OutSine => OutSine(t),
                Ease.InOutSine => InOutSine(t),
                Ease.InQuad => InQuad(t),
                Ease.OutQuad => OutQuad(t),
                Ease.InOutQuad => InOutQuad(t),
                Ease.InCubic => InCubic(t),
                Ease.OutCubic => OutCubic(t),
                Ease.InOutCubic => InOutCubic(t),
                Ease.InQuart => InQuart(t),
                Ease.OutQuart => OutQuart(t),
                Ease.InOutQuart => InOutQuart(t),
                Ease.InQuint => InQuint(t),
                Ease.OutQuint => OutQuint(t),
                Ease.InOutQuint => InOutQuint(t),
                Ease.InExpo => InExpo(t),
                Ease.OutExpo => OutExpo(t),
                Ease.InOutExpo => InOutExpo(t),
                Ease.InCirc => InCirc(t),
                Ease.OutCirc => OutCirc(t),
                Ease.InOutCirc => InOutCirc(t),
                Ease.InElastic => InElastic(t),
                Ease.OutElastic => OutElastic(t),
                Ease.InOutElastic => InOutElastic(t),
                Ease.InBack => InBack(t),
                Ease.OutBack => OutBack(t),
                Ease.InOutBack => InOutBack(t),
                Ease.InBounce => InBounce(t),
                Ease.OutBounce => OutBounce(t),
                Ease.InOutBounce => InOutBounce(t),
                Ease.Custom => 1,
                _ => t,
            };
        }


        #region Quad

        public static float InQuad(float t) {
            return t * t;
        }

        public static float OutQuad(float t) {
            return 1 - (1 - t) * (1 - t);
        }

        public static float InOutQuad(float t) {
            return t < 0.5f
                ? 2 * t * t
                : 1 - (math.pow(-2 * t + 2, 2) * 0.5f);
        }
        #endregion

        #region Cubic


        public static float InCubic(float t) {
            return t * t * t;
        }

        public static float OutCubic(float t) {
            return 1 - math.pow(1 - t, 3);
        }

        public static float InOutCubic(float t) {
            return t < 0.5f
                ? 4 * t * t * t
                : 1 - math.pow(-2 * t + 2, 3) * 0.5f;
        }
        #endregion

        #region Quart

        public static float InQuart(float t) {
            return t * t * t * t;
        }

        public static float OutQuart(float t) {
            return 1 - math.pow(1 - t, 4);
        }

        public static float InOutQuart(float t) {
            return t < 0.5f
                ? 8 * t * t * t * t
                : 1 - math.pow(-2 * t + 2, 4) * 0.5f;
        }
        #endregion

        #region Quint

        public static float InQuint(float t) {
            return t * t * t * t * t;
        }

        public static float OutQuint(float t) {
            return 1 - math.pow(1 - t, 5);
        }

        public static float InOutQuint(float t) {
            return t < 0.5f
                ? 16 * t * t * t * t * t
                : 1 - math.pow(-2 * t + 2, 5) * 0.5f;
        }
        #endregion

        #region Expo

        public static float InExpo(float t) {
            return t == 0
                ? 0
                : math.pow(2, 10 * (t - 1));
        }

        public static float OutExpo(float t) {
            return t == 1
                ? 1
                : -math.pow(2, -10 * t) + 1;
        }

        public static float InOutExpo(float t) {
            if (t == 0) return 0;
            if (t == 1) return 1;
            return t < 0.5f
                ? math.pow(2, 20 * t - 10) * 0.5f
                : (2 - math.pow(2, -20 * t + 10)) * 0.5f;
        }
        #endregion

        #region Circle

        public static float InCirc(float t) {
            return 1 - math.sqrt(1 - math.pow(t, 2));
        }

        public static float OutCirc(float t) {
            return math.sqrt(1 - math.pow(t - 1, 2));
        }

        public static float InOutCirc(float t) {
            return t < 0.5f
                ? (1 - math.sqrt(1 - math.pow(2 * t, 2))) * 0.5f
                : (math.sqrt(1 - math.pow(-2 * t + 2, 2)) + 1) * 0.5f;
        }
        #endregion

        #region Sin

        public static float InSine(float t) {
            return 1f - math.cos((t * PI) * 0.5f);
        }

        public static float OutSine(float t) {
            return math.sin((t * PI) * 0.5f);
        }

        public static float InOutSine(float t) {
            return -(math.cos(t * PI) - 1) * 0.5f;
        }
        #endregion

        #region Elastic

        public static float InElastic(float t) {
            if (t == 0) return 0;
            if (t == 1) return 1;
            return -math.sin(7.5f * PI * t) * math.pow(2, 10 * (t - 1));
        }

        public static float OutElastic(float t) {
            if (t == 0) return 0;
            if (t == 1) return 1;
            return math.sin(-7.5f * PI * (t + 1)) * math.pow(2, -10 * t) + 1;
        }

        public static float InOutElastic(float t) {
            if (t == 0) return 0;
            if (t == 1) return 1;
            return t < 0.5f
                ? 0.5f * math.sin(7.5f * PI * (2 * t)) * math.pow(2, 10 * (2 * t - 1))
                : 0.5f * (math.sin(-7.5f * PI * (2 * t - 1 + 1)) * math.pow(2, -10 * (2 * t - 1)) + 2);
        }
        #endregion

        #region Back

        public static float InBack(float t) {
            return t * t * t - t * math.sin(t * PI);
        }

        public static float OutBack(float t) {
            return 1 - (math.pow(1 - t, 3) - (1 - t) * math.sin((1 - t) * PI));
        }

        public static float InOutBack(float t) {
            if (t < 0.5f) {
                float f = 2 * t;
                return 0.5f * (f * f * f - f * math.sin(f * PI));
            } else {
                float f = 1 - (2 * t - 1);
                return 0.5f * (1 - (f * f * f - f * math.sin(f * PI))) + 0.5f;
            }
        }
        #endregion

        #region Bounce

        public static float InBounce(float t) {
            return 1 - OutBounce(1 - t);
        }

        public static float OutBounce(float t) {
            if (t < 1 / 2.75f) {
                return 7.5625f * t * t;
            } else if (t < 2 / 2.75f) {
                return 7.5625f * (t -= 1.5f / 2.75f) * t + 0.75f;
            } else if (t < 2.5 / 2.75f) {
                return 7.5625f * (t -= 2.25f / 2.75f) * t + 0.9375f;
            } else {
                return 7.5625f * (t -= 2.625f / 2.75f) * t + 0.984375f;
            }
        }

        public static float InOutBounce(float t) {
            return t < 0.5f
                ? (1 - InBounce(1 - 2 * t)) * 0.5f
                : (1 + OutBounce(2 * t - 1)) * 0.5f;
        }
        #endregion
    }
}
