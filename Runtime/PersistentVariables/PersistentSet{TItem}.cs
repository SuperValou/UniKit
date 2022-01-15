using System;
using System.Collections.Generic;
using Packages.UniKit.Runtime.Extensions;
using UnityEngine;

namespace Packages.UniKit.Runtime.PersistentVariables
{
    public abstract class PersistentSet<TItem> : ScriptableObject
    {
        private readonly HashSet<TItem> _items = new HashSet<TItem>();
        
        public IReadOnlyCollection<TItem> Items => _items;

        public event Action<TItem> ItemAdded;
        public event Action<TItem> ItemRemoved;

        public void Add(TItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (_items.Contains(item))
            {
                throw new InvalidOperationException($"Item '{item}' cannot be added to '{this.name}' ({nameof(PersistentSet<TItem>)}) " +
                                                    $"because it is already present.");
            }

            _items.Add(item);
            ItemAdded.SafeInvoke(item);
        }

        public bool Contains(TItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return _items.Contains(item);
        }

        public void Remove(TItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!_items.Contains(item))
            {
                throw new InvalidOperationException($"Item '{item}' cannot be removed from '{this.name}' ({nameof(PersistentSet<TItem>)}) " +
                                                    $"because it was not present in the first place. Did you forget a call to the {nameof(Add)} method?");
            }

            _items.Remove(item);
            ItemRemoved.SafeInvoke(item);
        }

        void OnDisable()
        {
            // Clear subscribers
            ItemAdded = null;
            ItemRemoved = null;

            // Clear items
            if (_items.Count == 0)
            {
                return;
            }

            Debug.LogWarning($"{this.name} ({this.GetType().Name}) is being disabled, but {_items.Count} {typeof(TItem).Name}(s) are still referenced. " +
                           $"Did you forgot some calls to the {nameof(Remove)} method?");
            _items.Clear();
        }
    }
}