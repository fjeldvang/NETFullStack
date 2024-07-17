# About:
The API from task 4 is setup as an ASP.NET Core Web API project, while the frontend is setup as a Blazor Server app.
Testing is done with NUnit.
All the tasks have been integrated within these three projects to provide a
functional, testable system (with the exception of database). 
As little as possible of the original models and functionality provided have been moved or changed.
(Logger provided also not implemented in solution.)

# Setup:
Open as visual studio solution. 
Three projects should be within (API, Frontend, Tests)
Build solution. It should be fairly plug and play.
Run Onyx.API first. 
Verify that the localhost port for the Onyx.API is correct in the frontend. This can
easily be rectified in the Vatverification.razor component under Frontend -> Pages.
Run frontend while Onyx.API is running. 
If both are running and the localhost port is correct in frontend, the frontend should send requests to the backend through the 
input form found on the navbar to the left called "Verify VAT"

# Task overview:
### Task 1

a): LINQ expression can be found in OnyxService.GetRepeatedGuestNames()
b): LINQ expression can be found in OnyxService.GetNumOfNightsByTravelAgent()
c): SQL Query can be found in OnyxRepository.GetTravelAgentsWithoutObservations()
d): SQL Query can be found in OnyxRepository.GetTravelAgentsObservedTwiceOrMore()

### Task 2

a) Logger is rewritten in Onyx.API->Models->Logger. 
Providers with interfaces replace the original parameters for better testability and to allow unit testing without relying on external input/output. 
Dependency injection is used through the constructor of these interfaces.

b) TestLoggerPrefixes() is written in the Tests project as a unit test. 
It creates mock instances of the IDateTimeProvider and IStreamWriterProvider interfaces,
constructs a logger from this then asserts that the contents being written when logging is the prefixed input data.

### Task 3

a) The missing Verify method is implemented through the VerifyAsync() asynchronous method
from the EU VIES WSDL in the VatVerifier class found in Onyx.API -> Models -> VatVerifier.cs. 
Method sends a request to check VAT validity, and returns VerificationStatus.Valid when valid.
Returns Invalid when invalid, and Unavailable when request fails or retry policy (deemed necessary due to rejection of requests/no response) runs out of max tries.

### Task 4

a) API with a controller Onyx.API -> Controllers -> VatController.cs is created, runs asynchronously and awaits VatVerifier.VerifyAsync() method.
Returns VerificationStatus enum.

b) Created in blazor. Parses full VAT ID, makes a request to check validity of provided VAT ID through the API.
Returns VerificationStatus in human-friendly format


# Note: Old README file content below: 
# Developer assignment 1 - Tønsberg

During the recruitment process we want the candidates to solve a few development tasks. We use these tasks in our interviews to assess the candidate's ability to solve relevant problems. These solutions will be discussed during the interview process, giving the candidate the opportunity to demonstrate her or his skill level.

## Instructions
1. Clone this repository.
2. Read through the whole assignment, the tasks are somewhat related.
3. Use Visual Studio or any other IDE you prefer.
4. To the best of your ability, try to solve each and every one of the following tasks.
   * Think about how you would solve tasks you may not be able to solve.
5. Prepare to demonstrate your solution, either online or on site.

## Assignment

### Task 1

Given the below classes:

* a) Create a LINQ expression for all guest names that occur on more than once (across all invoice groups and invoices, not per invoice group or invoice).

* b) Create a LINQ expression for the total number of nights per travel agent for invoice groups issued in 2015.

```c#
class InvoiceGroup
{
    public DateTime IssueDate { get; set; }
    public List<Invoice> Invoices { get; set; }
}

class Invoice
{
    public List<Observation> Observations { get; set; }
}

class Observation
{
    public string TravelAgent { get; set; }
    public string GuestName { get; set; }
    public int NumberOfNights { get; set; }
}

class TravelAgentInfo
{
    public string TravelAgent { get; set; }
    public int TotalNumberOfNights { get; set; }
}

var invoiceGroups = newList<InvoiceGroup>();

// a)
IEnumerable<string> repeatedGuestNames = invoiceGroups... 

// b)
IEnumerable<TravelAgentInfo> numberOfNightsByTravelAgent = invoiceGroups..

```

Given corresponding database tables `TravelAgent` and `Observation`, both with a `TravelAgent` field being the primary and foreign key, respectively:

* c) Write a SQL query that finds all travel agents that does not have any observations.

     `SELECT * FROM TravelAgent ...`

* d) Write a SQL query that finds all travel agents that have more than two observations.

    `SELECT * FROM TravelAgent ...`

### Task 2

Given the below `Logger` class:

* a) Refactor the class so that it can be unit-tested in isolation (independent of external input/output, like file system and current time).

* b) Write a unit test that asserts that the `Logger.Log` method prefixes the input string with date and time (avoiding external input/output if possible).

```c#
class Logger
{
    private readonly StreamWriter _writer;

    public Logger(string path)
    {
        _writer = new StreamWriter(File.Open(path, FileMode.Append))
        {
            AutoFlush = true
        };

        Log("Logger initialized");
    }

    public void Log(string str)
    {
        _writer.WriteLine(string.Format("[{0:dd.MM.yy HH:mm:ss}] {1}", DateTime.Now, str));
    }
}
```

### Task 3

Given the below `VatVerifier` class:

* a) Implement the missing Verify method that uses EU VIES web service to verify VAT IDs.
  * EU VIES WSDL URL: http://ec.europa.eu/taxation_customs/vies/checkVatService.wsdl
  * Example of valid German VAT ID: DE118856456

```c#
class VatVerifier
{
    enum VerificationStatus
    {
        Valid,
        Invalid,
        // Unable to get status (e.g. service unavailable)
        Unavailable
    }

    /// <summary>
    /// Verifies the given VAT ID for the given country using the EU VIES web service.
    /// </summary>
    /// <param name="countryCode"></param>
    /// <param name="vatId"></param>
    /// <returns>Verification status</returns>
    // TODO: Implement Verify method
}
```

### Task 4

* a) Create an API with a controller that uses the `VatVerifier` class to verify a VAT ID based on a provided country code and VAT ID.

* b) Create simple front-end using Angular, React or any other frontend framework of your choosing, with a simple user interface that integrates with the API created in sub-task a) and allows validating VAT IDs and show their validation status.