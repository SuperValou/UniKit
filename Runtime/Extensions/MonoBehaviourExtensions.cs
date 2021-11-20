using System;
using UnityEngine;

namespace Packages.UniKit.Runtime.Extensions
{
    public static class MonoBehaviourExtensions
    {
        public static TComponent GetOrThrow<TComponent>(this MonoBehaviour monoBehaviour)
            where TComponent : Component
        {
            if (ReferenceEquals(monoBehaviour, null))
            {
                throw new ArgumentNullException(nameof(monoBehaviour));
            }

            return GameObjectExtensions.GetOrThrow<TComponent>(monoBehaviour?.gameObject);
        }
    }
}