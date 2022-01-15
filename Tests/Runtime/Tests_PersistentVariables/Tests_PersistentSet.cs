using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Packages.UniKit.Runtime.PersistentVariables;
using UnityEngine;
using UnityEngine.TestTools;

namespace Packages.UniKit.Tests.Runtime.Tests_PersistentVariables
{
    public class Tests_PersistentSet
    {
        private const string StringA = "Hello!";
        private const string StringB = "Hi there.";

        [UnityTest]
        public IEnumerator Add_WITH_ValidValue_SHOULD_AddValue()
        {
            var persistentSet = ScriptableObject.CreateInstance<PersistentStringSet>();

            yield return null;

            persistentSet.Add(StringA);

            yield return null;

            Assert.AreEqual(1, persistentSet.Items.Count);
            Assert.AreEqual(StringA, persistentSet.Items.First());
        }

        [UnityTest]
        public IEnumerator Add_WITH_ValueAlreadyPresent_SHOULD_Throw()
        {
            var persistentSet = ScriptableObject.CreateInstance<PersistentStringSet>();
            persistentSet.Add(StringA);

            yield return null;

            Assert.Throws<InvalidOperationException>(() => persistentSet.Add(StringA));
        }

        [UnityTest]
        public IEnumerator Add_WITH_NullValue_SHOULD_Throw()
        {
            var persistentSet = ScriptableObject.CreateInstance<PersistentStringSet>();

            yield return null;

            Assert.Throws<ArgumentNullException>(() => persistentSet.Add(null));
        }

        [UnityTest]
        public IEnumerator Contains_WITH_ValuePresent_SHOULD_ReturnTrue()
        {
            var persistentSet = ScriptableObject.CreateInstance<PersistentStringSet>();
            persistentSet.Add(StringA);

            yield return null;

            Assert.IsTrue(persistentSet.Contains(StringA));
        }

        [UnityTest]
        public IEnumerator Contains_WITH_ValueNotPresent_SHOULD_ReturnFalse()
        {
            var persistentSet = ScriptableObject.CreateInstance<PersistentStringSet>();
            persistentSet.Add(StringB);

            yield return null;

            Assert.IsFalse(persistentSet.Contains(StringA));
        }

        [UnityTest]
        public IEnumerator Contains_WITH_Null_SHOULD_Throw()
        {
            var persistentSet = ScriptableObject.CreateInstance<PersistentStringSet>();
            
            yield return null;

            Assert.Throws<ArgumentNullException>(() => persistentSet.Contains(null));
        }

        [UnityTest]
        public IEnumerator Remove_WITH_ValidValue_SHOULD_RemoveValue()
        {
            var persistentSet = ScriptableObject.CreateInstance<PersistentStringSet>();
            persistentSet.Add(StringA);

            yield return null;

            persistentSet.Remove(StringA);

            yield return null;

            Assert.AreEqual(0, persistentSet.Items.Count);
        }

        [UnityTest]
        public IEnumerator Remove_WITH_ValueNotPresent_SHOULD_Throw()
        {
            var persistentSet = ScriptableObject.CreateInstance<PersistentStringSet>();
            persistentSet.Add(StringA);

            yield return null;

            Assert.Throws<InvalidOperationException>(() => persistentSet.Remove(StringB));
        }

        [UnityTest]
        public IEnumerator Remove_WITH_NullValue_SHOULD_Throw()
        {
            var persistentSet = ScriptableObject.CreateInstance<PersistentStringSet>();

            yield return null;

            Assert.Throws<ArgumentNullException>(() => persistentSet.Remove(null));
        }

        [UnityTest]
        public IEnumerator OnDisable_WITH_NoArg_SHOULD_ClearObject()
        {
            var onDisableMethod = typeof(PersistentSet<string>).GetMethod("OnDisable", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(onDisableMethod, "OnDisable method was not found.");

            var subscriber = new EventCounter();
            var persistentSet = ScriptableObject.CreateInstance<PersistentStringSet>();

            yield return null;

            persistentSet.ItemAdded += subscriber.ReceiveAddedItem;
            persistentSet.ItemRemoved += subscriber.ReceiveRemovedItem;
            try
            {
                persistentSet.Add(StringA);
                persistentSet.Add(StringB);
                persistentSet.Remove(StringA);
                
                onDisableMethod.Invoke(persistentSet, null);

                int countOnDisable = persistentSet.Items.Count;

                persistentSet.Add(StringA);
                persistentSet.Add(StringB);
                persistentSet.Remove(StringA);

                Assert.AreEqual(0, countOnDisable, "OnDisable should clear all items.");
                Assert.AreEqual(2, subscriber.AddedEventCount, "OnDisable should clear event subscribers");
                Assert.AreEqual(1, subscriber.RemovedEventCount, "OnDisable should clear event subscribers");
            }
            finally
            {
                persistentSet.ItemAdded -= subscriber.ReceiveAddedItem;
                persistentSet.ItemRemoved -= subscriber.ReceiveRemovedItem;
            }
        }

        [UnityTest]
        public IEnumerator OnDisable_WITH_EmptySet_SHOULD_ClearObjectWithoutWarning()
        {
            var onDisableMethod = typeof(PersistentSet<string>).GetMethod("OnDisable", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(onDisableMethod, "OnDisable method was not found.");
            
            var persistentSet = ScriptableObject.CreateInstance<PersistentStringSet>();

            yield return null;

            onDisableMethod.Invoke(persistentSet, null);

            //Assert.Pass();
        }

        private class PersistentStringSet : PersistentSet<string>
        {
        }

        private class EventCounter
        {
            public int AddedEventCount { get; private set; }
            public int RemovedEventCount { get; private set; }

            public void ReceiveAddedItem(string _)
            {
                AddedEventCount++;
            }

            public void ReceiveRemovedItem(string _)
            {
                RemovedEventCount++;
            }
        }
    }
}