using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using Smartroom.Models;
using Smartroom.Services;
using Smartroom.Helpers;
using System.IO;
using Smartroom.Views;

namespace Smartroom.ViewModels
{
    /// <summary>
    /// Middleman for function between the Models and the views
    /// </summary>
    public class TabletController : INotifyPropertyChanged
    {
        public bool listenFlag; //Flag for actively listening for speech commands
        public bool elevFlag;  //flag for interactive elevator scheduling
        public int elevNum;
        public event PropertyChangedEventHandler PropertyChanged;
        readonly static string logFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Constants.logName);

        ////////////////////////////////////View controllers
        ///These variables control view elements 

        private string message;                                                 //Debugger text
        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        private string clockBrightness;
        public string ClockBrightness
        {
            get => clockBrightness;
            set
            {
                if(value != clockBrightness)
                {
                    clockBrightness = value;
                    OnPropertyChanged("ClockBrightness");
                }
            }
        }

        private string elevatorMessage;                                         //Debugger text
        public string ElevatorMessage
        {
            get => elevatorMessage;
            set
            {
                elevatorMessage = value;
                OnPropertyChanged("ElevatorMessage");
            }
        }
        public string Hotword
        {
            get => RoomState.GetHotword();
            set
            {
                RoomState.SetHotword(value);
                OnPropertyChanged("Hotword");
            }
        }

        public bool VoiceMuted
        {
            get => RoomState.GetVoiceMuted();
            set
            {
                RoomState.SetVoiceMuted(value);
                OnPropertyChanged("VoiceMuted");
            }
        }

        private string roomNum;                                                 //Caster Input BorderColor Setting
        public String RoomNum
        {
            get => roomNum;
            set
            {
                roomNum = value;
                OnPropertyChanged("RoomNum");
            }
        }

        private string roomPin;                                                 //Caster Input BorderColor Setting
        public String RoomPin
        {
            get => roomPin;
            set
            {
                roomPin = value;
                OnPropertyChanged("RoomPin");
            }
        }

        private string soundBarPin = "----";                                               //Caster Input BorderColor Setting
        public String SoundBarPin
        {
            get => soundBarPin;
            set
            {
                soundBarPin = value;
                OnPropertyChanged("SoundBarPin");
            }
        }

        private string entrance;                                                //Entrance Lights Setting Text
        public string Entrance
        {
            get => entrance;
            set
            {
                entrance = value;
                OnPropertyChanged("Entrance");
            }
        }

        private string reading;                                                 //Reading Lights Setting Text
        public string Reading
        {
            get => reading;
            set
            {
                reading = value;
                OnPropertyChanged("Reading");
            }
        }

        private string ambient;                                                 //Ambeint Lights Setting Text
        public string Ambient
        {
            get => ambient;
            set
            {
                ambient = value;
                OnPropertyChanged("Ambient");
            }
        }

        private string bathroom;                                                //Bathroom Lights Setting Text
        public string Bathroom
        {
            get => bathroom;
            set
            {
                bathroom = value;
                OnPropertyChanged("Bathroom");
            }
        }

        private string exam;                                                    //Exam Lights Setting Text
        public string Exam
        {
            get => exam;
            set
            {
                exam = value;
                OnPropertyChanged("Exam");
            }
        }

        private float circlePercent;                                                    //Circle bar setting
        public float CirclePercent
        {
            get => circlePercent;
            set
            {
                circlePercent = value;
                OnPropertyChanged("CirclePercent");
            }
        }

        private bool entranceLightOnOff;                                        //Entrance Lights Toggle setting
        public bool EntranceLightOnOff
        {
            get => entranceLightOnOff;
            set
            {
                entranceLightOnOff = value;
                OnPropertyChanged("EntranceLightOnOff");
            }
        }

        private bool readingLightOnOff;                                         //Reading Lights Toggle Setting
        public bool ReadingLightOnOff
        {
            get => readingLightOnOff;
            set
            {
                readingLightOnOff = value;
                OnPropertyChanged("ReadingLightOnOff");
            }
        }

        private bool ambientLightOnOff;                                         // Ambient Lights Toggle Setting
        public bool AmbientLightOnOff
        {
            get => ambientLightOnOff;
            set
            {
                ambientLightOnOff = value;
                OnPropertyChanged("AmbientLightOnOff");
            }
        }

        private bool bathroomLightOnOff;                                        // Bathroom Lights Toggle Setting
        public bool BathroomLightOnOff
        {
            get => bathroomLightOnOff;
            set
            {
                bathroomLightOnOff = value;
                OnPropertyChanged("BathroomLightOnOff");
            }
        }

        private bool examLightOnOff;                                            // Exam Lights Toggle Setting
        public bool ExamLightOnOff
        {
            get => examLightOnOff;
            set
            {
                examLightOnOff = value;
                OnPropertyChanged("ExamLightOnOff");
            }
        }

