using System;
using System.Collections.Generic;
using Smartroom.ViewModels;
using Xamarin.Forms;

namespace Smartroom.Views
{
    public partial class MediaSound : ContentView
    {
        private MasterPage Master;
        private string selectedDevice;
        public MediaSound(MasterPage master, string deviceToControl)
        {
            InitializeComponent();
            Master = master;

            VolumeFrame.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .006), -Master.paddingCalculator(Master.TabHeight, .007), Master.paddingCalculator(Master.TabWidth, .034), Master.paddingCalculator(Master.TabHeight, .118));

            MuteButton.Padding = new Thickness(Master.paddingCalculator(Master.TabWidth, .063), Master.paddingCalculator(Master.TabHeight, .010), Master.paddingCalculator(Master.TabWidth, .060), 0);

            VolumeUpBase.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .034), Master.paddingCalculator(Master.TabHeight, .013), Master.paddingCalculator(Master.TabWidth, .034), 0);
            VolumeUpButton.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .049), Master.paddingCalculator(Master.TabHeight, .013), Master.paddingCalculator(Master.TabWidth, .047), 0);

            VolumeDownBase.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .034), Master.paddingCalculator(Master.TabHeight, .008), Master.paddingCalculator(Master.TabWidth, .034), 0);
            VolumeDownButton.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .049), Master.paddingCalculator(Master.TabHeight, .008), Master.paddingCalculator(Master.TabWidth, .047), 0);

            VolumeLabel.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .003), Master.paddingCalculator(Master.TabHeight, .001), 0, 0);

            setDeviceToControl(deviceToControl);
        }

        private void setDeviceToControl(string deviceToControl)
        {
            selectedDevice = deviceToControl;

            switch (deviceToControl)
            {
                case "TV":
                    VolumeUpButton.Command = Master.Controller.VolumeUpCommand;
                    VolumeDownButton.Command = Master.Controller.VolumeDownCommand;
                    MuteButton.Command = Master.Controller.VolumeMuteCommand;
                    break;
                case "SBAR":
                    VolumeUpButton.Command = Master.Controller.SBarVolumeUpCommand;
                    VolumeDownButton.Command = Master.Controller.SBarVolumeDownCommand;
                    MuteButton.Command = Master.Controller.SBarVolumeMuteCommand;
                    break;
            }

        }
        async void MuteButtonClicked(object sender, EventArgs args)
        {
        }

        async void VolumeUpClicked(object sender, EventArgs args)
        {
        }
        async void VolumeDownClicked(object sender, EventArgs args)
        {
        }
    }
}