using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smartroom.Views
{
    public partial class MediaPageAudio : ContentView
    {
        private MasterPage master;
        private MediaPageMaster mediaPageMaster;

        public MediaPageAudio(MasterPage master, MediaPageMaster mediaPageMaster)
        {
            InitializeComponent();
            this.master = master;
            this.mediaPageMaster = mediaPageMaster;
        }

        void BTSourceTapped(System.Object sender, System.EventArgs e)
        {
            BTSourceFrame.BackgroundColor = Color.LightGray;
            Task updateBTPairFrameBackgroundColor = updateFrameBackground(BTSourceFrame);
        }

        void BTPairingTapped(System.Object sender, System.EventArgs e)
        {
            BTPairFrame.BackgroundColor = Color.LightGray;
            Task updateBTPairFrameBackgroundColor = updateFrameBackground(BTPairFrame);
        }

        static async Task updateFrameBackground(Frame frame)
        {
            Task delay = Task.Delay(500);
            await delay;
            frame.BackgroundColor = Color.White;
        }
    }
}