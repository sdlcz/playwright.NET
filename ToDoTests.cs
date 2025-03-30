using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightTests
{
	public class ToDoTests
	{
		[Test]
		public async Task TestToDoApp()
		{
			using var playwright = await Playwright.CreateAsync();
			await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
			{
				Headless = false,
			});
			var context = await browser.NewContextAsync();
			var page = await context.NewPageAsync();

			await NavigateToTodoMVC(page);
			await VerifyPageElements(page);
			await InteractWithTodos(page);
			await VerifySnapshots(page);
		}

		private async Task NavigateToTodoMVC(IPage page)
		{
			await page.GotoAsync("https://demo.playwright.dev/todomvc/#/");
			await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "todos" })).ToBeVisibleAsync();
		}

		private async Task VerifyPageElements(IPage page)
		{
			await page.GetByText("Double-click to edit a todo").ClickAsync();

		}

		private async Task InteractWithTodos(IPage page)
		{
			var todos = new[] { "cycling", "reading", "gaming", "sleeping", "shopping", "cleaning" };
			foreach (var todo in todos)
			{
				await page.GetByText(todo).ClickAsync();
			}

			await page.GetByRole(AriaRole.Link, new() { Name = "Active" }).ClickAsync();
			await page.GetByRole(AriaRole.Link, new() { Name = "Completed" }).ClickAsync();
			await page.GetByRole(AriaRole.Link, new() { Name = "Active" }).ClickAsync();
			await page.GetByRole(AriaRole.Link, new() { Name = "All" }).ClickAsync();
		}

		private async Task VerifySnapshots(IPage page)
		{
			await page.ScreenshotAsync(new PageScreenshotOptions { Path = "body-cycling.png" });
			await Assertions.Expect(page.Locator("body")).ToContainTextAsync("cycling");
			await Assertions.Expect(page.Locator("body")).ToContainTextAsync("reading");
			await Assertions.Expect(page.Locator("body")).ToContainTextAsync("Active");
			await Assertions.Expect(page.Locator("body")).ToContainTextAsync("All");
			await Assertions.Expect(page.Locator("body")).ToContainTextAsync("All Active Completed");
			await Assertions.Expect(page.Locator("body")).ToContainTextAsync("Completed");

			var todosToToggle = new[] { "cycling", "gaming", "sleeping" };
			foreach (var todo in todosToToggle)
			{
				await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = todo }).GetByLabel("Toggle Todo").CheckAsync();
			}

			await page.ScreenshotAsync(new PageScreenshotOptions { Path = "body-cycling-toggled.png" });
			await Assertions.Expect(page.GetByText("cycling")).ToBeVisibleAsync();

			await page.GetByRole(AriaRole.Link, new() { Name = "Active" }).ClickAsync();
			await page.GetByRole(AriaRole.Link, new() { Name = "Completed" }).ClickAsync();
			await page.ScreenshotAsync(new PageScreenshotOptions { Path = "html-completed.png" });

			await page.GetByRole(AriaRole.Link, new() { Name = "Active" }).ClickAsync();
			await page.ScreenshotAsync(new PageScreenshotOptions { Path = "html-active.png" });

			await page.GetByRole(AriaRole.Link, new() { Name = "All" }).ClickAsync();
			await page.ScreenshotAsync(new PageScreenshotOptions { Path = "html-all.png" });
		}
	}
}
