using AngleSharp.Dom;
using AntDesign;
using AntDesign.Tests;
using BackOffice.Shared.UI.Notifications.Services;
using Bunit.Demo.Models;
using Bunit.Demo.Pages.Modals;
using Microsoft.Extensions.DependencyInjection;

namespace Bunit.Demo.Tests.Pages.Modals
{    
    public class ModalExampleTests : AntDesignTestBase
    {
        private readonly IToastService? _toastService;
        private readonly NotificationService _notificationService;

        public ModalExampleTests()
        {
            JSInterop.SetupVoid("AntDesign.interop.domManipulationHelper.addElementTo", _ => true);
            JSInterop.SetupVoid("AntDesign.interop.modalHelper.focusDialog", _ => true);
            JSInterop.SetupVoid("AntDesign.interop.domManipulationHelper.disableBodyScroll");


            // service mocking and registration
            _notificationService = new NotificationService();
            _toastService = new ToastService(_notificationService);

            Services.AddScoped(instance => _toastService);
        }

        [Fact]
        public void Should_Checkbox_Be_Unchecked()
        {
            // arrange
            var cut = RenderComponent<ModalExample>(parameters => 
            {
                parameters.Add(p => p.Model, new ModalModel { Checked = false });
            });

            // act
            var checkBox = cut.Find("#checkBoxCheckMe");
            var attrib = checkBox.Attributes.FirstOrDefault(a => a.LocalName == "checked");

            // assert
            Assert.Null(attrib);
        }

        [Fact]
        public void Should_Checkbox_Be_Checked()
        {
            // arrange
            var cut = RenderComponent<ModalExample>(parameters =>
            {
                parameters.Add(p => p.Model, new ModalModel { Checked = true });
            });

            var checkBox = cut.Find("#checkBoxCheckMe");

            // act
            var attrib = checkBox.Attributes.FirstOrDefault(a => a.LocalName == "checked");

            // assert
            Assert.NotNull(attrib);
            Assert.IsAssignableFrom<IAttr>(attrib);
        }

        [Fact]
        public void Should_Show_Sucess_Notification()
        {
            // arrange
            var cut = RenderComponent<ModalExample>();

            var okButton = cut.FindComponents<Button>()
                .Single(b => b.Markup.Contains("OK"));

            // act
            cut.WaitForState(() => { okButton.Instance.OnClick.InvokeAsync(); return true; });
            var notificationComponent = cut.FindComponent<Notification>();

            // assert
            Assert.Contains("ant-notification-notice-icon-success", notificationComponent.Markup);
        }

    }
}
