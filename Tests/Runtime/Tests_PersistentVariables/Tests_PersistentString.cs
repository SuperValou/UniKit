using System;
using System.Collections;
using System.Reflection;
using NUnit.Framework;
using Packages.UniKit.Runtime.PersistentVariables;
using UnityEngine;
using UnityEngine.TestTools;

namespace Packages.UniKit.Tests.Runtime.Tests_PersistentVariables
{
    public class Tests_PersistentString
    {
        private const string TestValue = "Hello!";

        [UnityTest]
        public IEnumerator ValueSetter_WITH_ValidValue_SHOULD_SetCorrectValue()
        {
            var persistentString = ScriptableObject.CreateInstance<PersistentString>();

            yield return null;

            persistentString.Value = TestValue;

            yield return null;

            Assert.AreEqual(TestValue, persistentString.Value);
        }

        [UnityTest]
        public IEnumerator ValueSetter_WITH_Null_SHOULD_Throw()
        {
            var persistentString = ScriptableObject.CreateInstance<PersistentString>();

            yield return null;

            Assert.Throws<ArgumentNullException>(() => persistentString.Value = null);
        }

        [UnityTest]
        public IEnumerator Set_WITH_ValidValue_SHOULD_SetCorrectValue()
        {
            var persistentString = ScriptableObject.CreateInstance<PersistentString>();

            yield return null;

            persistentString.Set(TestValue);

            yield return null;

            Assert.AreEqual(TestValue, persistentString.Value);
        }

        [UnityTest]
        public IEnumerator Set_WITH_Null_SHOULD_Throw()
        {
            var persistentString = ScriptableObject.CreateInstance<PersistentString>();

            yield return null;

            Assert.Throws<ArgumentNullException>(() => persistentString.Set(null));
        }

        [UnityTest]
        public IEnumerator ValueChangedEvent_WITH_NewValue_SHOULD_FireEvent()
        {
            var subscriber = new ValueReceiver();
            var persistentString = ScriptableObject.CreateInstance<PersistentString>();

            yield return null;

            persistentString.ValueChanged += subscriber.ReceiveValue;

            try
            {
                persistentString.Set(TestValue);
                Assert.AreEqual(TestValue, subscriber.ReceivedValue);
            }
            finally
            {
                persistentString.ValueChanged -= subscriber.ReceiveValue;
            }
        }

        [UnityTest]
        public IEnumerator ValueChangedEvent_WITH_SameValue_SHOULD_DoNothing()
        {
            var subscriber = new EventCounter();
            var persistentString = ScriptableObject.CreateInstance<PersistentString>();

            yield return null;

            persistentString.ValueChanged += subscriber.ReceiveValue;

            try
            {
                persistentString.Set(TestValue);
                persistentString.Set(TestValue);
                persistentString.Set(TestValue);

                Assert.AreEqual(1, subscriber.ReceivedValueCount);
            }
            finally
            {
                persistentString.ValueChanged -= subscriber.ReceiveValue;
            }
        }

        [UnityTest]
        public IEnumerator OnDisable_WITH_NoArg_SHOULD_ClearObject()
        {
            var onDisableMethod = typeof(PersistentString).GetMethod("OnDisable", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(onDisableMethod, "OnDisable method was not found.");

            var subscriber = new EventCounter();
            var persistentString = ScriptableObject.CreateInstance<PersistentString>();

            yield return null;

            persistentString.ValueChanged += subscriber.ReceiveValue;
            try
            {
                persistentString.Set(TestValue);
                
                onDisableMethod.Invoke(persistentString, null);

                string valueOnDisable = persistentString.Value;

                persistentString.Set(TestValue);

                Assert.AreEqual(string.Empty, valueOnDisable, "OnDisable should reset Value to the empty string");
                Assert.AreEqual(1, subscriber.ReceivedValueCount, "OnDisable should clear event subscribers");
            }
            finally
            {
                persistentString.ValueChanged -= subscriber.ReceiveValue;
            }
        }

        private class EventCounter
        {
            public int ReceivedValueCount { get; private set; }

            public void ReceiveValue(string _)
            {
                ReceivedValueCount++;
            }
        }

        private class ValueReceiver
        {
            public string ReceivedValue { get; private set; }

            public void ReceiveValue(string newValue)
            {
                ReceivedValue = newValue;
            }
        }
    }
}