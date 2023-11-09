using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Smartroom.Helpers;

namespace Smartroom.Views
{
    public partial class OptionsPage : ContentView
    {
        MasterPage Master;
        int speechtoggle;
        public Frame ShadowFrame;

        public string SortType;

        double TabHeight;
        double TabWidth;

        public OptionsPage(MasterPage super)
        {
            InitializeComponent();
            Master = super;
            ShadowFrame = new Frame();
            ShadowFrame.BackgroundColor = new Color(0, 0, 0, .5);

            RoomNum.FontSize = 39;
            RoomPin.FontSize = 39;
            OptionsGrid.Children.Add(ShadowFrame, 0, 4, 0, 15);
            if(Master.Controller.getRoomNum() != 0)
                roomNumEntry.Text = Master.Controller.getRoomNum().ToString();

            ShadowFrame.IsVisible = false;

            TabHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
            TabWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

            OptionsLabel.Margin = new Thickness(-super.paddingCalculator(TabWidth, .010), super.paddingCalculator(TabHeight, .041), 0, 0);
            ParingLabel.Padding = new Thickness(-super.paddingCalculator(TabWidth, .011), 0, 0, super.paddingCalculator(TabHeight, .008));

            RoomFrame.Margin = new Thickness(super.paddingCalculator(TabWidth, .099), super.paddingCalculator(TabHeight, .003), super.paddingCalculator(TabWidth, .097), super.paddingCalculator(TabHeight, .020));
            ControlFrame1.Margin = new Thickness(super.paddingCalculator(TabWidth, .099), super.paddingCalculator(TabHeight, .005), super.paddingCalculator(TabWidth, .097), super.paddingCalculator(TabHeight, .010));
            ControlFrame2.Margin = new Thickness(super.paddingCalculator(TabWidth, .099), super.paddingCalculator(TabHeight, .015), super.paddingCalculator(TabWidth, .097), -super.paddingCalculator(TabHeight, .004));
            ControlFrame3.Margin = new Thickness(super.paddingCalculator(TabWidth, .099), super.paddingCalculator(TabHeight, .015), super.paddingCalculator(TabWidth, .097), -super.paddingCalculator(TabHeight, .004));

            VoiceLabel.Padding = new Thickness(super.paddingCalculator(TabWidth, .008), 0, 0, super.paddingCalculator(TabHeight, .011));

            SpeechImage.Padding = new Thickness(super.paddingCalculator(TabWidth, .078), super.paddingCalculator(TabHeight, .010), super.paddingCalculator(TabWidth, .041), 0);

            MacLabel.Padding = new Thickness(super.paddingCalculator(TabWidth, .030), 0, 0, super.paddingCalculator(TabHeight, .026));
            CountdownImage.Padding = new Thickness(super.paddingCalculator(TabWidth, .053), super.paddingCalculator(TabHeight, .014), super.paddingCalculator(TabWidth, .050), 0);

            ListeningLabel.Padding = new Thickness(super.paddingCalculator(TabWidth, .003), 0, 0, super.paddingCalculator(TabHeight, .025));

            MuteImage.Padding = new Thickness(super.paddingCalculator(TabWidth, .055), super.paddingCalculator(TabHeight, .015), super.paddingCalculator(TabWidth, .050), 0);

            MuteLabel.Padding = new Thickness(super.paddingCalculator(TabWidth, .010), 0, 0, super.paddingCalculator(TabHeight, .025));

            //info board
            InfoLabel.Padding = new Thickness(0, super.paddingCalculator(TabHeight, .011), 0, 0);

            BoardIconOff.Padding = new Thickness(super.paddingCalculator(TabWidth, .070), super.paddingCalculator(TabHeight, .007), super.paddingCalculator(TabWidth, .036), 0);

            OnOffLabel.Padding = new Thickness(super.paddingCalculator(TabWidth, .040), 0, 0, super.paddingCalculator(TabHeight, .029));

            BoardIconSoft.Padding = new Thickness(super.paddingCalculator(TabWidth, .065), super.paddingCalculator(TabHeight, .013), super.paddingCalculator(TabWidth, .046), 0);

            DimLabel.Padding = new Thickness(super.paddingCalculator(TabWidth, .023), 0, 0, super.paddingCalculator(TabHeight, .030));

            BoardIconFull.Padding = new Thickness(super.paddingCalculator(TabWidth, .051), super.paddingCalculator(TabHeight, .025), super.paddingCalculator(TabWidth, .052), 0);

            BrightLabel.Padding = new Thickness(super.paddingCalculator(TabWidth, .010), 0, 0, super.paddingCalculator(TabHeight, .030));

            //clock
            ClockBrightnessLabel.Padding = new Thickness(0, super.paddingCalculator(TabHeight, .011), 0, 0);

            OptionsLabel.FontSize = 33;

            ParingLabel.FontSize = 30;
            VoiceLabel.FontSize = 30;
            MacLabel.FontSize = 30;
            ListeningLabel.FontSize = 30;
            MuteLabel.FontSize = 30;

            OnOffLabel.FontSize = 30;
            DimLabel.FontSize = 30;
            BrightLabel.FontSize = 30;

            InfoLabel.FontSize = 30;

            ClockOffLabel.FontSize = 30;
            ClockLowLabel.FontSize = 30;
            ClockMediumLabel.FontSize = 30;
            ClockHighLabel.FontSize = 30;

            ClockBrightnessLabel.FontSize = 30;

            RoomNum.FontSize = 30;
            RoomPin.FontSize = 30;

            OptionsLabel.CharacterSpacing = 3;
            ParingLabel.CharacterSpacing = 2.5;
            VoiceLabel.CharacterSpacing = 2.5;
            SetAccessibility(true);

            SwapSort.Text = Master.SortType;
            if (Master.scaleValue == 1)
                LargeSmall.Text = "Small Buttons Enabled";
            else
                LargeSmall.Text = "Large Buttons Enabled";
        }

