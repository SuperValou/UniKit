using System;
using System.Collections;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Packages.UniKit.Runtime.Extensions;
using UnityEngine;
using UnityEngine.TestTools;

namespace Packages.UniKit.Tests.Runtime.Tests_Extensions
{
    public class Tests_DelegateExtensions
    {
        private static readonly Regex ExpectedErrorMessageRegex = new Regex(".*Exception occured during invocation of method.*");

        [UnityTest]
        public IEnumerator SafeInvoke_WITH_NoIssue_SHOULD_ExecuteProperly()
        {
            var eventTest = new EventTest();
            var subscriber = new NiceSubscriber();
            try
            {
                eventTest.OnAction += subscriber.SetFlag;

                yield return null;

                eventTest.FireEvent();

                yield return null;

                Assert.IsTrue(subscriber.Flag);
            }
            finally
            {
                eventTest.OnAction -= subscriber.SetFlag;
            }
        }

        [UnityTest]
        public IEnumerator SafeInvoke_WITH_RaisingSubscriber_SHOULD_ExecuteProperly()
        {
            var eventTest = new EventTest();
            var niceSubscriber = new NiceSubscriber();
            var raisingSubscriber = new RaisingSubscriber();

            try
            {
                eventTest.OnAction += raisingSubscriber.Raise;
                eventTest.OnAction += niceSubscriber.SetFlag;

                LogAssert.Expect(LogType.Error, ExpectedErrorMessageRegex);

                yield return null;
                
                eventTest.FireEvent();

                yield return null;

                Assert.IsTrue(niceSubscriber.Flag);
            }
            finally
            {
                eventTest.OnAction -= niceSubscriber.SetFlag;
                eventTest.OnAction -= raisingSubscriber.Raise;
            }
        }

        [UnityTest]
        public IEnumerator SafeInvoke_WITH_NullAction_SHOULD_ExecuteProperly()
        {
            Action action = null;

            yield return null;

            action.SafeInvoke();

            yield return null;

            //Assert.Pass();
        }

        [UnityTest]
        public IEnumerator SafeInvokeTArg_WITH_NoIssue_SHOULD_ExecuteProperly()
        {
            var eventTest = new EventTest();
            var subscriber = new NiceSubscriber();
            try
            {
                eventTest.OnActionWithOneParameter += subscriber.SetFlag;

                yield return null;

                eventTest.FireEventWithOneParameter();

                yield return null;

                Assert.IsTrue(subscriber.Flag);
            }
            finally
            {
                eventTest.OnActionWithOneParameter -= subscriber.SetFlag;
            }
        }

        [UnityTest]
        public IEnumerator SafeInvokeTArg_WITH_RaisingSubscriber_SHOULD_ExecuteProperly()
        {
            var eventTest = new EventTest();
            var niceSubscriber = new NiceSubscriber();
            var raisingSubscriber = new RaisingSubscriber();

            try
            {
                eventTest.OnActionWithOneParameter += raisingSubscriber.Raise;
                eventTest.OnActionWithOneParameter += niceSubscriber.SetFlag;

                LogAssert.Expect(LogType.Error, ExpectedErrorMessageRegex);

                yield return null;

                eventTest.FireEventWithOneParameter();

                yield return null;

                Assert.IsTrue(niceSubscriber.Flag);
            }
            finally
            {
                eventTest.OnActionWithOneParameter -= niceSubscriber.SetFlag;
                eventTest.OnActionWithOneParameter -= raisingSubscriber.Raise;
            }
        }

        [UnityTest]
        public IEnumerator SafeInvokeTArg_WITH_NullActionTArg_SHOULD_ExecuteProperly()
        {
            Action<int> action = null;

            yield return null;

            action.SafeInvoke(0);

            yield return null;

            //Assert.Pass();
        }

        [UnityTest]
        public IEnumerator SafeInvokeTArg1Targ2_WITH_NoIssue_SHOULD_ExecuteProperly()
        {
            var eventTest = new EventTest();
            var subscriber = new NiceSubscriber();
            try
            {
                eventTest.OnActionWithTwoParameters += subscriber.SetFlag;

                yield return null;

                eventTest.FireEventWithTwoParameters();

                yield return null;

                Assert.IsTrue(subscriber.Flag);
            }
            finally
            {
                eventTest.OnActionWithTwoParameters -= subscriber.SetFlag;
            }
        }