        private string tempSetting;                                             //Temeprature Setting Text
        public string TempSetting
        {
            get => tempSetting;
            set
            {
                tempSetting = value;
                OnPropertyChanged("TempSetting");
            }
        }

        private string currentTemp;                                             //Temeprature Setting Text
        public string CurrentTemp
        {
            get => currentTemp;
            set
            {
                currentTemp = value;
                OnPropertyChanged("CurrentTemp");
            }
        }

        private int bBlindsValue;                                               //Blackout Blinds Setting Text
        public int BBlindsValue
        {
            get => bBlindsValue;
            set
            {
                bBlindsValue = value;
                OnPropertyChanged("BBlindsValue");
            }
        }

        private int iBlindsValue;                                               //Interior Blinds Setting Text
        public int IBlindsValue
        {
            get => iBlindsValue;
            set
            {
                iBlindsValue = value;
                OnPropertyChanged("IBlindsValue");
            }
        }

        private string originFloor;                                             //Origin Floor Setting Text
        public string OriginFloor
        {
            get => originFloor;
            set
            {
                originFloor = value;
                OnPropertyChanged("OriginFloor");
            }
        }


        private string destFloor;                                               //Destination Floor Setting Text
        public string DestFloor
        {
            get => destFloor;
            set
            {
                destFloor = value;
                OnPropertyChanged("DestFloor");
            }
        }

        private int elevBank;                                                   //Elevator Bank setting Text
        public int ElevBank
        {
            get => elevBank;
            set
            {
                elevBank = value;
                OnPropertyChanged("ElevBank");
            }
        }

        private string tvOnOff;                                                 //tv OnOff Border Color Setting
        public String TvOnOff
        {
            get => tvOnOff;
            set
            {
                tvOnOff = value;
                OnPropertyChanged("TvOnOff");
            }
        }

        private ImageSource muteOnOff;                                               //tv OnOff Border Color Setting      
        public ImageSource MuteOnOff
        {
            get => muteOnOff;
            set
            {
                muteOnOff = value;
                OnPropertyChanged("MuteOnOff");
            }
        }

        private string hdmiOnOff;                                               //AUX Input BorderColor Setting
        public String HDMIOnOff
        {
            get => hdmiOnOff;
            set
            {
                hdmiOnOff = value;
                OnPropertyChanged("HDMIOnOff");
            }
        }


        private string cableOnOff;                                              //CableBox Input BorderColor Setting 
        public String CableOnOff
        {
            get => cableOnOff;
            set
            {
                cableOnOff = value;
                OnPropertyChanged("CableOnOff");
            }
        }

        private string castOnOff;                                               //Caster Input BorderColor Setting
        public String CastOnOff
        {
            get => castOnOff;
            set
            {
                castOnOff = value;
                OnPropertyChanged("CastOnOff");
            }
        }

        private string tvbarOnOff;                                              //Caster Input BorderColor Setting
        public String TVBarOnOff
        {
            get => tvbarOnOff;
            set
            {
                tvbarOnOff = value;
                OnPropertyChanged("TVBarOnOff");
            }
        }

        private string blueOnOff;                                               //Caster Input BorderColor Setting
        public String BlueOnOff
        {
            get => blueOnOff;
            set
            {
                blueOnOff = value;
                OnPropertyChanged("BlueOnOff");
            }
        }

        private string night;                                                   //Night Button appearance setting
        public string Night
        {
            get => night;
            set
            {
                night = value;
                OnPropertyChanged("Night");
            }
        }

        private string tv;                                                      //TV Button appearance setting
        public string TV
        {
            get => tv;
            set
            {
                tv = value;
                OnPropertyChanged("TV");
            }
        }

        private string soft;                                                    //Soft Button appearance setting
        public string Soft
        {
            get => soft;
            set
            {
                soft = value;
                OnPropertyChanged("Soft");
            }
        }

        private string read;                                                    //Read Button appearance setting
        public string Read
        {
            get => read;
            set
            {
                read = value;
                OnPropertyChanged("Read");
            }
        }

        private string full;                                                    //full Button appearance setting
        public string Full
        {
            get => full;
            set
            {
                full = value;
                OnPropertyChanged("Full");
            }
        }

        private Color c27;                                                    //channel Background Color
        public Color C27
        {
            get => c27;
            set
            {
                c27 = value;
                OnPropertyChanged("C27");
            }
        }

        private ImageSource displayDim;
        public ImageSource DisplayDim
        {
            get => displayDim;
            set
            {
                displayDim = value;
                OnPropertyChanged("DisplayDim");
            }
        }

        private Color dimColor;
        public Color DimColor
        {
            get => dimColor;
            set
            {
                dimColor = value;
                OnPropertyChanged("DimColor");
            }
        }

        private ImageSource displayBright;
        public ImageSource DisplayBright
        {
            get => displayBright;
            set
            {
                displayBright = value;
                OnPropertyChanged("DisplayBright");
            }
        }

