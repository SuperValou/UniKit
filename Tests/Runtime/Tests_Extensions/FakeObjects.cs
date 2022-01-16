using UnityEngine;

namespace Packages.UniKit.Tests.Runtime
{

    public class FooComponent : MonoBehaviour
    {

    }

    public class BarComponent : MonoBehaviour
    {

    }

    public interface IBaz
    {

    }

    public class BazComponent : MonoBehaviour, IBaz
    {

    }
}