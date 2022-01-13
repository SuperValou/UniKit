using System;
using UnityEngine;

namespace Packages.UniKit.Runtime.Extensions
{
    public static class GameObjectExtensions
    {
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