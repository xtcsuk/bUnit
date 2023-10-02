using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Bunit.Demo.Pages
{
    public partial class AntDesignComplexScenarios
    {
        [Inject]
        private NotificationService Notification { get; set; } = default!;

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
            await Notification.Open(new NotificationConfig
            {
                NotificationType = NotificationType.Success,
                Message = "Success",
                Duration = 1.0
            });
            _ShowMessage = true;
        }
    }
}
