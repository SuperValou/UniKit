using System;
using UnityEngine.Events;

namespace Packages.UniKit.Runtime.Events
{
    /// <summary>
    /// Unity event where callbacks must take a float parameter.
    /// </summary>
    [Serializable]
    public class UnityFloatEvent : UnityEvent<float>
    {
    }
}