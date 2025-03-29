using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

using var playwright = await Playwright.CreateAsync();
await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
{
    Headless = false,
});
var context = await browser.NewContextAsync();

var page = await context.NewPageAsync();
await page.GotoAsync("https://demo.playwright.dev/todomvc/#/");
await page.GetByRole(AriaRole.Heading, new() { Name = "todos" }).ClickAsync();
await page.GetByRole(AriaRole.Textbox, new() { Name = "What needs to be done?" }).ClickAsync();
await page.GetByRole(AriaRole.Textbox, new() { Name = "What needs to be done?" }).FillAsync("shop");
await page.GetByRole(AriaRole.Textbox, new() { Name = "What needs to be done?" }).PressAsync("Enter");
await page.GetByRole(AriaRole.Textbox, new() { Name = "What needs to be done?" }).FillAsync("clean");
await page.GetByRole(AriaRole.Textbox, new() { Name = "What needs to be done?" }).PressAsync("Enter");
await page.GetByRole(AriaRole.Textbox, new() { Name = "What needs to be done?" }).FillAsync("read book");
await page.GetByRole(AriaRole.Textbox, new() { Name = "What needs to be done?" }).PressAsync("Enter");
await page.GetByRole(AriaRole.Textbox, new() { Name = "What needs to be done?" }).FillAsync("game");
await page.GetByRole(AriaRole.Textbox, new() { Name = "What needs to be done?" }).PressAsync("Enter");
await page.GetByRole(AriaRole.Textbox, new() { Name = "What needs to be done?" }).FillAsync("cycle");
await page.GetByRole(AriaRole.Textbox, new() { Name = "What needs to be done?" }).PressAsync("Enter");
await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "shop" }).GetByLabel("Toggle Todo").CheckAsync();
await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "shop" }).GetByLabel("Toggle Todo").UncheckAsync();
await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "read book" }).GetByLabel("Toggle Todo").CheckAsync();
await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "read book" }).GetByLabel("Toggle Todo").UncheckAsync();
await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "cycle" }).GetByLabel("Toggle Todo").CheckAsync();
await page.GetByText("cycle").ClickAsync();
await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "cycle" }).GetByLabel("Toggle Todo").UncheckAsync();
await page.GetByText("cycle").ClickAsync();
await page.GetByRole(AriaRole.Textbox, new() { Name = "Edit" }).FillAsync("sleep");
await page.GetByRole(AriaRole.Textbox, new() { Name = "Edit" }).PressAsync("Enter");
await page.GetByRole(AriaRole.Link, new() { Name = "Active" }).ClickAsync();
await page.GetByRole(AriaRole.Link, new() { Name = "Completed" }).ClickAsync();
await page.GetByRole(AriaRole.Link, new() { Name = "All" }).ClickAsync();
await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "shop" }).GetByLabel("Toggle Todo").CheckAsync();
await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "read book" }).GetByLabel("Toggle Todo").CheckAsync();
await page.GetByRole(AriaRole.Link, new() { Name = "Active" }).ClickAsync();
await page.GetByRole(AriaRole.Link, new() { Name = "Completed" }).ClickAsync();
await page.GetByRole(AriaRole.Link, new() { Name = "All" }).ClickAsync();
await page.GetByText("All Active Completed").ClickAsync();
await page.GetByRole(AriaRole.Link, new() { Name = "All" }).ClickAsync();
await page.GetByRole(AriaRole.Button, new() { Name = "Clear completed" }).ClickAsync();
await page.GetByRole(AriaRole.Link, new() { Name = "Completed" }).ClickAsync();
await page.GetByRole(AriaRole.Link, new() { Name = "Active" }).ClickAsync();
await page.GetByRole(AriaRole.Link, new() { Name = "All" }).ClickAsync();
await Expect(page.GetByRole(AriaRole.Heading, new() { Name = "todos" })).ToBeVisibleAsync();
await page.GetByRole(AriaRole.Textbox, new() { Name = "What needs to be done?" }).ClickAsync();
await page.GetByRole(AriaRole.Link, new() { Name = "All" }).ClickAsync();
await page.GetByRole(AriaRole.Link, new() { Name = "Active" }).ClickAsync();
await page.GetByRole(AriaRole.Link, new() { Name = "Completed" }).ClickAsync();
await page.GetByText("All Active Completed").ClickAsync();
