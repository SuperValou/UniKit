using System;
using UnityEngine;
using UnityEngine.Events;

namespace Packages.UniKit.Runtime.Events
{
    /// <summary>
    /// Unity event where callbacks must take a Collider parameter.
    /// </summary>
    [Serializable]
    public class UnityColliderEvent : UnityEvent<Collider>
    {
    }
}