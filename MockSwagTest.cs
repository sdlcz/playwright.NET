using Microsoft.Playwright.NUnit;

namespace NunitPlaywrightTests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class MockSwagTest : PageTest
    {
        [SetUp]
        public async Task SetUp()
        {
            // Code to set up before each test
            await Page.GotoAsync("https://www.saucedemo.com/v1/");
        }

        [Test]
        public async Task VerifyPageElements ()
        {
            var pageTitle = await Page.TitleAsync();
            Assert.AreEqual("Swag Labs", pageTitle, "Page title should be 'Swag Labs'.");
        }

        [Test]
        public async Task VerifyLogin()
        {

        }

        [TearDown]
        public async Task TearDown()
        {
            // Code to run after each test
            await Page.CloseAsync();
        }
    }
}
