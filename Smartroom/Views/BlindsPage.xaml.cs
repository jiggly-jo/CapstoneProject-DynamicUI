using System;
using Xamarin.Essentials;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

using Xamarin.Forms;

namespace Smartroom.Views
{
    // TODO: Need to add padding on the right of all pages to match the space taken by the menu bar indicator icon
    // it's <ColumnDefinition Width="7*"/> from SidebarView.xaml, how to translate that to specifics? probably
    // just need to do it at the mainpage level to add an extra column on the right that matches the spacing
    //merging

    public partial class BlindsPage : ContentView
    {
        public Frame ShadowFrame;

       
        public string ArrowButtonBaseSVG { get; set; } = "Smartroom.Resources.ArrowButtonBase.svg";
        public string ArrowBarNumberBackgroundSVG { get; set; } = "Smartroom.Resources.ArrowBarNumberBackground.svg";

        public string ArrowButtonUpSVG { get; set; } = "Smartroom.Resources.ArrowButtonUp.svg";
        public string ArrowButtonUpMaxSVG { get; set; } = "Smartroom.Resources.ArrowButtonUpMax.svg";
        public string ArrowButtonDownSVG { get; set; } = "Smartroom.Resources.ArrowButtonDown.svg";
        public string ArrowButtonDownMaxSVG { get; set; } = "Smartroom.Resources.ArrowButtonDownMax.svg";

        public string ArrowButtonEmptySVG { get; set; } = "Smartroom.Resources.ArrowButtonEmpty.svg";
        int BlindsMode = 2;

        MasterPage super;

        public BlindsPage(MasterPage context)
        {
            InitializeComponent();

            super = context;

            var TabHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
            var TabWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

            BlindsLabel.Padding = new Thickness(-super.paddingCalculator(TabWidth, .012), 0, 0, super.paddingCalculator(TabHeight, .044));

            ControlFrame.Margin = new Thickness(super.paddingCalculator(TabWidth, .108), super.paddingCalculator(TabHeight, .014), super.paddingCalculator(TabWidth, .145), super.paddingCalculator(TabHeight, .112));

            BlindsLabel.FontSize = 31;

            BlindsLabel.CharacterSpacing = 4.25;
        }

        void BlindsModeSelect(object sender, EventArgs args)
        {
            ImageButton sentBy = (ImageButton)sender;

            if (sentBy.Equals(BlackoutModeSelect))
            {
                BlindsMode = 0;

                BlackoutModeSelect.Source = ImageSource.FromResource("Smartroom.Icons.BlindBlackoutRed.png");
                SheerModeSelect.Source = ImageSource.FromResource("Smartroom.Icons.BlindSheer.png");
                AllModeSelect.Source = ImageSource.FromResource("Smartroom.Icons.BlindAll.png");

                BlackoutLabel.TextColor = Color.FromHex("#AC162C");
                SheerLabel.TextColor = Color.Black;
                AllLabel.TextColor = Color.Black;
            }
            else if (sentBy.Equals(SheerModeSelect))
            {
                BlindsMode = 1;

                BlackoutModeSelect.Source = ImageSource.FromResource("Smartroom.Icons.BlindBlackout.png");
                SheerModeSelect.Source = ImageSource.FromResource("Smartroom.Icons.BlindSheerRed.png");
                AllModeSelect.Source = ImageSource.FromResource("Smartroom.Icons.BlindAll.png");

                BlackoutLabel.TextColor = Color.Black;
                SheerLabel.TextColor = Color.FromHex("#AC162C");
                AllLabel.TextColor = Color.Black;
            }
            else if (sentBy.Equals(AllModeSelect))
            {
                BlindsMode = 2;

                BlackoutModeSelect.Source = ImageSource.FromResource("Smartroom.Icons.BlindBlackout.png");
                SheerModeSelect.Source = ImageSource.FromResource("Smartroom.Icons.BlindSheer.png");
                AllModeSelect.Source = ImageSource.FromResource("Smartroom.Icons.BlindAllRed.png");

                BlackoutLabel.TextColor = Color.Black;
                SheerLabel.TextColor = Color.Black;
                AllLabel.TextColor = Color.FromHex("#AC162C");
            }
        }

        void BlindsUp(object sender, EventArgs args)
        {
            switch (BlindsMode)
            {
                case 0:
                    super.Controller.BBlindsUpCommand.Execute(null);
                    break;
                case 1:
                    super.Controller.IBlindsUpCommand.Execute(null);
                    break;
                case 2:
                    super.Controller.BBlindsUpCommand.Execute(null);
                    super.Controller.IBlindsUpCommand.Execute(null);
                    break;
            }
        }

        void BlindsOpen(object sender, EventArgs args)
        {
            switch (BlindsMode)
            {
                case 0:
                    super.Controller.BBlindsOpenCommand.Execute(null);
                    break;
                case 1:
                    super.Controller.IBlindsOpenCommand.Execute(null);
                    break;
                case 2:
                    super.Controller.BBlindsOpenCommand.Execute(null);
                    super.Controller.IBlindsOpenCommand.Execute(null);
                    break;
            }
        }

        void BlindsClose(object sender, EventArgs args)
        {
            switch (BlindsMode)
            {
                case 0:
                    super.Controller.BBlindsClosedCommand.Execute(null);
                    break;
                case 1:
                    super.Controller.IBlindsClosedCommand.Execute(null);
                    break;
                case 2:
                    super.Controller.BBlindsClosedCommand.Execute(null);
                    super.Controller.IBlindsClosedCommand.Execute(null);
                    break;
            }
        }

        void BlindsDown(object sender, EventArgs args)
        {
            switch (BlindsMode)
            {
                case 0:
                    super.Controller.BBlindsDownCommand.Execute(null);
                    break;
                case 1:
                    super.Controller.IBlindsDownCommand.Execute(null);
                    break;
                case 2:
                    super.Controller.BBlindsDownCommand.Execute(null);
                    super.Controller.IBlindsDownCommand.Execute(null);
                    break;
            }
        }
    }
}
