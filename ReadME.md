Online marketplace using ASP.NET MVC using onion architecture

## Layers

- JoJo: MVC Project
- JoJo.Core: Entitiy Models (created by Entity Framework)
- JoJo.Repo: Repositories and Unit of Work
- JoJo.Service: Service Layer (where we use UnitOfWork)

## Note

There is a sample service demostrating CRUD operations under service layer (Sample.cs)
