using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace NTween.Tests.Runtime {
    
    public class BindTest {

        private const float FLOAT_TOLERANCE = 0.0001f;

        [UnityTest]
        public IEnumerator Bind_LocalVariable() {

            float value = 0f;
            float endValue = 10f;

            NTween.Create(0f, endValue, 1f)
                .Bind(x => value = x);
            yield return new WaitForSeconds(1.1f);

            Assert.That(value, Is.EqualTo(endValue).Within(FLOAT_TOLERANCE));
        }
    }
}
