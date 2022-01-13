using System;
using UnityEngine;

namespace Packages.UniKit.Runtime.Extensions
{
    public static class MonoBehaviourExtensions
    {
        /// <summary>
        /// Returns the component of type TComponent attached to this game object, or throws an exception if it's missing.
        /// </summary>
        /// <typeparam name="TComponent">Type of component to get.</typeparam>
        /// <param name="monoBehaviour">The monobehaviour.</param>
        /// <returns>The component.</returns>
        public static TComponent GetOrThrow<TComponent>(this MonoBehaviour monoBehaviour)
        {
            if (ReferenceEquals(monoBehaviour, null))
            {
                throw new ArgumentNullException(nameof(monoBehaviour));
            }

            return GameObjectExtensions.GetOrThrow<TComponent>(monoBehaviour?.gameObject);
        }
    }
}