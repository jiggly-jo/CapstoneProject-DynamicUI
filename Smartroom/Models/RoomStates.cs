using System;
using Smartroom.Helpers;

namespace Smartroom.Models
{
    /// <summary>
    /// Class to keep track of every setting in the paired room. 
    /// </summary>
    public class RoomState
    {
        private static ushort RoomNumber;                                       // number of the room this app is controlling
        private static ushort RoomPin;

        private static int SpeechCountdown = 1;                                 // setting for speech delay
        private static bool VoiceMuted = false;

        private static bool EntranceLightsOn;                                   // On/Off status and intenisty settings for lights
        private static int EntranceLightsSetting;
        private static bool ReadingLightsOn;
        private static int ReadingLightsSetting;
        private static bool AmbientLightsOn;
        private static int AmbientLightsSetting;
        private static bool BathroomLightsOn;
        private static int BathroomLightsSetting;
        private static bool ExamLightsOn;
        private static int ExamLightsSetting;

        private static string LightsPage = "LightsSimple";
        private static string LightsSceneSelected = "None";

        private static bool SpokenSpeech;                                       // setting for speech or noise response

        private static string TVInput = "CableMedia";
        private static string CableMode = "NumberPad";
        private static string Hotword = "Mac";

        private static int Volume = 13;                                         // Source, volume, and mute status for the soundbar    
        private static bool Muted;

        private static int BBlinds;                                             // setting of the two blinds, 10 = Fully raised, 0 = completly lowered 
        private static int IBlinds;

        private static double SettingTemp = 70;                                 //default Temp setting.  (should be overwriten on connection to middleare)

        private static int ElevOriginFloor = 1;                                 // elevator call settngs
        private static int ElevDestFloor = 1;
        private static int ElevBank = 3;
        private static int ElevTime = 10;

        static RoomState()
        {

        }


        public static ushort GetRoomNumber()                                    // Get/Set Room Number
        {
            return RoomNumber;
        }
        public static void SetRoomNumber(ushort newRoom)
        {
            RoomNumber = newRoom;
        }


        public static ushort GetRoomPin()                                       // Get/Set Room pin
        {
            return RoomPin;
        }
        public static void SetRoomPin(ushort newPin)
        {
            RoomPin = newPin;
        }


        public static int GetCountdown()                                        // Get/Set speech delay
        {
            return SpeechCountdown;
        }
        public static void SetCountdown(int newValue)
        {
            if (newValue != 1 && newValue != 3 && newValue != 5)
            {
                Console.WriteLine("ERROR: tried to set RoomState Countdown to something other than 1, 3 or 5.");
                //only 1, 3 or 5 currently supported
                throw new ArgumentOutOfRangeException();
            }
            else
                SpeechCountdown = newValue;
        }


        public static bool GetSpeech()                                          // Get/Set speech toggle
        {
            return SpokenSpeech;
        }
        public static void SetSpeech(bool newValue)
        {
            SpokenSpeech = newValue;
        }

        public static string GetHotword()                                       // Get/Set TV Input Source (Cable, AUX, Caster)
        {
            return Hotword;
        }
        public static void SetHotword(string newHotword)
        {
            Hotword = newHotword;
        }

        public static string GetTVInput()                                       // Get/Set TV Input Source (Cable, AUX, Caster)
        {
            return TVInput;
        }
        public static void SetTVInput(string newInput)
        {
            TVInput = newInput;
        }


        public static int GetVolume()                                           // Get/Set Volume Setting  (0-20, 0 = Off) 
        {
            return Volume;
        }
        public static void SetVolume(bool UpDown)
        {
            if (UpDown)
            {
                if (Volume < 75)
                {
                    Volume += 5;
                }
            }
            else
            {
                if (Volume > 0)
                {
                    Volume += 5;
                }
            }
        }
        public static void SetVolume(int Value)
        {
            Volume = Value;
        }


        public static bool GetMute()                                            // Get/Set Mute Status (true = mute, false = no mute)
        {
            return Muted;
        }
        public static void SetMute(bool value)
        {
            Muted = value;
        }


