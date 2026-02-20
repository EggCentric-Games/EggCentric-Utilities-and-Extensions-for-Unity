## [1.6.2] - 2026-02-20

### Hierarchy changes

- Moved IValueProvider and ITrackableValue into ValueProviders package
- Now DataContainers is extension of the main ValueProviders package

## [1.6.1] - 2026-01-26

### Default IValueProviders

Provided a bunch of concrete IValueProvider implementations
- Implemented FixedValueProvider
- Implemented SequentialValueProvider
- Implemented AutomatedSequentialProvider
- Implemented PropertyWrapper
- Implemented Getter

## [1.6.0] - 2026-01-06

### Ordered sets

- Implemented OrderedSet

## [1.5.1] - 2026-01-06

### Value Provider

- Implemented IValueProvider - a thin wrapper

## [1.5.0] - 2025-12-14

### Trackable values

- Implemented ITrackableValue
- Converted IDataCache to ITrackableValue
- Minor improvemnt of TimeDependentDataCache validity check

## [1.4.2] - 2025-12-09

### DataCache initialization fix

- Fixed DataCache initialization

## [1.4.1] - 2025-12-09

### Cleanup
- Renamed DataCache to PersistentDataCache
- Changed file structure
- Performed a minor code cleanup

## [1.4.0] - 2025-07-17

### Read only access

- Implemented IReadOnlyField

## [1.3.1] - 2025-07-07

### Cache Invalidation

- Implemented cache invalidation methods

## [1.3.0] - 2025-07-07

### Caching System Revamp

- **Removed** CachedValue class - use AutomatedDataCache instead
- **Revamped** DataCache functionality. Old functionality moved into DataRecord
- Added AutomatedDataCache decorator that supports lazy initialization of other caches
- Added TimeDependentCache that invalidates itself after some time

## [1.2.0] - 2025-06-16

### Implicit Conversion Implementation

- Implemented implicit conversion of Data Containers to their according values
- Changed directory of CachedValue script

## [1.1.0] - 2025-06-16

### Cached Value Implementation

- Implemented CachedValue class that should reduce unnecessary data recalculations
- Fixed DataCache inheritance issue

## [1.0.0] - 2025-05-24

### First Release

- Implemented Field class that supports value changes tracking
- Implemented DataCache class that has a limited lifespan
- Implemented Register class that supports tracking changes in collection elements
