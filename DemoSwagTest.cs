<<<<<<< HEAD
﻿using NUnit.Framework;
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
        {
            // Negative test case for invalid login
            await Page.FillAsync("//input[@id='user-name']", "invalid_user");
            await Page.FillAsync("//input[@id='password']", "  ");
            await Page.ClickAsync("//input[@id='login-button']");

            var errorMessage = await Page.InnerTextAsync("//*[@id=\"login_button_container\"]/div/form/h3");
            Assert.That(errorMessage, Is.EqualTo("Epic sadface: Username and password do not match any user in this service"), "Error message should be displayed for invalid login");

        }

        [TearDown]
        public async Task TearDown()
        {
            await Page.CloseAsync();
        }
    }
}
=======
using NUnit.Framework;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System.Threading.Tasks;

namespace NunitPlaywrightTests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class DemoSwagTest : PageTest
    {
        [SetUp]
        public async Task SetUp()
        {
            await Page.GotoAsync("https://www.saucedemo.com/v1/");
            // Launch the browser
            //var playwright = await Playwright.CreateAsync();
        }

        [Test]
        public async Task VerifyPageElements()
        {
            var pageTitle = await Page.TitleAsync();
            Assert.That(pageTitle, Is.EqualTo("Swag Labs"), "Page title should be 'Swag Labs'.");

            var loginButton = await Page.QuerySelectorAsync("//input[@id='login-button']");
            Assert.IsNotNull(loginButton, "Login button should be present on the page");
        }

        [Test]
        public async Task VerifyInValidLogin()
        {
            // Negative test case for invalid login
            await Page.FillAsync("//input[@id='user-name']", "invalid_user");
            await Page.FillAsync("//input[@id='password']", "  ");
            await Page.ClickAsync("//input[@id='login-button']");

            var errorMessage = await Page.InnerTextAsync("//*[@id=\"login_button_container\"]/div/form/h3");
            Assert.That(errorMessage, Is.EqualTo("Epic sadface: Username and password do not match any user in this service"), "Error message should be displayed for invalid login");
        }

        [Test]
        public async Task VerifyValidLogin()
        { //Positive
          //Create auth directory to load existing authenticated state
          // Create Playwright instance
            var playwright = await Playwright.CreateAsync();

            // Launch the browser
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });

            // Load the authenticated storage state
            var context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                StorageStatePath = "auth.json"
            });

            // Open a new page in the authenticated context
            var page = await context.NewPageAsync();

            // Navigate to a page that requires authentication
            var inventoryPageUrl = "https://www.saucedemo.com/v1/inventory.html";
            await Page.GotoAsync(inventoryPageUrl); // Ensure the page navigates to the inventory URL
            await Expect(Page).ToHaveURLAsync(inventoryPageUrl);

            // Perform tests or interact with the authenticated page
            var title = await page.TitleAsync();
            System.Console.WriteLine($"Page Title: {title}");

        }


        [Test]
        public async Task VerifyTheInventoryPage() {
            var currentUrl = Page.Url;
            Assert.That(currentUrl, Does.Contain("inventory"), "The URL should contain 'inventory'.");

            // Verify all items are displayed with correct names, prices, and images
            var items = await Page.QuerySelectorAllAsync(".inventory_item");
            Assert.That(items.Count, Is.GreaterThan(0), "There should be items displayed on the inventory page.");

            foreach (var item in items)
            {
                var itemName = await item.QuerySelectorAsync(".inventory_item_name");
                var itemPrice = await item.QuerySelectorAsync(".inventory_item_price");
                var itemImage = await item.QuerySelectorAsync(".inventory_item_img");

                Assert.IsNotNull(itemName, "Item name should be present.");
                Assert.IsNotNull(itemPrice, "Item price should be present.");
                Assert.IsNotNull(itemImage, "Item image should be present.");

                var itemNameText = await itemName.InnerTextAsync();
                var itemPriceText = await itemPrice.InnerTextAsync();
                var itemImageSrc = await itemImage.GetAttributeAsync("src");

                Assert.That(itemNameText, Is.Not.Empty, "Item name should not be empty.");
                Assert.That(itemPriceText, Is.Not.Empty, "Item price should not be empty.");
                Assert.That(itemImageSrc, Is.Not.Empty, "Item image source should not be empty.");
            }

        }


        [TearDown]
        public async Task TearDown()
        {
            await Page.CloseAsync();
        }
    }
}
>>>>>>> ef160ce7fcda20e2726b3e1508fda39ea377a25d
