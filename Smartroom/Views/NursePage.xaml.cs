using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartroom.Views
{
    public partial class NursePage : ContentView
    {
        MasterPage master;
        
        public NursePage(MasterPage super)
        {
            InitializeComponent();
            master = super;
        }

        void ButtonPressed(object sender, EventArgs args)
        {
            Button sent = (Button)sender;
            sent.BackgroundColor = Color.DarkSlateGray;
        }
        void ButtonReleased(object sender, EventArgs args)
        {
            Button sent = (Button)sender;
            sent.BackgroundColor = Color.Transparent;
        }

        void WaterRequestButton_Clicked(System.Object sender, System.EventArgs e)
        {
            master.Controller.NurseRequestCommand.Execute("WATER");
        }

        void MedsRequestButton_Clicked(System.Object sender, System.EventArgs e)
        {
            master.Controller.NurseRequestCommand.Execute("MEDS");
        }

        void BathroomRequestButton_Clicked(System.Object sender, System.EventArgs e)
        {
            master.Controller.NurseRequestCommand.Execute("BATHROOM");
        }

        void CatheterRequestButton_Clicked(System.Object sender, System.EventArgs e)
        {
            master.Controller.NurseRequestCommand.Execute("CATHETER");
        }
    }
}