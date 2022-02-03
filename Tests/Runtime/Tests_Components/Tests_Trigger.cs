using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Packages.UniKit.Runtime.Components;
using UnityEngine;
using UnityEngine.TestTools;

namespace Packages.UniKit.Tests.Runtime.Tests_Components
{
    public class Tests_Trigger
    {
        private const string RigidbodyTag = "Player";
        private const float RigidbodyInitialHeight = 2;
        private const float ExitDelay = 1; // wait a solid second to let the body exit
        private readonly GameObjectManager _manager = new GameObjectManager();

        private float FallingTime => Mathf.Sqrt(2f * RigidbodyInitialHeight / Physics.gravity.magnitude);

        private MethodInfo OnTriggerEnterMethod => typeof(Trigger).GetMethod("OnTriggerEnter", BindingFlags.Instance | BindingFlags.NonPublic);
        private MethodInfo OnTriggerExitMethod => typeof(Trigger).GetMethod("OnTriggerExit", BindingFlags.Instance | BindingFlags.NonPublic);

        private static readonly Regex ExpectedWarningMessageRegex = new Regex(".*collider is not set to Trigger.*");

        private Trigger _trigger;
        private Rigidbody _fallingBody;

        [SetUp]
        public void SetUp()
        {
            // Trigger
            GameObject triggerGameObject = _manager.Instantiate("TriggerGameObject");
            triggerGameObject.transform.position = Vector3.zero;
            triggerGameObject.transform.rotation = Quaternion.identity;
            triggerGameObject.transform.localScale = Vector3.one;

            var boxCollider = triggerGameObject.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3(10, 0.1f, 10);
            boxCollider.isTrigger = true;

            _trigger = triggerGameObject.AddComponent<Trigger>();
        
            // Falling body
            GameObject fallingObject = _manager.Instantiate("FallingRigidbody");
            fallingObject.transform.position = Vector3.up * RigidbodyInitialHeight;
            fallingObject.AddComponent<SphereCollider>();

            _fallingBody = fallingObject.AddComponent<Rigidbody>();
            _fallingBody.useGravity = true;
        }

        [TearDown]
        public void TearDown()
        {
            _trigger.onTriggerEnter.RemoveAllListeners();
            _trigger.onTriggerExit.RemoveAllListeners();
            _manager.DestroyAll();
        }

        [UnityTest]
        public IEnumerator OnTriggerEnter_WTIH_TriggeringGameObject_SHOULD_FireEvent()
        {
            var witness = new TriggerWitness();
            _trigger.onTriggerEnter.AddListener(witness.Listen);

            yield return new WaitForSeconds(FallingTime + Time.fixedDeltaTime);

            Assert.IsTrue(witness.WasFired);
        }

        [UnityTest]
        public IEnumerator OnTriggerEnter_WTIH_TriggeringGameObjectWithTag_SHOULD_FireEvent()
        {
            _trigger.TriggeringTags.Add(RigidbodyTag);
            _fallingBody.gameObject.tag = RigidbodyTag;

            var witness = new TriggerWitness();
            _trigger.onTriggerEnter.AddListener(witness.Listen);

            yield return new WaitForSeconds(FallingTime + Time.fixedDeltaTime);

            Assert.IsTrue(witness.WasFired);
        }

        [UnityTest]
        public IEnumerator OnTriggerEnter_WTIH_IgnoredGameObject_SHOULD_NotFireEvent()
        {
            _trigger.TriggeringTags.Add(RigidbodyTag);

            var witness = new TriggerWitness();
            _trigger.onTriggerEnter.AddListener(witness.Listen);

            yield return new WaitForSeconds(FallingTime + Time.fixedDeltaTime);

            Assert.IsFalse(witness.WasFired);
        }

        [UnityTest]
        public IEnumerator OnTriggerEnter_WTIH_NullCollider_SHOULD_NotFireEvent()
        {
            Assert.IsNotNull(OnTriggerEnterMethod, $"{nameof(OnTriggerEnterMethod)} not found");

            var witness = new TriggerWitness();
            _trigger.onTriggerEnter.AddListener(witness.Listen);

            _fallingBody.isKinematic = true;

            yield return null;

            var bodyCollider = _fallingBody.GetComponent<Collider>();
            Object.DestroyImmediate(bodyCollider);
            OnTriggerEnterMethod.Invoke(_trigger, new object[] { bodyCollider });

            yield return null;

            Assert.IsFalse(witness.WasFired);
        }

