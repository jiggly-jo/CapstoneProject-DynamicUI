using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Smartroom.Views
{
    public partial class MediaCableLeft : ContentView
    {
        MasterPage Master;
        public MediaCableLeft(MasterPage context)
        {
            InitializeComponent();
            Master = context;
            ChannelFrame.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .010), -Master.paddingCalculator(Master.TabHeight, .007), 0, Master.paddingCalculator(Master.TabHeight, .118));

            PowerButton.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .054), 0, Master.paddingCalculator(Master.TabWidth, .054), 0);

            ChannelUpBase.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .030), Master.paddingCalculator(Master.TabHeight, .025), Master.paddingCalculator(Master.TabWidth, .030), 0);
            ChannelUp.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .044), Master.paddingCalculator(Master.TabHeight, .025), Master.paddingCalculator(Master.TabWidth, .044), 0);

            CHLabel.Margin = new Thickness(0, 0, 0, Master.paddingCalculator(Master.TabHeight, .008));

            ChannelDownBase.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .030), -Master.paddingCalculator(Master.TabHeight, .009), Master.paddingCalculator(Master.TabWidth, .030), 0);
            ChannelDown.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .044), -Master.paddingCalculator(Master.TabHeight, .009), Master.paddingCalculator(Master.TabWidth, .044), 0);
        }
        async void PowerButtonClicked(object sender, EventArgs args)
        {
        }
        async void ChannelUpClicked(object sender, EventArgs args)
        {
        }
        async void ChannelDownClicked(object sender, EventArgs args)
        {
        }
    }
}
