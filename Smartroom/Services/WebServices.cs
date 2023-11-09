using System;
using System.Threading;
using System.Threading.Tasks;
using Smartroom.Models;
using Smartroom.Helpers;
using Smartroom.ViewModels;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.IO;
using System.Linq;

[assembly: Dependency(typeof(Smartroom.Services.WebDataService))]
namespace Smartroom.Services
{

    public class WebDataService : IDataService
    {
        private static TabletController controller;
        static Socket s;
        private static IPEndPoint endpoint;
        readonly static string logFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Constants.logName);
        static Boolean sent = false;
        public void InsertClick(String command)
        {
            using (var db = new UHospitalContext())
            {
                var item = db.roomButton.FirstOrDefault(i => i.APIName == command);
                if (item == null)
                    return;
                Click c = new Click();
                c.ButtonID = item.id;
                c.RoomNumber = RoomState.GetRoomNumber();
                c.TimePressed = DateTime.Now;
                db.Add(c);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Makes a dialgoflow call
        /// </summary>
        /// <param name="query">query string</param>
        /// <returns>response string</returns>
        public Task<string> QueryDialogFlowAsync(string query)
        {

            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)2, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), query);
            Byte[] bytesSent = package.Package();

            //log request
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + query + "\" to dialogflow");
            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.FromResult(" ");
        }

        /// <summary>
        /// Sends an interactive elevator statement
        /// </summary>
        /// <param name="statement">statement</param>
        /// <returns></returns>
        public Task<string> ElevDialogFlowAsync(string statement)
        {
            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)3, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), statement);
            Byte[] bytesSent = package.Package();

            //log request
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + statement + "\" as a dialog based elevator request");
            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.FromResult(" ");
        }

        #region media control, TV & soundbar
        /// <summary>
        /// Send TV Control requests to the middelware 
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// ON: Turn TV on
        /// OFF: Turn TV off
        /// Up: Change Channel Up
        /// Down: Change Channel Down</param>
        public Task TVControls(Actions act)
        {
            string command = "";

            //Select Command Type
            switch (act)
            {
                case Actions.On:
                    command = "TV_POWER";
                    InsertClick(command);//tv power on/off
                    break;
                case Actions.Up:
                    command = "TV_CH_UP";
                    InsertClick(command);//tv channel up
                    break;
                case Actions.Down:
                    command = "TV_CH_DN";
                    InsertClick(command);//tv channel down
                    break; 

            }

            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();

            //log request
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }

            //send socket package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Send volume control requests to middleware
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// Up:  Change volume up
        /// Down: Change volume down
        /// Off:  Mute/Unmute Volume</param>
        public Task Volume(Actions act, string device)
        {
            StringBuilder command = new StringBuilder();

            //Select Command Type
            switch (device)
            {
                case "TV":                
                    command.Append("SBAR_");
                    break;
                case "SBAR":
                    command.Append("SBARD_"); //TODO not in db
                    break;
            }

            //Select Command Type
            switch (act)
            {
                case Actions.Up:                                                //Soundbar volume up                     
                    command.Append("VOL_UP");
                    InsertClick(command.ToString());
                    break;
                case Actions.Down:                                              //Soundbar volume down
                    command.Append("VOL_DN");
                    InsertClick(command.ToString());
                    break;
                case Actions.Off:                                               //Mute/Unmute  Soundbar
                    if (device.CompareTo("SBAR") == 0)
                    {
                        command.Append("VOL_MUTE"); //TODO not in db
                        InsertClick(command.ToString());
                    }
                    else
                    {
                        command.Append("VOL_MUTE_BP");
                        InsertClick(command.ToString());

                    }
                    break;
            }

            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command.ToString());
            Byte[] bytesSent = package.Package();

            //log request
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Send TV Channel change requests to the middelware 
        /// </summary>
        /// <param name="newChannel")(int) channel to change to
        ///</param>
        public Task TVChannel(int newChannel)
        {
            string command;

            command = "TV_CH" + newChannel.ToString();

            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();

            //log request
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }

            //send socket package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            Thread.Sleep(50);
            return Task.CompletedTask;
        }

        public Task ControlApple(int type)
        {
            string command = "";

            //Select Command Type
            switch (type)
            {
                case 0:
                    command = "ATV_SELECT";
                    InsertClick(command);
                    break;
                case 1:
                    command = "ATV_UP";
                    InsertClick(command);
                    break;
                case 2:
                    command = "ATV_DOWN";
                    InsertClick(command);
                    break;
                case 3:
                    command = "ATV_LEFT";
                    InsertClick(command);
                    break;
                case 4:
                    command = "ATV_RIGHT";
                    InsertClick(command);
                    break;
                case 5:
                    command = "ATV_PLAY";
                    InsertClick(command);
                    break;
                case 6:
                    command = "ATV_MENU";
                    InsertClick(command);
                    break;

            }

            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();

            //log request
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Sends TVInput change request to middleware
        /// </summary>
        /// <param name="preset">which input to use,
        /// 1: Chromecast
        /// 2: Cable
        /// 3: Aux</param>
        public Task TVInput(int preset)
        {
            string command = "";

            //Select Command Type
            switch (preset)
            {
                case 1:
                    command = "TV_SOURCE_CAST";
                    InsertClick(command);//Change TV Input Source to Chromecast
                    break;
                case 2:
                    command = "TV_SOURCE_CABLE";
                    InsertClick(command);//Change TV Input Source to Cable box
                    break;
                case 3:
                    command = "TV_SOURCE_HDMI";
                    InsertClick(command);//Change TV Input Source to AUX Port
                    break;
            }

            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();

            //log request
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Sends soundbar change request to middleware
        /// </summary>
        /// <param name="preset">which input to use,
        /// 1: TV
        /// 2: Bluetooth
        /// </param>
        public Task SoundbarInput(int preset)
        {
            string command = "";

            //Select Command Type
            switch (preset)
            {
                case 1:                                                         //Change TV Input Source to Chromecast
                    command = "SBAR_VOL_TV";
                    InsertClick(command);
                    break;
                case 2:
                    command = "SBAR_VOL_BLUETOOTH";
                    InsertClick(command); //Change TV Input Source to Cable box
                    break;
            }

            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();

            //log request
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        public Task PairBluetooth()
        {
            string command = "SBAR_PAIR";
            InsertClick(command);

            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();

            //log request
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        
        public Task SoundBarPowerCommand(string powerType)
        {
            string command = $"SBAR_VOL_{powerType}";
            InsertClick(command);

            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();

            //log request
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        #endregion media control, TV & soundbar

        /// <summary>
        /// Controls the infodisplay 
        /// </summary>
        /// <param name="mode">
        /// 0:Display Off
        /// 1:Display Dim
        /// 2:Display On
        /// </param>
        /// <returns></returns>
        public Task DisplayControl(int mode)
        {
            string command = "";

            //Select Command Type
            switch (mode)
            {
                case 0:
                    command = "WB_POWER";
                    InsertClick(command);//Info Board Power On/Off
                    break;
                case 1:
                    command = "WB_DIM";
                    InsertClick(command);//Dim Info Board
                    break;
                case 2:
                    command = "WB_BRIGHT";
                    InsertClick(command);//Brighten Info Board
                    break;
            }

            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();

            //log request
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Controls the clock brightness 
        /// </summary>
        /// <param name="mode">
        /// 0:Clock Off
        /// 1:Clock Low
        /// 2:Clock Medium
        /// 3:Clock High
        /// </param>
        /// <returns></returns>
        public Task ClockControl(int mode)
        {
            string command = "";

            //Select Command Type
            switch (mode)
            {
                case 0:
                    command = "CLOCKBRIGHTNESS%OFF";//TODO not in db
                    InsertClick(command);
                    break;
                case 1:
                    command = "CLOCKBRIGHTNESS%LOW";//TODO not in db
                    InsertClick(command);
                    break;
                case 2:
                    command = "CLOCKBRIGHTNESS%MEDIUM";//TODO not in db
                    InsertClick(command);
                    break;
                case 3:
                    command = "CLOCKBRIGHTNESS%HIGH";//TODO not in db
                    InsertClick(command);
                    break;
            }

            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();

            //log request
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Send Door requests to middleware
        /// </summary>
        /// <param name="act">action to perform</param>
        public Task Door(Actions act)       //todo: test door requests
        {
            string command = "";

            //Select Command Type
            switch (act)
            {
                case Actions.Open:                                              //Open Door
                    command = "DOOR_OPEN";
                    InsertClick(command);
                    break;
                case Actions.Close:                                             //Close Door
                    command = "DOOR_CLOSE";
                    InsertClick(command);
                    break;
                case Actions.Partial:                                           //Open Door Partially
                    command = "DOOR_OPEN_PARTIAL";
                    InsertClick(command);
                    break;
            }

            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();

            //log request
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }


            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        #region lights control

        /// <summary>
        /// Send Control requests for the Entrance lights
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// On: Turn on entrance lights, to previous setting
        /// Off: Turn off entrance lights
        /// Up: Turn entrance lights setting up, to 1 if off
        /// Down: Turn entrance lights setting down, off if 1</param>
        public Task ELights(Actions act)
        {
            string command = "";

            //Select Command Type
            switch (act)
            {
                case Actions.Off:
                    if (RoomState.GetEntranceLightsOn())
                        command = "LTG_OFF_1"; //Entrance Lights Off
                    else
                        return Task.CompletedTask;
                    break;
                case Actions.On:
                    
                    if (!RoomState.GetEntranceLightsOn())
                        command = "LTG_ON_1";
                      //Entrance Lights On
                    else
                        return Task.CompletedTask;
                    break;
                case Actions.Up:
                    if (RoomState.GetEntranceLightsSetting() != 10)             //Turn Up Entrance Light Intensity
                        command = "LTG_DIM_1_RAISE";
                    else
                        return Task.CompletedTask;
                    break;
                case Actions.Down:
                    if (RoomState.GetEntranceLightsSetting() != 0)              //Turn Down Entrance Light Intensity
                        command = "LTG_DIM_1_LOWER";
                    else
                        return Task.CompletedTask;
                    break;
            }
            InsertClick(command);
            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();

            Thread.Sleep(500);
            //log request
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Send Control requests for the Reading lights
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// On: Turn on Reading lights, to previous setting
        /// Off: Turn off Reading lights
        /// Up: Turn Reading lights setting up, to 1 if off
        /// Down: Turn Reading lights setting down, off if 1</param>
        public Task RLights(Actions act)
        {
            string command = "";

            //Select Command Type
            switch (act)
            {
                case Actions.Off:
                    if (RoomState.GetReadingLightsOn())                         //Reading Lights Off
                        command = "LTG_OFF_2";
                    else
                        return Task.CompletedTask;
                    break;
                case Actions.On:
                    if (!RoomState.GetReadingLightsOn())
                        command = "LTG_ON_2";                                   //Reading Lights On
                    else
                        return Task.CompletedTask;
                    break;
                case Actions.Up:
                    if (RoomState.GetReadingLightsSetting() != 10)
                        command = "LTG_DIM_2_RAISE";                            //Turn Up Reading Light Intensity
                    else
                        return Task.CompletedTask;
                    break;
                case Actions.Down:
                    if (RoomState.GetReadingLightsSetting() != 0)
                        command = "LTG_DIM_2_LOWER";                            //Turn Down Reading Light Intensity
                    else
                        return Task.CompletedTask;
                    break;
            }
            InsertClick(command);

            //create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();
            Thread.Sleep(500);
            //log request
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }


            return Task.CompletedTask;
        }

        /// <summary>
        /// Send Control requests for the Ambient lights
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// On: Turn on Ambeint lights, to previous setting
        /// Off: Turn off Ambient lights
        /// Up: Turn Ambient lights setting up, to 1 if off
        /// Down: Turn Ambient lights setting down, off if 1</param>
        public Task ALights(Actions act)
        {
            string command = "";

            //Select Command Type
            switch (act)
            {
                case Actions.Off:
                    if (RoomState.GetAmbientLightsOn())
                        command = "LTG_OFF_0";                                  //Ambient Lights Off
                    else
                        return Task.CompletedTask;
                    break;
                case Actions.On:
                    if (!RoomState.GetAmbientLightsOn())
                        command = "LTG_ON_0";                                   //Ambient Lights On
                    else
                        return Task.CompletedTask;
                    break;
                case Actions.Up:
                    if (RoomState.GetAmbientLightsSetting() != 10)
                        command = "LTG_DIM_0_RAISE";                            //Turn up Ambient Light Intensity
                    else
                        return Task.CompletedTask;
                    break;
                case Actions.Down:
                    if (RoomState.GetAmbientLightsSetting() != 0)
                        command = "LTG_DIM_0_LOWER";                            //Turn Down Ambient Light Intensity
                    else
                        return Task.CompletedTask;
                    break;
            }

            InsertClick(command);
            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();

            Thread.Sleep(500);
            //write request to log
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");

            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Send Control requests for the Examlights
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// On: Turn on Exam lights, to previous setting
        /// Off: Turn off Exam lights
        /// Up: Turn Exam lights setting up, to 1 if off
        /// Down: Turn Exam lights setting down, off if 1</param>
        public Task ExLights(Actions act)
        {
            string command = "";

            //Select Command Type
            switch (act)
            {
                case Actions.Off:
                    if (RoomState.GetExamLightsOn())
                        command = "LTG_OFF_4";                                  //Exam Lights Off
                    else
                        return Task.CompletedTask;
                    break;
                case Actions.On:
                    if (!RoomState.GetExamLightsOn())
                        command = "LTG_ON_4";                                   //Exam Lights On
                    else
                        return Task.CompletedTask;
                    break;
                case Actions.Up:
                    if (RoomState.GetExamLightsSetting() != 10)                 //Turn Up Exam Light Intensity
                        command = "LTG_DIM_4_RAISE";
                    else
                        return Task.CompletedTask;
                    break;
                case Actions.Down:
                    if (RoomState.GetExamLightsSetting() != 0)                  //Turn Down Exam Light Intensity
                        command = "LTG_DIM_4_LOWER";
                    else
                        return Task.CompletedTask;
                    break;
            }
            InsertClick(command);
            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();
            Thread.Sleep(500);
            //write to log file
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Send Control requests for the Bathroom lights
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// On: Turn on Bathroom lights, to previous setting
        /// Off: Turn off Bathroom lights
        /// Up: Turn Bathroom lights setting up, to 1 if off
        /// Down: Turn Bathroom lights setting down, off if 1</param>
        public Task BathLights(Actions act)
        {
            string command = "";

            //Select Command Type
            switch (act)
            {
                case Actions.Off:
                    if (RoomState.GetBathroomLightsOn())
                        command = "LTG_OFF_3";                                  //Bathroom Lights Off
                    else
                        return Task.CompletedTask;
                    break;
                case Actions.On:
                    if (!RoomState.GetBathroomLightsOn())
                        command = "LTG_ON_3";                                   //Bathroom Lights On
                    else
                        return Task.CompletedTask;
                    break;
                case Actions.Up:
                    if (RoomState.GetBathroomLightsSetting() != 10)
                        command = "LTG_DIM_3_RAISE";                            //Turn Up Bathroom Light Intensity
                    else
                        return Task.CompletedTask;
                    break;
                case Actions.Down:
                    if (RoomState.GetBathroomLightsSetting() != 0)              //Turn Down Bathroom Light Intensity
                        command = "LTG_DIM_3_LOWER";
                    else
                        return Task.CompletedTask;
                    break;
            }
            InsertClick(command);
            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();
            Thread.Sleep(500);
            //write to log file
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Send lighting preset requests to the middleware
        /// </summary>
        /// <param name="preset">Corisponding preset setting
        /// 0: All Off
        /// 1: All on
        /// 2: Reading Preset
        /// 3: Soft Preset</param>
        public Task LightsPreset(int preset)
        {
            string command = "";

            //Select Command Type
            switch (preset)
            {
                case 0:
                    command = "LTG_DARK";                                       //All Lights Off
                    break;
                case 1:
                    command = "LTG_FULL";                                       //All Lights On
                    break;
                case 2:
                    command = "LTG_READ";                                       //Reading Preset
                    break;
                case 3:
                    command = "LTG_SOFT";                                       //Soft Preset
                    break;
                case 4:
                    command = "LTG_TV";                                         //TV Preset
                    break;
            }
            InsertClick(command);
            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();
            Thread.Sleep(500);
            //write to log file
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        #endregion lights control

        /// <summary>
        /// Send Blackout Blind requests to middleware
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// Open:  Open Blackout Blinds all the way
        /// Up: Raise Blackout blinds a bit
        /// Down: Lower Blackout blinds a bit
        /// Close: Close BLackout Blinds all the way</param>
        public Task BBlinds(Actions act)
        {
            string command = "";

            //Select Command Type
            switch (act)
            {
                case Actions.Open:
                    command = "BLINDS_BLKOUT_OPEN";                         //Open Blackout Blinds all the way
                    break;
                case Actions.Up:
                    command = "BLINDS_BLKOUT_UP_1";                         //Raise Blackout Blinds up a bit
                    break;
                case Actions.Down:
                    command = "BLINDS_BLKOUT_DOWN_1";                       //Lower Blackout Blinds down a bit
                    break;
                case Actions.Close:
                    command = "BLINDS_BLKOUT_CLOSE";                        //Close Blackout Blinds all the way
                    break;
            }
            InsertClick(command);
            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();

            //write to log
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }

            // Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Send Interior Blind requests to middleware
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// Open:  Open Interior Blinds all the way
        /// Up: Raise Interior blinds a bit
        /// Down: Lower Interior blinds a bit
        /// Close: Close Interior Blinds all the way</param>
        public Task IBlinds(Actions act)
        {
            string command = "";

            //Select Command Type                           
            switch (act)
            {
                case Actions.Open:
                    command = "BLINDS_OPAQUE_OPEN";                             //Open Interior Blinds all the way
                    break;
                case Actions.Up:
                    command = "BLINDS_OPAQUE_UP_1";                             //Raise Interior Blinds a bit
                    break;
                case Actions.Down:
                    command = "BLINDS_OPAQUE_DOWN_1";                           //Lower Interior Blinds a bit
                    break;
                case Actions.Close:
                    command = "BLINDS_OPAQUE_CLOSE";                            //Close Interoir Blinds all the way
                    break;
            }
            InsertClick(command);
            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();


            //write to log
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Send Temperature control requests to middleware; 
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// Up: Turn Temperature Up
        /// Down: Turn Temperature Down</param>
        public Task Temperature(Actions act)
        {
            string command = "";

            //Select Command Type
            switch (act)
            {
                case Actions.Up:
                    if (RoomState.GetSettingTemp() < Constants.maxTemp)         //Turn Temperature Up
                        command = "HVAC_TEMP_UP";
                    else
                        return Task.CompletedTask;
                    break;
                case Actions.Down:
                    if (RoomState.GetSettingTemp() > Constants.minTemp)         //Turn Temperature Down
                        command = "HVAC_TEMP_DN";
                    else
                        return Task.CompletedTask;
                    break;
            }
            InsertClick(command);
            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();

            //write to log
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Send nurse call request to middleware
        /// </summary>
        public Task NurseRequest(string requestType)
        {
            string command = $"CALLNURSE%{requestType}";
            InsertClick(command);//TODO not in db
            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();
            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Send nurse call request to middleware
        /// </summary>
        public Task CallNurse(string requestType)
        {

            string command = $"CALLNURSE%{requestType}";
            InsertClick(command);//TODO not in db
            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), command);
            Byte[] bytesSent = package.Package();

            //write to log
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + command + "\" request");
            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Sends elevator schedule request to middleware
        /// </summary>
        /// <param name="type">(int) type of elevator scheduling
        /// 1: scheduled elevator call
        /// 2: call elevaotr now</param>
        /// <returns></returns>
        public Task Elevator(int type)
        {
            string calltype = "ELEVATORCALL%" + RoomState.GetRoomNumber() + "%" + RoomState.GetElevOriginFloor() +
                "%" + RoomState.GetElevDestFloor() + "%" + RoomState.GetElevBank();
            InsertClick(calltype);//TODO not in db
            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), (ushort)RoomState.GetRoomPin(), calltype);
            Byte[] bytesSent = package.Package();

            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent \"" + calltype + "\" request");
            }
            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// request server to provide current status on room devices
        /// </summary>
        public Task GetStatus()
        {
            //Create Socket Package
            SocketPackage package = new SocketPackage((Byte)1, (ushort)RoomState.GetRoomNumber(), RoomState.GetRoomPin(), "GET_STATUS");
            Byte[] bytesSent = package.Package();

            //write to log file
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine(now.ToString() + ": Sent a Room Status request");
            }

            //Send Socket Package
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            return Task.CompletedTask;
        }

        #region Socket Connector
        /// <summary>
        /// Creates connection to the Viewmodel for View Changes
        /// Creates the WebSocket Connection to the Middleware       
        /// </summary>
        /// <param name="context">Viewmodel that is connecting to the Socket</param>
        public void SocketConnect(TabletController context)
        {
            controller = context;                                               //set controller to associated viewmodel
            //Attemps to connect to Middleware
            try                                                                 //If successful, Latches this Socket on to the WebService 
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(Constants.serverUsing);
                endpoint = new IPEndPoint(hostEntry.AddressList[0], Constants.Port);
                Socket tempSocket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                tempSocket.Connect(endpoint);
                s = tempSocket;

                //write to log
                using (StreamWriter sw = File.CreateText(logFileName))
                {
                    DateTime now = DateTime.Now;
                    sw.WriteLine(now.ToString() + ": Connection to Middleware Successsful");
                }
            }
            catch (Exception e)                                                 //Error: could not connect to middleware
            {
                Console.WriteLine(e.ToString());

                //write to log file
                using (StreamWriter sw = File.CreateText(logFileName))
                {
                    DateTime now = DateTime.Now;
                    sw.WriteLine(now.ToString() + ":ERROR::Could not connect to Middleware server at " + Constants.serverUsing + " " + e.ToString());
                }

                controller.RoomSpan = 2;
                controller.RoomNum = "Error: Could not Connect to Server";
                controller.RoomPin = "";
                controller.RoomColor = Constants.UofURed;
            }

            Task BackgroudTask = Task.Run(BackgroundListener);                  //Start socket listener
        }
        #endregion Socket Connector

        #region Socket Listener
        /// <summary>
        /// Listens for Asyncronous responses from the middleware
        /// These are confirmations that the request were recieved and that the environment has changed
        /// Then they update the views accordingly
        /// </summary>
        private static void BackgroundListener()
        {
            //Keep Listening
            while (true)
            {
                Byte[] bytesReceived = new Byte[2048];
                s.Receive(bytesReceived);

                //When it receives a response, determine its type and change views accordingly
                string responseString = Encoding.UTF8.GetString(bytesReceived, 0, bytesReceived.Length - 1);
                BackgroundActionTaker(responseString);
            }
        }

       

        private static void BackgroundActionTaker(string responseString)
        {
            //Connection successfull response
            if (responseString.Contains("roomNum%"))
            {
                string[] splitresponse = responseString.Split('%');         //Load room number and BYOD pin

                string roomString = splitresponse[1];
                string pinString = splitresponse[3];

                pinString.Trim('\0');

                RoomState.SetRoomNumber(ushort.Parse(roomString));
                RoomState.SetRoomPin(ushort.Parse(pinString));

                //write to log
                using (StreamWriter sw = File.AppendText(logFileName))
                {
                    DateTime now = DateTime.Now;
                    sw.WriteLine(now.ToString() + ": Paired to room: " + roomString);
                    controller.RoomNum = "Room Num: " + roomString;
                }

                controller.RoomPin = "PIN: " + RoomState.GetRoomPin();
                Thread.Sleep(750);
            }
       
            //TV On response
            if (responseString.Contains("TV_POWER_FB"))
            {
                if (RoomState.GetTVInput().Equals("Cable"))                 //Update TV source UI
                {
                    controller.HDMIOnOff = "Black";
                    controller.CableOnOff = "Red";
                    controller.CastOnOff = "Black";
                }
                else if (RoomState.GetTVInput().Equals("AUX"))
                {
                    controller.HDMIOnOff = "Red";
                    controller.CableOnOff = "Black";
                    controller.CastOnOff = "Black";
                }
                else if (RoomState.GetTVInput().Equals("Caster"))
                {
                    controller.HDMIOnOff = "Black";
                    controller.CableOnOff = "Black";
                    controller.CastOnOff = "Red";
                }


            }

            // TV Channel responses
            if (responseString.Contains("TV_CH_UP_FB"))
            {
            }//TV Channnel Up response 
            else if (responseString.Contains("TV_CH_DN_FB"))
            {
            }//TV Channel Down response
            else if (responseString.Contains("TV_CH"))                      //TV Channel Number control response
            {
                int index = responseString.IndexOf("TV_CH") + 5;
                char digit = responseString[index];

                if (Char.IsDigit(digit))
                {
                }
            }

            //Volume Up response
            if (responseString.Contains("SBAR_VOL_UP_FB"))
            {
                if (RoomState.GetMute())
                {
                    RoomState.SetMute(false);
                    //controller.MuteOnOff = "Black";
                }
                if (RoomState.GetVolume() != 20)
                {
                    RoomState.SetVolume(true);
                    //controller.MuteOnOff = "Red";
                }
            }

            // Volume Down response
            if (responseString.Contains("SBAR_VOL_DN_FB"))
            {
                if (RoomState.GetVolume() != 0 || !RoomState.GetMute())
                {
                    if (RoomState.GetVolume() != 5)
                        RoomState.SetVolume(false);
                    else
                    {
                        RoomState.SetMute(true);
                        controller.MuteOnOff = "Red";
                    }
                }
            }

            // Mute/Unmute volume
            if (responseString.Contains("SBAR_VOL_MUTE_IS_ON_FB"))
            {
                RoomState.SetMute(true);
                controller.MuteOnOff = ImageSource.FromResource("Smartroom.Icons.MediaSoundUnmuteButton.png");
            }

            // Mute/Unmute volume
            if (responseString.Contains("SBAR_VOL_MUTE_IS_OFF_FB"))
            {
                RoomState.SetMute(false);
                controller.MuteOnOff = ImageSource.FromResource("Smartroom.Icons.MediaSoundMuteButton.png");
            }

            // volume setting update
            if (responseString.Contains("SBAR_VOL_0"))
            {
                int index = responseString.IndexOf("SBAR_VOL_0") + 10;
                char[] digits = new char[] { responseString[index], responseString[index + 1] };
                int value = Int16.Parse(new string(digits));

                RoomState.SetVolume(value);

                if (value == 0)
                    RoomState.SetMute(true);
                else
                {
                    RoomState.SetMute(false);
                    controller.MuteOnOff = "Black";
                }
            }

            // TV Source Changed to Caster response
            if (responseString.Contains("TV_SOURCE_CAST_FB"))
            {
                RoomState.SetTVInput("AppleMedia");

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    controller.SelectMedia();
                });
            }

            // TV Source Chagned to AUX response
            if (responseString.Contains("TV_SOURCE_HDMI_FB"))
            {
                RoomState.SetTVInput("HDMIMedia");


                MainThread.BeginInvokeOnMainThread(() =>
                {
                    controller.SelectMedia();
                });
            }

            // TV Source chagned to Cable box response
            if (responseString.Contains("TV_SOURCE_CABLE_FB"))
            {
                RoomState.SetTVInput("CableMedia");

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    controller.SelectMedia();
                });
            }

            // Soundbar source changed to TV
            if (responseString.Contains("SBAR_VOL_TV_FB"))
            {
                controller.TVBarOnOff = "Red";
                controller.BlueOnOff = "Black";
            }

            // Soundbar source chagned to Bluetooth
            if (responseString.Contains("SBAR_VOL_BLUETOOTH_FB"))
            {
                controller.BlueOnOff = "Red";
                controller.TVBarOnOff = "Black";
            }

            // Soundbar pin received
            if (responseString.Contains("SBAR_PIN"))
            {
                string[] splitString = responseString.Split('_');

                string soundbarPin = splitString[3];
                controller.setSoundBarPin(soundbarPin);
            }

            // Open the door
            if (responseString.Contains("DOOR_OPEN_FB"))
            {
            }

            // close the door
            if (responseString.Contains("DOOR_CLOSE_FB"))
            {
            }

            // Open the door partially
            if (responseString.Contains("DOOR_OPEN_PARTIAL_FB"))
            {
            }

            //Ambient lights On
            if (responseString.Contains("LTG_ON_0_FB"))
            {
                RoomState.SetAmbientLightsOn(true);

                controller.AmbientLightOnOff = true;
                int value = RoomState.GetAmbientLightsSetting();
                controller.Ambient = value.ToString();

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";
            }

            //Entrance Lights On
            if (responseString.Contains("LTG_ON_1_FB"))
            {
                RoomState.SetEntranceLightsOn(true);

                controller.EntranceLightOnOff = true;
                int value = RoomState.GetEntranceLightsSetting();
                controller.Entrance = value.ToString();

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";
            }

            //Reading lights ON
            if (responseString.Contains("LTG_ON_2_FB"))
            {
                RoomState.SetReadingLightsOn(true);

                controller.ReadingLightOnOff = true;
                int value = RoomState.GetReadingLightsSetting();
                controller.Reading = value.ToString();

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";
            }

            //Bathroom lights ON
            if (responseString.Contains("LTG_ON_3_FB"))
            {
                RoomState.SetBathroomLightsOn(true);

                controller.BathroomLightOnOff = true;
                int value = RoomState.GetBathroomLightsSetting();
                controller.Bathroom = value.ToString();

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";
            }

            //Exam lights ON
            if (responseString.Contains("LTG_ON_4_FB"))
            {
                RoomState.SetExamLightsOn(true);

                controller.ExamLightOnOff = true;
                int value = RoomState.GetExamLightsSetting();
                controller.Exam = value.ToString();

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";
            }

            //Ambient Light Off
            if (responseString.Contains("LTG_OFF_0_FB"))
            {
                RoomState.SetAmbientLightsOn(false);

                controller.AmbientLightOnOff = false;
                controller.Ambient = "0";

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";
            }

            //Entrance Lights Off
            if (responseString.Contains("LTG_OFF_1_FB"))
            {
                RoomState.SetEntranceLightsOn(false);

                controller.EntranceLightOnOff = false;
                controller.Entrance = "0";

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";
            }

            //Reading Lights Off
            if (responseString.Contains("LTG_OFF_2_FB"))
            {
                RoomState.SetReadingLightsOn(false);

                controller.ReadingLightOnOff = false;
                controller.Reading = "0";

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";
            }

            //Bathroom Lights Off
            if (responseString.Contains("LTG_OFF_3_FB"))
            {
                RoomState.SetBathroomLightsOn(false);

                controller.BathroomLightOnOff = false;
                controller.Bathroom = "0";

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";
            }

            //Exam Lights Off
            if (responseString.Contains("LTG_OFF_4_FB"))
            {
                RoomState.SetExamLightsOn(false);

                controller.ExamLightOnOff = false;
                controller.Exam = "0";

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";
            }

            //Raise Ambient Lights
            if (responseString.Contains("LTG_DIM_0_RAISE_FB"))
            {
                if (RoomState.GetAmbientLightsOn())
                {
                    if (RoomState.GetAmbientLightsSetting() != 10)
                        RoomState.SetAmbientLightsSetting(true);
                }
                else
                {
                    controller.AmbientLightOnOff = true;
                    RoomState.SetAmbientLightsOn(true);
                }

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";

                controller.Ambient = RoomState.GetAmbientLightsSetting().ToString();
            }

            //Lower Ambient Lights
            if (responseString.Contains("LTG_DIM_0_LOWER_FB"))
            {
                if (RoomState.GetAmbientLightsOn())
                {
                    if (RoomState.GetAmbientLightsSetting() != 1)
                    {
                        RoomState.SetAmbientLightsSetting(false);
                        controller.Ambient = RoomState.GetAmbientLightsSetting().ToString();
                    }
                    else
                    {
                        RoomState.SetAmbientLightsOn(false);
                        controller.Ambient = "0";
                    }
                }

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";
            }

            //Change ambient Lights
            if (responseString.Contains("LTG_DIM_0_"))
            {
                //int index = responseString.IndexOf("LTG_DIM_0_") + 10;
                int index = responseString.LastIndexOf("LTG_DIM_0_") + 10;
                char[] digits = new char[] { responseString[index], responseString[index + 1] };

                if (Char.IsDigit(digits[0]))
                {
                    int value;
                    if (digits[1].Equals('_'))
                        value = Int16.Parse(digits[0].ToString());
                    else
                        value = Int16.Parse(new string(digits));

                    RoomState.SetAmbientLightsSetting(value);

                    if (value == (0))
                    {
                        RoomState.SetAmbientLightsOn(false);
                        controller.AmbientLightOnOff = false;
                    }
                    else
                    {
                        RoomState.SetAmbientLightsOn(true);
                        controller.AmbientLightOnOff = true;
                    }

                    controller.Ambient = value.ToString();
                }

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";
            }

            //Raise Entrance Lights
            if (responseString.Contains("LTG_DIM_1_RAISE_FB"))
            {
                if (RoomState.GetEntranceLightsOn())
                {
                    if (RoomState.GetEntranceLightsSetting() != 10)
                        RoomState.SetEntranceLightsSetting(true);
                }
                else
                {
                    RoomState.SetEntranceLightsOn(true);
                    controller.EntranceLightOnOff = true;
                }

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";

                controller.Entrance = RoomState.GetEntranceLightsSetting().ToString();
            }

            //Lower Entrance Lights
            if (responseString.Contains("LTG_DIM_1_LOWER_FB"))
            {
                if (RoomState.GetEntranceLightsOn())
                {
                    if (RoomState.GetEntranceLightsSetting() != 1)
                    {
                        RoomState.SetEntranceLightsSetting(false);
                        controller.Entrance = RoomState.GetEntranceLightsSetting().ToString();
                    }
                    else
                    {
                        RoomState.SetEntranceLightsOn(false);
                        controller.Entrance = "0";
                    }
                }

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";
            }

            //Change Entrance Lights
            if (responseString.Contains("LTG_DIM_1_"))
            {

                int index = responseString.LastIndexOf("LTG_DIM_1_") + 10;

                char[] digits = new char[] { responseString[index], responseString[index + 1] };

                if (Char.IsDigit(digits[0]))
                {
                    int value;
                    if (digits[1].Equals('_'))
                        value = Int16.Parse(digits[0].ToString());
                    else
                        value = Int16.Parse(new string(digits));

                    RoomState.SetEntranceLightsSetting(value);

                    if (value == (0))
                    {
                        RoomState.SetEntranceLightsOn(false);
                        controller.EntranceLightOnOff = false;
                    }
                    else
                    {
                        RoomState.SetEntranceLightsOn(true);
                        controller.EntranceLightOnOff = true;
                    }

                    controller.Entrance = value.ToString();
                }

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";
            }

            //Raise reading lights
            if (responseString.Contains("LTG_DIM_2_RAISE_FB"))
            {
                if (RoomState.GetReadingLightsOn())
                {
                    if (RoomState.GetReadingLightsSetting() != 10)
                        RoomState.SetReadingLightsSetting(true);
                }
                else
                {
                    RoomState.SetReadingLightsOn(true);
                    controller.ReadingLightOnOff = true;
                }

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";

                controller.Reading = RoomState.GetReadingLightsSetting().ToString();
            }

            //Lower Reading Lights  
            if (responseString.Contains("LTG_DIM_2_LOWER_FB"))
            {
                if (RoomState.GetReadingLightsOn())
                {
                    if (RoomState.GetReadingLightsSetting() != 1)
                    {
                        RoomState.SetReadingLightsSetting(false);
                        controller.Reading = RoomState.GetReadingLightsSetting().ToString();
                    }
                    else
                    {
                        RoomState.SetReadingLightsOn(false);
                        controller.Reading = "0";
                    }
                }

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";
            }

            //Change Reading Lights
            if (responseString.Contains("LTG_DIM_2_"))
            {

                int index = responseString.LastIndexOf("LTG_DIM_2_") + 10;

                char[] digits = new char[] { responseString[index], responseString[index + 1] };

                if (Char.IsDigit(digits[0]))
                {
                    int value;
                    if (digits[1].Equals('_'))
                        value = Int16.Parse(digits[0].ToString());
                    else
                        value = Int16.Parse(new string(digits));

                    RoomState.SetReadingLightsSetting(value);

                    if (value == (0))
                    {
                        RoomState.SetReadingLightsOn(false);
                        controller.ReadingLightOnOff = false;
                    }
                    else
                    {
                        RoomState.SetReadingLightsOn(true);
                        controller.ReadingLightOnOff = true;
                    }

                    controller.Reading = value.ToString();
                }

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";
            }

            //Raise Bathroom lights
            if (responseString.Contains("LTG_DIM_3_RAISE_FB"))
            {
                if (RoomState.GetBathroomLightsOn())
                {
                    if (RoomState.GetBathroomLightsSetting() != 10)
                        RoomState.SetBathroomLightsSetting(true);
                }
                else
                {
                    RoomState.SetBathroomLightsOn(true);
                    controller.BathroomLightOnOff = true;
                }

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";

                controller.Reading = RoomState.GetReadingLightsSetting().ToString();
            }

            //Lower Bathroom Lights    
            if (responseString.Contains("LTG_DIM_3_LOWER_FB"))
            {
                if (RoomState.GetBathroomLightsOn())
                {
                    if (RoomState.GetBathroomLightsSetting() != 1)
                    {
                        RoomState.SetBathroomLightsSetting(false);
                        controller.Bathroom = RoomState.GetBathroomLightsSetting().ToString();
                    }
                    else
                    {
                        RoomState.SetBathroomLightsOn(false);
                        controller.Bathroom = "0";
                    }
                }

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";
            }

            //Change Bathroom Lights
            if (responseString.Contains("LTG_DIM_3_"))
            {

                int index = responseString.LastIndexOf("LTG_DIM_3_") + 10;

                char[] digits = new char[] { responseString[index], responseString[index + 1] };

                if (Char.IsDigit(digits[0]))
                {
                    int value;
                    if (digits[1].Equals('_'))
                        value = Int16.Parse(digits[0].ToString());
                    else
                        value = Int16.Parse(new string(digits));

                    RoomState.SetBathroomLightsSetting(value);

                    if (value == (0))
                    {
                        RoomState.SetBathroomLightsOn(false);
                        controller.BathroomLightOnOff = false;
                    }
                    else
                    {
                        RoomState.SetBathroomLightsOn(true);
                        controller.BathroomLightOnOff = true;
                    }

                    controller.Night = "Transparent";
                    controller.TV = "Transparent";
                    controller.Soft = "Transparent";
                    controller.Read = "Transparent";
                    controller.Full = "Transparent";

                    controller.Bathroom = value.ToString();
                }
            }

            //Raise Exam lights
            if (responseString.Contains("LTG_DIM_4_RAISE_FB"))
            {
                if (RoomState.GetExamLightsOn())
                {
                    if (RoomState.GetExamLightsSetting() != 10)
                        RoomState.SetExamLightsSetting(true);
                }
                else
                {
                    RoomState.SetExamLightsOn(true);
                    controller.ExamLightOnOff = true;
                }

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";

                controller.Exam = RoomState.GetExamLightsSetting().ToString();
            }

            //Lower Exam Lights  
            if (responseString.Contains("LTG_DIM_4_LOWER_FB"))
            {
                if (RoomState.GetExamLightsOn())
                {
                    if (RoomState.GetExamLightsSetting() != 1)
                    {
                        RoomState.SetExamLightsSetting(false);
                        controller.Exam = RoomState.GetExamLightsSetting().ToString();
                    }
                    else
                    {
                        RoomState.SetExamLightsOn(false);
                        controller.Exam = "0";
                    }
                }

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";
            }

            //Change Exam Lights
            if (responseString.Contains("LTG_DIM_4_"))
            {

                int index = responseString.LastIndexOf("LTG_DIM_4_") + 10;

                char[] digits = new char[] { responseString[index], responseString[index + 1] };

                if (Char.IsDigit(digits[0]))
                {
                    int value;
                    if (digits[1].Equals('_'))
                        value = Int16.Parse(digits[0].ToString());
                    else
                        value = Int16.Parse(new string(digits));

                    RoomState.SetExamLightsSetting(value);

                    if (value == (0))
                    {
                        RoomState.SetExamLightsOn(false);
                        controller.ExamLightOnOff = false;
                    }
                    else
                    {
                        RoomState.SetExamLightsOn(true);
                        controller.ExamLightOnOff = true;
                    }

                    controller.Exam = value.ToString();
                }

                controller.Night = "Transparent";
                controller.TV = "Transparent";
                controller.Soft = "Transparent";
                controller.Read = "Transparent";
                controller.Full = "Transparent";
            }

            //Open All Blinds 
            if (responseString.Contains("BLINDS_ALL_OPEN_FB"))
            {
                RoomState.SetBBlinds(10);
                RoomState.SetIBlinds(10);

                controller.BBlindsValue = 10;
                controller.IBlindsValue = 10;
            }

            //Close All Blinds
            if (responseString.Contains("BLINDS_ALL_CLOSE_FB"))
            {
                RoomState.SetBBlinds(0);
                RoomState.SetIBlinds(0);

                controller.BBlindsValue = 0;
                controller.IBlindsValue = 0;
            }

            // Open Blackout Blinds
            if (responseString.Contains("BLINDS_BLKOUT_OPEN_FB"))
            {
                RoomState.SetBBlinds(10);

                controller.BBlindsValue = 10;
            }

            // Close Blackout Blinds
            if (responseString.Contains("BLINDS_BLKOUT_CLOSE_FB"))
            {
                RoomState.SetBBlinds(0);

                controller.BBlindsValue = 0;
            }

            // Open Ambient Blinds
            if (responseString.Contains("BLINDS_OPAQUE_OPEN_FB"))
            {
                RoomState.SetIBlinds(10);

                controller.IBlindsValue = 10;
            }

            // Close Ambient Blinds
            if (responseString.Contains("BLINDS_OPAQUE_CLOSE_FB"))
            {
                RoomState.SetIBlinds(0);

                controller.IBlindsValue = 0;
            }

            // Close Ambient Blinds
            if (responseString.Contains("BLINDS_ALL_UP_1_FB"))
            {
                int newBValue = RoomState.GetBBlinds() + 1;
                int newIValue = RoomState.GetIBlinds() + 1;

                if (newBValue <= 10)
                {
                    RoomState.SetBBlinds(newBValue);
                    controller.BBlindsValue = newBValue;
                }
                if (newIValue <= 10)
                {
                    RoomState.SetIBlinds(newIValue);
                    controller.IBlindsValue = newIValue;
                }
            }

            // Close Ambient Blinds
            if (responseString.Contains("BLINDS_ALL_DOWN_1_FB"))
            {
                int newBValue = RoomState.GetBBlinds() - 1;
                int newIValue = RoomState.GetIBlinds() - 1;

                if (newBValue >= 0)
                {
                    RoomState.SetBBlinds(newBValue);
                    controller.BBlindsValue = newBValue;
                }
                if (newIValue >= 0)
                {
                    RoomState.SetIBlinds(newIValue);
                    controller.IBlindsValue = newIValue;
                }
            }


            if (responseString.Contains("BLINDS_BLKOUT_UP_1_FB"))           // Close Ambient Blinds
            {
                int newBValue = RoomState.GetBBlinds() + 1;

                if (newBValue <= 10)
                {
                    RoomState.SetBBlinds(newBValue);
                    controller.BBlindsValue = newBValue;
                }
            }
            else if (responseString.Contains("BLINDS_BLKOUT_DOWN_1_FB"))    // Close Ambient Blinds
            {
                int newBValue = RoomState.GetBBlinds() - 1;

                if (newBValue >= 0)
                {
                    RoomState.SetBBlinds(newBValue);
                    controller.BBlindsValue = newBValue;
                }
            }
            else if (responseString.Contains("BLINDS_BLKOUT_"))             // Close Ambient Blinds
            {
                int index = responseString.IndexOf("BLINDS_BLKOUT_") + 14;

                char[] digits = new char[] { responseString[index], responseString[index + 1] };
                if (Char.IsDigit(digits[0]))
                {
                    int value;
                    if (digits[1].Equals('_'))
                        value = Int16.Parse(digits[0].ToString());
                    else
                        value = Int16.Parse(new string(digits));

                    RoomState.SetBBlinds(value);
                    controller.BBlindsValue = value;
                }
            }


            if (responseString.Contains("BLINDS_OPAQUE_UP_FB"))             // Close Ambient Blinds
            {
                int newIValue = RoomState.GetIBlinds() + 1;

                if (newIValue <= 10)
                {
                    RoomState.SetIBlinds(newIValue);
                    controller.IBlindsValue = newIValue;
                }
            }
            else if (responseString.Contains("BLINDS_OPAQUE_DOWN_1_FB"))    // Close Ambient Blinds
            {
                int newIValue = RoomState.GetIBlinds() - 1;

                if (newIValue >= 0)
                {
                    RoomState.SetIBlinds(newIValue);
                    controller.IBlindsValue = newIValue;
                }
            }

            else if (responseString.Contains("BLINDS_OPAQUE_"))             // Close Ambient Blinds
            {
                int index = responseString.IndexOf("BLINDS_OPAQUE_") + 14;

                char[] digits = new char[] { responseString[index], responseString[index + 1] };

                if (Char.IsDigit(digits[0]))
                {
                    int value;
                    if (digits[1].Equals('_'))
                        value = Int16.Parse(digits[0].ToString());
                    else
                        value = Int16.Parse(new string(digits));

                    RoomState.SetIBlinds(value);
                    controller.IBlindsValue = value;
                }
            }
            if (responseString.Contains("BLINDS_ALL_"))                     // Close Ambient Blinds
            {
                int index = responseString.IndexOf("BLINDS_ALL_") + 11;

                char[] digits = new char[] { responseString[index], responseString[index + 1] };
                if (Char.IsDigit(digits[0]))
                {
                    int value;
                    if (digits[1].Equals('_'))
                        value = Int16.Parse(digits[0].ToString());
                    else
                        value = Int16.Parse(new string(digits));

                    RoomState.SetBBlinds(value);
                    RoomState.SetIBlinds(value);
                    controller.BBlindsValue = value;
                    controller.IBlindsValue = value;
                }
            }

            // Set Temperature Up
            if (responseString.Contains("HVAC_TEMP_UP_FB"))
            {
                double value = RoomState.GetSettingTemp();

                if (value <= Constants.maxTemp)
                {
                    RoomState.SetSettingTemp(true);
                    controller.TempSetting = RoomState.GetSettingTemp().ToString() + "°F";
                }
            }

            // Set Temperature Down
            if (responseString.Contains("HVAC_TEMP_DN_FB"))
            {
                double value = RoomState.GetSettingTemp();

                if (value >= Constants.minTemp)
                {
                    RoomState.SetSettingTemp(false);
                    controller.TempSetting = RoomState.GetSettingTemp().ToString() + "°F";
                }
            }

            // Update Current Temperature
            if (responseString.Contains("HVAC_TEMP_CURRENT_"))
            {
                int index = responseString.IndexOf("HVAC_TEMP_CURRENT_") + 18;
                char[] digits = new char[] { responseString[index], responseString[index + 1], responseString[index + 2], responseString[index + 3] };

                double value = 0;
                if (digits[3].Equals('F'))
                {
                    char[] sDigits = new char[] { digits[0], digits[1] };
                    value = Double.Parse(new string(sDigits));
                    RoomState.SetSettingTemp(value);
                }
                else
                {
                    value = Double.Parse(new string(digits));
                    RoomState.SetSettingTemp(value);
                }
                controller.CurrentTemp = value.ToString() + "°F";
            }

            // Set Temperature to specfic value 
            if (responseString.Contains("HVAC_TEMP_SETPOINT_TO_"))
            {
                int index = responseString.IndexOf("HVAC_TEMP_SETPOINT_TO_") + 22;
                char[] digits = new char[] { responseString[index], responseString[index + 1], responseString[index + 2], responseString[index + 3] };

                double value;
                if (digits[3].Equals('F'))
                {
                    char[] sDigits = new char[] { digits[0], digits[1] };
                    value = Double.Parse(new string(sDigits));
                    RoomState.SetSettingTemp(value);
                }
                else
                {
                    value = Double.Parse(new string(digits));
                    RoomState.SetSettingTemp(value);
                }

                controller.TempSetting = value.ToString() + "°F";
            }

            if (responseString.Contains("HOTWORD%"))
            {
                int index = responseString.IndexOf('%') + 1;

                char firstChar = responseString[index];

                if (firstChar.Equals('M'))
                {
                    if (RoomState.GetHotword().Equals("sam"))
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            controller.SetHotword("sam");
                        });
                    }

                }
                else
                {
                    if (RoomState.GetHotword().Equals("mac"))
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            controller.SetHotword("mac");
                        });
                    }
                }
            }

            if (responseString.Contains("VOICETOGGLE%"))
            {
                int index = responseString.IndexOf('%') + 1;

                char firstChar = responseString[index];

                if (firstChar.Equals('E'))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        controller.ToggleVoice(true);

                    });
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        controller.ToggleVoice(false);

                    });
                }
            }

            if (responseString.Contains("VOICETIMETOGGLE%"))
            {
                int index = responseString.IndexOf('%') + 1;

                char firstChar = responseString[index];

                if (firstChar.Equals('1'))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        controller.UpdateListeningTime(1);
                    });
                }
                else if (firstChar.Equals('3'))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        controller.UpdateListeningTime(3);
                    });
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        controller.UpdateListeningTime(5);
                    });
                }
            }

            if (responseString.Contains("CHANGEPAGE%"))
            {

                int index = responseString.IndexOf('%') + 1;

                char firstChar = responseString[index];

                if (firstChar.Equals('M'))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        controller.SelectMedia();
                    });
                }
                else if (firstChar.Equals('D'))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        controller.SelectDoor();
                    });
                }
                else if (firstChar.Equals('L'))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        controller.SelectLights();
                    });
                }
                else if (firstChar.Equals('B'))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        controller.SelectBlinds();
                    });
                }
                else if (firstChar.Equals('C'))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        controller.SelectClimate();
                    });
                }
                else if (firstChar.Equals('N'))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        controller.SelectNurse();
                    });
                }
                else if (firstChar.Equals('O'))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        controller.SelectOption();
                    });
                }
            }
            // Paged Nurse
            if (responseString.Contains("NURSEREQUEST_SUCCESS"))
            {
                controller.NurseSuccessColor = Color.Red;
                controller.NurseLock = false;
                controller.NurseText = "Nurse Successfully Paged\nIf urgent, use call light";
                Thread.Sleep(300000);
                controller.NurseSuccessColor = Color.Transparent;
                controller.NurseLock = true;
            }

            if (responseString.Contains("NURSEREQUEST_FAILED"))
            {
                controller.NurseSuccessColor = Color.Red;
                controller.NurseText = "Nurse request Failed\nPlease try again";
                Thread.Sleep(3000);
                controller.NurseSuccessColor = Color.Transparent;
            }

            if (responseString.Contains("WB_POWER_FB"))
            {
                controller.DisplayDim = ImageSource.FromResource("Smartroom.Icons.LightIconSoft.png");
                controller.DisplayBright = ImageSource.FromResource("Smartroom.Icons.LightIconFull.png");

                controller.DimColor = Color.Black;
                controller.BrightColor = Color.Black;
            }

            if (responseString.Contains("CLOCKBRIGHTNESS_"))
            {
                string brightnessValue = responseString.Split('_')[1].Split('\\')[0];

                if (brightnessValue.Contains("HIGH"))
                {
                    controller.UpdateClockBrightness("HIGH");
                }
                else if (brightnessValue.Contains("MEDIUM"))
                {
                    controller.UpdateClockBrightness("MEDIUM");
                }
                else if (brightnessValue.Contains("LOW"))
                {
                    controller.UpdateClockBrightness("LOW");
                }
                else if (brightnessValue.Contains("OFF"))
                {
                    controller.UpdateClockBrightness("OFF");
                }
                else
                {
                    //TODO indicate something wasn't right.
                }
            }

            //Elevator was succefully scheduled
            if (responseString.Contains("Elevator Called Succesfully"))
            {
                controller.ElevatorMessage = "Elevator Called Succesfully";
            }

            if (responseString.Contains("WB_DIM_FB"))
            {
                controller.DisplayDim = ImageSource.FromResource("Smartroom.Icons.LightIconSoftRed.png");
                controller.DisplayBright = ImageSource.FromResource("Smartroom.Icons.LightIconFull.png");
                controller.DimColor = Color.FromHex("#AC162C");
                controller.BrightColor = Color.Black;
            }

            if (responseString.Contains("WB_BRIGHT_FB"))
            {
                controller.DisplayDim = ImageSource.FromResource("Smartroom.Icons.LightIconSoft.png");
                controller.DisplayBright = ImageSource.FromResource("Smartroom.Icons.LightIconFullRed.png");

                controller.DimColor = Color.Black;
                controller.BrightColor = Color.FromHex("#AC162C");
            }

            //Elevator was succefully scheduled
            if (responseString.Contains("Elevator Called Succesfully"))
            {
                controller.ElevatorMessage = "Elevator Called Succesfully";
            }

            //start elevator schedule voice session
            if (responseString.Contains("startInteractiveSession"))
            {
                controller.elevNum = 0;
                controller.PlaySpeech(1);
                controller.ElevatorMessage = "What is the Elevator Bank?";
                controller.SetElev(true);
            }

            //handle results from elevator voice queries  
            if (responseString.Contains("elevatorResult"))
            {
                int value = Int32.Parse(responseString.Split('%')[1]);

                switch (controller.elevNum)
                {
                    case 0:                                                 //elevator bank answer
                        {
                            if (value < Constants.minBank || value > Constants.maxBank)
                            {
                                responseString = "elevatorRetry";
                                break;
                            }

                            controller.elevNum++;
                            RoomState.SetElevBank(value);
                            controller.PlaySpeech(2);
                            controller.ElevatorMessage = "What is the Origin Floor?";
                            break;
                        }
                    case 1:                                                 //origin floor answer
                        {
                            if (value < Constants.minFloor || value > Constants.maxFloor)
                            {
                                responseString = "elevatorRetry";
                                break;
                            }

                            controller.elevNum++;
                            controller.PlaySpeech(3);
                            RoomState.SetElevOriginFloor(value);
                            controller.ElevatorMessage = "What is the Destination Floor?";
                            break;
                        }
                    case 2:                                                 //destination floor answer 
                        {
                            if (value < Constants.minFloor || value > Constants.maxFloor)
                            {
                                responseString = "elevatorRetry";
                                break;
                            }
                            controller.ElevatorMessage = "";

                            controller.SetElev(false);
                            RoomState.SetElevDestFloor(value);

                            controller.CallNowCommand.Execute(null);           //schedule elevator

                            controller.elevFlag = false;                        //end elevator schedule voice session
                            break;
                        }
                }
            }

            //invalid elevator voice query result
            if (responseString.Contains("elevatorRetry"))
            {
                switch (controller.elevNum)
                {
                    case 0:                                                 //Play Valid answer voice responses
                        {
                            controller.PlaySpeech(4);
                            controller.ElevatorMessage = "What is the Elevator Bank? Valid answers are from 3-6";
                            break;
                        }
                    case 1:
                        {
                            controller.PlaySpeech(5);
                            controller.ElevatorMessage = "What is the Origin Floor? Valid answers are from 1-5 and Lower Level 1";
                            break;
                        }
                    case 2:
                        {
                            controller.PlaySpeech(5);
                            controller.ElevatorMessage = "What is the Destination Floor? Valid answers are from 1-5 and Lower Level 1";
                            break;
                        }
                }
            }

            //cancel an elevator voice session
            if (responseString.Contains("elevatorCancel"))
            {
                controller.ElevatorMessage = "";
                controller.elevFlag = false;
                controller.SetElev(false);
            }
        }
        #endregion Socket Listener
    }

    /// <summary>
    /// Class to help construct the Socket packege to send
    /// </summary>
    public class SocketPackage
    {
        private readonly Byte messageType;
        private readonly ushort roomNum;
        private readonly UInt16 roomPin;
        private readonly string command;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="messageType">1 for button presses, 2 for speech requests</param>
        /// <param name="roomNum">room number of room to control</param>
        /// <param name="roomPin">Special pin for security</param>
        /// <param name="command">Command to change environment</param>
        public SocketPackage(Byte messageType, ushort roomNum, UInt16 roomPin, string command)
        {
            this.messageType = messageType;
            this.roomNum = roomNum;
            this.roomPin = roomPin;
            this.command = command;
        }

        /// <summary>
        /// Construct a Sendable ByteString for a socket 
        /// </summary>
        /// <returns>Byte string appropriate to send through a socket</returns>
        public Byte[] Package()
        {
            Byte[] commandBytes = Encoding.UTF8.GetBytes(command);              //convert the command string into Bytes

            Byte[] byteString = new Byte[commandBytes.Length + 6];              //Create new byte array based on the projected length of the request
            byteString[1] = messageType;                                        // and add message type and room number bytes

            Byte[] roomN = BitConverter.GetBytes(roomNum);
            byteString[2] = roomN[1];
            byteString[3] = roomN[0];

            Byte[] pinB = BitConverter.GetBytes(roomPin);                       //Convert roompin int to Bytes
            byteString[4] = pinB[1];                                            // and add it to the byte array
            byteString[5] = pinB[0];

            System.Buffer.BlockCopy(commandBytes, 0, byteString, 6, commandBytes.Length); //add command bytes to bytes array

            byteString[0] = (Byte)byteString.Length;                            //add byte representing length of byte array

            return byteString;
        }
    }

    /// <summary>
    /// Common Action types for the environmental machines
    /// </summary>
    public enum Actions
    {
        Off = 0,
        On = 1,
        Up = 2,
        Down = 3,
        Open = 4,
        Close = 5,
        Partial = 6
    }
}
