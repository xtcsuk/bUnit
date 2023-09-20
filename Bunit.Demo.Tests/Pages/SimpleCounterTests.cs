using Bunit.Demo.Pages;

namespace Bunit.Demo.Tests.Pages
{
    public class SimpleCounterTests : TestContext
    {
        [Fact]
        public void Find_Button_And_Match_Markup_After_Button_Click()
        {
            // arrange
            var cut = RenderComponent<SimpleCounter>();

            var button = cut.Find("button");

            // act
            button.Click();

            // assert
            Assert.NotNull(button);
            cut.Find("p").MarkupMatches("<p role=\"status\">Current count: 1</p>");
        }

        [Fact]
        public void Find_Button_And_Markup_Difference_After_Button_Click()
        {
            // arrange
            var cut = RenderComponent<SimpleCounter>();

            var button = cut.Find("button");

            // act
            button.Click();

            // assert - find differences between first render and click
            var diffs = cut.GetChangesSinceFirstRender();
            // only expect there to be one change
            diffs.ShouldHaveSingleTextChange("Current count: 1");
        }

    }
}
