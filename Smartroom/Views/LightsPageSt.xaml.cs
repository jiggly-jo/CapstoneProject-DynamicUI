using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Smartroom.Utilities;

namespace Smartroom.Views
{
    public partial class LightsPageStandard : ContentView
    {
        MasterPage Master;
        public Frame ShadowFrame;
        public int mode = 4;

        public LightsPageStandard(MasterPage super)
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

            CycleImage.Margin = new Thickness(super.paddingCalculator(TabWidth, .005), super.paddingCalculator(TabHeight, .034), super.paddingCalculator(TabWidth, .035), -super.paddingCalculator(TabHeight, .034));
            CycleLabel.Margin = new Thickness(0, 0, 0, -super.paddingCalculator(TabHeight, .012));
            CycleLabel.Padding = new Thickness(-super.paddingCalculator(TabWidth, .030), 0, 0, super.paddingCalculator(TabHeight, .012));

            LightLabel.Padding = new Thickness(super.paddingCalculator(TabWidth, .009), 0, 0, super.paddingCalculator(TabHeight, .006));

            SceneLabel.Padding = new Thickness(0, 0, 0, super.paddingCalculator(TabHeight, .014));

            SceneFrame.Margin = new Thickness(super.paddingCalculator(TabWidth, .010), 0, super.paddingCalculator(TabWidth, .036), super.paddingCalculator(TabHeight, .010));

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

            CircleFrame.Margin = new Thickness(super.paddingCalculator(TabWidth, .097), super.paddingCalculator(TabHeight, .059), super.paddingCalculator(TabWidth, .056), 0);

            ArrowFrame.Margin = new Thickness(super.paddingCalculator(TabWidth, .003), super.paddingCalculator(TabHeight, .110), super.paddingCalculator(TabWidth, .108), super.paddingCalculator(TabHeight, .119));

            if (DeviceDisplay.MainDisplayInfo.Density == 2 && DeviceDisplay.MainDisplayInfo.Height == 1668 && DeviceDisplay.MainDisplayInfo.Width == 2224)
            {
                ArrowFrame.CornerRadius = 67;
            }
            else if (DeviceDisplay.MainDisplayInfo.Density == 2 && DeviceDisplay.MainDisplayInfo.Height == 1668 && DeviceDisplay.MainDisplayInfo.Width == 2388)
            {
                ArrowFrame.CornerRadius = 72;
            }

            ArrowUpButton.Margin = new Thickness(super.paddingCalculator(TabWidth, .005), super.paddingCalculator(TabHeight, .020), super.paddingCalculator(TabWidth, .005), 0);
            ArrowDownButton.Margin = new Thickness(super.paddingCalculator(TabWidth, .005), 0, super.paddingCalculator(TabWidth, .005), super.paddingCalculator(TabHeight, .010));

            CycleLabel.FontSize = 30;
            LightLabel.FontSize = 32;
            SceneLabel.FontSize = 30;

            OffLabel.FontSize = 30;
            NightLabel.FontSize = 30;
            TVLabel.FontSize = 30;
            SoftLabel.FontSize = 30;
            ReadingLabel.FontSize = 30;
            FullLabel.FontSize = 30;

            CycleLabel.CharacterSpacing = 0.75;
            LightLabel.CharacterSpacing = 3.75;

            NightLabel.CharacterSpacing = 0.5;
            ReadingLabel.CharacterSpacing = 0.5;

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

        }

        void LightCycle(object sender, EventArgs args)
        {
            Master.ChangePage("LightsAdvanced");
        }

        void OffClicked(object sender, EventArgs args)
        {

            CircleBar.SetBinding(CircleProgressBar.ValueProperty, "Ambient");
            if (sender != null)
            {
                Master.Controller.AmbientLightsOffCommand.Execute(null);
                Master.Controller.ReadingLightsOffCommand.Execute(null);
                Master.Controller.EntranceLightsOffCommand.Execute(null);
                Master.Controller.BathLightsOffCommand.Execute(null);
                Master.Controller.ExamLightsOffCommand.Execute(null);
            }

            Master.Controller.UpdateLightsSceneSelected("Off");
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
            double width = ArrowFrame.Width;

            CircleBar.SetBinding(CircleProgressBar.ValueProperty, "Entrance");

            if (sender != null)
                Master.Controller.NightSceneCommand.Execute(null);

            Master.Controller.UpdateLightsSceneSelected("Night");
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
            CircleBar.SetBinding(CircleProgressBar.ValueProperty, "Ambient");
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
            CircleBar.SetBinding(CircleProgressBar.ValueProperty, "Ambient");

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
            CircleBar.SetBinding(CircleProgressBar.ValueProperty, "Reading");

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
            CircleBar.SetBinding(CircleProgressBar.ValueProperty, "Ambient");

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

        void ArrowUpClicked(object sender, EventArgs args)
        {
            double width = ArrowFrame.Width;
            switch (mode)
            {
                case 1:
                    Master.Controller.EntranceLightsUpCommand.Execute(null);
                    break;
                case 2:
                    Master.Controller.AmbientLightsUpCommand.Execute(null);
                    Master.Controller.EntranceLightsUpCommand.Execute(null);
                    break;
                case 3:
                    Master.Controller.AmbientLightsUpCommand.Execute(null);
                    Master.Controller.ReadingLightsUpCommand.Execute(null);
                    Master.Controller.EntranceLightsUpCommand.Execute(null);
                    break;
                case 4:
                    Master.Controller.AmbientLightsUpCommand.Execute(null);
                    Master.Controller.ReadingLightsUpCommand.Execute(null);
                    break;
                case 5:
                    Master.Controller.AmbientLightsUpCommand.Execute(null);
                    Master.Controller.ReadingLightsUpCommand.Execute(null);
                    Master.Controller.EntranceLightsUpCommand.Execute(null);
                    break;
            }
        }

        void ArrowDownClicked(object sender, EventArgs args)
        {
            switch (mode)
            {
                case 1:
                    Master.Controller.EntranceLightsDownCommand.Execute(null);
                    break;
                case 2:
                    Master.Controller.AmbientLightsDownCommand.Execute(null);
                    Master.Controller.EntranceLightsDownCommand.Execute(null);
                    break;
                case 3:
                    Master.Controller.AmbientLightsDownCommand.Execute(null);
                    Master.Controller.ReadingLightsDownCommand.Execute(null);
                    Master.Controller.EntranceLightsDownCommand.Execute(null);
                    break;
                case 4:
                    Master.Controller.AmbientLightsDownCommand.Execute(null);
                    Master.Controller.ReadingLightsDownCommand.Execute(null);
                    break;
                case 5:
                    Master.Controller.AmbientLightsDownCommand.Execute(null);
                    Master.Controller.ReadingLightsDownCommand.Execute(null);
                    Master.Controller.EntranceLightsDownCommand.Execute(null);
                    break;
            }
        }
    }
}
