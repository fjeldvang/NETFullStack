# About
ASP.NET Core Web API solution to demonstrate typical backend-frontend interaction. 
Frontend is setup as a Blazor Server app.

The project queries the EU VIES web service using SOAP protocol to verify VAT IDs input by the user, which is accessible through this API with a RESTful approach to architecture
by using standard HTTP methods, caching and statelessness.

The project includes a preset example of a typical repository pattern separating the data layer from the business logic, with an example usage of dapper as well, all integrated as API endpoints. 
Other, probably better packages/libraries for database handling depending on use case like Entity Framework Core are also easily integrated into the existing logic.
The endpoint(s) used to verify VAT IDs are integrated into the Blazor frontend, with some frontend validation before the data is sent to the endpoint.

# Setup
Open as visual studio solution. 
Three projects should be within (Backend, Frontend, Tests)

Build solution. It should be fairly plug and play.
Run Backend project first. 
Verify that the localhost port for the backend is correct in the frontend. This can
easily be rectified in the Vatverification.razor component under Frontend -> Pages, or set manually in launchSettings.json.

Run frontend while backend is running. 
If both are running and the localhost port is correct in frontend, the frontend should send requests to the backend through the 
input form found on the navbar to the left called "Verify VAT"
