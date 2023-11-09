using Smartroom.ViewModels;
using Xamarin.Forms;
using Xamarin.Essentials;
using System;
using System.Threading.Tasks;

namespace Smartroom.Views
{
    public partial class MasterPage : ContentPage
    {
        public TabletController Controller;
        public TutorialPage Tutor;
        public VoicePage VoiceOverlay;
        public SidebarView Sidebar;
        //AdvancedLightPageActive is used to allow the toggle switches to not trigger action when LightsPage (advanced) is created or dereferenced.
        //this allows the zone toggle actions to be triggered after page is initialized it is set to true and when about to change pages it is set to false.
        public bool AdvancedLightsPageIsActive = false;
        public string SortType = "Most Recently Clicked";
        public double scaleValue = 1;

        public double TabWidth;
        public double TabHeight;

        public MasterPage(TabletController viewModel)
        {
            InitializeComponent();
            viewModel.InitializeMaster(this);

            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = this.Controller = viewModel;

            TabHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
            TabWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

            Sidebar = new SidebarView(this);
            SidebarColumn.Content = Sidebar;
        }

        public void StartTutorial()
        {
            Tutor = new TutorialPage(this);
            GridFrame.Children.Add(Tutor, 1, 0);
        }

        public void EndTutorial()
        {
            GridFrame.Children.Remove(Tutor);
            Tutor = null;
        }

        
        /// <summary>
        /// Should only be called from SidebarView class other than switching content within the view ie, media or lights pages
        /// </summary>
        /// <param name="ScreenName"></param>
        public void ChangePage(string ScreenName)
        {
            AdvancedLightsPageIsActive = false;
            switch (ScreenName)
            {
                case "Tutorial":
                    Tutor.NextClicked(this, null);
                    break;
                //from sidebar media menu item
                case "Media":
                    mainContent.Content= new MediaPageMaster(this);
                    break;
                case "Door":
                    mainContent.Content = new DoorPage(this);
                    break;

                case "Lights":
                    switch (Controller.GetLightsPage())
                    {
                        case "LightsSimple":
                            Controller.UpdateLightsPageSelected("LightsSimple");
                            mainContent.Content = new LightsPageSimple(this);
                            break;
                        case "LightsStandard":
                            Controller.UpdateLightsPageSelected("LightsStandard");
                            mainContent.Content = new LightsPageStandard(this);
                            break;
                        case "LightsAdvanced":
                            Controller.UpdateLightsPageSelected("LightsAdvanced");
                            mainContent.Content = new LightsPage(this);
                            break;
                    }
                    break;

                #region from lights page
                case "LightsSimple":
                    Controller.UpdateLightsPageSelected("LightsSimple");
                    mainContent.Content = new LightsPageSimple(this);
                    break;
                case "LightsStandard":
                    Controller.UpdateLightsPageSelected("LightsStandard");
                    mainContent.Content = new LightsPageStandard(this);
                    break;
                case "LightsAdvanced":
                    Controller.UpdateLightsPageSelected("LightsAdvanced");
                    mainContent.Content = new LightsPage(this);
                    break;
                #endregion

                case "Blinds":
                    mainContent.Content = new BlindsPage(this);
                    break;

                case "Climate":
                    mainContent.Content = new TempPage(this);
                    break;

                case "Elevator":
                    mainContent.Content = new ElevatorPage(this);
                    break;

                case "Nurse":
                    mainContent.Content = new NursePage(this);
                    break;
                case "Options":
                    mainContent.Content = new OptionsPage(this);
                    break;
                case "DynamicUI":
                    mainContent.Content = new DynamicUI(this);
                    break;
            }

            //Collect Memory
            GC.Collect();
        }

        public void ShowVoiceOverlay()
        {
            //Create and display voicePage to show user what is recognized
            VoiceOverlay = new VoicePage(this);
            GridFrame.Children.Add(VoiceOverlay, 0, 2, 0, 1);
        }

        public async Task HideVoiceOverlay()
        {
            //Display voicePage to show user what is recognized
            await Task.Delay(3000);

            GridFrame.Children.Remove(VoiceOverlay);
            VoiceOverlay = null;
        }

        protected override void OnAppearing()
        {
            Controller.VoiceControl();
        }

        public double paddingCalculator(double length, double percentage)
        {
            return (percentage * length);
        }
    }
}