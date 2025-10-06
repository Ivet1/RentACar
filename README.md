Project: Rent-a-Car - car rental management system.
Developed with: ASP.NET Core MVC, SQL Server, Entity Framework. Development environment: Visual Studio 2022 (.NET 9.0). Main goal: Simplify the car rental and leasing process.

System architecture:
Three-tier architecture:
Presentation layer (View) - Interacts with the user via Razor Pages and Bootstrap.
Business layer (Controllers & Services) - Processes requests and business logic.
Data (Data Layer) - Uses Entity Framework Core to work with the database (SQL Server).

Roles and users:
The system has two types of users:
Administrator:
Manages users.
Adds, edits and deletes cars.
Approves rental requests.
User:
Views available cars.
Sends rental requests.

Main functionalities:
Car management: Add, edit, delete.
Rental Process:
User selects a car and sends a request.
Administrator processes requests.
System that prevents duplicate booking.
Security:
ASP.NET Core Identity for user authentication and management.
Validation for unique EGN , username and email.

Technologies and tools:
Backend: ASP.NET Core MVC, C#, Entity Framework Core.
Frontend: Razor Pages, Bootstrap.
Database: SQL Server, managed via Entity Framework.
Security: ASP.NET Core Identity
Database:
Main tables:
Users: Contains information about users.
Cars: Details about available cars.
RentalRequests: Stores rental requests.
Roles: Manage user roles.

Navigation and views:
Home: Home page with an overview of available cars.
Account: Login, register, logout.
Cars: Details, adding, editing and deleting cars (for administrators).
RentalRequests: Submitting and managing rental requests.

Conclusion:
System Benefits:
Automates management of rental cars.
Enables efficient administration of requests.
Ensures security and data protection.
Easily facilitates interaction between users and administrators.
Future improvements:
Implement online payments.
Expand car filtering options.
Improve UI/UX design.
