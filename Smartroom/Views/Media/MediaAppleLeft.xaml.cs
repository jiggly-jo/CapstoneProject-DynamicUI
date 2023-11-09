using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Smartroom.Views
{
    public partial class MediaAppleLeft : ContentView
    {
        MasterPage Master;
        public MediaAppleLeft(MasterPage context)
        {
            InitializeComponent();
            Master = context;

            AppleLeftGrid.Margin = new Thickness(0, 0, 0, Master.paddingCalculator(Master.TabHeight, .125));

            PowerFrame.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .030), -Master.paddingCalculator(Master.TabHeight, .007), Master.paddingCalculator(Master.TabWidth, .010), Master.paddingCalculator(Master.TabHeight, .017));
            PowerButton.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .039), 0, Master.paddingCalculator(Master.TabWidth, .039), 0);

            MenuFrame.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .030), Master.paddingCalculator(Master.TabHeight, .010), Master.paddingCalculator(Master.TabWidth, .010), Master.paddingCalculator(Master.TabHeight, .010));
            MenuButton.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .009), 0, Master.paddingCalculator(Master.TabWidth, .009), 0);

            PlayPauseFrame.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .030), Master.paddingCalculator(Master.TabHeight, .017), Master.paddingCalculator(Master.TabWidth, .010), -Master.paddingCalculator(Master.TabHeight, .007));
            PlayPauseButton.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .009), 0, Master.paddingCalculator(Master.TabWidth, .009), 0);
        }
    }
}