        private ImageSource optionListeningIcon;
        public ImageSource OptionListeningIcon
        {
            get => optionListeningIcon;
            set
            {
                optionListeningIcon = value;
                OnPropertyChanged("OptionListeningIcon");
            }
        }

        private Color brightColor;
        public Color BrightColor
        {
            get => brightColor;
            set
            {
                brightColor = value;
                OnPropertyChanged("BrightColor");
            }
        }

        private int roomSpan;
        public int RoomSpan
        {
            get => roomSpan;
            set
            {
                roomSpan = value;
                OnPropertyChanged("RoomSpan");
            }
        }

        private Color roomColor;
        public Color RoomColor
        {
            get => roomColor;
            set
            {
                roomColor = value;
                OnPropertyChanged("RoomColor");
            }
        }


        private bool mediaVisibility = true;
        public bool MediaVisibility
        {
            get => mediaVisibility;
            set
            {
                mediaVisibility = value;
                OnPropertyChanged("MediaVisibility");
            }
        }

        private bool doorVisibility = true;
        public bool DoorVisibility
        {
            get => doorVisibility;
            set
            {
                doorVisibility = value;
                OnPropertyChanged("DoorVisibility");
            }
        }

        private bool lightsVisibility = true;
        public bool LightsVisibility
        {
            get => lightsVisibility;
            set
            {
                lightsVisibility = value;
                OnPropertyChanged("LightsVisibility");
            }
        }

        private bool blindsVisibility = true;
        public bool BlindsVisibility
        {
            get => blindsVisibility;
            set
            {
                blindsVisibility = value;
                OnPropertyChanged("BlindsVisibility");
            }
        }

        private bool elevatorVisibility = false;
        public bool ElevatorVisibility
        {
            get => elevatorVisibility;
            set
            {
                elevatorVisibility = value;
                OnPropertyChanged("ElevatorVisibility");
            }
        }

        private bool temperatureVisibility = true;
        public bool TemperatureVisibility
        {
            get => temperatureVisibility;
            set
            {
                temperatureVisibility = value;
                OnPropertyChanged("TemperatureVisibility");
            }
        }

        private bool nurseVisibility = false;
        public bool NurseVisibility
        {
            get => nurseVisibility;
            set
            {
                nurseVisibility = value;
                OnPropertyChanged("NurseVisibility");
            }
        }

        private bool optionsVisibility = true;
        public bool OptionsVisibility
        {
            get => optionsVisibility;
            set
            {
                optionsVisibility = value;
                OnPropertyChanged("OptionsVisibility");
            }
        }

        private Color nurseSuccessColor;
        public Color NurseSuccessColor
        {
            get => nurseSuccessColor;
            set
            {
                nurseSuccessColor = value;
                OnPropertyChanged("NurseSuccessColor");
            }
        }

        private bool nurseLock;
        public bool NurseLock
        {
            get => nurseLock;
            set
            {
                nurseLock = value;
                OnPropertyChanged("NurseLock");
            }
        }

        private string nurseText;
        public string NurseText
        {
            get => nurseText;
            set
            {
                nurseText = value;
                OnPropertyChanged("NurseText");
            }
        }


        public static IDataService DataService { get; } = DependencyService.Get<IDataService>();
        public string LightsSceneSelected => RoomState.getLightsSceneSelected();

