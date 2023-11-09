using System;
using System.Threading.Tasks;
using Smartroom.Models;
using Smartroom.ViewModels;

namespace Smartroom.Services
{
    /// <summary>
    /// Interface for Web Socket Functions
    /// Allows Viewmodels to access the Socket.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Send TV Control requests to the middelware 
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// ON: Turn TV on
        /// OFF: Turn TV off
        /// Up: Change Channel Up
        /// Down: Change Channel Down</param>
        Task TVControls(Actions act);

        /// <summary>
        /// Send volume control requests to middleware
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// <param name="device"> string indicating which device to change sound level
        /// Up:  Change volume up
        /// Down: Change volume down
        /// Off:  Mute/Unmute Volume</param>
        Task Volume(Actions act, string device);

        /// <summary>
        /// Send TV Channel change requests to the middelware 
        /// </summary>
        /// <param name="newChannel")(int) channel to change to
        ///</param>
        Task TVChannel(int newChannel);

        /// <summary>
        /// Sends TVInput change request to middleware
        /// </summary>
        /// <param name="preset">which input to use,
        /// 1: Chromecast
        /// 2: Cable
        /// 3: Aux</param>
        Task TVInput(int preset);

        /// <summary>
        /// Sends soundbar change request to middleware
        /// </summary>
        /// <param name="preset">which input to use,
        /// 1: TV
        /// 2: Bluetooth
        /// </param>
        Task SoundbarInput(int preset);

        /// <summary>
        /// Send Door requests to middleware
        /// </summary>
        /// <param name="act">action to perform</param>
        Task Door(Actions act);

        /// <summary>
        /// Send Control requests for the Entrance lights
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// On: Turn on entrance lights, to previous setting
        /// Off: Turn off entrance lights
        /// Up: Turn entrance lights setting up, to 1 if off
        /// Down: Turn entrance lights setting down, off if 1</param>
        Task ELights(Actions act);

        /// <summary>
        /// Send Control requests for the Reading lights
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// On: Turn on Reading lights, to previous setting
        /// Off: Turn off Reading lights
        /// Up: Turn Reading lights setting up, to 1 if off
        /// Down: Turn Reading lights setting down, off if 1</param>
        Task RLights(Actions act);

        /// <summary>
        /// Send Control requests for the Ambient lights
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// On: Turn on Ambeint lights, to previous setting
        /// Off: Turn off Ambient lights
        /// Up: Turn Ambient lights setting up, to 1 if off
        /// Down: Turn Ambient lights setting down, off if 1</param>
        Task ALights(Actions act);

        /// <summary>
        /// Send Control requests for the Ambient lights
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// On: Turn on Exam lights, to previous setting
        /// Off: Turn off Exam lights
        /// Up: Turn Exam lights setting up, to 1 if off
        /// Down: Turn Exam lights setting down, off if 1</param>
        Task ExLights(Actions act);

        /// <summary>
        /// Send Control requests for the Bathroom lights
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// On: Turn on Bathroom lights, to previous setting
        /// Off: Turn off Bathroom lights
        /// Up: Turn Bathroom lights setting up, to 1 if off
        /// Down: Turn Bathroom lights setting down, off if 1</param>
        Task BathLights(Actions act);

        /// <summary>
        /// Send lighting preset requests to the middleware
        /// </summary>
        /// <param name="preset">Corisponding preset setting
        /// 0: All Off
        /// 1: All on
        /// 2: Reading Preset
        /// 3: Soft Preset</param>
        Task LightsPreset(int preset);

        /// <summary>
        /// Send nurse call request to middleware
        /// </summary>
        Task CallNurse(string requestType);

        /// <summary>
        /// Send nurse call request to middleware
        /// </summary>
        Task NurseRequest(string userRequest);

        /// <summary>
        /// Send Blackout Blind requests to middleware
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// Open:  Open Blackout Blinds all the way
        /// Up: Raise Blackout blinds a bit
        /// Down: Lower Blackout blinds a bit
        /// Close: Close BLackout Blinds all the way</param>
        Task BBlinds(Actions act);

        /// <summary>
        /// Send Interior Blind requests to middleware
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// Open:  Open Interior Blinds all the way
        /// Up: Raise Interior blinds a bit
        /// Down: Lower Interior blinds a bit
        /// Close: Close Interior Blinds all the way</param>
        Task IBlinds(Actions act);

        /// <summary>
        /// Send Temperature control requests to middleware; 
        /// </summary>
        /// <param name="act">(Actions) Action to perform
        /// Up: Turn Temperature Up
        /// Down: Turn Temperature Down</param>
        Task Temperature(Actions act);

        /// <summary>
        /// Sends Elevator request to the middleware
        /// </summary>
        /// <param name="mode">call mode to send
        /// 1: schedule for later date
        /// 2: call elevator now</param>
        /// <returns></returns>
        Task Elevator(int mode);

        /// <summary>
        /// Requests server to provide current status on room devices.
        /// </summary>
        /// <returns></returns>
        Task GetStatus();

        /// <summary>
        /// Initializes connection to middleware. 
        /// </summary>
        /// <returns>nothing</returns>
        void SocketConnect(TabletController context);

        /// <summary>
        /// Makes a dialgoflow call
        /// </summary>
        /// <param name="query">query string</param>
        /// <returns>response string</returns>
        Task<string> QueryDialogFlowAsync(string query);

        /// <summary>
        /// Makes a interactive elevator call
        /// </summary>
        /// <param name="statement">query string</param>
        /// <returns>response string</returns>
        Task<string> ElevDialogFlowAsync(string statement);

        /// <summary>
        /// Controls the info display 
        /// </summary>
        /// <param name="mode">
        /// 0:Display Off
        /// 1:Display Dim
        /// 2:Display On
        /// </param>
        /// <returns></returns>
        Task DisplayControl(int mode);

        /// <summary>
        /// Controls the info display 
        /// </summary>
        /// <param name="mode">
        /// 0:Display Off
        /// 1:Display Dim
        /// 2:Display On
        /// </param>
        /// <returns></returns>
        Task ClockControl(int mode);


        /// <summary>
        /// Controls the Apple TV
        /// </summary>
        /// <param name="type">
        /// 0:Select/OK Button
        /// 1:Up Button
        /// 2:Down Button
        /// 3:Left Button
        /// 4:Right Button
        /// 5:Play/Pause Button
        /// 6:Menu Button
        /// </param>
        /// <returns></returns>
        Task ControlApple(int type);

        /// <summary>
        /// Send a pair request signal to the Bluetooth Soundbar
        /// </summary>
        /// <returns></returns>
        Task PairBluetooth();

        /// <summary>
        /// Send Power command to the Bluetooth Soundbar, string containing On or Off
        /// </summary>
        /// <param name="powertype">On or Off</param>
        /// <returns></returns>
        Task SoundBarPowerCommand(string powerType);
    }
}