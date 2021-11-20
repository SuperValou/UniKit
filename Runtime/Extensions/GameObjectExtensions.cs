using System;
using UnityEngine;

namespace Packages.UniKit.Runtime.Extensions
{
    public static class GameObjectExtensions
    {
        public static TComponent GetOrThrow<TComponent>(this GameObject gameObj)
            where TComponent : Component
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

        public static bool IsOnLayer(this GameObject gameObject, LayerMask layerMask)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            int gameObjectLayerMask = 1 << gameObject.layer;
            int overlap = gameObjectLayerMask & layerMask.value;
            return overlap != 0;
        }
    }
}