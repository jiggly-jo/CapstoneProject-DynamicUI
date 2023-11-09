using Smartroom.Models;
using Xamarin.Forms;

namespace Smartroom.Helpers
{
    /// <summary>
    /// Handy Class contaning important settings
    /// </summary>
    public class Constants
    {
        public static readonly string Localhost = "localhost";
        public static readonly string DevServer = "dev.nrh-smartroom.med.utah.edu";                                //dev server address
        public static readonly string ProductionServer = "prod.nrh-smartroom.med.utah.edu";                         //dev server address
        public static readonly int Port = 6003;                                                    //Socket port
        public static readonly string BaseAddress = "https://" + ProductionServer + ":" + Port;    //full connection address

        public static readonly int minFloor = 0;                                //range of setttings available for patient

        public static readonly int maxFloor = 6;
        public static readonly int minBank = 3;
        public static readonly int maxBank = 6;
        public static readonly int minTemp = 67;
        public static readonly int maxTemp = 75;

        public static string serverUsing = DevServer;                    //CHANGE THIS!!!  middleware server version the app is connecting to

        public static string logName = "Logs.txt";                              //log file


        public static Color UofULightGray = Color.FromHex("#f8f8f8");           //UofU Brand Colors
        public static Color UofUBlack = Color.FromHex("#414042");
        public static Color UofURed = Color.FromHex("#AC162C");

        public static Color UofUGreen = Color.FromHex("#7eddd3");

        public static string[] TutorialScript = { "Hi! This is the Craig H. Neilsen Rehabilitation Hospital Smart Room tutorial.\n\nLet's learn how it works!",      //Tutorial Text
            "This is the Sidebar\n\nThese buttons enable you to switch between the Different control systems. You can access them at anytime.",
            "Select this button to show the Media Page, or say \""+RoomState.GetHotword()+", select Media.\"",
            "Select this button to show the Door Control Page, or say \""+RoomState.GetHotword()+", select Door.\"",
            "Select this button to show the Lights Page, or say \""+RoomState.GetHotword()+", select Lights.\"",
            "Select this button to show the Blinds Page, or say \""+RoomState.GetHotword()+", select Blinds.\"",
            "Select this button to show the Temeperature Page, or say \""+RoomState.GetHotword()+", select Temperature.\"",
            "Select this button to show the Options Page, or say \""+RoomState.GetHotword()+", select Options.\"",
            "Now let's look at the different Control Pages",
            "This is the Media Page\n\nThese buttons control the TV and Sound Systems.",
            "Select this button to toggle the TV on and off, or say \""+RoomState.GetHotword()+", TV power.\"",
            "Select this button to increase the channel, or say \""+RoomState.GetHotword()+", Channel Up.\"",
            "Select this button to decrease the channel, or say \""+RoomState.GetHotword()+", Channel Down.\"",
            "These buttons control the channels\n\nSelect these buttons to directly change the channel.\n\nYou can also say \""+RoomState.GetHotword()+", Channel Number.\"",
            "Select this button to show the channel listings.",
            "Select this button to return to the channel buttons",
            "Select this button to Mute/Unute the Volume, or say \""+RoomState.GetHotword()+", Mute Volume.\"",
            "Select this button to raise the Volume, or say \""+RoomState.GetHotword()+", Volume Up.\"",
            "Select this button to lower the Volume, or say \""+RoomState.GetHotword()+", Volume Down.\"",
            "These buttons control the inputs to the TV.",
            "Select this button to open the Apple TV, or say \""+RoomState.GetHotword()+", Apple TV.\"",
            "Select this button to cast from HDMI, or say \""+RoomState.GetHotword()+", HDMI.\"",
            "Select this button to watch Cable TV, or say \""+RoomState.GetHotword()+", Cable TV.\"",
            "Select this button to listen to music on the soundbar, or say \""+RoomState.GetHotword()+", Soundbar.\"",
            "Select this button to pair your bluetooth audio device, or say \""+RoomState.GetHotword()+", pair bluetooth.\"",


        };
    }
}