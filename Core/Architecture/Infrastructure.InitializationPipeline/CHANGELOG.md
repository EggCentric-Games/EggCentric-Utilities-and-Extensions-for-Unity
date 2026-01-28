## [1.0.0] - 2025-07-24

### First Release

- Implemented API class used as static reference to access global game parameters
- Implemented game EntryPoint. It used as a single place where game initialization begins
- Implemented IComposition root. It can and should be used as a place where all services are created
- Implemented robust runtime initialization pipeline using IGameBootstrapper and IBootstrapSteps
- Implemented ProjectContext to provide access to all the services and dependencies used in the project