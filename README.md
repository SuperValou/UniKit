# UnitKit
A set of basic utilities for Unity.

## Extension methods
### MonoBehaviourExtensions.GetOrThrow

**Declaration**
```csharp
public static TComponent GetOrThrow<TComponent>(this MonoBehaviour monoBehaviour)
```

**Description**
Returns the component of type TComponent attached to this game object, or throws an exception if it's missing.

**Example**
```csharp
public class RedMesh : MonoBehaviour
{	
	private MeshRenderer _renderer;

	void Start()
	{		
		_renderer = this.GetOrThrow<MeshRenderer>();
		_renderer.material.color = Color.red;
	}
}
```
### DelegateExtensions.SafeInvoke

**Declaration**
```csharp
public static void SafeInvoke(this Action actionToInvoke)
```
*Overloads with up to 3 parameters are also available:*
```csharp
public static void SafeInvoke<TEventArg>(this Action<TEventArg> actionToInvoke, TEventArg arg)
public static void SafeInvoke<TEventArg1, TEventArg2>(this Action<TEventArg1, TEventArg2> actionToInvoke, TEventArg1 arg1, TEventArg2 arg2)
public static void SafeInvoke<TEventArg1, TEventArg2, TEventArg3>(this Action<TEventArg1, TEventArg2, TEventArg3> actionToInvoke, TEventArg1 arg1, TEventArg2 arg2, TEventArg3 arg3)
```

**Description**
Invoke all registered callbacks, even if some of them throw an exception. Exceptions get logged to Debug.LogError instead.

**Example**
```csharp
public class Foo
{
    public event Action OnEvent;

    public void Execute()
    {
	    foo.OnEvent += () => throw new InvalidOperationException();
        foo.OnEvent += () => Debug.Log("Hello");
        
        OnEvent.SafeInvoke(); // will always print Hello
    }
}

```