        #region Voice Control
        public void SpeechToggle(object sender, EventArgs args)
        {
            if (Master.Controller.Hotword.Equals("mac"))
            {
                Master.Controller.SetHotword("sam");
                SpeechImage.Source = ImageSource.FromResource("Smartroom.Icons.OptionIconSam.png");
            }
            else
            {
                Master.Controller.SetHotword("mac");
                SpeechImage.Source = ImageSource.FromResource("Smartroom.Icons.OptionIconMac.png");
            }
        }

        public void CountdownToggle(object sender, EventArgs args)
        {

            if (Master.Controller.GetCountdown() == 5)
            {
                Master.Controller.UpdateListeningTime(1);
            }
            else if (Master.Controller.GetCountdown() == 1)
            {
                Master.Controller.UpdateListeningTime(3);
            }
            else if (Master.Controller.GetCountdown() == 3)
            {
                Master.Controller.UpdateListeningTime(5);
            }
        }

        public void ListeningToggle(object sender, EventArgs args)
        {
            if (!Master.Controller.VoiceMuted)
            {
                Master.Controller.ToggleVoice(true);
                MuteImage.Source = ImageSource.FromResource("Smartroom.Icons.OptionMicMute.png");
                MuteImage.Padding = new Thickness(Master.paddingCalculator(TabWidth, .048), Master.paddingCalculator(TabHeight, .015), Master.paddingCalculator(TabWidth, .045), 0);
                MuteLabel.Text = "Unmute";
            }
            else
            {
                Master.Controller.ToggleVoice(false);
                MuteImage.Source = ImageSource.FromResource("Smartroom.Icons.OptionIconSamMac.png");
                MuteImage.Padding = new Thickness(Master.paddingCalculator(TabWidth, .055), Master.paddingCalculator(TabHeight, .015), Master.paddingCalculator(TabWidth, .050), 0);
                MuteLabel.Text = "Mute";

            }
        }

        public int getSpeechToggle()
        {
            return speechtoggle;
        }
        #endregion

        #region Info Board
        void DisplayOff(object sender, EventArgs args)
        {
            Master.Controller.DisplayOffCommand.Execute(null);
        }
        void DisplayLow(object sender, EventArgs args)
        {
            Master.Controller.DisplayDimCommand.Execute(null);
        }
        void DisplayOn(object sender, EventArgs args)
        {
            Master.Controller.DisplayOnCommand.Execute(null);
        }
        #endregion

        #region Clock Control
        void ClockOff(object sender, EventArgs args)
        {
            Master.Controller.ClockOffCommand.Execute(null);
        }
        void ClockLow(object sender, EventArgs args)
        {
            Master.Controller.ClockLowCommand.Execute(null);
        }
        void ClockMedium(object sender, EventArgs args)
        {
            Master.Controller.ClockMediumCommand.Execute(null);
        }
        void ClockHigh(object sender, EventArgs args)
        {
            Master.Controller.ClockHighCommand.Execute(null);
        }
        #endregion

        //TODO swap icon?
        void ButtonPressed(object sender, EventArgs args)
        {
            //(sender as ImageButton).BackgroundColor = Color.DarkSlateGray;
        }
        void ButtonReleased(object sender, EventArgs args)
        {
            //(sender as ImageButton).BackgroundColor = Color.Transparent;
        }

        private void SetAccessibility(bool setTo)
        {
            AutomationProperties.SetIsInAccessibleTree(SpeechImage, setTo);
            AutomationProperties.SetIsInAccessibleTree(CountdownImage, setTo);
            AutomationProperties.SetIsInAccessibleTree(MuteImage, setTo);
            AutomationProperties.SetIsInAccessibleTree(BoardIconOff, setTo);
            AutomationProperties.SetIsInAccessibleTree(BoardIconSoft, setTo);
            AutomationProperties.SetIsInAccessibleTree(BoardIconFull, setTo);
        }

        void StartTutorial(System.Object sender, System.EventArgs e)
        {
            Master.StartTutorial();
        }

        void OnSwapButtonClicked (object sender, EventArgs args)
        {
            var btn = (Button)sender;

            
            if(Master.SortType == "Most Recently Clicked")
            {
                SwapSort.Text = "Most Frequently Used";
                Master.SortType = "Most Frequently Used";
            }
            else if(Master.SortType == "Most Frequently Used")
            {
                SwapSort.Text = "Time of Day Sort";
                Master.SortType = "Time of Day Sort";
            }
            else
            {
                SwapSort.Text = "Most Recently Clicked";
                Master.SortType = "Most Recently Clicked";
            }
        }
        void SwapButtonSize(object sender, EventArgs args)
        {
            var btn = (Button)sender;

            if (Master.scaleValue == 1)
            {
                Master.scaleValue = 1.35;
                LargeSmall.Text = "Large Buttons Enabled";
            }
            else
            {
                Master.scaleValue = 1;
                LargeSmall.Text = "Small Buttons Enabled";

            }
        }
        void UpdateRoomNumber(object sender, EventArgs args)
        {
            var text = ((Entry)sender).Text;
            try
            {
                Master.Controller.SetRoomNumber((ushort)Int32.Parse(text));
            }
            catch
            {
                Master.Controller.SetRoomNumber(0);
            }
        }
        void UpdatedRoomNumber(System.Object sender, System.Object parameter)
        {

        }

    }
}