        #region Commands
        //////////
        //Todo: move to Commands file
        public Command NurseRequestCommand { get; private set; }
        public Command VolumeUpCommand { get; private set; }
        public Command VolumeDownCommand { get; private set; }
        public Command VolumeMuteCommand { get; private set; }
        public Command SBarVolumeUpCommand { get; private set; }
        public Command SBarVolumeDownCommand { get; private set; }
        public Command SBarVolumeMuteCommand { get; private set; }
        public Command LightsCommand { get; private set; }
        public Command TVOnOffCommand { get; private set; }
        public Command ChannelUpCommand { get; private set; }
        public Command ChannelDownCommand { get; private set; }
        public Command TempUpCommand { get; private set; }
        public Command TempDownCommand { get; private set; }
        public Command BBlindsOpenCommand { get; private set; }
        public Command BBlindsUpCommand { get; private set; }
        public Command BBlindsDownCommand { get; private set; }
        public Command BBlindsClosedCommand { get; private set; }
        public Command IBlindsOpenCommand { get; private set; }
        public Command IBlindsUpCommand { get; private set; }
        public Command IBlindsDownCommand { get; private set; }
        public Command IBlindsClosedCommand { get; private set; }
        public Command OptionsCommand { get; private set; }
        public Command DoorCloseCommand { get; private set; }
        public Command DoorPartialCommand { get; private set; }
        public Command DoorOpenCommand { get; private set; }
        public Command EntranceLightsOnCommand { get; private set; }
        public Command EntranceLightsOffCommand { get; private set; }
        public Command EntranceLightsUpCommand { get; private set; }
        public Command EntranceLightsDownCommand { get; private set; }
        public Command ReadingLightsOnCommand { get; private set; }
        public Command ReadingLightsOffCommand { get; private set; }
        public Command ReadingLightsUpCommand { get; private set; }
        public Command ReadingLightsDownCommand { get; private set; }
        public Command AmbientLightsOnCommand { get; private set; }
        public Command AmbientLightsOffCommand { get; private set; }
        public Command AmbientLightsUpCommand { get; private set; }
        public Command AmbientLightsDownCommand { get; private set; }
        public Command ExamLightsOnCommand { get; private set; }
        public Command ExamLightsOffCommand { get; private set; }
        public Command ExamLightsUpCommand { get; private set; }
        public Command ExamLightsDownCommand { get; private set; }
        public Command BathLightsOnCommand { get; private set; }
        public Command BathLightsOffCommand { get; private set; }
        public Command BathLightsUpCommand { get; private set; }
        public Command BathLightsDownCommand { get; private set; }
        public Command NightSceneCommand { get; private set; }
        public Command TVSceneCommand { get; private set; }
        public Command ReadingSceneCommand { get; private set; }
        public Command SoftSceneCommand { get; private set; }
        public Command FullSceneCommand { get; private set; }
        public Command OriginUpCommand { get; private set; }
        public Command OriginDownCommand { get; private set; }
        public Command DFloorUpCommand { get; private set; }
        public Command DFloorDownCommand { get; private set; }
        public Command BankUpCommand { get; private set; }
        public Command BankDownCommand { get; private set; }
        public Command CallUpCommand { get; private set; }
        public Command CallDownCommand { get; private set; }
        public Command CurrentUpCommand { get; private set; }
        public Command CurrentDownCommand { get; private set; }
        public Command CDFloorUpCommand { get; private set; }
        public Command CDFloorDownCommand { get; private set; }
        public Command HDMICommand { get; private set; }
        public Command CableCommand { get; private set; }
        public Command CasterCommand { get; private set; }
        public Command TVBarCommand { get; private set; }
        public Command BluetoothCommand { get; private set; }
        public Command CallNowCommand { get; private set; }
        public Command FanOnCommand { get; private set; }
        public Command FanOffCommand { get; private set; }
        public Command Fan1Command { get; private set; }
        public Command Fan2Command { get; private set; }
        public Command Fan3Command { get; private set; }
        public Command CallElevCommand { get; private set; }
        public Command DisplayOffCommand { get; private set; }
        public Command DisplayDimCommand { get; private set; }
        public Command DisplayOnCommand { get; private set; }
        public Command ClockOffCommand { get; private set; }
        public Command ClockLowCommand { get; private set; }
        public Command ClockMediumCommand { get; private set; }
        public Command ClockHighCommand { get; private set; }
        public Command PairBluetoothCommand { get; private set; }
        public Command SoundBarPowerOnCommand { get; private set; }
        public Command SoundBarPowerOffCommand { get; private set; }
        public Command AppleSelectCommand { get; private set; }
        public Command AppleUpCommand { get; private set; }
        public Command AppleDownCommand { get; private set; }
        public Command AppleLeftCommand { get; private set; }
        public Command AppleRightCommand { get; private set; }
        public Command ApplePlayCommand { get; private set; }
        public Command AppleMenuCommand { get; private set; }
        public Command GetStatusCommand { get; private set; }
        #endregion

        public ISpeechToText _speechRecongnitionInstance;                       // Create speech recongition instance
        public IExternalIntents _externalIntentInstance;

        private MasterPage master;

