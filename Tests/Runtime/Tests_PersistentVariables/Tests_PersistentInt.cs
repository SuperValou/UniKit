using System.Collections;
using System.Reflection;
using NUnit.Framework;
using Packages.UniKit.Runtime.PersistentVariables;
using UnityEngine;
using UnityEngine.TestTools;

namespace Packages.UniKit.Tests.Runtime.Tests_PersistentVariables
{
    public class Tests_PersistentInt
    {
        private const int TestValue = 255;

        [UnityTest]
        public IEnumerator ValueSetter_WITH_ValidValue_SHOULD_SetCorrectValue()
        {
            var persistentInt = ScriptableObject.CreateInstance<PersistentInt>();

            yield return null;

            persistentInt.Value = TestValue;

            yield return null;

            Assert.AreEqual(TestValue, persistentInt.Value);
        }
        
        [UnityTest]
        public IEnumerator Set_WITH_ValidValue_SHOULD_SetCorrectValue()
        {
            var persistentInt = ScriptableObject.CreateInstance<PersistentInt>();

            yield return null;

            persistentInt.Set(TestValue);

            yield return null;

            Assert.AreEqual(TestValue, persistentInt.Value);
        }
        
        [UnityTest]
        public IEnumerator ValueChangedEvent_WITH_NewValue_SHOULD_FireEvent()
        {
            var subscriber = new ValueReceiver();
            var persistentInt = ScriptableObject.CreateInstance<PersistentInt>();

            yield return null;

            persistentInt.ValueChanged += subscriber.ReceiveValue;

            try
            {
                persistentInt.Set(TestValue);
                Assert.AreEqual(TestValue, subscriber.ReceivedValue);
            }
            finally
            {
                persistentInt.ValueChanged -= subscriber.ReceiveValue;
            }
        }

        [UnityTest]
        public IEnumerator ValueChangedEvent_WITH_SameValue_SHOULD_DoNothing()
        {
            var subscriber = new EventCounter();
            var persistentInt = ScriptableObject.CreateInstance<PersistentInt>();

            yield return null;

            persistentInt.ValueChanged += subscriber.ReceiveValue;

            try
            {
                persistentInt.Set(TestValue);
                persistentInt.Set(TestValue);
                persistentInt.Set(TestValue);

                Assert.AreEqual(1, subscriber.ReceivedValueCount);
            }
            finally
            {
                persistentInt.ValueChanged -= subscriber.ReceiveValue;
            }
        }

        [UnityTest]
        public IEnumerator OnDisable_WITH_NoArg_SHOULD_ClearObject()
        {
            var onDisableMethod = typeof(Persistent<int>).GetMethod("OnDisable", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(onDisableMethod, "OnDisable method was not found.");

            var subscriber = new EventCounter();
            var persistentInt = ScriptableObject.CreateInstance<PersistentInt>();

            yield return null;

            persistentInt.ValueChanged += subscriber.ReceiveValue;
            try
            {
                persistentInt.Set(TestValue);
                
                onDisableMethod.Invoke(persistentInt, null);

                int valueOnDisable = persistentInt.Value;

                persistentInt.Set(TestValue);

                Assert.AreEqual(default(int), valueOnDisable, "OnDisable should reset the Value property");
                Assert.AreEqual(1, subscriber.ReceivedValueCount, "OnDisable should clear event subscribers");
            }
            finally
            {
                persistentInt.ValueChanged -= subscriber.ReceiveValue;
            }
        }

        private class EventCounter
        {
            public int ReceivedValueCount { get; private set; }

            public void ReceiveValue(int _)
            {
                ReceivedValueCount++;
            }
        }

        private class ValueReceiver
        {
            public int ReceivedValue { get; private set; }

            public void ReceiveValue(int newValue)
            {
                ReceivedValue = newValue;
            }
        }
    }
}