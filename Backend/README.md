# About:
.NET 7 Core Web API solution to demonstrate typical backend-frontend interaction. Frontend is setup as a Blazor Server app.

The project queries the EU VIES web service using SOAP protocol to verify VAT IDs input by the user, which is accessible through this API with a RESTful approach to architecture by using standard HTTP methods, caching and statelessness.

The project includes a preset example of a typical repository pattern separating the data layer from the business logic, with an example usage of dapper as well, all integrated as API endpoints. 
Other, probably better packages/libraries for database handling depending on use case like Entity Framework Core are also easily integrated into the existing logic. 
The endpoint(s) used to verify VAT IDs are integrated into the Blazor frontend, with some frontend validation before the data is sent to the endpoint.

# Setup:
Open as visual studio solution. 
Three projects should be within (Backend, Frontend, Tests)

Build solution. It should be fairly plug and play. 
Run Backend project first. 
Verify that the localhost port for the backend is correct in the frontend. 
This can easily be rectified in the Vatverification.razor component under Frontend -> Pages, or set manually in launchSettings.json.

Run frontend while backend is running. 
If both are running and the localhost port is correct in frontend, the frontend should send requests to the backend through the input form found on the navbar to the left called "Verify VAT"

# Project's use as a preset:
While the project is setup to simply use a single helper class doing SOAP queries to a web service as an endpoint, it can very easily be expanded by applying your business logic to the BackendService class and database queries to the BackendRepository. 
Two examples of this is provided with the hypothetical case of a travel agency and hypothetical endpoints in these classes. 

Better practice however would be to rename these or create new service(s) with their own responsibilities, and not just a general name like "backend" which is purely named as such for demonstration purposes. 
These classes then as per .NET architecture will have to be added to the builder as transient services (or scoped/singletons depending on use) as demonstrated in the program.cs. 

Then, endpoints are to be made in the controller classes for the new methods in the service(s) you created. These will then appear in swagger UI and can be called. 
Any frontend of your choosing can then be made to interact with them.
