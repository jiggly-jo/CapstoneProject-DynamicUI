using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Smartroom.Views
{
    public partial class TempPage : ContentView
    {
        MasterPage Master;
        public Frame ShadowFrame;

        public TempPage(MasterPage super)
        {
            InitializeComponent();
            Master = super;

            ShadowFrame = new Frame();
            ShadowFrame.CornerRadius = 0;
            ShadowFrame.HasShadow = false;
            ShadowFrame.Margin = new Thickness(-14, -20, -20, -20);
            ShadowFrame.BackgroundColor = new Color(0, 0, 0, .5);
            ShadowFrame.IsVisible = false;
            TempGrid.Children.Add(ShadowFrame, 0, 5, 0, 7);

            TempLabel.FontSize = 33;

            var TabHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
            var TabWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

            TempLabel.Padding = new Thickness(super.paddingCalculator(TabWidth, .064), 0, 0, -super.paddingCalculator(TabHeight, .017));

            CurrentFrame.Margin = new Thickness(super.paddingCalculator(TabWidth, .025), 0, super.paddingCalculator(TabWidth, .003), super.paddingCalculator(TabHeight, .017));
            ControlFrame.Margin = new Thickness(super.paddingCalculator(TabWidth, .007), 0, super.paddingCalculator(TabWidth, .036), super.paddingCalculator(TabHeight, .017));

            CurrentTempLabel.Padding = new Thickness(0, 0, 0, super.paddingCalculator(TabHeight, .149));
            SettingLabel.Padding = new Thickness(0, 0, 0, -super.paddingCalculator(TabHeight, .010));

            TempUpButton.Padding = new Thickness(super.paddingCalculator(TabWidth, .121), 0, super.paddingCalculator(TabWidth, .109), super.paddingCalculator(TabWidth, .010));
            TempDownButton.Padding = new Thickness(super.paddingCalculator(TabWidth, .121), 0, super.paddingCalculator(TabWidth, .109), super.paddingCalculator(TabWidth, .080));
            TempLabel.CharacterSpacing = 3.25;

            CurrentTempLabel.FontSize = 53;
            SettingLabel.FontSize = 53;

            SetAccessibility(true);
        }

        void TempUp(object sender, EventArgs args)
        {
            Master.Controller.TempUpCommand.Execute(null);
        }

        void TempDown(object sender, EventArgs args)
        {
            Master.Controller.TempDownCommand.Execute(null);
        }

        private void SetAccessibility(bool setTo)
        {
            AutomationProperties.SetIsInAccessibleTree(TempUpButton, setTo);
            AutomationProperties.SetIsInAccessibleTree(TempDownButton, setTo);
        }
    }
}