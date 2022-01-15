using UnityEngine;

namespace Packages.UniKit.Runtime.PersistentVariables
{
    [CreateAssetMenu(fileName = nameof(PersistentFloat), 
        menuName = nameof(UniKit) + "/" + nameof(PersistentVariables) + "/" + nameof(PersistentFloat))]
    public class PersistentFloat : Persistent<float>
    {
    }
}