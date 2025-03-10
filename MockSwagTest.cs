using NUnit.Framework;
using Microsoft.Playwright;
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
            await Page.GotoAsync("https://www.saucedemo.com/v1/");
        }

        [Test]
        public async Task VerifyPageElements ()
        {
            var pageTitle = await Page.TitleAsync();
            Assert.AreEqual("Swag Labs", pageTitle, "Page title should be 'Swag Labs'.");

            var loginButton = await Page.QuerySelectorAsync("#login-button");
            Assert.IsNotNull(loginButton,"Login button should be present on the page");
        }

        [Test]
        public async Task VerifyValidLogin()
        { //Positive
           //Create auth directory to load existing authenticated state

        }

        [Test]
        public async Task VerifyInValidLogin()
        { //Negative


        }

        [TearDown]
        public async Task TearDown()
        {
            await Page.CloseAsync();
        }
    }
}
