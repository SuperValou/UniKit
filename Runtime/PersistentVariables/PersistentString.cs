using System;
using Packages.UniKit.Runtime.Extensions;
using UnityEngine;

namespace Packages.UniKit.Runtime.PersistentVariables
{
    [CreateAssetMenu(fileName = nameof(PersistentString), 
        menuName = nameof(UniKit) + "/" + nameof(PersistentVariables) + "/" + nameof(PersistentString))]
    public class PersistentString : ScriptableObject
    {
        [SerializeField]
        private string _value = string.Empty;

        public string Value
        {
            get => _value;
            set => Set(value);
        }

        public event Action<string> ValueChanged;

        public void Set(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (_value == str)
            {
                return;
            }

            _value = str;
            ValueChanged.SafeInvoke(_value);
        }

        void OnDisable()
        {
            // Clear subscribers
            ValueChanged = null;

            // Clear value
            _value = string.Empty;
        }
    }
}