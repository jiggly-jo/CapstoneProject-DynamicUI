using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Smartroom.Views
{
    public partial class LightsPageSimple : ContentView
    {
        MasterPage Master;
        public Frame ShadowFrame;

        public LightsPageSimple(MasterPage super)
        {
            InitializeComponent();
            Master = super;

            ShadowFrame = new Frame();
            ShadowFrame.CornerRadius = 0;
            ShadowFrame.HasShadow = false;
            ShadowFrame.Margin = new Thickness(-14, 0, -20, -20);
            ShadowFrame.BackgroundColor = new Color(0, 0, 0, .5);
            ShadowFrame.IsVisible = false;
            LightsGrid.Children.Add(ShadowFrame, 0, 3, 0, 4);

            var TabHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
            var TabWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

            CycleImage.Margin = new Thickness(super.paddingCalculator(TabWidth, .005), super.paddingCalculator(TabHeight, .034), super.paddingCalculator(TabWidth, .033), -super.paddingCalculator(TabHeight, .034));
            CycleLabel.Margin = new Thickness(0, 0, 0, -super.paddingCalculator(TabHeight, .012));
            CycleLabel.Padding = new Thickness(-super.paddingCalculator(TabWidth, .030), 0, 0, super.paddingCalculator(TabHeight, .012));

            LightLabel.Padding = new Thickness(super.paddingCalculator(TabWidth, .012), 0, 0, super.paddingCalculator(TabHeight, .006));

            SwitchFrame.Margin = new Thickness(super.paddingCalculator(TabWidth, .013), super.paddingCalculator(TabHeight, .269), super.paddingCalculator(TabWidth, .265), super.paddingCalculator(TabHeight, .402));

            CycleLabel.FontSize = 30;
            LightLabel.FontSize = 32;

            CycleLabel.CharacterSpacing = 0.75;
            LightLabel.CharacterSpacing = 3.75;

            MasterSwitch.Scale = 3.30;

            MasterSwitch.Toggled += MasterSwitchToggled;
        }

        void LightCycle(object sender, EventArgs args)
        {
            MasterSwitch.Toggled -= MasterSwitchToggled;
            Master.ChangePage("LightsStandard");
        }

        void MasterSwitchToggled(object sender, EventArgs args)
        {
            Switch sentSwitch = (Switch)sender;
            Boolean toggled = sentSwitch.IsToggled;

            if (toggled)
            {
                Master.Controller.FullSceneCommand.Execute(null);
            }
            else
            {
                Master.Controller.AmbientLightsOffCommand.Execute(null);
                Master.Controller.ReadingLightsOffCommand.Execute(null);
                Master.Controller.EntranceLightsOffCommand.Execute(null);
                Master.Controller.BathLightsOffCommand.Execute(null);
                Master.Controller.ExamLightsOffCommand.Execute(null);
            }
        }
    }
}
