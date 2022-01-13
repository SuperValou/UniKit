## [1.0.0-rc02] - 2022-01-13
- Fix package info
- Add documentation

## [1.0.0-rc01] - 2022-01-13
- Extension methods
	- Removed GetEnumMemberAttribute method on Enums
	- Removed IsOnLayer method on GameObjects
	- Added SafeInvoke on Action (without argument)
- Tests
	- Added unit tests to reach 100% coverage

## [0.1.0-alpha02] - 2021-12-29
- Extension methods
	- GetOrThrow<TComponent> methods no longer require TComponent to inherit from the Component class
- Tests
	- Added tests to GetOrThrow an interface
	
## [0.1.0-alpha01] - 2021-11-19
- Extension methods
	- Added GetOrThrow<TComponent>() on gameObjects and monobehaviours
	- Added IsOnLayer() on gameObjects
	- Added SafeInvoke() on events with 1, 2 or 3 parameters
	- Added GetEnumMemberAttribute() on enum
- Tests
	- Added tests on MonoBehaviour extensions (100%)
	- Added tests on GameObject extensions (55%)

