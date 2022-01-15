using UnityEngine;

namespace Packages.UniKit.Runtime.PersistentVariables
{
    [CreateAssetMenu(fileName = nameof(PersistentQuaternion), 
        menuName = nameof(UniKit) + "/" + nameof(PersistentVariables) + "/" + nameof(PersistentQuaternion))]
    public class PersistentQuaternion : Persistent<Quaternion>
    {
    }
}