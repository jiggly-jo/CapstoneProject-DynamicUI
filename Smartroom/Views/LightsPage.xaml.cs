using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Smartroom.Views
{
    public partial class LightsPage : ContentView
    {
        MasterPage Master;
        public Frame ShadowFrame;

        public LightsPage(MasterPage super)
        {
            InitializeComponent();
            Master = super;

            ShadowFrame = new Frame();
            ShadowFrame.CornerRadius = 0;
            ShadowFrame.HasShadow = false;
            ShadowFrame.Margin = new Thickness(-14, 0, -20, -20);
            ShadowFrame.BackgroundColor = new Color(0, 0, 0, .5);
            ShadowFrame.IsVisible = false;
            LightsGrid.Children.Add(ShadowFrame, 0, 2, 0, 3);

            var TabHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
            var TabWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

            CycleImage.Margin = new Thickness(super.paddingCalculator(TabWidth, .005), super.paddingCalculator(TabHeight, .035), super.paddingCalculator(TabWidth, .113), -super.paddingCalculator(TabHeight, .035));
            CycleLabel.Margin = new Thickness(0, 0, super.paddingCalculator(TabHeight, .160), -super.paddingCalculator(TabHeight, .012));
            CycleLabel.Padding = new Thickness(super.paddingCalculator(TabWidth, .008), 0, 0, super.paddingCalculator(TabHeight, .010));

            LightLabel.Padding = new Thickness(-super.paddingCalculator(TabWidth, .010), 0, 0, super.paddingCalculator(TabHeight, .004));

            SceneLabel.Padding = new Thickness(0, 0, 0, super.paddingCalculator(TabHeight, .010));

            SceneFrame.Margin = new Thickness(super.paddingCalculator(TabWidth, .010), super.paddingCalculator(TabHeight, .004), super.paddingCalculator(TabWidth, .036), super.paddingCalculator(TabHeight, .002));

            OffButton.Padding = new Thickness(super.paddingCalculator(TabWidth, .029), super.paddingCalculator(TabHeight, .013), super.paddingCalculator(TabWidth, .025), 0);
            OffLabel.Padding = new Thickness(0, -super.paddingCalculator(TabHeight, .010), 0, 0);

            NightButton.Padding = new Thickness(super.paddingCalculator(TabWidth, .034), super.paddingCalculator(TabHeight, .008), super.paddingCalculator(TabWidth, .033), 0);
            NightLabel.Padding = new Thickness(0, -super.paddingCalculator(TabHeight, .010), 0, 0);

            TVButton.Padding = new Thickness(super.paddingCalculator(TabWidth, .024), super.paddingCalculator(TabHeight, .020), super.paddingCalculator(TabWidth, .026), 0);
            TVLabel.Padding = new Thickness(0, -super.paddingCalculator(TabHeight, .010), 0, 0);

            SoftButton.Padding = new Thickness(super.paddingCalculator(TabWidth, .024), super.paddingCalculator(TabHeight, .011), super.paddingCalculator(TabWidth, .030), 0);
            SoftLabel.Padding = new Thickness(-super.paddingCalculator(TabWidth, .008), -super.paddingCalculator(TabHeight, .010), 0, 0);

            ReadingButton.Padding = new Thickness(super.paddingCalculator(TabWidth, .022), super.paddingCalculator(TabHeight, .011), super.paddingCalculator(TabWidth, .031), 0);
            ReadingLabel.Padding = new Thickness(-super.paddingCalculator(TabWidth, .008), -super.paddingCalculator(TabHeight, .010), 0, 0);

            FullButton.Padding = new Thickness(super.paddingCalculator(TabWidth, .017), super.paddingCalculator(TabHeight, .012), super.paddingCalculator(TabWidth, .030), 0);
            FullLabel.Padding = new Thickness(-super.paddingCalculator(TabWidth, .012), -super.paddingCalculator(TabHeight, .010), 0, 0);

            AmbientLabel.Padding = new Thickness(-super.paddingCalculator(TabWidth, .017), super.paddingCalculator(TabHeight, .014), 0, 0);
            AmbientFrame.Margin = new Thickness(super.paddingCalculator(TabWidth, .013), 0, super.paddingCalculator(TabWidth, .020), super.paddingCalculator(TabHeight, .017));

            AmbientUpButton.Padding = new Thickness(super.paddingCalculator(TabWidth, .007), -super.paddingCalculator(TabHeight, .035), super.paddingCalculator(TabWidth, .007), 0);
            AmbientValueLabel.Padding = new Thickness(0, -super.paddingCalculator(TabHeight, .018), 0, 0);
            AmbientDownButton.Padding = new Thickness(super.paddingCalculator(TabWidth, .007), 0, super.paddingCalculator(TabWidth, .007), super.paddingCalculator(TabHeight, .010));

            EntranceLabel.Padding = new Thickness(0, super.paddingCalculator(TabHeight, .014), 0, 0);
            EntranceValueLabel.Padding = new Thickness(0, -super.paddingCalculator(TabHeight, .018), 0, 0);
            EntranceFrame.Margin = new Thickness(super.paddingCalculator(TabWidth, .008), 0, super.paddingCalculator(TabWidth, .025), super.paddingCalculator(TabHeight, .017));

            EntranceUpButton.Padding = new Thickness(super.paddingCalculator(TabWidth, .007), -super.paddingCalculator(TabHeight, .035), super.paddingCalculator(TabWidth, .007), 0);

            ReadLabel.Padding = new Thickness(-super.paddingCalculator(TabWidth, .026), super.paddingCalculator(TabHeight, .014), 0, 0);
            ReadingValueLabel.Padding = new Thickness(0, -super.paddingCalculator(TabHeight, .018), 0, 0);
            ReadingFrame.Margin = new Thickness(super.paddingCalculator(TabWidth, .003), 0, super.paddingCalculator(TabWidth, .031), super.paddingCalculator(TabHeight, .017));

            ReadingUpButton.Padding = new Thickness(super.paddingCalculator(TabWidth, .007), -super.paddingCalculator(TabHeight, .035), super.paddingCalculator(TabWidth, .007), 0);

            BathLabel.Padding = new Thickness(-super.paddingCalculator(TabWidth, .026), super.paddingCalculator(TabHeight, .014), 0, 0);
            BathroomValueLabel.Padding = new Thickness(0, -super.paddingCalculator(TabHeight, .018), 0, 0);
            BathFrame.Margin = new Thickness(-super.paddingCalculator(TabWidth, .002), 0, super.paddingCalculator(TabWidth, .035), super.paddingCalculator(TabHeight, .017));

            BathroomUpButton.Padding = new Thickness(super.paddingCalculator(TabWidth, .007), -super.paddingCalculator(TabHeight, .035), super.paddingCalculator(TabWidth, .007), 0);

            ExamLabel.Padding = new Thickness(-super.paddingCalculator(TabWidth, .039), super.paddingCalculator(TabHeight, .014), 0, 0);
            ExamValueLabel.Padding = new Thickness(0, -super.paddingCalculator(TabHeight, .018), 0, 0);
            ExamFrame.Margin = new Thickness(-super.paddingCalculator(TabWidth, .007), 0, super.paddingCalculator(TabWidth, .040), super.paddingCalculator(TabHeight, .017));

            ExamUpButton.Padding = new Thickness(super.paddingCalculator(TabWidth, .007), -super.paddingCalculator(TabHeight, .035), super.paddingCalculator(TabWidth, .007), 0);

            AmbientToggle.Scale = 1.75;
            EntranceToggle.Scale = 1.75;
            ReadingToggle.Scale = 1.75;
            BathroomToggle.Scale = 1.75;
            ExamToggle.Scale = 1.75;

            OffLabel.FontSize = 30;
            NightLabel.FontSize = 30;
            TVLabel.FontSize = 30;
            SoftLabel.FontSize = 30;
            ReadingLabel.FontSize = 30;
            FullLabel.FontSize = 30;

            SceneLabel.FontSize = 30;

            CycleLabel.FontSize = 30;

            LightLabel.FontSize = 32;

            AmbientLabel.FontSize = 30;
            EntranceLabel.FontSize = 30;
            ReadLabel.FontSize = 30;
            BathLabel.FontSize = 30;
            ExamLabel.FontSize = 30;

            AmbientValueLabel.FontSize = 52;
            EntranceValueLabel.FontSize = 52;
            ReadingValueLabel.FontSize = 52;
            BathroomValueLabel.FontSize = 52;
            ExamValueLabel.FontSize = 52;

            LightLabel.CharacterSpacing = 3.75;

            NightLabel.CharacterSpacing = 0.5;
            ReadingLabel.CharacterSpacing = 0.5;

            AmbientLabel.CharacterSpacing = 2.25;
            EntranceLabel.CharacterSpacing = 2.25;
            ReadLabel.CharacterSpacing = 2.25;
            BathLabel.CharacterSpacing = 2.25;
            ExamLabel.CharacterSpacing = 2.25;

            switch (Master.Controller.LightsSceneSelected)
            {
                case "Off":
                    OffClicked(null, null);
                    break;
                case "Night":
                    NightClicked(null, null);
                    break;
                case "TV":
                    TVClicked(null, null);
                    break;
                case "Soft":
                    SoftClicked(null, null);
                    break;
                case "Reading":
                    ReadingClicked(null, null);
                    break;
                case "Full":
                    FullClicked(null, null);
                    break;
            }
            Master.AdvancedLightsPageIsActive = true;
        }

        void HandleAmbientToggle(System.Object sender, System.EventArgs e)
        {
            if (Master.AdvancedLightsPageIsActive)
            {
                if (AmbientToggle.IsToggled)
                    Master.Controller.AmbientLightsOnCommand.Execute(null);

                else
                    Master.Controller.AmbientLightsOffCommand.Execute(null);
            }
        }

        void HandleEntranceToggle(System.Object sender, System.EventArgs e)
        {
            if (Master.AdvancedLightsPageIsActive)
            {
                if (EntranceToggle.IsToggled)
                    Master.Controller.EntranceLightsOnCommand.Execute(null);
                else
                    Master.Controller.EntranceLightsOffCommand.Execute(null);
            }
        }

        void HandleReadingToggle(System.Object sender, System.EventArgs e)
        {
            if (Master.AdvancedLightsPageIsActive)
            {
                if (ReadingToggle.IsToggled)
                    Master.Controller.ReadingLightsOnCommand.Execute(null);
                else
                    Master.Controller.ReadingLightsOffCommand.Execute(null);
            }
        }

        void HandleExamToggle(System.Object sender, System.EventArgs e)
        {
            if (Master.AdvancedLightsPageIsActive)
            {
                if (ExamToggle.IsToggled)
                    Master.Controller.ExamLightsOnCommand.Execute(null);
                else
                    Master.Controller.ExamLightsOffCommand.Execute(null);
            }
        }

        void HandleBathroomToggle(System.Object sender, System.EventArgs e)
        {
            if (Master.AdvancedLightsPageIsActive)
            {
                if (BathroomToggle.IsToggled)
                    Master.Controller.BathLightsOnCommand.Execute(null);
                else
                    Master.Controller.BathLightsOffCommand.Execute(null);
            }
        }

        void ButtonPressed(object sender, EventArgs args)
        {
            ImageButton sent = (ImageButton)sender;
            sent.BackgroundColor = Color.DarkSlateGray;
        }

        void ButtonReleased(object sender, EventArgs args)
        {

            ImageButton sent = (ImageButton)sender;
            sent.BackgroundColor = Color.Transparent;
        }

        void LightCycle(object sender, EventArgs args)
        {
            Master.ChangePage("LightsSimple");
        }

        void OffClicked(object sender, EventArgs args)
        {
            Master.Controller.UpdateLightsSceneSelected("Off");
            if (sender != null)
            {
                Master.Controller.AmbientLightsOffCommand.Execute(null);
                Master.Controller.ReadingLightsOffCommand.Execute(null);
                Master.Controller.EntranceLightsOffCommand.Execute(null);
                Master.Controller.BathLightsOffCommand.Execute(null);
                Master.Controller.ExamLightsOffCommand.Execute(null);
            }

            NightButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconNight.png");
            TVButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconTV.png");
            SoftButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconSoft.png");
            ReadingButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconReading.png");
            FullButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconFull.png");

            NightLabel.TextColor = Color.FromHex("#333333");
            TVLabel.TextColor = Color.FromHex("#333333");
            SoftLabel.TextColor = Color.FromHex("#333333");
            ReadingLabel.TextColor = Color.FromHex("#333333");
            FullLabel.TextColor = Color.FromHex("#333333");
        }

        void NightClicked(object sender, EventArgs args)
        {
            Master.Controller.UpdateLightsSceneSelected("Night");
            if (sender != null)
                Master.Controller.NightSceneCommand.Execute(null);

            NightButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconNightRed.png");
            TVButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconTV.png");
            SoftButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconSoft.png");
            ReadingButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconReading.png");
            FullButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconFull.png");

            NightLabel.TextColor = Color.FromHex("#AC162C");
            TVLabel.TextColor = Color.FromHex("#333333");
            SoftLabel.TextColor = Color.FromHex("#333333");
            ReadingLabel.TextColor = Color.FromHex("#333333");
            FullLabel.TextColor = Color.FromHex("#333333");
        }

        void TVClicked(object sender, EventArgs args)
        {
            if (sender != null)
                Master.Controller.TVSceneCommand.Execute(null);

            Master.Controller.UpdateLightsSceneSelected("TV");
            NightButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconNight.png");
            TVButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconTVRed.png");
            SoftButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconSoft.png");
            ReadingButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconReading.png");
            FullButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconFull.png");

            NightLabel.TextColor = Color.FromHex("#333333");
            TVLabel.TextColor = Color.FromHex("#AC162C");
            SoftLabel.TextColor = Color.FromHex("#333333");
            ReadingLabel.TextColor = Color.FromHex("#333333");
            FullLabel.TextColor = Color.FromHex("#333333");
        }

        void SoftClicked(object sender, EventArgs args)
        {
            if (sender != null)
                Master.Controller.SoftSceneCommand.Execute(null);

            Master.Controller.UpdateLightsSceneSelected("Soft");
            NightButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconNight.png");
            TVButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconTV.png");
            SoftButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconSoftRed.png");
            ReadingButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconReading.png");
            FullButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconFull.png");

            NightLabel.TextColor = Color.FromHex("#333333");
            TVLabel.TextColor = Color.FromHex("#333333");
            SoftLabel.TextColor = Color.FromHex("#AC162C");
            ReadingLabel.TextColor = Color.FromHex("#333333");
            FullLabel.TextColor = Color.FromHex("#333333");
        }

        void ReadingClicked(object sender, EventArgs args)
        {
            if (sender != null)
                Master.Controller.ReadingSceneCommand.Execute(null);

            Master.Controller.UpdateLightsSceneSelected("Reading");
            NightButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconNight.png");
            TVButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconTV.png");
            SoftButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconSoft.png");
            ReadingButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconReadingRed.png");
            FullButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconFull.png");

            NightLabel.TextColor = Color.FromHex("#333333");
            TVLabel.TextColor = Color.FromHex("#333333");
            SoftLabel.TextColor = Color.FromHex("#333333");
            ReadingLabel.TextColor = Color.FromHex("#AC162C");
            FullLabel.TextColor = Color.FromHex("#333333");
        }

        void FullClicked(object sender, EventArgs args)
        {
            if (sender != null)
                Master.Controller.FullSceneCommand.Execute(null);

            Master.Controller.UpdateLightsSceneSelected("Full");
            NightButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconNight.png");
            TVButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconTV.png");
            SoftButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconSoft.png");
            ReadingButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconReading.png");
            FullButton.Source = ImageSource.FromResource("Smartroom.Icons.LightIconFullRed.png");

            NightLabel.TextColor = Color.FromHex("#333333");
            TVLabel.TextColor = Color.FromHex("#333333");
            SoftLabel.TextColor = Color.FromHex("#333333");
            ReadingLabel.TextColor = Color.FromHex("#333333");
            FullLabel.TextColor = Color.FromHex("#AC162C");
        }
    }
}