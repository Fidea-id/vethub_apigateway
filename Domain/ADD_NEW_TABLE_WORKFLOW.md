# Workflow: Adding a New Table/Entity to VetHub

This document outlines the standard procedure for adding a new table (entity) to the VetHub project. Follow these steps to ensure all layers are correctly implemented and integrated.

## 1. Domain Layer (`VetHubAPI\Domain`)

All core definitions, interfaces, and DTOs live in the `VetHubAPI\Domain` project.

### Step 1.1: Create the Entity Model
- **Path**: `Entities\Models\Clients\[EntityName].cs`
- **Action**: Create a class that inherits from `BaseEntity`.
- **Note**: `Id`, `IsActive`, `CreatedAt`, and `UpdatedAt` are inherited.

### Step 1.2: Create the Filter Model
- **Path**: `Entities\Filters\Clients\[EntityName]Filter.cs`
- **Action**: Create a class that inherits from `BaseEntityFilter`.
- **Note**: Add properties that will be used for searching/filtering in the repository.

### Step 1.3: Create Request & Response DTOs
- **Request Path**: `Entities\Requests\Clients\[EntityName]Request.cs`
- **Response Path**: `Entities\Responses\Clients\[EntityName]Response.cs`
- **Action**: Define the data structures for incoming requests (Create/Update) and outgoing responses.

### Step 1.4: Create the Repository Interface
- **Path**: `Interfaces\Clients\I[EntityName]Repository.cs`
- **Action**: Define an interface that inherits from `IGenericRepository<[EntityName], [EntityName]Filter>`.

### Step 1.5: Update the Unit of Work Interface
- **Path**: `Interfaces\Clients\IUnitOfWork.cs`
- **Action**: Add a read-only property for the new repository:
  ```csharp
  I[EntityName]Repository [EntityName]Repository { get; }
  ```

---

## 2. Infrastructure Layer (`ClientVetHub\Infrastructure`)

Implementation of data access and repository patterns.

### Step 2.1: Create the Repository Implementation
- **Path**: `Repositories\[EntityName]Repository.cs`
- **Action**: Create a class that inherits from `GenericRepository<[EntityName], [EntityName]Filter>` and implements `I[EntityName]Repository`.

### Step 2.2: Update the Unit of Work Implementation
- **Path**: `Data\UnitOfWork.cs`
- **Action**: 
  1. Add a private field for the repository.
  2. Implement the public property with lazy initialization (check if null, then instantiate).

### Step 2.3: Register in Infrastructure Dependency Injection
- **Path**: `ConfigureServices.cs`
- **Action**: Register the repository in `AddInfrastructureServices`:
  ```csharp
  services.AddScoped<I[EntityName]Repository, [EntityName]Repository>();
  ```

---

## 3. Application Layer (`ClientVetHub\Application`)

Business logic and service orchestration.

### Step 3.1: Create the Service Interface
- **Path**: `Services\Contracts\I[EntityName]Service.cs`
- **Action**: Create an interface that inherits from `IGenericService<[EntityName], [EntityName]Request, [EntityName]Response, [EntityName]Filter>`.

### Step 3.2: Create the Service Implementation
- **Path**: `Services\Implementations\[EntityName]Service.cs`
- **Action**: Create a class that inherits from `GenericService<[EntityName], [EntityName]Request, [EntityName]Response, [EntityName]Filter>` and implements `I[EntityName]Service`.

### Step 3.3: Register in Application Dependency Injection
- **Path**: `ConfigureServices.cs`
- **Action**: Register the service in `AddApplicationServices`:
  ```csharp
  services.AddScoped<I[EntityName]Service, [EntityName]Service>();
  ```

### Step 3.4: Configure AutoMapper Mappings
- **Path**: `Utils\MappingProfile.cs`
- **Action**: Add mappings for the new entity:
  ```csharp
  CreateMap<[EntityName], [EntityName]Response>();
  CreateMap<[EntityName]Request, [EntityName]>();
  ```
