using UnityEngine;

namespace Packages.UniKit.Runtime.PersistentVariables
{
    [CreateAssetMenu(fileName = nameof(PersistentBool), 
        menuName = nameof(UniKit) + "/" + nameof(PersistentVariables) + "/" + nameof(PersistentBool))]
    public class PersistentBool : Persistent<bool>
    {
    }
}