        public TabletController()
        {                                         //set UI elements
            RoomState.SetHotword("mac");

            RoomNum = "Room Num: ";
            RoomPin = "PIN: ";
            RoomSpan = 1;
            RoomColor = Color.FromHex("#414042");
            Entrance = "0";
            Reading = "0";
            Ambient = "0";
            Exam = "0";
            Bathroom = "0";
            TempSetting = "70°F";
            CurrentTemp = "70°F";
            NurseSuccessColor = Color.Transparent;
            nurseLock = true;
            nurseText = "Nurse Successfully Paged\nIf urgent, use call light";


            MuteOnOff = ImageSource.FromResource("Smartroom.Icons.MediaSoundMuteButton.png");

            OriginFloor = "1";
            ElevBank = 3;
            DestFloor = "1";

            DisplayDim = ImageSource.FromResource("Smartroom.Icons.LightIconSoft.png");
            DimColor = Color.Black;
            DisplayBright = ImageSource.FromResource("Smartroom.Icons.LightIconFull.png");
            BrightColor = Color.Black;

            UpdateListeningTime(GetCountdown());

            #region web socket commands
            ///Pair Commands with Web Socket calls
            TVOnOffCommand = new Command(() => DataService.TVControls(Actions.On));
            VolumeUpCommand = new Command(() => DataService.Volume(Actions.Up, "TV"));
            VolumeDownCommand = new Command(() => DataService.Volume(Actions.Down, "TV"));
            VolumeMuteCommand = new Command(() => DataService.Volume(Actions.Off, "TV"));
            SBarVolumeUpCommand = new Command(() => DataService.Volume(Actions.Up, "SBAR"));
            SBarVolumeDownCommand = new Command(() => DataService.Volume(Actions.Down, "SBAR"));
            SBarVolumeMuteCommand = new Command(() => DataService.Volume(Actions.Off, "SBAR"));
            HDMICommand = new Command(() => DataService.TVInput(3));
            CableCommand = new Command(() => DataService.TVInput(2));
            CasterCommand = new Command(() => DataService.TVInput(1));
            TVBarCommand = new Command(() => DataService.SoundbarInput(1));
            BluetoothCommand = new Command(() => DataService.SoundbarInput(2));
            NurseRequestCommand = new Command((requestType) => DataService.CallNurse(requestType as string));
            ChannelUpCommand = new Command(() => DataService.TVControls(Actions.Up));
            ChannelDownCommand = new Command(() => DataService.TVControls(Actions.Down));
            TempUpCommand = new Command(() => DataService.Temperature(Actions.Up));
            TempDownCommand = new Command(() => DataService.Temperature(Actions.Down));
            BBlindsOpenCommand = new Command(() => DataService.BBlinds(Actions.Open));
            BBlindsUpCommand = new Command(() => DataService.BBlinds(Actions.Up));
            BBlindsDownCommand = new Command(() => DataService.BBlinds(Actions.Down));
            BBlindsClosedCommand = new Command(() => DataService.BBlinds(Actions.Close));
            IBlindsOpenCommand = new Command(() => DataService.IBlinds(Actions.Open));
            IBlindsUpCommand = new Command(() => DataService.IBlinds(Actions.Up));
            IBlindsDownCommand = new Command(() => DataService.IBlinds(Actions.Down));
            IBlindsClosedCommand = new Command(() => DataService.IBlinds(Actions.Close));
            DoorOpenCommand = new Command(() => DataService.Door(Actions.Open));
            DoorCloseCommand = new Command(() => DataService.Door(Actions.Close));
            DoorPartialCommand = new Command(() => DataService.Door(Actions.Partial));
            EntranceLightsOnCommand = new Command(() => DataService.ELights(Actions.On));
            EntranceLightsOffCommand = new Command(() => DataService.ELights(Actions.Off));
            EntranceLightsUpCommand = new Command(() => DataService.ELights(Actions.Up));
            EntranceLightsDownCommand = new Command(() => DataService.ELights(Actions.Down));
            ReadingLightsOnCommand = new Command(() => DataService.RLights(Actions.On));
            ReadingLightsOffCommand = new Command(() => DataService.RLights(Actions.Off));
            ReadingLightsUpCommand = new Command(() => DataService.RLights(Actions.Up));
            ReadingLightsDownCommand = new Command(() => DataService.RLights(Actions.Down));
            AmbientLightsOnCommand = new Command(() => DataService.ALights(Actions.On));
            AmbientLightsOffCommand = new Command(() => DataService.ALights(Actions.Off));
            AmbientLightsUpCommand = new Command(() => DataService.ALights(Actions.Up));
            AmbientLightsDownCommand = new Command(() => DataService.ALights(Actions.Down));
            ExamLightsOnCommand = new Command(() => DataService.ExLights(Actions.On));
            ExamLightsOffCommand = new Command(() => DataService.ExLights(Actions.Off));
            ExamLightsUpCommand = new Command(() => DataService.ExLights(Actions.Up));
            ExamLightsDownCommand = new Command(() => DataService.ExLights(Actions.Down));
            BathLightsOnCommand = new Command(() => DataService.BathLights(Actions.On));
            BathLightsOffCommand = new Command(() => DataService.BathLights(Actions.Off));
            BathLightsUpCommand = new Command(() => DataService.BathLights(Actions.Up));
            BathLightsDownCommand = new Command(() => DataService.BathLights(Actions.Down));
            NightSceneCommand = new Command(() => DataService.LightsPreset(0));
            ReadingSceneCommand = new Command(() => DataService.LightsPreset(2));
            TVSceneCommand = new Command(() => DataService.LightsPreset(4));
            SoftSceneCommand = new Command(() => DataService.LightsPreset(3));
            FullSceneCommand = new Command(() => DataService.LightsPreset(1));
            OriginUpCommand = new Command(() => ChangeOrigin(Actions.Up));
            OriginDownCommand = new Command(() => ChangeOrigin(Actions.Down));
            DFloorUpCommand = new Command(() => ChangeDFloor(Actions.Up));
            DFloorDownCommand = new Command(() => ChangeDFloor(Actions.Down));
            BankUpCommand = new Command(() => ChangeBank(Actions.Up));
            BankDownCommand = new Command(() => ChangeBank(Actions.Down));
            CallElevCommand = new Command(() => DataService.Elevator(1));
            CallNowCommand = new Command(() => DataService.Elevator(2));
            DisplayOffCommand = new Command(() => DataService.DisplayControl(0));
            DisplayDimCommand = new Command(() => DataService.DisplayControl(1));
            DisplayOnCommand = new Command(() => DataService.DisplayControl(2));
            ClockOffCommand = new Command(() => DataService.ClockControl(0));
            ClockLowCommand = new Command(() => DataService.ClockControl(1));
            ClockMediumCommand = new Command(() => DataService.ClockControl(2));
            ClockHighCommand = new Command(() => DataService.ClockControl(3));
            PairBluetoothCommand = new Command(() => DataService.PairBluetooth());
            SoundBarPowerOnCommand = new Command(() => DataService.SoundBarPowerCommand("ON"));
            SoundBarPowerOffCommand = new Command(() => DataService.SoundBarPowerCommand("OFF"));
            AppleSelectCommand = new Command(() => DataService.ControlApple(0));
            AppleUpCommand = new Command(() => DataService.ControlApple(1));
            AppleDownCommand = new Command(() => DataService.ControlApple(2));
            AppleLeftCommand = new Command(() => DataService.ControlApple(3));
            AppleRightCommand = new Command(() => DataService.ControlApple(4));
            ApplePlayCommand = new Command(() => DataService.ControlApple(5));
            AppleMenuCommand = new Command(() => DataService.ControlApple(6));
            GetStatusCommand = new Command(() => DataService.GetStatus());
            #endregion

            DataService.SocketConnect(this);

            _speechRecongnitionInstance = DependencyService.Get<ISpeechToText>();            //start speech recognition service to listen for hotword
            _speechRecongnitionInstance.StartSpeechToText();
            _externalIntentInstance = DependencyService.Get<IExternalIntents>();
            SetMenuItems();
            GetStatusCommand.Execute(null);
        }