        [UnityTest]
        public IEnumerator OnTriggerEnter_WTIH_NullGameObject_SHOULD_NotFireEvent()
        {
            Assert.IsNotNull(OnTriggerEnterMethod, $"{nameof(OnTriggerEnterMethod)} not found");

            var witness = new TriggerWitness();
            _trigger.onTriggerEnter.AddListener(witness.Listen);

            _fallingBody.isKinematic = true;

            yield return null;

            var bodyCollider = _fallingBody.GetComponent<Collider>();
            Object.DestroyImmediate(bodyCollider.gameObject);
            OnTriggerEnterMethod.Invoke(_trigger, new object[] { bodyCollider });

            yield return null;

            Assert.IsFalse(witness.WasFired);
        }

        [UnityTest]
        public IEnumerator OnTriggerExit_WTIH_TriggeringGameObject_SHOULD_FireEvent()
        {
            var witness = new TriggerWitness();
            _trigger.onTriggerExit.AddListener(witness.Listen);

            yield return new WaitForSeconds(FallingTime + ExitDelay); 

            Assert.IsTrue(witness.WasFired);
        }

        [UnityTest]
        public IEnumerator OnTriggerExit_WTIH_TriggeringGameObjectWithTag_SHOULD_FireEvent()
        {
            _trigger.TriggeringTags.Add(RigidbodyTag);
            _fallingBody.gameObject.tag = RigidbodyTag;

            var witness = new TriggerWitness();
            _trigger.onTriggerExit.AddListener(witness.Listen);

            yield return new WaitForSeconds(FallingTime + ExitDelay);

            Assert.IsTrue(witness.WasFired);
        }

        [UnityTest]
        public IEnumerator OnTriggerExit_WTIH_IgnoredGameObject_SHOULD_NotFireEvent()
        {
            _trigger.TriggeringTags.Add(RigidbodyTag);

            var witness = new TriggerWitness();
            _trigger.onTriggerExit.AddListener(witness.Listen);

            yield return new WaitForSeconds(FallingTime + ExitDelay);

            Assert.IsFalse(witness.WasFired);
        }


        [UnityTest]
        public IEnumerator OnTriggerExit_WTIH_NullCollider_SHOULD_NotFireEvent()
        {
            Assert.IsNotNull(OnTriggerExitMethod, $"{nameof(OnTriggerExitMethod)} not found");

            var witness = new TriggerWitness();
            _trigger.onTriggerExit.AddListener(witness.Listen);

            _fallingBody.isKinematic = true;

            yield return null;

            var bodyCollider = _fallingBody.GetComponent<Collider>();
            Object.DestroyImmediate(bodyCollider);
            OnTriggerExitMethod.Invoke(_trigger, new object[] { bodyCollider });

            yield return null;

            Assert.IsFalse(witness.WasFired);
        }

        [UnityTest]
        public IEnumerator OnTriggerExit_WTIH_NullGameObject_SHOULD_NotFireEvent()
        {
            Assert.IsNotNull(OnTriggerExitMethod, $"{nameof(OnTriggerExitMethod)} not found");

            var witness = new TriggerWitness();
            _trigger.onTriggerExit.AddListener(witness.Listen);

            _fallingBody.isKinematic = true;

            yield return null;

            var bodyCollider = _fallingBody.GetComponent<Collider>();
            Object.DestroyImmediate(bodyCollider.gameObject);
            OnTriggerExitMethod.Invoke(_trigger, new object[] { bodyCollider });

            yield return null;

            Assert.IsFalse(witness.WasFired);
        }

        [UnityTest]
        public IEnumerator Start_WTIH_WrongCollider_SHOULD_IssueWarning()
        {
            GameObject triggerGameObject = _manager.Instantiate("TriggerGameObject");
            var box = triggerGameObject.AddComponent<BoxCollider>();
            box.isTrigger = false;
            triggerGameObject.AddComponent<Trigger>();

            LogAssert.Expect(LogType.Warning, ExpectedWarningMessageRegex);

            yield return null;

            //Assert.Pass();
        }

        private class TriggerWitness
        {
            public bool WasFired { get; private set; }

            public void Listen(Collider _)
            {
                WasFired = true;
            }
        }
    }
}