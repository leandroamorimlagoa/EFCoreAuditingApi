# Entity Framework Core 8 - Auditing Sample Project

This repository contains a sample project demonstrating how to implement auditing with Entity Framework Core 8. 
The project code is a part of a blog post, and it showcases how to track and log changes in your entities.

## Project Structure

The project is structured into the following main parts:

1. **Auditings**: Folder where the files necessary for Auditing are located.
- 1.1 **Model**: Objects used to store the desired audit data and information. These are the entities `FOR THE AUDITING APPLICATION`.
- 1.2 **Repositories**: Here is the repository with the methods used in the audit scope, both to create records and to consult.
- 1.3 **Services**: Service that is called to interact with the audit repository. Even when saving the business entity.
2. **Contexts**: This folder contains the `BusinessContext` for the entity classes used in the Application.
3. **Controllers**: This folder contains the controllers that handle HTTP requests and responses.
4. **Core**: This folder contains the classes that are shared across the application.
5. **Models**: This folder contains the entity classes that represent the database tables `FOR THE BUSINESS APPLICATION`.


## Setup and Running

The idea is to simplify the process so you can quickly view the example.

To set up and run the project, follow these steps:

1. Clone the repository to your local machine.
2. Open the project in Visual Studio.		
3. Restore the packages.
5. Press `F5` to run the project.

The swagger will open, and you can test the endpoints.

The database storage role is handled by DbContext `BusinessContext` with InMemory database, 

Also with the `AuditingDatabase` class that simulates the audit database.

Both databases are configured as a Singleton instance. They simulate separate databases, as recommended for this purpose.

To be audited the entity must inherits the `AuditableEntity` baseClass, which contains the `Id` property and the `Modified` property.

## Auditing Implementation

The auditing is implemented using Entity Framework Core's Change Tracking API. 

The `SaveChanges` method in the `BusinessContext` has been overridden to automatically log changes whenever entities are added, modified, or deleted.


## How to Use

There is a `AuditingsController` that exposes the following endpoints:

1. `GET /api/Auditings/{entityName}`: Returns all audit records from the specified entity.
2. `GET /api/Auditings/{entityName}/{id}`: Returns the audit record with the specified ID.

## Conclusion

This project demonstrates how to implement auditing with Entity Framework Core 8.
The auditing is implemented using Entity Framework Core's Change Tracking API, and it logs changes whenever entities are added, modified, or deleted.

# Contacts
Email: leamorim@outlook.com
LinkedIn: https://www.linkedin.com/in/leandrolagoa
Blog: https://leandrolagoa.wordpress.com