using System.ServiceModel;
using VatService;

public class VatVerifier
{
    public enum VerificationStatus
    {
        Valid,
        Invalid,
        Unavailable
    }

    public static async Task<VerificationStatus> VerifyAsync(string countryCode, string vatId)
    {
        int maxRetries = 5;
        int retryDelayMilliseconds = 2000;

        try
        {
            using (var client = new checkVatPortTypeClient())
            {
                for (int retryCount = 0; retryCount < maxRetries; retryCount++)
                {
                    var request = new checkVatRequest
                    {
                        countryCode = countryCode,
                        vatNumber = vatId
                    };

                    try
                    {
                        var response = await client.checkVatAsync(request);

                        if (response.valid)
                        {
                            retryCount = maxRetries;
                            return VerificationStatus.Valid;
                        }
                        else
                        {
                            retryCount = maxRetries;
                            return VerificationStatus.Invalid;
                        }
                    }
                    catch (FaultException ex) when (ex.Code.Name == "Receiver" && ex.Code.Namespace == "http://schemas.xmlsoap.org/soap/envelope/")
                    {
                        Console.WriteLine($"checkVatService API is unavailable or failed processing a valid request: {ex.Message}");
                        return VerificationStatus.Unavailable;
                    }
                    catch (FaultException ex) when (ex.Message == "INVALID_INPUT")
                    {
                        return VerificationStatus.Invalid;
                    }
                    catch (FaultException ex) when (ex.Message == "MS_MAX_CONCURRENT_REQ")
                    {
                        await Task.Delay(retryDelayMilliseconds + new Random().Next(0, 1000));
                    }
                    catch (EndpointNotFoundException)
                    {
                        Console.WriteLine("Backend issues, endpoint not found. Start backend");
                    }
                    catch (FaultException ex)
                    {
                        Console.WriteLine($"SOAP fault exception: {ex.Message}");
                        await Task.Delay(retryDelayMilliseconds + new Random().Next(0, 1000));
                    }
                }
            }
            // retries failed
            return VerificationStatus.Unavailable;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected exception: {ex.Message}");
            return VerificationStatus.Unavailable;
        }
    }


}
