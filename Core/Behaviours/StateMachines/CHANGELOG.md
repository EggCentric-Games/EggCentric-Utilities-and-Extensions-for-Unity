## [2.3.1] - 2026-01-1

### Tickable states

- Implemented ITickableState

## [2.3.0] - 2025-11-28

### New transition request building logic

- Implemented IgnoreConditions flag
- Forced() now creates urgent request with all flags "on"
- Implemented ability to call IgnoreLocks while building a TransitionRequest
- Implemented ability to call IgnoreConditions while building a TransitionRequest
- Renamed Forced flag to IgnoreLocks
- Renamed TransitionBuilderBase to TransitionBuilder
- Renamed TransitionBuilder to PlainTransitionBuilder
- Fixed TransitionFlags reference in TransitionRequest

## [2.2.1] - 2025-11-28

### Small debug improvements

- Revamped OnTransitionAdded debug event

## [2.2.0] - 2025-11-28

### Debug events and much more!

- Implemented ability to get current state machine lock level
- Implemented support of demug events for request receiving
- Revamped debug events return types


## [2.1.0] - 2025-11-28

### Debug events and much more!

- Implemented debugging events support
- Implemented additional generic restriction for TransitionRequests
- Implemented transition request ExecutionPolicies
- Moved request execution and resolving logic into TransitionRequest 
- Renamed ICommonState to IPlainState
- Fixed transition request priority
- Other improvements

## [2.0.1] - 2025-11-26

### Small hotfix

- Implemented infinite lifetime of AwaiFor transition request by default

## [2.0.0] - 2025-11-24

### Global system update

- Implemented DefaultState support
- Implemented conditional transitions support
- Implemented state change requests support
- Implemented StateMachine locking support
- Implemented request priority support
- Implemented UrgentRequests
- Implemented DelayedRequests

- Improved initialization pipeline

## [1.0.0] - 2025-07-17

### First Release

- Implemented generic StateMachine class
- Implemented basic state managment
- Implemented IState and IPayloadedState
