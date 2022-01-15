using UnityEngine;

namespace Packages.UniKit.Runtime.PersistentVariables
{
    [CreateAssetMenu(fileName = nameof(PersistentVector3), 
        menuName = nameof(UniKit) + "/" + nameof(PersistentVariables) + "/" + nameof(PersistentVector3))]
    public class PersistentVector3 : Persistent<Vector3>
    {
    }
}