        public static bool GetEntranceLightsOn()                                // Get/Set Entrance lights status  (Off = False, On = True)
        {
            return EntranceLightsOn;
        }
        public static void SetEntranceLightsOn(bool state)
        {
            EntranceLightsOn = state;
        }


        public static int GetEntranceLightsSetting()                            // Get/Set Entrance Lights Setting  (0-10, 0 = Off)
        {
            return EntranceLightsSetting;
        }
        public static void SetEntranceLightsSetting(bool UpDown)
        {
            if (EntranceLightsOn)
            {
                if (UpDown)
                {
                    if (EntranceLightsSetting < 10)
                    {
                        EntranceLightsSetting++;
                    }
                }
                else
                {
                    if (EntranceLightsSetting > 0)
                    {
                        EntranceLightsSetting--;
                    }
                }
            }
        }
        public static void SetEntranceLightsSetting(int newSetting)
        {
            if (newSetting <= 10 && newSetting >= 0)
            {
                EntranceLightsSetting = newSetting;
            }
        }


        public static bool GetReadingLightsOn()                                 // Get/Set reading  lights status (Off = False, On = True)
        {
            return ReadingLightsOn;
        }
        public static void SetReadingLightsOn(bool state)
        {
            ReadingLightsOn = state;
        }


        public static int GetReadingLightsSetting()                             // Get/Set Reading Lights Setting  (0-10, 0 = Off)                                                         
        {
            return ReadingLightsSetting;
        }
        public static void SetReadingLightsSetting(bool UpDown)
        {
            if (ReadingLightsOn)
            {
                if (UpDown)
                {
                    if (ReadingLightsSetting < 10)
                    {
                        ReadingLightsSetting++;
                    }
                }
                else
                {
                    if (ReadingLightsSetting > 0)
                    {
                        ReadingLightsSetting--;
                    }
                }
            }
        }
        public static void SetReadingLightsSetting(int newSetting)
        {
            if (newSetting <= 10 && newSetting >= 0)
            {
                ReadingLightsSetting = newSetting;
            }
        }


        public static bool GetAmbientLightsOn()                                 // Get/Set  Ambient lights status (Off = False, On = True)
        {
            return AmbientLightsOn;
        }
        public static void SetAmbientLightsOn(bool state)
        {
            AmbientLightsOn = state;
        }


        public static int GetAmbientLightsSetting()                             //  Get/Set    Ambient Lights Setting  (0-10, 0 = Off) 
        {
            return AmbientLightsSetting;
        }
        public static void SetAmbientLightsSetting(bool UpDown)
        {
            if (AmbientLightsOn)
            {
                if (UpDown)
                {
                    if (AmbientLightsSetting < 10)
                    {
                        AmbientLightsSetting++;
                    }
                }
                else
                {
                    if (AmbientLightsSetting > 0)
                    {
                        AmbientLightsSetting--;
                    }
                }
            }
        }
        public static void SetAmbientLightsSetting(int newSetting)
        {
            if (newSetting <= 10 && newSetting >= 0)
            {
                AmbientLightsSetting = newSetting;
            }
        }


        public static bool GetBathroomLightsOn()                                 // Get/Set  Bathroom lights status (Off = False, On = True)
        {
            return BathroomLightsOn;
        }
        public static void SetBathroomLightsOn(bool state)
        {
            BathroomLightsOn = state;
        }


        public static int GetBathroomLightsSetting()                             //  Get/Set    Bathroom Lights Setting  (0-10, 0 = Off) 
        {
            return BathroomLightsSetting;
        }
        public static void SetBathroomLightsSetting(bool UpDown)
        {
            if (BathroomLightsOn)
            {
                if (UpDown)
                {
                    if (BathroomLightsSetting < 10)
                    {
                        BathroomLightsSetting++;
                    }
                }
                else
                {
                    if (BathroomLightsSetting > 0)
                    {
                        BathroomLightsSetting--;
                    }
                }
            }
        }
        public static void SetBathroomLightsSetting(int newSetting)
        {
            if (newSetting <= 10 && newSetting >= 0)
            {
                BathroomLightsSetting = newSetting;
            }
        }

