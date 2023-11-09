using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Smartroom.Views
{
    public partial class MediaPowerLeft : ContentView
    {
        MasterPage Master;
        public MediaPowerLeft(MasterPage context)
        {
            InitializeComponent();
            Master = context;

            ChannelFrame.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .010), -Master.paddingCalculator(Master.TabHeight, .007), 0, Master.paddingCalculator(Master.TabHeight, .119));
            PowerButton.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .040), 0, Master.paddingCalculator(Master.TabWidth, .040), 0);
        }
    }
}
