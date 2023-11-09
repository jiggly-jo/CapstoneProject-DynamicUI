using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Smartroom.Views
{
    public partial class MediaPageArrows : ContentView
    {
        MasterPage Master;
        public MediaPageArrows(MasterPage context)
        {
            InitializeComponent();
            Master = context;

            ArrowFrame.Margin = new Thickness(0, -Master.paddingCalculator(Master.TabHeight, .007), 0, Master.paddingCalculator(Master.TabHeight, .119));
        }
    }
}
