using Microsoft.AspNetCore.Mvc;
using static VatVerifier;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VatController : ControllerBase
    {

        [HttpGet("VerifyVat")]
        public async Task<ActionResult<VerificationStatus>> VerifyVatAsync(string countryCode, string vatId)
        {
            var verificationStatus = await VatVerifier.VerifyAsync(countryCode, vatId);

            if (verificationStatus == VerificationStatus.Valid)
            {
                Response.Headers.Add("Cache-Control", "max-age=3600"); // cache for 1 hr
            }
            return Ok(verificationStatus);
        }
    }
}
