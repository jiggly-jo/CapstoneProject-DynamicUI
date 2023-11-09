using System;
using System.Collections.Generic;
using Smartroom.Helpers;
using System.Threading;
using Smartroom.Utilities;
using Xamarin.Forms;

namespace Smartroom.Views
{
    public partial class MediaPageChannels : ContentView
    {
        MasterPage Master;
        MediaPageMaster Media;
        public MediaPageChannels(MasterPage context, MediaPageMaster super)
        {
            InitializeComponent();
            Master = context;
            Media = super;

            ChannelListFrame.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .002), -Master.paddingCalculator(Master.TabHeight, .007), -Master.paddingCalculator(Master.TabWidth, .005), 0);

            ListLabel.Margin = new Thickness(-Master.paddingCalculator(Master.TabWidth, .002), 0, 0, Master.paddingCalculator(Master.TabHeight, .004));

            NumberPadButton.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .033), Master.paddingCalculator(Master.TabHeight, .033), Master.paddingCalculator(Master.TabWidth, .101), 0);
            ChannelListIcon.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .106), Master.paddingCalculator(Master.TabHeight, .030), Master.paddingCalculator(Master.TabWidth, .025), 0);
        }

        void ChannelTapped(object sender, EventArgs args)
        {
            ResetChannels();

            SidebarLabel sent = (SidebarLabel)sender;

            string channel = sent.StyleId;

            char[] numbers = channel.ToCharArray();


            ColorChannel(int.Parse(channel.Substring(7, 2)));

            Master.Controller.ChangeChannel(int.Parse(numbers[7].ToString()));
            Thread.Sleep(500);
            Master.Controller.ChangeChannel(int.Parse(numbers[8].ToString()));


        }

        void ResetChannels()
        {
            Channel27.BackgroundColor = Color.Transparent;
            Channel28.BackgroundColor = Color.Transparent;
            Channel29.BackgroundColor = Color.Transparent;
            Channel30.BackgroundColor = Color.Transparent;
            Channel31.BackgroundColor = Color.Transparent;
            Channel32.BackgroundColor = Color.Transparent;
            Channel33.BackgroundColor = Color.Transparent;
            Channel34.BackgroundColor = Color.Transparent;
            Channel35.BackgroundColor = Color.Transparent;
            Channel36.BackgroundColor = Color.Transparent;
            Channel37.BackgroundColor = Color.Transparent;
            Channel38.BackgroundColor = Color.Transparent;
            Channel39.BackgroundColor = Color.Transparent;
            Channel40.BackgroundColor = Color.Transparent;
            Channel41.BackgroundColor = Color.Transparent;
            Channel42.BackgroundColor = Color.Transparent;
            Channel43.BackgroundColor = Color.Transparent;
            Channel44.BackgroundColor = Color.Transparent;
            Channel45.BackgroundColor = Color.Transparent;
            Channel46.BackgroundColor = Color.Transparent;
            Channel47.BackgroundColor = Color.Transparent;
            Channel48.BackgroundColor = Color.Transparent;
            Channel49.BackgroundColor = Color.Transparent;
            Channel50.BackgroundColor = Color.Transparent;
            Channel51.BackgroundColor = Color.Transparent;
            Channel52.BackgroundColor = Color.Transparent;
            Channel53.BackgroundColor = Color.Transparent;
            Channel54.BackgroundColor = Color.Transparent;
            Channel55.BackgroundColor = Color.Transparent;
            Channel56.BackgroundColor = Color.Transparent;
            Channel57.BackgroundColor = Color.Transparent;
            Channel58.BackgroundColor = Color.Transparent;
            Channel59.BackgroundColor = Color.Transparent;
            Channel60.BackgroundColor = Color.Transparent;
            Channel61.BackgroundColor = Color.Transparent;
            Channel62.BackgroundColor = Color.Transparent;
            Channel63.BackgroundColor = Color.Transparent;
            Channel64.BackgroundColor = Color.Transparent;
            Channel65.BackgroundColor = Color.Transparent;
            Channel66.BackgroundColor = Color.Transparent;
            Channel67.BackgroundColor = Color.Transparent;
            Channel68.BackgroundColor = Color.Transparent;
            Channel69.BackgroundColor = Color.Transparent;
            Channel70.BackgroundColor = Color.Transparent;
            Channel71.BackgroundColor = Color.Transparent;
            Channel72.BackgroundColor = Color.Transparent;
            Channel73.BackgroundColor = Color.Transparent;
            Channel74.BackgroundColor = Color.Transparent;
            Channel75.BackgroundColor = Color.Transparent;
            Channel76.BackgroundColor = Color.Transparent;
            Channel77.BackgroundColor = Color.Transparent;
            Channel78.BackgroundColor = Color.Transparent;
            Channel79.BackgroundColor = Color.Transparent;
            Channel80.BackgroundColor = Color.Transparent;
            Channel81.BackgroundColor = Color.Transparent;
            Channel82.BackgroundColor = Color.Transparent;
            Channel83.BackgroundColor = Color.Transparent;
            Channel84.BackgroundColor = Color.Transparent;
            Channel85.BackgroundColor = Color.Transparent;
            Channel86.BackgroundColor = Color.Transparent;

            Channel27Num.BackgroundColor = Color.Transparent;
            Channel28Num.BackgroundColor = Color.Transparent;
            Channel29Num.BackgroundColor = Color.Transparent;
            Channel30Num.BackgroundColor = Color.Transparent;
            Channel31Num.BackgroundColor = Color.Transparent;
            Channel32Num.BackgroundColor = Color.Transparent;
            Channel33Num.BackgroundColor = Color.Transparent;
            Channel34Num.BackgroundColor = Color.Transparent;
            Channel35Num.BackgroundColor = Color.Transparent;
            Channel36Num.BackgroundColor = Color.Transparent;
            Channel37Num.BackgroundColor = Color.Transparent;
            Channel38Num.BackgroundColor = Color.Transparent;
            Channel39Num.BackgroundColor = Color.Transparent;
            Channel40Num.BackgroundColor = Color.Transparent;
            Channel41Num.BackgroundColor = Color.Transparent;
            Channel42Num.BackgroundColor = Color.Transparent;
            Channel43Num.BackgroundColor = Color.Transparent;
            Channel44Num.BackgroundColor = Color.Transparent;
            Channel45Num.BackgroundColor = Color.Transparent;
            Channel46Num.BackgroundColor = Color.Transparent;
            Channel47Num.BackgroundColor = Color.Transparent;
            Channel48Num.BackgroundColor = Color.Transparent;
            Channel49Num.BackgroundColor = Color.Transparent;
            Channel50Num.BackgroundColor = Color.Transparent;
            Channel51Num.BackgroundColor = Color.Transparent;
            Channel52Num.BackgroundColor = Color.Transparent;
            Channel53Num.BackgroundColor = Color.Transparent;
            Channel54Num.BackgroundColor = Color.Transparent;
            Channel55Num.BackgroundColor = Color.Transparent;
            Channel56Num.BackgroundColor = Color.Transparent;
            Channel57Num.BackgroundColor = Color.Transparent;
            Channel58Num.BackgroundColor = Color.Transparent;
            Channel59Num.BackgroundColor = Color.Transparent;
            Channel60Num.BackgroundColor = Color.Transparent;
            Channel61Num.BackgroundColor = Color.Transparent;
            Channel62Num.BackgroundColor = Color.Transparent;
            Channel63Num.BackgroundColor = Color.Transparent;
            Channel64Num.BackgroundColor = Color.Transparent;
            Channel65Num.BackgroundColor = Color.Transparent;
            Channel66Num.BackgroundColor = Color.Transparent;
            Channel67Num.BackgroundColor = Color.Transparent;
            Channel68Num.BackgroundColor = Color.Transparent;
            Channel69Num.BackgroundColor = Color.Transparent;
            Channel70Num.BackgroundColor = Color.Transparent;
            Channel71Num.BackgroundColor = Color.Transparent;
            Channel72Num.BackgroundColor = Color.Transparent;
            Channel73Num.BackgroundColor = Color.Transparent;
            Channel74Num.BackgroundColor = Color.Transparent;
            Channel75Num.BackgroundColor = Color.Transparent;
            Channel76Num.BackgroundColor = Color.Transparent;
            Channel77Num.BackgroundColor = Color.Transparent;
            Channel78Num.BackgroundColor = Color.Transparent;
            Channel79Num.BackgroundColor = Color.Transparent;
            Channel80Num.BackgroundColor = Color.Transparent;
            Channel81Num.BackgroundColor = Color.Transparent;
            Channel82Num.BackgroundColor = Color.Transparent;
            Channel83Num.BackgroundColor = Color.Transparent;
            Channel84Num.BackgroundColor = Color.Transparent;
            Channel85Num.BackgroundColor = Color.Transparent;
            Channel86Num.BackgroundColor = Color.Transparent;

            Channel27.TextColor = Color.FromHex("#29282a");
            Channel28.TextColor = Color.FromHex("#29282a");
            Channel29.TextColor = Color.FromHex("#29282a");
            Channel30.TextColor = Color.FromHex("#29282a");
            Channel31.TextColor = Color.FromHex("#29282a");
            Channel32.TextColor = Color.FromHex("#29282a");
            Channel33.TextColor = Color.FromHex("#29282a");
            Channel34.TextColor = Color.FromHex("#29282a");
            Channel35.TextColor = Color.FromHex("#29282a");
            Channel36.TextColor = Color.FromHex("#29282a");
            Channel37.TextColor = Color.FromHex("#29282a");
            Channel38.TextColor = Color.FromHex("#29282a");
            Channel39.TextColor = Color.FromHex("#29282a");
            Channel40.TextColor = Color.FromHex("#29282a");
            Channel41.TextColor = Color.FromHex("#29282a");
            Channel42.TextColor = Color.FromHex("#29282a");
            Channel43.TextColor = Color.FromHex("#29282a");
            Channel44.TextColor = Color.FromHex("#29282a");
            Channel45.TextColor = Color.FromHex("#29282a");
            Channel46.TextColor = Color.FromHex("#29282a");
            Channel47.TextColor = Color.FromHex("#29282a");
            Channel48.TextColor = Color.FromHex("#29282a");
            Channel49.TextColor = Color.FromHex("#29282a");
            Channel50.TextColor = Color.FromHex("#29282a");
            Channel51.TextColor = Color.FromHex("#29282a");
            Channel52.TextColor = Color.FromHex("#29282a");
            Channel53.TextColor = Color.FromHex("#29282a");
            Channel54.TextColor = Color.FromHex("#29282a");
            Channel55.TextColor = Color.FromHex("#29282a");
            Channel56.TextColor = Color.FromHex("#29282a");
            Channel57.TextColor = Color.FromHex("#29282a");
            Channel58.TextColor = Color.FromHex("#29282a");
            Channel59.TextColor = Color.FromHex("#29282a");
            Channel60.TextColor = Color.FromHex("#29282a");
            Channel61.TextColor = Color.FromHex("#29282a");
            Channel62.TextColor = Color.FromHex("#29282a");
            Channel63.TextColor = Color.FromHex("#29282a");
            Channel64.TextColor = Color.FromHex("#29282a");
            Channel65.TextColor = Color.FromHex("#29282a");
            Channel66.TextColor = Color.FromHex("#29282a");
            Channel67.TextColor = Color.FromHex("#29282a");
            Channel68.TextColor = Color.FromHex("#29282a");
            Channel69.TextColor = Color.FromHex("#29282a");
            Channel70.TextColor = Color.FromHex("#29282a");
            Channel71.TextColor = Color.FromHex("#29282a");
            Channel72.TextColor = Color.FromHex("#29282a");
            Channel73.TextColor = Color.FromHex("#29282a");
            Channel74.TextColor = Color.FromHex("#29282a");
            Channel75.TextColor = Color.FromHex("#29282a");
            Channel76.TextColor = Color.FromHex("#29282a");
            Channel77.TextColor = Color.FromHex("#29282a");
            Channel78.TextColor = Color.FromHex("#29282a");
            Channel79.TextColor = Color.FromHex("#29282a");
            Channel80.TextColor = Color.FromHex("#29282a");
            Channel81.TextColor = Color.FromHex("#29282a");
            Channel82.TextColor = Color.FromHex("#29282a");
            Channel83.TextColor = Color.FromHex("#29282a");
            Channel84.TextColor = Color.FromHex("#29282a");
            Channel85.TextColor = Color.FromHex("#29282a");
            Channel86.TextColor = Color.FromHex("#29282a");

            Channel27Num.TextColor = Color.FromHex("#29282a");
            Channel28Num.TextColor = Color.FromHex("#29282a");
            Channel29Num.TextColor = Color.FromHex("#29282a");
            Channel30Num.TextColor = Color.FromHex("#29282a");
            Channel31Num.TextColor = Color.FromHex("#29282a");
            Channel32Num.TextColor = Color.FromHex("#29282a");
            Channel33Num.TextColor = Color.FromHex("#29282a");
            Channel34Num.TextColor = Color.FromHex("#29282a");
            Channel35Num.TextColor = Color.FromHex("#29282a");
            Channel36Num.TextColor = Color.FromHex("#29282a");
            Channel37Num.TextColor = Color.FromHex("#29282a");
            Channel38Num.TextColor = Color.FromHex("#29282a");
            Channel39Num.TextColor = Color.FromHex("#29282a");
            Channel40Num.TextColor = Color.FromHex("#29282a");
            Channel41Num.TextColor = Color.FromHex("#29282a");
            Channel42Num.TextColor = Color.FromHex("#29282a");
            Channel43Num.TextColor = Color.FromHex("#29282a");
            Channel44Num.TextColor = Color.FromHex("#29282a");
            Channel45Num.TextColor = Color.FromHex("#29282a");
            Channel46Num.TextColor = Color.FromHex("#29282a");
            Channel47Num.TextColor = Color.FromHex("#29282a");
            Channel48Num.TextColor = Color.FromHex("#29282a");
            Channel49Num.TextColor = Color.FromHex("#29282a");
            Channel50Num.TextColor = Color.FromHex("#29282a");
            Channel51Num.TextColor = Color.FromHex("#29282a");
            Channel52Num.TextColor = Color.FromHex("#29282a");
            Channel53Num.TextColor = Color.FromHex("#29282a");
            Channel54Num.TextColor = Color.FromHex("#29282a");
            Channel55Num.TextColor = Color.FromHex("#29282a");
            Channel56Num.TextColor = Color.FromHex("#29282a");
            Channel57Num.TextColor = Color.FromHex("#29282a");
            Channel58Num.TextColor = Color.FromHex("#29282a");
            Channel59Num.TextColor = Color.FromHex("#29282a");
            Channel60Num.TextColor = Color.FromHex("#29282a");
            Channel61Num.TextColor = Color.FromHex("#29282a");
            Channel62Num.TextColor = Color.FromHex("#29282a");
            Channel63Num.TextColor = Color.FromHex("#29282a");
            Channel64Num.TextColor = Color.FromHex("#29282a");
            Channel65Num.TextColor = Color.FromHex("#29282a");
            Channel66Num.TextColor = Color.FromHex("#29282a");
            Channel67Num.TextColor = Color.FromHex("#29282a");
            Channel68Num.TextColor = Color.FromHex("#29282a");
            Channel69Num.TextColor = Color.FromHex("#29282a");
            Channel70Num.TextColor = Color.FromHex("#29282a");
            Channel71Num.TextColor = Color.FromHex("#29282a");
            Channel72Num.TextColor = Color.FromHex("#29282a");
            Channel73Num.TextColor = Color.FromHex("#29282a");
            Channel74Num.TextColor = Color.FromHex("#29282a");
            Channel75Num.TextColor = Color.FromHex("#29282a");
            Channel76Num.TextColor = Color.FromHex("#29282a");
            Channel77Num.TextColor = Color.FromHex("#29282a");
            Channel78Num.TextColor = Color.FromHex("#29282a");
            Channel79Num.TextColor = Color.FromHex("#29282a");
            Channel80Num.TextColor = Color.FromHex("#29282a");
            Channel81Num.TextColor = Color.FromHex("#29282a");
            Channel82Num.TextColor = Color.FromHex("#29282a");
            Channel83Num.TextColor = Color.FromHex("#29282a");
            Channel84Num.TextColor = Color.FromHex("#29282a");
            Channel85Num.TextColor = Color.FromHex("#29282a");
            Channel86Num.TextColor = Color.FromHex("#29282a");  
        }

        void ColorChannel(int channel)
        {
            switch (channel)
            {
                case 27:
                    Channel27.BackgroundColor = Constants.UofURed;
                    Channel27Num.BackgroundColor = Constants.UofURed;
                    Channel27.TextColor = Color.White;
                    Channel27Num.TextColor = Color.White;
                    break;
                case 28:
                    Channel28.BackgroundColor = Constants.UofURed;
                    Channel28Num.BackgroundColor = Constants.UofURed;
                    Channel28.TextColor = Color.White;
                    Channel28Num.TextColor = Color.White;
                    break;
                case 29:
                    Channel29.BackgroundColor = Constants.UofURed;
                    Channel29Num.BackgroundColor = Constants.UofURed;
                    Channel29.TextColor = Color.White;
                    Channel29Num.TextColor = Color.White;
                    break;
                case 30:
                    Channel30.BackgroundColor = Constants.UofURed;
                    Channel30Num.BackgroundColor = Constants.UofURed;
                    Channel30.TextColor = Color.White;
                    Channel30Num.TextColor = Color.White;
                    break;
                case 31:
                        Channel31.BackgroundColor = Constants.UofURed;
                        Channel31Num.BackgroundColor = Constants.UofURed;
                        Channel31.TextColor = Color.White;
                        Channel31Num.TextColor = Color.White;
                        break;
                    case 32:
                        Channel32.BackgroundColor = Constants.UofURed;
                        Channel32Num.BackgroundColor = Constants.UofURed;
                        Channel32.TextColor = Color.White;
                        Channel32Num.TextColor = Color.White;
                        break;
                    case 33:
                        Channel33.BackgroundColor = Constants.UofURed;
                        Channel33Num.BackgroundColor = Constants.UofURed;
                        Channel33.TextColor = Color.White;
                        Channel33Num.TextColor = Color.White;
                        break;
                    case 34:
                        Channel34.BackgroundColor = Constants.UofURed;
                        Channel34Num.BackgroundColor = Constants.UofURed;
                        Channel34.TextColor = Color.White;
                        Channel34Num.TextColor = Color.White;
                        break;
                    case 35:
                        Channel35.BackgroundColor = Constants.UofURed;
                        Channel35Num.BackgroundColor = Constants.UofURed;
                        Channel35.TextColor = Color.White;
                        Channel35Num.TextColor = Color.White;
                        break;
                    case 36:
                        Channel36.BackgroundColor = Constants.UofURed;
                        Channel36Num.BackgroundColor = Constants.UofURed;
                        Channel36.TextColor = Color.White;
                        Channel36Num.TextColor = Color.White;
                        break;
                    case 37:
                        Channel37.BackgroundColor = Constants.UofURed;
                        Channel37Num.BackgroundColor = Constants.UofURed;
                        Channel37.TextColor = Color.White;
                        Channel37Num.TextColor = Color.White;
                        break;
                    case 38:
                        Channel38.BackgroundColor = Constants.UofURed;
                        Channel38Num.BackgroundColor = Constants.UofURed;
                        Channel38.TextColor = Color.White;
                        Channel38Num.TextColor = Color.White;
                        break;
                    case 39:
                        Channel39.BackgroundColor = Constants.UofURed;
                        Channel39Num.BackgroundColor = Constants.UofURed;
                        Channel39.TextColor = Color.White;
                        Channel39Num.TextColor = Color.White;
                        break;
                    case 40:
                        Channel40.BackgroundColor = Constants.UofURed;
                        Channel40Num.BackgroundColor = Constants.UofURed;
                        Channel40.TextColor = Color.White;
                        Channel40Num.TextColor = Color.White;
                        break;
                    case 41:
                        Channel41.BackgroundColor = Constants.UofURed;
                        Channel41Num.BackgroundColor = Constants.UofURed;
                        Channel41.TextColor = Color.White;
                        Channel41Num.TextColor = Color.White;
                        break;
                    case 42:
                        Channel42.BackgroundColor = Constants.UofURed;
                        Channel42Num.BackgroundColor = Constants.UofURed;
                        Channel42.TextColor = Color.White;
                        Channel42Num.TextColor = Color.White;
                        break;
                    case 43:
                        Channel43.BackgroundColor = Constants.UofURed;
                        Channel43Num.BackgroundColor = Constants.UofURed;
                        Channel43.TextColor = Color.White;
                        Channel43Num.TextColor = Color.White;
                        break;
                    case 44:
                        Channel44.BackgroundColor = Constants.UofURed;
                        Channel44Num.BackgroundColor = Constants.UofURed;
                        Channel44.TextColor = Color.White;
                        Channel44Num.TextColor = Color.White;
                        break;
                    case 45:
                        Channel45.BackgroundColor = Constants.UofURed;
                        Channel45Num.BackgroundColor = Constants.UofURed;
                        Channel45.TextColor = Color.White;
                        Channel45Num.TextColor = Color.White;
                        break;
                    case 46:
                        Channel46.BackgroundColor = Constants.UofURed;
                        Channel46Num.BackgroundColor = Constants.UofURed;
                        Channel46.TextColor = Color.White;
                        Channel46Num.TextColor = Color.White;
                        break;
                    case 47:
                        Channel47.BackgroundColor = Constants.UofURed;
                        Channel47Num.BackgroundColor = Constants.UofURed;
                        Channel47.TextColor = Color.White;
                        Channel47Num.TextColor = Color.White;
                        break;
                    case 48:
                        Channel48.BackgroundColor = Constants.UofURed;
                        Channel48Num.BackgroundColor = Constants.UofURed;
                        Channel48.TextColor = Color.White;
                        Channel48Num.TextColor = Color.White;
                        break;
                    case 49:
                        Channel49.BackgroundColor = Constants.UofURed;
                        Channel49Num.BackgroundColor = Constants.UofURed;
                        Channel49.TextColor = Color.White;
                        Channel49Num.TextColor = Color.White;
                        break;
                    case 50:
                        Channel50.BackgroundColor = Constants.UofURed;
                        Channel50Num.BackgroundColor = Constants.UofURed;
                        Channel50.TextColor = Color.White;
                        Channel50Num.TextColor = Color.White;
                        break;
                    case 51:
                        Channel51.BackgroundColor = Constants.UofURed;
                        Channel51Num.BackgroundColor = Constants.UofURed;
                        Channel51.TextColor = Color.White;
                        Channel51Num.TextColor = Color.White;
                        break;
                    case 52:
                        Channel52.BackgroundColor = Constants.UofURed;
                        Channel52Num.BackgroundColor = Constants.UofURed;
                        Channel52.TextColor = Color.White;
                        Channel52Num.TextColor = Color.White;
                        break;
                    case 53:
                        Channel53.BackgroundColor = Constants.UofURed;
                        Channel53Num.BackgroundColor = Constants.UofURed;
                        Channel53.TextColor = Color.White;
                        Channel53Num.TextColor = Color.White;
                        break;
                    case 54:
                        Channel54.BackgroundColor = Constants.UofURed;
                        Channel54Num.BackgroundColor = Constants.UofURed;
                        Channel54.TextColor = Color.White;
                        Channel54Num.TextColor = Color.White;
                        break;
                    case 55:
                        Channel55.BackgroundColor = Constants.UofURed;
                        Channel55Num.BackgroundColor = Constants.UofURed;
                        Channel55.TextColor = Color.White;
                        Channel55Num.TextColor = Color.White;
                        break;
                    case 56:
                        Channel56.BackgroundColor = Constants.UofURed;
                        Channel56Num.BackgroundColor = Constants.UofURed;
                        Channel56.TextColor = Color.White;
                        Channel56Num.TextColor = Color.White;
                        break;
                    case 57:
                        Channel57.BackgroundColor = Constants.UofURed;
                        Channel57Num.BackgroundColor = Constants.UofURed;
                        Channel57.TextColor = Color.White;
                        Channel57Num.TextColor = Color.White;
                        break;
                    case 58:
                        Channel58.BackgroundColor = Constants.UofURed;
                        Channel58Num.BackgroundColor = Constants.UofURed;
                        Channel58.TextColor = Color.White;
                        Channel58Num.TextColor = Color.White;
                        break;
                    case 59:
                        Channel59.BackgroundColor = Constants.UofURed;
                        Channel59Num.BackgroundColor = Constants.UofURed;
                        Channel59.TextColor = Color.White;
                        Channel59Num.TextColor = Color.White;
                        break;
                    case 60:
                        Channel60.BackgroundColor = Constants.UofURed;
                        Channel60Num.BackgroundColor = Constants.UofURed;
                        Channel60.TextColor = Color.White;
                        Channel60Num.TextColor = Color.White;
                        break;
                    case 61:
                        Channel61.BackgroundColor = Constants.UofURed;
                        Channel61Num.BackgroundColor = Constants.UofURed;
                        Channel61.TextColor = Color.White;
                        Channel61Num.TextColor = Color.White;
                        break;
                    case 62:
                        Channel62.BackgroundColor = Constants.UofURed;
                        Channel62Num.BackgroundColor = Constants.UofURed;
                        Channel62.TextColor = Color.White;
                        Channel62Num.TextColor = Color.White;
                        break;
                    case 63:
                        Channel63.BackgroundColor = Constants.UofURed;
                        Channel63Num.BackgroundColor = Constants.UofURed;
                        Channel63.TextColor = Color.White;
                        Channel63Num.TextColor = Color.White;
                        break;
                    case 64:
                        Channel64.BackgroundColor = Constants.UofURed;
                        Channel64Num.BackgroundColor = Constants.UofURed;
                        Channel64.TextColor = Color.White;
                        Channel64Num.TextColor = Color.White;
                        break;
                    case 65:
                        Channel65.BackgroundColor = Constants.UofURed;
                        Channel65Num.BackgroundColor = Constants.UofURed;
                        Channel65.TextColor = Color.White;
                        Channel65Num.TextColor = Color.White;
                        break;
                    case 66:
                        Channel66.BackgroundColor = Constants.UofURed;
                        Channel66Num.BackgroundColor = Constants.UofURed;
                        Channel66.TextColor = Color.White;
                        Channel66Num.TextColor = Color.White;
                        break;
                    case 67:
                        Channel67.BackgroundColor = Constants.UofURed;
                        Channel67Num.BackgroundColor = Constants.UofURed;
                        Channel67.TextColor = Color.White;
                        Channel67Num.TextColor = Color.White;
                        break;
                    case 68:
                        Channel68.BackgroundColor = Constants.UofURed;
                        Channel68Num.BackgroundColor = Constants.UofURed;
                        Channel68.TextColor = Color.White;
                        Channel68Num.TextColor = Color.White;
                        break;
                    case 69:
                        Channel69.BackgroundColor = Constants.UofURed;
                        Channel69Num.BackgroundColor = Constants.UofURed;
                        Channel69.TextColor = Color.White;
                        Channel69Num.TextColor = Color.White;
                        break;
                    case 70:
                        Channel70.BackgroundColor = Constants.UofURed;
                        Channel70Num.BackgroundColor = Constants.UofURed;
                        Channel70.TextColor = Color.White;
                        Channel70Num.TextColor = Color.White;
                        break;
                    case 71:
                        Channel71.BackgroundColor = Constants.UofURed;
                        Channel71Num.BackgroundColor = Constants.UofURed;
                        Channel71.TextColor = Color.White;
                        Channel71Num.TextColor = Color.White;
                        break;
                    case 72:
                        Channel72.BackgroundColor = Constants.UofURed;
                        Channel72Num.BackgroundColor = Constants.UofURed;
                        Channel72.TextColor = Color.White;
                        Channel72Num.TextColor = Color.White;
                        break;
                    case 73:
                        Channel73.BackgroundColor = Constants.UofURed;
                        Channel73Num.BackgroundColor = Constants.UofURed;
                        Channel73.TextColor = Color.White;
                        Channel73Num.TextColor = Color.White;
                        break;
                    case 74:
                        Channel74.BackgroundColor = Constants.UofURed;
                        Channel74Num.BackgroundColor = Constants.UofURed;
                        Channel74.TextColor = Color.White;
                        Channel74Num.TextColor = Color.White;
                        break;
                    case 75:
                        Channel75.BackgroundColor = Constants.UofURed;
                        Channel75Num.BackgroundColor = Constants.UofURed;
                        Channel75.TextColor = Color.White;
                        Channel75Num.TextColor = Color.White;
                        break;
                    case 76:
                        Channel76.BackgroundColor = Constants.UofURed;
                        Channel76Num.BackgroundColor = Constants.UofURed;
                        Channel76.TextColor = Color.White;
                        Channel76Num.TextColor = Color.White;
                        break;
                    case 77:
                        Channel77.BackgroundColor = Constants.UofURed;
                        Channel77Num.BackgroundColor = Constants.UofURed;
                        Channel77.TextColor = Color.White;
                        Channel77Num.TextColor = Color.White;
                        break;
                    case 78:
                        Channel78.BackgroundColor = Constants.UofURed;
                        Channel78Num.BackgroundColor = Constants.UofURed;
                        Channel78.TextColor = Color.White;
                        Channel78Num.TextColor = Color.White;
                        break;
                    case 79:
                        Channel79.BackgroundColor = Constants.UofURed;
                        Channel79Num.BackgroundColor = Constants.UofURed;
                        Channel79.TextColor = Color.White;
                        Channel79Num.TextColor = Color.White;
                        break;
                    case 80:
                        Channel80.BackgroundColor = Constants.UofURed;
                        Channel80Num.BackgroundColor = Constants.UofURed;
                        Channel80.TextColor = Color.White;
                        Channel80Num.TextColor = Color.White;
                        break;
                    case 81:
                        Channel81.BackgroundColor = Constants.UofURed;
                        Channel81Num.BackgroundColor = Constants.UofURed;
                        Channel81.TextColor = Color.White;
                        Channel81Num.TextColor = Color.White;
                        break;
                    case 82:
                        Channel82.BackgroundColor = Constants.UofURed;
                        Channel82Num.BackgroundColor = Constants.UofURed;
                        Channel82.TextColor = Color.White;
                        Channel82Num.TextColor = Color.White;
                        break;
                    case 83:
                        Channel83.BackgroundColor = Constants.UofURed;
                        Channel83Num.BackgroundColor = Constants.UofURed;
                        Channel83.TextColor = Color.White;
                        Channel83Num.TextColor = Color.White;
                        break;
                    case 84:
                        Channel84.BackgroundColor = Constants.UofURed;
                        Channel84Num.BackgroundColor = Constants.UofURed;
                        Channel84.TextColor = Color.White;
                        Channel84Num.TextColor = Color.White;
                        break;
                    case 85:
                        Channel85.BackgroundColor = Constants.UofURed;
                        Channel85Num.BackgroundColor = Constants.UofURed;
                        Channel85.TextColor = Color.White;
                        Channel85Num.TextColor = Color.White;
                        break;
                    case 86:
                        Channel86.BackgroundColor = Constants.UofURed;
                        Channel86Num.BackgroundColor = Constants.UofURed;
                        Channel86.TextColor = Color.White;
                        Channel86Num.TextColor = Color.White;
                        break;
                
                default:
                    ResetChannels();
                    break;
            }
        }

        public void ShowNumberPad(object sender, EventArgs args)
        {
            Master.Controller.UpdateChannelList("NumberPad");
            Media.ChangeCableCenter("NumberPad");
        }
    }
}
