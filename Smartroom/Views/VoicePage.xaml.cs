using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smartroom.Views
{
    public partial class VoicePage : ContentView
    {
        MasterPage super;
        bool initialize = false;
        public VoicePage(MasterPage context)
        {
            InitializeComponent();
            super = context;
        }
    }
}