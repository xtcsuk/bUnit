using Microsoft.AspNetCore.Components.Web;

namespace Bunit.Demo.Pages
{
    public partial class AntDesignComplexScenarios
    {
        private bool _moreThanOneInstanceModal = false;

        private void OpenMoreThanOneInstanceModal(MouseEventArgs e)
        {
            _moreThanOneInstanceModal = true;
        }

        private void CloseMoreThanOneInstanceModal()
        {
            _moreThanOneInstanceModal = false;
        }
    }
}
