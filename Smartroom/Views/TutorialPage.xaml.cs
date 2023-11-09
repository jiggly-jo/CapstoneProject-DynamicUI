using System;
using System.Collections.Generic;
using Xamarin.Forms;
using static Smartroom.Helpers.Constants;

namespace Smartroom.Views
{
    public partial class TutorialPage : ContentView
    {
        MasterPage Master;

        public int tutorialStep = 0;

        public TutorialPage(MasterPage super)
        {
            InitializeComponent();
            Master = super;
            // CableMedia = (MediaPageCable)context.MainContent;
            // LightsSimple = (LightsPageSimple)context.MainContent;
            // Blinds = (BlindsPage)context.MainContent;
            // Options = (OptionsPage)context.MainContent;
        }

        public void NextClicked(object sender, EventArgs args)
        {
                tutorialStep++;
                if (tutorialStep < TutorialScript.Length)
                {
                    TutorialLabel.Text = TutorialScript[tutorialStep];
                }
                switch (tutorialStep)
                {
                    //case 1:
                    //Master.Sidebar.ShadowFrame.IsVisible = false;
                    //Master.Sidebar.ArrowShadowFrame.IsVisible = false;
                    //    break;
                    //case 2:
                    //    NextButton.IsVisible = false;
                    //    SkipButton.IsVisible = true;
                    //Master.Sidebar.ArrowShadowFrame.IsVisible = true;
                    //Master.Sidebar.ShadowFrame.IsVisible = true;
                    //Master.Sidebar.SidebarGrid.RaiseChild(Master.Sidebar.MediaRow);
                    //Master.Sidebar.SidebarGrid.RaiseChild(Master.Sidebar.ArrowShadowFrame);
                    //    break;
                    //case 3:
                    //    NextButton.IsVisible = false;
                    //    SkipButton.IsVisible = true;

                    //if (!Master.Sidebar.DoorRow.IsVisible)
                    //    {
                    //        NextClicked(this, null);
                    //    }
                    //Master.Sidebar.SidebarGrid.LowerChild(Master.Sidebar.MediaRow);
                    //Master.Sidebar.SidebarGrid.RaiseChild(Master.Sidebar.DoorRow);
                    //Master.Sidebar.SidebarGrid.RaiseChild(Master.Sidebar.ArrowShadowFrame);
                    //    break;
                    //case 4:
                    //Master.Sidebar.SidebarGrid.LowerChild(Master.Sidebar.DoorRow);
                    //Master.Sidebar.SidebarGrid.RaiseChild(Master.Sidebar.LightsRow);
                    //Master.Sidebar.SidebarGrid.RaiseChild(Master.Sidebar.ArrowShadowFrame);
                    //    break;
                    //case 5:
                    //Master.Sidebar.SidebarGrid.LowerChild(Master.Sidebar.LightsRow);
                    //Master.Sidebar.SidebarGrid.RaiseChild(Master.Sidebar.BlindsRow);
                    //Master.Sidebar.SidebarGrid.RaiseChild(Master.Sidebar.ArrowShadowFrame);
                    //    break;
                    //case 6:
                    //Master.Sidebar.SidebarGrid.LowerChild(Master.Sidebar.BlindsRow);
                    //Master.Sidebar.SidebarGrid.RaiseChild(Master.Sidebar.TempRow);
                    //    break;
                    //case 7:
                    //Master.Sidebar.SidebarGrid.LowerChild(Master.Sidebar.TempRow);
                    //Master.Sidebar.SidebarGrid.RaiseChild(Master.Sidebar.OptionsRow);
                    //    break;
                    //case 8:
                    //    NextButton.IsVisible = true;
                    //    SkipButton.IsVisible = false;
                    //    break;
                    //case 9:
                    //    NextButton.IsVisible = true;
                    //    SkipButton.IsVisible = false;
                    //Master.Sidebar.ShadowFrame.IsVisible = true;
                    //    TutorialFrame.TranslationY = 200;
                    //Master.Sidebar.MediaClicked(this, EventArgs.Empty);
                    //Master.Sidebar.SidebarGrid.RaiseChild(Master.Sidebar.ShadowFrame);
                    //    CableMedia.ShadowFrame.IsVisible = false;
                    //    break;
                    //case 10:
                    //    NextButton.IsVisible = false;
                    //    SkipButton.IsVisible = true;
                    //    CableMedia.MediaGrid.RaiseChild(CableMedia.ShadowFrame);
                    //    CableMedia.ShadowFrame.IsVisible = true;
                    //Master.Sidebar.ArrowShadowFrame.IsVisible = true;
                    //    TutorialFrame.TranslationY = 0;
                    //    TutorialFrame.TranslationX = 100;
                    //    CableMedia.ShadowFrame.IsVisible = true;
                    //    CableMedia.MediaGrid.RaiseChild(CableMedia.ControlFrame1);
                    //    CableMedia.ChannelShadowFrame.IsVisible = true;
                    //    CableMedia.MediaOnOffButton.BackgroundColor = Color.FromHex("#f8f8f8");
                    //    CableMedia.PowerGrid.RaiseChild(CableMedia.MediaOnOffButton);
                    //    break;
                    //case 11:
                    //    CableMedia.MediaOnOffButton.BackgroundColor = Color.Transparent;
                    //    CableMedia.PowerGrid.LowerChild(CableMedia.MediaOnOffButton);
                    //    CableMedia.PowerGrid.RaiseChild(CableMedia.ChannelUpButton);
                    //    break;
                    //case 12:
                    //    CableMedia.PowerGrid.LowerChild(CableMedia.ChannelUpButton);
                    //    CableMedia.PowerGrid.RaiseChild(CableMedia.ChannelDownButton);
                    //    break;
                    //case 13:
                    //    TutorialFrame.TranslationY = 200;
                    //    TutorialFrame.TranslationX = 100;
                    //    CableMedia.PowerGrid.LowerChild(CableMedia.ChannelDownButton);
                    //    CableMedia.ChannelShadowFrame.IsVisible = false;
                    //    CableMedia.MediaGrid.LowerChild(CableMedia.ControlFrame1);
                    //    CableMedia.MediaGrid.RaiseChild(CableMedia.ControlFrame2);
                    //    break;
                    //case 14:
                    //    TutorialFrame.TranslationY = -150;
                    //    TutorialFrame.TranslationX = 0;
                    //    CableMedia.NumberShadowFrame.IsVisible = true;
                    //    CableMedia.ChannelGrid.RaiseChild(CableMedia.ChannelListButton);
                    //    CableMedia.ChannelListButton.BackgroundColor = Color.FromHex("f8f8f8");
                    //    break;
                    //case 15:
                    //    CableMedia.ChannelListButton.BackgroundColor = Color.Transparent;
                    //    CableMedia.MediaGrid.RaiseChild(CableMedia.ShadowFrame);
                    //    CableMedia.MediaGrid.RaiseChild(CableMedia.ControlFrame2);
                    //    CableMedia.ChannelGrid.RaiseChild(CableMedia.NumberShadowFrame);
                    //    CableMedia.ChannelGrid.RaiseChild(CableMedia.NumpadButton);
                    //    CableMedia.NumpadButton.BackgroundColor = Color.FromHex("f8f8f8");
                    //    break;
                    //case 16:
                    //    TutorialFrame.TranslationY = 0;
                    //    TutorialFrame.TranslationX = -100;
                    //    CableMedia.NumpadButton.BackgroundColor = Color.Transparent;
                    //    CableMedia.ChannelGrid.RaiseChild(CableMedia.NumberShadowFrame);
                    //    CableMedia.MediaGrid.LowerChild(CableMedia.ControlFrame2);
                    //    CableMedia.NumberShadowFrame.IsVisible = false;
                    //    CableMedia.MediaGrid.RaiseChild(CableMedia.ControlFrame3);
                    //    CableMedia.VolumeShadowFrame.IsVisible = true;
                    //    CableMedia.VolumeGrid.RaiseChild(CableMedia.MuteButton);
                    //    CableMedia.MuteButton.BackgroundColor = Color.FromHex("f8f8f8");
                    //    break;
                    //case 17:
                    //    CableMedia.MuteButton.BackgroundColor = Color.Transparent;
                    //    CableMedia.VolumeGrid.LowerChild(CableMedia.MuteButton);
                    //    CableMedia.VolumeGrid.RaiseChild(CableMedia.VolumeUpButton);
                    //    break;
                    //case 18:
                    //    CableMedia.VolumeGrid.LowerChild(CableMedia.VolumeUpButton);
                    //    CableMedia.VolumeGrid.RaiseChild(CableMedia.VolumeDownButton);
                    //    break;
                    //case 19:
                    //Master.Tutor.NextButton.IsVisible = true;
                    //    SkipButton.IsVisible = false;
                    //    CableMedia.VolumeGrid.LowerChild(CableMedia.VolumeDownButton);
                    //    CableMedia.MediaGrid.LowerChild(CableMedia.ControlFrame3);
                    //    CableMedia.VolumeShadowFrame.IsVisible = false;
                    //    //   CableMedia.MediaGrid.RaiseChild(CableMedia.SelectionGrid);
                    //    CableMedia.BluetoothShadowFrame.IsVisible = true;
                    //    break;
                    //case 20:
                    //    //  CableMedia.SelectionGrid.RaiseChild(CableMedia.SourceFrame);
                    //    CableMedia.SelectionShadowFrame.IsVisible = true;
                    //    CableMedia.SourceGrid.RaiseChild(CableMedia.AppleButton);
                    //    CableMedia.SourceGrid.RaiseChild(CableMedia.AppleLabel);
                    //    break;
                    //case 21:
                    //    CableMedia.SourceGrid.LowerChild(CableMedia.AppleButton);
                    //    CableMedia.SourceGrid.RaiseChild(CableMedia.HDMIButton);
                    //    break;
                    //case 22:
                    //    CableMedia.SourceGrid.LowerChild(CableMedia.HDMIButton);
                    //    CableMedia.SourceGrid.RaiseChild(CableMedia.CableButton);
                    //    break;
                    //case 23:
                    //    CableMedia.SourceGrid.LowerChild(CableMedia.CableButton);
                    //    CableMedia.SourceGrid.RaiseChild(CableMedia.SoundButton);
                    //    break;
                    //case 24:
                    //    CableMedia.SourceGrid.LowerChild(CableMedia.SoundButton);
                    //    CableMedia.BluetoothShadowFrame.IsVisible = false;
                    //    break;

                    //default:
                    //    QuitClicked(this, null);
                    //    break;
            }
        }

