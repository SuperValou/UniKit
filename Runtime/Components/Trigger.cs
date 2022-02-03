using System.Collections.Generic;
using Packages.UniKit.Runtime.Events;
using UnityEngine;

namespace Packages.UniKit.Runtime.Components
{
    public class Trigger : MonoBehaviour
    {
        // -- Inspector

        [Header("Values")]
        [Tooltip("If non-empty, only game objects having one these tags will activate the trigger.")]
        [SerializeField]
        private List<string> _triggeringTags = new List<string>();

        /// <summary>
        /// Fired when a game object enters the trigger.
        /// </summary>
        [Header("Events")]
        public UnityColliderEvent onTriggerEnter = new UnityColliderEvent();

        /// <summary>
        /// Fired when a game object exits the trigger.
        /// </summary>
        public UnityColliderEvent onTriggerExit = new UnityColliderEvent();


        // -- Class

        public IList<string> TriggeringTags => _triggeringTags;

        void Start()
        {
            if (this.TryGetComponent(out Collider col) && !col.isTrigger)
            {
                Debug.LogWarning($"{this} has a collider, but the collider is not set to Trigger.");
            }
        }

        void OnTriggerEnter(Collider col)
        {
            if (col == null)
            {
                return;
            }

            if (_triggeringTags.Count > 0 && !_triggeringTags.Contains(col.gameObject.tag))
            {
                return;
            }

            onTriggerEnter.Invoke(col);
        }

        void OnTriggerExit(Collider col)
        {
            if (col == null)
            {
                return;
            }
            
            if (_triggeringTags.Count > 0 && !_triggeringTags.Contains(col.gameObject.tag))
            {
                return;
            }

            onTriggerExit.Invoke(col);
        }
    }
}