using BackOffice.Shared.UI.Notifications.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Bunit.Demo.Pages
{
    public partial class AntDesignComplexScenarios
    {
        [Inject]
        public IToastService? ToastService { get; set; }

        public bool Showodal = false;
        private bool _showPanel = false;
        private bool _ShowMessage = false;

        private void OpenModal(MouseEventArgs e)
        {
            Showodal = true;
        }

        private void CloseModal()
        {
            Showodal = false;
        }

        private void ShowPanel()
        {
            _showPanel = !_showPanel;
        }

        private async Task ShowMessage()
        {
            await ToastService!.OpenNotification(AntDesign.NotificationType.Success, "Success");
            _ShowMessage = true;
        }
    }
}
