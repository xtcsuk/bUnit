using AngleSharp.Dom;
using AntDesign;
using AntDesign.JsInterop;
using AntDesign.Tests;
using BackOffice.Shared.UI.Notifications.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bunit.Demo.Tests.Pages
{
    public class AntDesignComplexScenariosTests : AntDesignTestBase
    {
        private readonly IToastService? _toastService;
        private readonly NotificationService _notificationService;

        public AntDesignComplexScenariosTests()
        {
            JSInterop.Setup<Window>("AntDesign.interop.domInfoHelper.getWindow");
            JSInterop.SetupVoid("AntDesign.interop.domManipulationHelper.addElementTo", _ => true);

            JSInterop.Setup<Window>("AntDesign.interop.domInfoHelper.getWindow");
            JSInterop.SetupVoid("AntDesign.interop.domManipulationHelper.addElementTo", _ => true);
            JSInterop.SetupVoid("AntDesign.interop.modalHelper.focusDialog", _ => true);
            JSInterop.SetupVoid("AntDesign.interop.domManipulationHelper.disableBodyScroll");

            // service mocking and registration
            _notificationService = new NotificationService();
            _toastService = new ToastService(_notificationService);

            Services.AddSingleton(instance => _toastService);
        }

        [Fact]
        public void Should_Button_Count_Be_As_Expected()
        {
            // arrange
            const int expectedCount = 3;

            var cut = base.RenderComponent<Demo.Pages.AntDesignComplexScenarios>();

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
            var cut = base.RenderComponent<Demo.Pages.AntDesignComplexScenarios>();

            // act
            var exception = Record.Exception(() => cut.Find("#panelShowPanel"));

            // assert
            Assert.IsType<ElementNotFoundException>(exception);
        }

        [Fact]
        public void Should_Panel_Be_Visible_When_Show_Panel_Button_Clicked_First_Time()
        {
            // arrange
            var cut = base.RenderComponent<Demo.Pages.AntDesignComplexScenarios>();

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
        public void Should_Modal_Be_NoInVisible_On_First_Render()
        {
            // arrange
            var cut = base.RenderComponent<Demo.Pages.AntDesignComplexScenarios>();

            // act, assert
            Assert.True(!cut.Instance.Showodal);
        }

        [Fact]
        public void Should_Modal_Be_Visible_When_Show_Modal_Button_Clicked()
        {
            // arrange
            var cut = base.RenderComponent<Demo.Pages.AntDesignComplexScenarios>();

            var button = cut.Find("#buttonShowModal");

            // act
            button.Click();
            var modalTitleClassDiv = cut.Find(".ant-modal-title");

            // assert
            Assert.True(cut.Instance.Showodal);
            Assert.Contains("Modal example", modalTitleClassDiv.InnerHtml);
        }

        [Fact]
        public async Task Should_Show_Success_Notification()
        {
            // arrange
            var cut = RenderComponent<Demo.Pages.AntDesignComplexScenarios>();

            var okButton = cut.FindComponents<Button>()
                .Single(b => b.Markup.Contains("Show Message"));

            var notificationComponent = cut.FindComponent<Notification>();

            // act
            await cut.InvokeAsync(okButton.Instance.OnClick.InvokeAsync);

            // assert
            Assert.Contains("ant-notification", notificationComponent.Markup);
        }

        [Fact]
        public async Task Should_Show_Success_Notification_Message()
        {
            // arrange
            var cut = base.RenderComponent<Demo.Pages.AntDesignComplexScenarios>();

            var okButton = cut.FindComponents<Button>()
                .Single(b => b.Markup.Contains("Show Message"));

            // act
            await cut.InvokeAsync(okButton.Instance.OnClick.InvokeAsync);
            
            //cut.WaitForElement("#divShowMessage");  // use this to wait for the element update relying on the result of click and when click is async and not awaited
            var showMessageDiv = cut.Find("#divShowMessage");

            // assert
            Assert.Contains("Success", showMessageDiv.InnerHtml);
        }

    }
}
