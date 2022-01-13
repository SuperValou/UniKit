using System;
using UnityEngine;

namespace Packages.UniKit.Runtime.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Returns the component of type TComponent attached to the game object, or throws an exception if it's missing.
        /// </summary>
        /// <typeparam name="TComponent">Type of component to get.</typeparam>
        /// <param name="gameObj">The game object.</param>
        /// <returns>The component.</returns>
        public static TComponent GetOrThrow<TComponent>(this GameObject gameObj)
        {
            if (ReferenceEquals(gameObj, null))
            {
                throw new ArgumentNullException(nameof(gameObj));
            }

            var component = gameObj.GetComponent<TComponent>();
            if (component == null)
            {
                throw new ArgumentException($"Missing '{typeof(TComponent).Name}' component on '{gameObj}' game object.");
            }

            return component;
        }
    }
}