        public static bool GetVoiceMuted()
        {
            return VoiceMuted;
        }
        public static void SetVoiceMuted(bool toSet)
        {
            VoiceMuted = toSet;
        }

        public static bool GetExamLightsOn()                                 // Get/Set  Exam lights status (Off = False, On = True)
        {
            return ExamLightsOn;
        }
        public static void SetExamLightsOn(bool state)
        {
            ExamLightsOn = state;
        }


        public static int GetExamLightsSetting()                             //  Get/Set    Exam Lights Setting  (0-10, 0 = Off) 
        {
            return ExamLightsSetting;
        }
        public static void SetExamLightsSetting(bool UpDown)
        {
            if (ExamLightsOn)
            {
                if (UpDown)
                {
                    if (ExamLightsSetting < 10)
                    {
                        ExamLightsSetting++;
                    }
                }
                else
                {
                    if (ExamLightsSetting > 0)
                    {
                        ExamLightsSetting--;
                    }
                }
            }
        }
        public static void SetExamLightsSetting(int newSetting)
        {
            if (newSetting <= 10 && newSetting >= 0)
            {
                ExamLightsSetting = newSetting;
            }
        }

        public static int GetBBlinds()                                          // Get/Set Blackout Blinds Setting  (0-10,  0 = all way down, 10 = all way up)
        {
            return BBlinds;
        }
        public static void SetBBlinds(int newValue)
        {
            BBlinds = newValue;
        }


        public static int GetIBlinds()                                          // Get/Set Interoir Blinds Setting  (0-10,  0 = all way down, 10 = all way up)
        {
            return IBlinds;
        }
        public static void SetIBlinds(int newValue)
        {
            IBlinds = newValue;
        }


        public static double GetSettingTemp()                                      // Get/Set Temp setting
        {
            return SettingTemp;
        }
        public static void SetSettingTemp(bool UpDown)
        {
            if (UpDown)
            {
                if (SettingTemp < Constants.maxTemp)
                {
                    SettingTemp += .5;
                }
            }
            else
            {
                if (SettingTemp > Constants.minTemp)
                {
                    SettingTemp -= .5;
                }
            }
        }
        public static void SetSettingTemp(double value)
        {
            if (value <= Constants.maxTemp && value >= Constants.minTemp)
            {
                SettingTemp = value;
            }
        }

        
        public static string GetCableMode()
        {
            return CableMode;
        }
        public static void SetCableMode(string NewCableMode)
        {
            CableMode = NewCableMode;
        }

        public static int GetElevOriginFloor()                                  // get/set Origin Floor of elevator call
        {
            return ElevOriginFloor;
        }
        public static void SetElevOriginFloor(int newFloor)
        {
            ElevOriginFloor = newFloor;
        }

        public static int GetElevDestFloor()                                    // get/set Destination floor of elevator call
        {
            return ElevDestFloor;
        }
        public static void SetElevDestFloor(int newFloor)
        {
            ElevDestFloor = newFloor;
        }

        public static int GetElevBank()                                         // get/set elevator bank of elevator call 
        {
            return ElevBank;
        }
        public static void SetElevBank(int newBank)
        {
            ElevBank = newBank;
        }

        public static int GetElevTime()                                         // get/set elevator Time of elevator call 
        {
            return ElevTime;
        }
        public static void SetElevTime(int newTime)
        {
            ElevTime = newTime;
        }

        internal static string GetLightsPage()
        {
            return LightsPage;
        }

        internal static void SetLightsPage(string NewLightsPageSelected)
        {
            LightsPage = NewLightsPageSelected;
        }

        internal static void SetLightsScene(string NewLightsSceneSelected)
        {
            LightsSceneSelected = NewLightsSceneSelected;
        }

        internal static string getLightsSceneSelected()
        {
            return LightsSceneSelected;
        }
    }
}