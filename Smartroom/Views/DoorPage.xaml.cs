using System;
using System.Collections.Generic;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Essentials;
using Smartroom.Helpers;

namespace Smartroom.Views
{
    public partial class DoorPage : ContentView
    {
        public string DoorIconCloseSVG { get; set; } = "Smartroom.Resources.DoorIconClose.svg";
        public string DoorIconOpenSVG { get; set; } = "Smartroom.Resources.DoorIconOpen.svg";
        public string DoorIconOpenPartiallySVG { get; set; } = "Smartroom.Resources.DoorIconOpenPartially.svg";

        MasterPage super;

        public DoorPage(MasterPage context)
        {
            InitializeComponent();

            super = context;

            var TabHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
            var TabWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

            //DoorLabel.Margin = new Thickness(-super.paddingCalculator(TabWidth, .011), 0, 0, super.paddingCalculator(TabHeight, .005));

            ControlFrame.Margin = new Thickness(super.paddingCalculator(TabWidth, .075), super.paddingCalculator(TabHeight, .243), super.paddingCalculator(TabWidth, .111), super.paddingCalculator(TabHeight, .321));

            
            DoorCloseButton.Padding = new Thickness(super.paddingCalculator(TabWidth, .063), super.paddingCalculator(TabHeight, .056), super.paddingCalculator(TabWidth, .064), 0);
            CloseLabel.Padding = new Thickness(super.paddingCalculator(TabWidth, .006), super.paddingCalculator(TabHeight, .002), 0, 0);

            DoorPartialButton.Padding = new Thickness(super.paddingCalculator(TabWidth, .051), super.paddingCalculator(TabHeight, .074), super.paddingCalculator(TabWidth, .077), 0);
            PartialLabel.Padding = new Thickness(-super.paddingCalculator(TabWidth, .014), super.paddingCalculator(TabHeight, .002), 0, 0);

            DoorOpenButton.Padding = new Thickness(super.paddingCalculator(TabWidth, .024), super.paddingCalculator(TabHeight, .074), super.paddingCalculator(TabWidth, .057), 0);
            OpenLabel.Padding = new Thickness(-super.paddingCalculator(TabWidth, .035), super.paddingCalculator(TabHeight, .002), 0, 0);


            DoorLabel.FontSize = 32;



            CloseLabel.FontSize = 29;
            PartialLabel.FontSize = 29;
            OpenLabel.FontSize = 29;

            DoorLabel.CharacterSpacing = 3;

            CloseLabel.CharacterSpacing = 0.75;
            PartialLabel.CharacterSpacing = 1;
            OpenLabel.CharacterSpacing = 0.25;
            SetAccessibility(true);
        }
        private void SetAccessibility(bool setTo)
        {
            AutomationProperties.SetIsInAccessibleTree(DoorCloseButton, setTo);
            AutomationProperties.SetIsInAccessibleTree(DoorPartialButton, setTo);
            AutomationProperties.SetIsInAccessibleTree(DoorOpenButton, setTo);
        }
    }
}