using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Smartroom.Views
{
    public partial class MediaPageNumber : ContentView
    {
        MasterPage Master;
        MediaPageMaster Media;
        public MediaPageNumber(MasterPage context, MediaPageMaster super)
        {
            InitializeComponent();

            Master = context;
            Media = super;
            NumberFrame.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .002), -Master.paddingCalculator(Master.TabHeight, .007), -Master.paddingCalculator(Master.TabWidth, .005), 0);

            

            Label1.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .005), 0, 0, Master.paddingCalculator(Master.TabHeight, .013));
            Label2.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .005), 0, 0, Master.paddingCalculator(Master.TabHeight, .013));
            Label3.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .005), 0, 0, Master.paddingCalculator(Master.TabHeight, .013));
            Label4.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .005), 0, 0, Master.paddingCalculator(Master.TabHeight, .013));
            Label5.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .005), 0, 0, Master.paddingCalculator(Master.TabHeight, .013));
            Label6.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .005), 0, 0, Master.paddingCalculator(Master.TabHeight, .013));
            Label7.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .005), 0, 0, Master.paddingCalculator(Master.TabHeight, .013));
            Label8.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .005), 0, 0, Master.paddingCalculator(Master.TabHeight, .013));
            Label9.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .005), 0, 0, Master.paddingCalculator(Master.TabHeight, .013));
            Label0.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .005), 0, 0, Master.paddingCalculator(Master.TabHeight, .013));

            NumberPadIcon.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .040), Master.paddingCalculator(Master.TabHeight, .040), Master.paddingCalculator(Master.TabWidth, .044), 0);

            ChannelListIcon.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .035), Master.paddingCalculator(Master.TabHeight, .036), Master.paddingCalculator(Master.TabWidth, .018), 0);

            Channel1Button.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .005),0, Master.paddingCalculator(Master.TabWidth, .004), 0);
            Channel2Button.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .005), 0, Master.paddingCalculator(Master.TabWidth, .004), 0);
            Channel3Button.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .005), 0, Master.paddingCalculator(Master.TabWidth, .004), 0);
            Channel0Button.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .005), 0, Master.paddingCalculator(Master.TabWidth, .004), 0);

        }

        public void ShowChannelList(object sender, EventArgs args)
        {
            Master.Controller.UpdateChannelList("ChannelList");
            Media.ChangeCableCenter("ChannelList");
        }

        void NumberClicked(object sender, EventArgs args)
        {
            if (sender.Equals(Channel1Button))
            {
                Master.Controller.ChangeChannel(1);
            }
            else if (sender.Equals(Channel2Button))
            {
                Master.Controller.ChangeChannel(2);
            }
            else if (sender.Equals(Channel3Button))
            {
                Master.Controller.ChangeChannel(3);
            }
            else if (sender.Equals(Channel4Button))
            {
                Master.Controller.ChangeChannel(4);
            }
            else if (sender.Equals(Channel5Button))
            {
                Master.Controller.ChangeChannel(5);
            }
            else if (sender.Equals(Channel6Button))
            {
                Master.Controller.ChangeChannel(6);
            }
            else if (sender.Equals(Channel7Button))
            {
                Master.Controller.ChangeChannel(7);
            }
            else if (sender.Equals(Channel8Button))
            {
                Master.Controller.ChangeChannel(8);
            }
            else if (sender.Equals(Channel9Button))
            {
                Master.Controller.ChangeChannel(9);
            }
            else if (sender.Equals(Channel0Button))
            {
                Master.Controller.ChangeChannel(0);
            }
        }
    }
}