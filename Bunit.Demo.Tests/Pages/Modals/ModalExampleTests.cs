using AngleSharp.Dom;
using AntDesign.JsInterop;
using AntDesign.Tests;
using Bunit.Demo.Models;
using Bunit.Demo.Pages.Modals;

namespace Bunit.Demo.Tests.Pages.Modals
{
    public class ModalExampleTests : AntDesignTestBase
    {
        public ModalExampleTests()
        {
            JSInterop.Setup<Window>("AntDesign.interop.domInfoHelper.getWindow");
            JSInterop.SetupVoid("AntDesign.interop.domManipulationHelper.addElementTo", _ => true);
            JSInterop.SetupVoid("AntDesign.interop.modalHelper.focusDialog", _ => true);
            JSInterop.SetupVoid("AntDesign.interop.domManipulationHelper.disableBodyScroll");
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
    }
}
