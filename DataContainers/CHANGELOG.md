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