        public void SetMenuItems()
        {
            ushort roomNum = RoomState.GetRoomNumber();

            //set door visibility
            if (roomNum.ToString().Equals("316")
                || (roomNum.ToString().Equals("317"))
                || (roomNum.ToString().Equals("515")))
            {
                DoorVisibility = true;
            }
            else
            {
                DoorVisibility = false;
            }

            //set nurse visibility
            if ((roomNum >= 300 && roomNum <= 325)
                || roomNum == 515
                || roomNum == 516)
            {
                NurseVisibility = true;
            }
            else
            {
                NurseVisibility = false;
            }
        }
        public void SetRoomNumber(ushort num)
        {
            RoomState.SetRoomNumber(num);
        }

        public void SelectMedia()
        {
            master.Sidebar.ChangePageFromVoiceCommand("Media");
        }
        public void SelectDoor()
        {
            master.Sidebar.ChangePageFromVoiceCommand("Door");
        }
        public void SelectBlinds()
        {
            master.Sidebar.ChangePageFromVoiceCommand("Blinds");
        }
        public void SelectLights()
        {
            master.Sidebar.ChangePageFromVoiceCommand("Lights");
        }
        public void SelectClimate()
        {
            master.Sidebar.ChangePageFromVoiceCommand("Climate");
        }
        public void SelectNurse()
        {
            master.Sidebar.ChangePageFromVoiceCommand("Nurse");
        }
        public void SelectOption()
        {
            master.Sidebar.ChangePageFromVoiceCommand("Options");
        }
        public void InitializeMaster(MasterPage sent)
        {
            if (master == null)
            {
                master = sent;
            }
        }

        public void UpdateChannelList(string CenterType)
        {
            RoomState.SetCableMode(CenterType);
        }
        public void SetHotword(string newHotword)
        {
            RoomState.SetHotword(newHotword);
        }

        public void ToggleVoice(bool toEnable)
        {
            RoomState.SetVoiceMuted(toEnable);

            if (toEnable)
                _speechRecongnitionInstance.StopSpeechToText();
            else
                _speechRecongnitionInstance.StartSpeechToText();
        }

