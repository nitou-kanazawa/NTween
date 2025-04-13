using System;

namespace NTween {

    /// <summary>
    /// 
    /// </summary>
    public delegate void TweenCallback();

    /// <summary>
    /// 
    /// </summary>
    public delegate void TweenCallback<in T>(T value);
}
