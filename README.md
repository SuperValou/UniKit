
# UniKit
A set of basic utilities for Unity.

![version](https://img.shields.io/badge/version-1.2.0-blue)
![coverage](https://img.shields.io/badge/coverage-96%25-green)

## GameObject pooling
**Description**

Some GameObjects (like bullets) can be spawned and despawned frequently. To avoid numerous instantiations/destructions of these objects, you can inherit from `PooledMonoBehaviour` instead of `MonoBehaviour` to make sure your objects gets instantiated once, then reused.

**Example**
```csharp
public class Gun : MonoBehaviour
{
	public BulletPool bulletPool;

	public void FireBullet()
	{
		// Use the Spawn() method to get a new instance
		bulletPool.Spawn(this.transform.position, this.transform.rotation);
	}
}

public class BulletPool : Pool<Bullet>
{
	// Attach this MonoBehaviour to a game object that will represent the pool of bullets,
	// then set the prefab of the pool in the inspector.
	// The Pool<T> class will handle the rest.
}

public class Bullet : PooledMonoBehaviour
{
	private Rigidbody _rigidbody;

	public override void FirstStart()
	{
		// Equivalent of the Start() method, called only once
		_rigidbody = this.GetComponent<Rigidbody>();
	}

	public override void OnRespawn()
	{
		// Called every time the game object gets respawned
		_rigidbody.velocity = this.transform.forward;
	}
	
	void OnCollisionEnter(Collision collision)
	{
		// Disable() will return the game object back to the pool
		base.Disable();	
	}
}
```

## Persistent variables

**Description**

A "persistent variable" is a ScriptableObject holding some data. Persistent variables are useful to communicate data accross multiple Scenes, like the Vector3 representing the player position for example.

Persistent variables are available by default for `bool`, `float`, `int`, `string`, `Vector3`, and `Quaternion`, but any struct (like an `enum`) can also be created by inheriting from the `Persistent<TStruct>` class.

**Example**
```csharp
// Component put on a game object in Scene1
public class Player : MonoBehaviour
{
	public PersistentVector3 playerPosition;

	void Update()
	{
		playerPosition.Set(this.transform.position);
	}
}

// Component put on a game object in Scene2
public class Follower : MonoBehaviour
{
	public PersistentVector3 playerPosition;

	void Update()
	{
		this.transform.position = playerPosition.Value;
	}
}
```

## Trigger

**Description**

A component firing some UnityEvents when the `OnTriggerEnter` and `OnTriggerExit` methods gets called. The involved Collider is also passed to the fired events if needed. 

Useful to avoid hard-coding these two methods into other MonoBehaviours, freeing them from having to care about trigger collision.

**Example**

```csharp
public class MessageDisplayer : MonoBehaviour
{
	// Example of a method to call from one of the Trigger event (through the Inspector)
	// instead of rewriting OnTriggerEnter and needing to have a collider
	public void DisplayMessage()
	{		
		Debug.Log("Hello!");
	}
}
```

## Extension methods
### GetOrThrow

**Description**

Returns the component of type `TComponent` attached to this game object, or throws an exception if it's missing.


**Declaration**

```csharp
public static TComponent GetOrThrow<TComponent>(this MonoBehaviour monoBehaviour)
```


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
### SafeInvoke

**Description**

Invoke all registered callbacks, even if some of them throw an exception. Exceptions get logged to `Debug.LogError` instead.


**Declaration**
```csharp
public static void SafeInvoke(this Action actionToInvoke)
```
*Overloads with up to 3 parameters are also available:*
```csharp
public static void SafeInvoke<TEventArg>(this Action<TEventArg> actionToInvoke, TEventArg arg);
public static void SafeInvoke<TEventArg1, TEventArg2>(this Action<TEventArg1, TEventArg2> actionToInvoke, TEventArg1 arg1, TEventArg2 arg2);
public static void SafeInvoke<TEventArg1, TEventArg2, TEventArg3>(this Action<TEventArg1, TEventArg2, TEventArg3> actionToInvoke, TEventArg1 arg1, TEventArg2 arg2, TEventArg3 arg3);
```


**Example**
```csharp
public class Foo
{
    public event Action OnEvent;

    public void FireEvent()
    {
	OnEvent += () => throw new InvalidOperationException();
        OnEvent += () => Debug.Log("Hello");
        
        OnEvent.SafeInvoke(); // will always print Hello
    }
}

```
