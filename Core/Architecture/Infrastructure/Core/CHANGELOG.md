## [1.2.1] - 2026-02-13

### Initialization module

- Provided IInitializable interface
- Implemented InitializableComponenent
- Implemented Initializer

## [1.2.0] - 2025-12-29

### Package separation

- Removed EntryPoint and GameBootstrapper templates
- Minor improvements in package structure

## [1.1.0] - 2025-11-26

### LifeCycleProvider extension

- LifeCycleProvider is now inherited from ICoroutineRunner, since it is MonoBehaviour by definition
- Implemented StopCoroutine support for ICoroutineRunner

## [1.0.0] - 2025-07-24

### First Release

- Implemented IService marker
- Implemented naive EntryPoint template
- Implemented naive GameBootstrapper template
- Implemented ICoroutineRunner
- Implemented ILifeCycleProvider
- Created default ILifeCycleProvider implementation
