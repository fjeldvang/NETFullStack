using Moq;
using NUnit.Framework;
using System.Text;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task VerifyVatAsync()
        {
            string countryCode = "DE";
            string vatId = "118856456";

            var result = await VatVerifier.VerifyAsync(countryCode, vatId);

            Assert.That(result, Is.EqualTo(VatVerifier.VerificationStatus.Valid));
        }
    }
}