# [0.1.0-alpha02] - 2021-12-29
- Extension methods
	- GetOrThrow<TComponent> methods no longer require TComponent to inherit from the Component class
- Tests
	- Added tests to GetOrThrow an interface
	
# [0.1.0-alpha01] - 2021-11-19
- Extension methods
	- Added GetOrThrow<TComponent>() on gameObjects and monobehaviours
	- Added IsOnLayer() on gameObjects
	- Added SafeInvoke() on events with 1, 2 or 3 parameters
	- Added GetEnumMemberAttribute() on enum
- Tests
	- Added tests on MonoBehaviour extensions (100%)
	- Added tests on GameObject extensions (55%)