        [UnityTest]
        public IEnumerator SafeInvokeTArg1TArg2_WITH_RaisingSubscriber_SHOULD_ExecuteProperly()
        {
            var eventTest = new EventTest();
            var niceSubscriber = new NiceSubscriber();
            var raisingSubscriber = new RaisingSubscriber();

            try
            {
                eventTest.OnActionWithTwoParameters += raisingSubscriber.Raise;
                eventTest.OnActionWithTwoParameters += niceSubscriber.SetFlag;

                LogAssert.Expect(LogType.Error, ExpectedErrorMessageRegex);

                yield return null;

                eventTest.FireEventWithTwoParameters();

                yield return null;

                Assert.IsTrue(niceSubscriber.Flag);
            }
            finally
            {
                eventTest.OnActionWithTwoParameters -= niceSubscriber.SetFlag;
                eventTest.OnActionWithTwoParameters -= raisingSubscriber.Raise;
            }
        }

        [UnityTest]
        public IEnumerator SafeInvokeTArg1TArg2_WITH_NullActionTArg1TArg2_SHOULD_ExecuteProperly()
        {
            Action<int, int> action = null;

            yield return null;

            action.SafeInvoke(0, 0);

            yield return null;

            //Assert.Pass();
        }






        [UnityTest]
        public IEnumerator SafeInvokeTArg1Targ2TArg3_WITH_NoIssue_SHOULD_ExecuteProperly()
        {
            var eventTest = new EventTest();
            var subscriber = new NiceSubscriber();
            try
            {
                eventTest.OnActionWithThreeParameters += subscriber.SetFlag;

                yield return null;

                eventTest.FireEventWithThreeParameters();

                yield return null;

                Assert.IsTrue(subscriber.Flag);
            }
            finally
            {
                eventTest.OnActionWithThreeParameters -= subscriber.SetFlag;
            }
        }

        [UnityTest]
        public IEnumerator SafeInvokeTArg1TArg2TArg3_WITH_RaisingSubscriber_SHOULD_ExecuteProperly()
        {
            var eventTest = new EventTest();
            var niceSubscriber = new NiceSubscriber();
            var raisingSubscriber = new RaisingSubscriber();

            try
            {
                eventTest.OnActionWithThreeParameters += raisingSubscriber.Raise;
                eventTest.OnActionWithThreeParameters += niceSubscriber.SetFlag;

                LogAssert.Expect(LogType.Error, ExpectedErrorMessageRegex);

                yield return null;

                eventTest.FireEventWithThreeParameters();

                yield return null;

                Assert.IsTrue(niceSubscriber.Flag);
            }
            finally
            {
                eventTest.OnActionWithThreeParameters -= niceSubscriber.SetFlag;
                eventTest.OnActionWithThreeParameters -= raisingSubscriber.Raise;
            }
        }

        [UnityTest]
        public IEnumerator SafeInvokeTArg1TArg2TArg3_WITH_NullActionTArg1TArg2TArg3_SHOULD_ExecuteProperly()
        {
            Action<int, int, int> action = null;

            yield return null;

            action.SafeInvoke(0, 0, 0);

            yield return null;

            //Assert.Pass();
        }


        // -- Test classes --


        private class EventTest
        {
            public event Action OnAction;
            public event Action<int> OnActionWithOneParameter;
            public event Action<int, int> OnActionWithTwoParameters;
            public event Action<int, int, int> OnActionWithThreeParameters;

            public void FireEvent()
            {
                OnAction.SafeInvoke();
            }

            public void FireEventWithOneParameter()
            {
                OnActionWithOneParameter.SafeInvoke(0);
            }

            public void FireEventWithTwoParameters()
            {
                OnActionWithTwoParameters.SafeInvoke(0, 0);
            }

            public void FireEventWithThreeParameters()
            {
                OnActionWithThreeParameters.SafeInvoke(0, 0, 0);
            }
        }

        private class NiceSubscriber
        {
            public bool Flag { get; private set; }

            public void SetFlag()
            {
                Flag = true;
            }

            public void SetFlag(int _)
            {
                Flag = true;
            }

            public void SetFlag(int _, int _1)
            {
                Flag = true;
            }

            public void SetFlag(int _, int _1, int _2)
            {
                Flag = true;
            }
        }

        private class RaisingSubscriber
        {
            public void Raise()
            {
                throw new InvalidOperationException();
            }

            public void Raise(int _)
            {
                throw new InvalidOperationException();
            }

            public void Raise(int _, int _1)
            {
                throw new InvalidOperationException();
            }

            public void Raise(int _, int _1, int _2)
            {
                throw new InvalidOperationException();
            }
        }
    }
}