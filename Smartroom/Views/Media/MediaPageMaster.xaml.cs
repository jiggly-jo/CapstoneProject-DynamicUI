using System;
using System.Xml.Linq;
using System.Collections.Generic;
using FFImageLoading.Forms.Handlers;
using FFImageLoading.Svg.Forms;
using Smartroom.Models;
using Smartroom.Helpers;
using Xamarin.Forms;

namespace Smartroom.Views
{
    public partial class MediaPageMaster : ContentView
    {

        MasterPage Master;
        private MediaInputButton SelectedInputButton { get; set; }

        public MediaPageMaster(MasterPage context)
        {
            InitializeComponent();

            Master = context;

            MediaLabel.Margin = new Thickness(Master.paddingCalculator(Master.TabWidth, .007), 0, 0, Master.paddingCalculator(Master.TabHeight, .015));

            
            ChangeMediaContent(Master.Controller.GetMediaInputSelected(), false);
            
        }

        public void ChangeMediaContent(string input, bool executeInputCommand)
        {
            switch (input)
            {
                case "Apple TV" or "AppleMedia":
                    if (executeInputCommand)
                        Master.Controller.CasterCommand.Execute(null);
                    else
                    {
                        SelectedInputButton = AppleMedia;
                        SelectedInputButton.IsSelected = true;
                    }

                    Master.Controller.UpdateMediaInputSelected("AppleMedia");
                    MediaCenter.Content = new MediaPageArrows(Master);
                    MediaLeft.Content = new MediaAppleLeft(Master);
                    MediaRightSound.Content = new MediaSound(Master, "TV");
                    break;

                case "HDMI" or "HDMIMedia":
                    if (executeInputCommand)
                        Master.Controller.HDMICommand.Execute(null);
                    else
                    {
                        SelectedInputButton = HDMIMedia;
                        SelectedInputButton.IsSelected = true;
                    }
                    Master.Controller.UpdateMediaInputSelected("HDMIMedia");
                    MediaCenter.Content = new MediaPageHDMI(Master, this);
                    MediaLeft.Content = new MediaPowerLeft(Master);
                    MediaRightSound.Content = new MediaSound(Master, "TV");
                    break;

                case "Cable" or "CableMedia":
                    if (executeInputCommand)
                        Master.Controller.CableCommand.Execute(null);
                    else
                    {
                        SelectedInputButton = CableMedia;
                        SelectedInputButton.IsSelected = true;
                    }
                    Master.Controller.UpdateMediaInputSelected("CableMedia");

                    if (Master.Controller.GetCableTypeSelected().Equals("NumberPad"))
                    {
                        MediaCenter.Content = new MediaPageNumber(Master, this);
                    }
                    else if (Master.Controller.GetCableTypeSelected().Equals("ChannelList"))
                    {
                        MediaCenter.Content = new MediaPageChannels(Master, this);
                    }
                    MediaLeft.Content = new MediaCableLeft(Master);
                    MediaRightSound.Content = new MediaSound(Master, "TV");
                    break;

                case "Soundbar" or "AudioMedia":
                    if (!executeInputCommand)
                    {
                        SelectedInputButton = AudioMedia;
                        SelectedInputButton.IsSelected = true;
                    }
                    Master.Controller.UpdateMediaInputSelected("AudioMedia");
                    MediaCenter.Content = null;
                    MediaLeft.Content = new MediaCableLeft(Master);
                    MediaCenter.Content = new MediaPageAudio(Master, this);
                    MediaRightSound.Content = new MediaSound(Master, "SBAR");
                    break;
            }
        }

        /// <summary>
        /// This is used by MediaPageChannels and MediaPageMaster to switch the center cable content.
        /// </summary>
        /// <param name="newMode"></param>
        public void ChangeCableCenter(string newMode)
        {
            if (newMode.Equals("ChannelList"))
            {
                MediaCenter.Content = new MediaPageChannels(Master, this);
            }
            else if (newMode.Equals("NumberPad"))
            {
                MediaCenter.Content = new MediaPageNumber(Master, this);
            }
        }

        /// <summary>
        /// This function is passed as a command to the input buttons for changing which input is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="parameter"></param>
        void InputButtonItem_ItemClicked(System.Object sender, System.Object parameter)
        {
            if (!(sender is MediaInputButton sendingMediaInputButton) || sendingMediaInputButton == SelectedInputButton)
            {
                return;
            }

            //Change the selected status.
            //Can change this to use bindings in the future instead of directly modifying.
            if (!(SelectedInputButton is null))
            {
                // Deselect current mediaInputButton
                SelectedInputButton.IsSelected = false;

                // Set new mediaInputButton and select
                SelectedInputButton = sendingMediaInputButton;
                SelectedInputButton.IsSelected = true;

                //Change the view.
                ChangeMediaContent(sendingMediaInputButton.IconLabelText, true);
            }
        }
    }
}