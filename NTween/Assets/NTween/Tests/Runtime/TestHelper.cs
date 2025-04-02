using UnityEngine;

namespace NTween.Tests {

    public static class TestHelper {

        public static void SetupScene() {

            // Camera
            var camera = new GameObject("Main Camera").AddComponent<Camera>();
            camera.transform.position = new Vector3(0, 0, -7);
            camera.orthographic = false;
            camera.clearFlags = CameraClearFlags.Skybox;

            // Light
            var light = new GameObject("Light").AddComponent<Light>();
            light.type = LightType.Directional;
            light.transform.eulerAngles = new Vector3(50, -30, 0);
        }
    }
}
