# SolutionWebApplicationPatients
NET Core 6.0 Challenge

Requirements:
•	C# 
•	.NET 6
•	REST API
•	SQL Server Express
•	Swagger
•	Security / authentication
•	Validation

I did:
- Local DB on project
- Patient schema.
- Use Entity Framework.
- Patients CRUD and validate that User is authanticate before responds the request.
- Additional add Seed method in order to create fake patient registers for testing. 
- Apply Pipelines UseAuthentication and UseAuthorization.
- Apply JwtBearer token.
- User schema for testing (just simple schema).
- Addtional methods login, addUser (I don´t apply this method security in order to create new user for testing).
- Apply Memory cache and Pagination

Note:
if database not work, you execute this commands:
  * Add-Migration Third
  * Update-Database
Use Administrator of package´s console

For testing, you can use the user: admin and password: 123456. However, you can create other users.

