# About:
ASP.NET Core Web API solution to demonstrate typical backend-frontend interaction. 
Frontend is setup as a Blazor Server app.
The project queries the EU VIES web service using SOAP protocol to verify VAT IDs input by the user, which is accessible through this API with a RESTful approach to architecture
by using standard HTTP methods, caching and statelessness.

# Setup:
Open as visual studio solution. 
Three projects should be within (Backend, Frontend, Tests)

Build solution. It should be fairly plug and play.
Run Backend project first. 
Verify that the localhost port for the backend is correct in the frontend. This can
easily be rectified in the Vatverification.razor component under Frontend -> Pages.

Run frontend while backend is running. 
If both are running and the localhost port is correct in frontend, the frontend should send requests to the backend through the 
input form found on the navbar to the left called "Verify VAT"
