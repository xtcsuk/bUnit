using Bunit.Demo.Pages;

namespace Bunit.Demo.Tests.Pages
{
    public class StandardInputTests : TestContext
    {
        [Fact]
        public void Should_Reflect_Changes()
        {
            var cut = RenderComponent<StandardInput>();
            var inputField = cut.Find("input");

            // Add first item
            inputField.Change("First item");
            inputField.KeyUp(key: "Enter");

            // Assert that first item was added correctly
            var diffs = cut.GetChangesSinceFirstRender();
            diffs.ShouldHaveSingleChange()
              .ShouldBeAddition("<li>First item</li>");

            // Save snapshot of current DOM nodes
            cut.SaveSnapshot();

            // Add a second item
            inputField.Change("Second item");
            inputField.KeyUp(key: "Enter");

            // Assert that both first and second item was added
            // since the first render
            diffs = cut.GetChangesSinceFirstRender();
            diffs.ShouldHaveChanges(
              diff => diff.ShouldBeAddition("<li>First item</li>"),
              diff => diff.ShouldBeAddition("<li>Second item</li>")
            );

            // Assert that only the second item was added
            // since the call to SaveSnapshot()
            diffs = cut.GetChangesSinceSnapshot();
            diffs.ShouldHaveSingleChange()
              .ShouldBeAddition("<li>Second item</li>");

            // Save snapshot again of current DOM nodes
            cut.SaveSnapshot();

            // Click last item to remove it from list
            cut.Find("li:last-child").Click();

            // Assert that the second item was removed
            // since the call to SaveSnapshot()
            diffs = cut.GetChangesSinceSnapshot();
            diffs.ShouldHaveSingleChange();

        }
    }
}
