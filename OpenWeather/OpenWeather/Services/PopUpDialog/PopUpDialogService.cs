using Xamarin.Forms;

namespace OpenWeather.Services.PopUpDialog
{
    public class PopUpDialogService : IPopUpDialogService
    {
        public void ShowMessage(string Message)
        {
            DependencyService.Get<IPopUpDialogService>().ShowMessage(Message);
        }
    }
}
