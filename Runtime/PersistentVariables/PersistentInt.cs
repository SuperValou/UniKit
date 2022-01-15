using UnityEngine;

namespace Packages.UniKit.Runtime.PersistentVariables
{
    [CreateAssetMenu(fileName = nameof(PersistentInt), 
        menuName = nameof(UniKit) + "/" + nameof(PersistentVariables) + "/" + nameof(PersistentInt))]
    public class PersistentInt : Persistent<int>
    {
    }
}