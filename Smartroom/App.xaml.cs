using Xamarin.Forms;
using Smartroom.Views;
using Smartroom.Models;

namespace Smartroom
{
    public partial class App : Application
    {
        ViewModels.TabletController VM = new ViewModels.TabletController();
        public App()
        {
            InitializeComponent();
            var navPage = new NavigationPage(new MasterPage(VM));
            MainPage = navPage;
        }

        protected override void OnResume()
        {
            VM.ReconnectSocket();
            VM.GetStatusCommand.Execute(null);
            VM._speechRecongnitionInstance = DependencyService.Get<ISpeechToText>();
            VM._speechRecongnitionInstance.StartSpeechToText();
        }

        protected override void OnSleep()
        {
            VM._speechRecongnitionInstance.StopSpeechToText();
            VM._speechRecongnitionInstance = null;
        }
    }
}