        /// <summary>
        /// handle speech messages from the speech recognition service
        /// </summary>
        public void VoiceControl()
        {
            MessagingCenter.Subscribe<ISpeechToText, string>(this, "STT", async (sender, args) =>
            {
                if (args.ToLower().Equals(Hotword)&&!listenFlag)                         // if you heard hotword
                {
                    Message = "...";

                    master.ShowVoiceOverlay();

                    listenFlag = true;                                        // Pay attention to what comes next           
                    elevFlag = false;

                    if (RoomState.GetSpeech())
                    {
                        Thread.Sleep(1500);                                   // play speech sample, sleeps for audio timing
                        _speechRecongnitionInstance.PlayYesSpeech();
                        Thread.Sleep(1000);
                    }
                    else                                                      // play sound for indication of listening
                    {
                        _speechRecongnitionInstance.PlayYesSound();
                    }

                    using (StreamWriter sw = File.AppendText(logFileName))
                    {
                        DateTime now = DateTime.Now;
                        sw.WriteLine(now.ToString() + ": Hotword recognized");
                    }
                }
                else if (args.ToLower().Contains("netflix") && listenFlag)
                {
                    _externalIntentInstance.OpenExternalApp();
                }
                else if (listenFlag)                                          // handle speech response after hotwork heard
                {
                    using (StreamWriter sw = File.AppendText(logFileName))                  //log text
                    {
                        DateTime now = DateTime.Now;
                        sw.WriteLine(now.ToString() + ": Received \"" + args + "\" from voice recognizer");
                    }

                    if (args.Equals("_") || args.Equals("No"))                  //Handle speech errors
                    {
                        listenFlag = false;
                        Message = "Sorry, Timed Out";                         // if speech error, play "bad Sound"
                        _speechRecongnitionInstance.PlayNoSound();
                        await master.HideVoiceOverlay();
                    }
                    else if (args.ToLower().Equals("start elevator") || elevFlag)       //for elevator control state
                    {
                        string dresponse;
                        elevFlag = true;

                        if (args.ToLower().Equals("start elevator"))
                        {
                            dresponse = await DataService.QueryDialogFlowAsync(args); //notfiy middleware elevator dialog start

                        }
                        else if (args.ToLower().Equals("never mind") || args.ToLower().Equals("cancel"))
                        {
                            dresponse = await DataService.ElevDialogFlowAsync(args);
                            _speechRecongnitionInstance.PlayNoSound();
                            _speechRecongnitionInstance.SetSTTFlag(false);
                        }
                        else if (elevNum != 2)
                        {
                            dresponse = await DataService.ElevDialogFlowAsync(args);   //send value to middleware

                        }
                        else if (elevNum == 2 && (args.ToLower().Equals("one") || args.ToLower().Equals("two") || args.ToLower().Equals("three") ||
                        args.ToLower().Equals("four") || args.ToLower().Equals("five") || args.ToLower().Equals("lower level one") || args.ToLower().Equals("lower one")))
                        {
                            dresponse = await DataService.ElevDialogFlowAsync(args);
                            _speechRecongnitionInstance.PlayOKSound();
                            _speechRecongnitionInstance.SetSTTFlag(false);
                        }

                        else
                        {
                            dresponse = await DataService.ElevDialogFlowAsync(args);
                        }
                    }
                    else
                    {
                        listenFlag = false;
                        string dresponse = await DataService.QueryDialogFlowAsync(args);
                        Console.WriteLine(dresponse);
                        _speechRecongnitionInstance.PlayOKSound();
                        Message = args + ".";
                        await master.HideVoiceOverlay();
                    }
                }

                if (!RoomState.GetMute())
                {
                    _speechRecongnitionInstance = DependencyService.Get<ISpeechToText>();
                    _speechRecongnitionInstance.StartSpeechToText();
                }
                //restart speech recognizer                
            });

            MessagingCenter.Subscribe<ISpeechToText, string>(this, "VTT", async (sender, args) =>
            {
                if (args.StartsWith("Mac") || args.StartsWith("Mack") || args.StartsWith("Sam") && listenFlag)             //removes confusing MAc and Sam from start of message
                    args.Remove(0, 3);

                if (listenFlag)                                                 //displays in progress recognized string
                    Message = args + "...";
            });
        }

        public void ReconnectSocket()
        {
            DataService.SocketConnect(this);
        }

        /// <summary>
        /// Change room state settings for the origin floor elevator controls
        /// from (1-10)
        /// </summary>
        /// <param name="act">(Actions) Direction to change </param>
        private void ChangeOrigin(Actions act)
        {
            if (act.Equals(Actions.Up) && RoomState.GetElevOriginFloor() != Constants.maxFloor)
            {
                RoomState.SetElevOriginFloor(RoomState.GetElevOriginFloor() + 1);
            }
            else if (act.Equals(Actions.Down) && RoomState.GetElevOriginFloor() != Constants.minFloor)
            {
                RoomState.SetElevOriginFloor(RoomState.GetElevOriginFloor() - 1);
            }

            OriginFloor = RoomState.GetElevOriginFloor().ToString();

            if (RoomState.GetElevOriginFloor() == 0)
            {
                OriginFloor = "LL1";
            }
        }

        public void ChannelWhiteout()
        {
            C27 = Color.Transparent;
        }