        void QuitClicked(object sender, EventArgs args)
        {
            //TODO
            //determine which page we are on and reset those elements only

            //context.MainSidebar.ShadowFrame.IsVisible = false;
            //context.MainSidebar.ArrowShadowFrame.IsVisible = false;
            //CableMedia.ShadowFrame.IsVisible = false;
            //LightsSimple.ShadowFrame.IsVisible = false;
            //Blinds.ShadowFrame.IsVisible = false;

            //Options.ShadowFrame.IsVisible = false;
            //CableMedia.MediaOnOffButton.BackgroundColor = Color.Transparent;
            //CableMedia.MuteButton.BackgroundColor = Color.Transparent;
            //CableMedia.NumpadButton.BackgroundColor = Color.Transparent;
            //CableMedia.ChannelListButton.BackgroundColor = Color.Transparent;
            //CableMedia.ChannelShadowFrame.IsVisible = false;
            //CableMedia.NumberShadowFrame.IsVisible = false;
            //CableMedia.VolumeShadowFrame.IsVisible = false;
            //CableMedia.SelectionShadowFrame.IsVisible = false;
            //CableMedia.BluetoothShadowFrame.IsVisible = false;

            //CableMedia.MediaGrid.RaiseChild(CableMedia.ShadowFrame);
            //CableMedia.PowerGrid.RaiseChild(CableMedia.ChannelShadowFrame);
            //CableMedia.ChannelGrid.RaiseChild(CableMedia.NumberShadowFrame);
            //CableMedia.VolumeGrid.RaiseChild(CableMedia.VolumeShadowFrame);
            //CableMedia.SourceGrid.RaiseChild(CableMedia.SelectionShadowFrame);
            ////   CableMedia.SelectionGrid.RaiseChild(CableMedia.BluetoothShadowFrame);
            Master.EndTutorial();
        }
    }
}