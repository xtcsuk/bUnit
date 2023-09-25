using AngleSharp.Dom;
using AntDesign;
using AntDesign.JsInterop;
using AntDesign.Tests;
using Bunit.Demo.Pages;

namespace Bunit.Demo.Tests.Pages
{
    public class AntDesignSimpleCounterTests : AntDesignTestBase
    {
        public AntDesignSimpleCounterTests()
        {
            JSInterop.Setup<Window>("AntDesign.interop.domInfoHelper.getWindow");
            JSInterop.SetupVoid("AntDesign.interop.domManipulationHelper.addElementTo", _ => true);
        }

        [Fact]
        public void Should_Button_Count_Be_As_Expected()
        {
            // arrange
            const int expectedCount = 2;

            var cut = RenderComponent<AntDesignComplexScenarios>();

            var buttons = cut.FindComponents<Button>();

            // act
            var actualCount = buttons.Count;

            // assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void Should_Panel_Be_InVisible_On_First_Render()
        {
            // arrange
            var cut = RenderComponent<AntDesignComplexScenarios>();

            // act
            var exception = Record.Exception(() => cut.Find("#panelShowPanel"));

            // assert
            Assert.IsType<ElementNotFoundException>(exception);
        }

        [Fact]
        public void Should_Panel_Be_Visible_When_Show_Panel_Button_Clicked_First_Time()
        {
            // arrange
            var cut = RenderComponent<AntDesignComplexScenarios>();

            IElement? button = null;

            #region find by Id
            button = cut.Find("#buttonShowPanel");
            #endregion find by Id

            #region find all and pick the one we after
            var buttons = cut.FindAll("button");
            button = buttons.Single(b => b.TextContent.Contains("Show Panel"));
            #endregion find all and pick the one we after

            // act
            button.Click();

            // assert
            var paragraph = cut.Find("#panelShowPanel");
            Assert.Contains("<strong>bUnit demo</strong>", paragraph.Html());
        }

        [Fact]
        public void Should_Modal_Be_InVisible_On_First_Render()
        {
            // arrange
            var cut = RenderComponent<AntDesignComplexScenarios>();

            // act, assert
            Assert.True(!cut.Instance.Showodal);
        }

        [Fact]
        public void Should_Modal_Be_Visible_When_Show_Modal_Button_Clicked()
        {
            // arrange
            var cut = RenderComponent<AntDesignComplexScenarios>();

            var button = cut.Find("#buttonShowModal");

            // act
            button.Click();

            // assert
            Assert.True(cut.Instance.Showodal);
            Assert.Contains("Id=\"modalForm\"", cut.Markup);
        }
    }
}
