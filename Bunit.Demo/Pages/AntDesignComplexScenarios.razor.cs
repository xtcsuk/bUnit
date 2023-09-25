using Microsoft.AspNetCore.Components.Web;

namespace Bunit.Demo.Pages
{
    public partial class AntDesignComplexScenarios
    {
        public bool Showodal = false;
        private bool _showPanel = false;

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
    }
}
