using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Smartroom.Views
{
    public partial class MediaPageHDMI : ContentView
    {
        private MasterPage master;
        private MediaPageMaster mediaPageMaster;

        public MediaPageHDMI(MasterPage master, MediaPageMaster mediaPageMaster)
        {
            InitializeComponent();
            this.master = master;
            this.mediaPageMaster = mediaPageMaster;
        }
    }
}