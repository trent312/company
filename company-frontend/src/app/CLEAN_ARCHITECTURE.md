Clean Architecture (folder-based) for Angular app

This project uses a folder-based, single-repo approach to follow Clean Architecture principles.

Recommended folders under src/app/

- domain/
  - models/           <- Entities / domain models (pure data structures)

- application/
  - ports/            <- Interfaces (ports) that define repository/use-case boundaries
  - usecases/         <- Application use-cases (optional)

- infrastructure/
  - repositories/     <- Concrete implementations of ports (data access, HTTP, local storage)

- presentation/
  - components/       <- Angular components, containers
  - services/         <- UI-facing services that call application use-cases

Usage
- Keep domain models free of framework code.
- Define interfaces in application/ports and depend on those in higher layers.
- Implement interfaces in infrastructure and register concrete implementations with Angular DI (providers).

Example: InMemoryCompanyRepository implements CompanyRepository (application port).
