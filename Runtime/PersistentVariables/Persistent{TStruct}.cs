using System;
using Packages.UniKit.Runtime.Extensions;
using UnityEngine;

namespace Packages.UniKit.Runtime.PersistentVariables
{
    public abstract class Persistent<TStruct> : ScriptableObject
        where TStruct : struct
    {
        [SerializeField]
        private TStruct _value;

        public TStruct Value
        {
            get => _value;
            set => Set(value);
        }

        public event Action<TStruct> ValueChanged;

        public void Set(TStruct value)
        {
            if (_value.Equals(value))
            {
                return;
            }

            _value = value;
            ValueChanged.SafeInvoke(_value);
        }

        void OnDisable()
        {
            // Clear subscribers
            ValueChanged = null;

            // Clear value
            _value = default;
        }
    }
}