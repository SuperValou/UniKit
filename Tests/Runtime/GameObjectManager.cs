using System.Collections.Generic;
using UnityEngine;

namespace Packages.UniKit.Tests.Runtime
{
    public class GameObjectManager
    {
        private readonly List<GameObject> _gameObjects = new List<GameObject>();

        public GameObject Instantiate(string name)
        {
            var gameObj = new GameObject(name);
            _gameObjects.Add(gameObj);
            return gameObj;
        }

        public void DestroyAll()
        {
            foreach (var gameObject in _gameObjects)
            {
                Object.DestroyImmediate(gameObject);
            }
        }
    }
}