        /// <summary>
        /// Controls the voice service and plays speech based audio
        /// </summary>
        /// <param name="speechNum"></param>
        public void PlaySpeech(int speechNum)
        {
            _speechRecongnitionInstance.StopSpeechToText();
            switch (speechNum)
            {
                case 1:
                    Thread.Sleep(800);
                    _speechRecongnitionInstance.PlaySpeech(speechNum);
                    Thread.Sleep(1500);
                    break;
                case 2:
                    Thread.Sleep(800);
                    _speechRecongnitionInstance.PlaySpeech(speechNum);
                    Thread.Sleep(1500);
                    break;
                case 3:
                    Thread.Sleep(800);
                    _speechRecongnitionInstance.PlaySpeech(speechNum);
                    Thread.Sleep(1500);
                    break;
                case 4:
                    Thread.Sleep(800);
                    _speechRecongnitionInstance.PlaySpeech(speechNum);
                    Thread.Sleep(1500);
                    break;
                case 5:
                    Thread.Sleep(800);
                    _speechRecongnitionInstance.PlaySpeech(speechNum);
                    Thread.Sleep(1500);
                    break;
            }
            _speechRecongnitionInstance.StartSpeechToText();
        }

        /// <summary>
        /// Change room state settings for the destination floor elevator controls
        /// from (1-10)       
        /// </summary>
        /// <param name="act">(Actions) Direction to change </param>
		private void ChangeDFloor(Actions act)
        {
            if (act.Equals(Actions.Up) && RoomState.GetElevDestFloor() != Constants.maxFloor)
            {
                RoomState.SetElevDestFloor(RoomState.GetElevDestFloor() + 1);
            }
            else if (act.Equals(Actions.Down) && RoomState.GetElevDestFloor() != Constants.minFloor)
            {
                RoomState.SetElevDestFloor(RoomState.GetElevDestFloor() - 1);
            }

            DestFloor = RoomState.GetElevDestFloor().ToString();

            if (RoomState.GetElevDestFloor() == 0)
            {
                DestFloor = "LL1";
            }
        }

        /// <summary>
        /// Change room state settings for the elevator bank elevator controls
        /// from (1-5)
        /// </summary>
        /// <param name="act">(Actions) Direction to change </param>
        private void ChangeBank(Actions act)
        {
            if (act.Equals(Actions.Up) && RoomState.GetElevBank() != Constants.maxBank)
            {
                RoomState.SetElevBank(RoomState.GetElevBank() + 1);
            }
            else if (act.Equals(Actions.Down) && RoomState.GetElevBank() != Constants.minBank)
            {
                RoomState.SetElevBank(RoomState.GetElevBank() - 1);
            }

            ElevBank = RoomState.GetElevBank();
        }


        /// <summary>
        /// calls a TV channel web request
        /// </summary>
        /// <param name="channel">(int) channel to change to</param>
        public void ChangeChannel(int channel)
        {
            DataService.TVChannel(channel);
        }

        /// <summary>
        /// change speech countdown setting
        /// </summary>
        /// <param name="newValue">new value to set it to</param>
        public void UpdateListeningTime(int countdown)
        {
            RoomState.SetCountdown(countdown);
            SetOptionsListeningIcon(countdown);
        }

        public void SetOptionsListeningIcon(int countdown)
        {
            if (countdown == 1)
                OptionListeningIcon = ImageSource.FromResource("Smartroom.Icons.OptionListen1s.png");
            else if (countdown == 3)
                OptionListeningIcon = ImageSource.FromResource("Smartroom.Icons.OptionListen3s.png");
            else if (countdown == 5)
                OptionListeningIcon = ImageSource.FromResource("Smartroom.Icons.OptionListen5s.png");
        }

        public int getRoomNum()
        {
            return RoomState.GetRoomNumber();
        }

        public void SetElev(bool toSet)
        {
            _speechRecongnitionInstance.SetElevFlag(toSet);
        }

        /// <summary>
        /// notifies the view that data has changed and should reload the element
        /// </summary>
        /// <param name="propertyName">element to change</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string GetMediaInputSelected()
        {
            return RoomState.GetTVInput();
        }

        public string GetCableTypeSelected()
        {
            return RoomState.GetCableMode();
        }

        public string GetLightsPage()
        {
            return RoomState.GetLightsPage();
        }
        public int GetCountdown()
        {
            return RoomState.GetCountdown();
        }
        public void UpdateMediaInputSelected(string InputName)
        {
            RoomState.SetTVInput(InputName);
        }

        public void UpdateLightsPageSelected(string LightsPageSelected)
        {
            RoomState.SetLightsPage(LightsPageSelected);
        }
        public void UpdateLightsSceneSelected(string LightsSceneSelected)
        {
            RoomState.SetLightsScene(LightsSceneSelected);
        }

        internal void setSoundBarPin(string newSoundBarPin)
        {
            SoundBarPin = newSoundBarPin;
        }
        public void UpdateClockBrightness(string newBrightness)
        {
            ClockBrightness = newBrightness;
        }
    }
}