using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;
using Smartroom.Models;
using System.Diagnostics;
using System.Collections.Generic;
using Smartroom.Views;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Smartroom.ViewModels
{
    public class DynamicPageViewModel
    {
        MasterPage super;
        public DynamicPageViewModel(MasterPage s)
        {
            super = s;
        }

        public List<RoomButtonCategory> SortByFrequency()
        {
            using (var db = new UHospitalContext())
            {
                var query = from cl in db.clicks
                            join btn in db.roomButton
                            on cl.ButtonID equals btn.id
                            join rC in db.roomButtonCategory
                            on btn.Category equals rC.id
                            where cl.RoomNumber == super.Controller.getRoomNum() && rC.WidgetXName != "NA"
                            group rC by new
                            {
                                ID = rC.id,
                                WidgetXName = rC.WidgetXName,
                                FrameFile = rC.FrameFile,
                                HeightRatio = rC.HeightRatio,
                                WidthRatio = rC.WidthRatio,
                                HumanReadableName = rC.HumanReadableName
                            }
                        into roomCateGroups
                            select new
                            {
                                item = roomCateGroups.Key,
                                count = roomCateGroups.Count()
                            } into temp
                            orderby temp.count descending
                            select temp;
                Array a = query.ToArray();
                List < RoomButtonCategory > l = new List<RoomButtonCategory>();
                foreach (var r in query.ToArray())
                {
                    l.Add(db.roomButtonCategory.FirstOrDefault(i => i.id == r.item.ID));
                }
                return l;
            }
        }

        public List<RoomButtonCategory> SortByRecentlyUsed()
        {
            using (var db = new UHospitalContext())
            {
                //var query = from cl in db.clicks
                //            select cl;
                var query = from cl in db.clicks
                            join btn in db.roomButton
                            on cl.ButtonID equals btn.id
                            join rC in db.roomButtonCategory
                            on btn.Category equals rC.id
                            where cl.RoomNumber == super.Controller.getRoomNum() && rC.WidgetXName != "NA"
                            select new
                            {
                                ID = rC.id,
                                WidgetXName = rC.WidgetXName,
                                FrameFile = rC.FrameFile,
                                HeightRatio = rC.HeightRatio,
                                WidthRatio = rC.WidthRatio,
                                TimePressed = cl.TimePressed,
                                HumanReadableName = rC.HumanReadableName
                            }
                           into temp
                            orderby temp.TimePressed descending
                            select temp;
                //join rC2 in db.roomButtonCategory
                //           on final.Key.WidgetXName equals rC2.WidgetXName
                //           select rC2
                //Array a = query.ToArray();
                List<RoomButtonCategory> l = new List<RoomButtonCategory>();
                foreach (var r in query.ToArray())
                {
                    if (!l.Contains(db.roomButtonCategory.FirstOrDefault(i => i.id == r.ID)))
                        l.Add(db.roomButtonCategory.FirstOrDefault(i => i.id == r.ID));
                }
                return l;
            }
        }
        public List<RoomButtonCategory> SortByTimeOfDayMostUsed()
        {
  
            int now = DateTime.Now.Hour;


            using (var db = new UHospitalContext())
            {


                var query = from cl in db.clicks
                            where cl.TimePressed.Hour == now
                            join btn in db.roomButton
                            on cl.ButtonID equals btn.id
                            join rC in db.roomButtonCategory
                            on btn.Category equals rC.id
                            where cl.RoomNumber == super.Controller.getRoomNum() && rC.WidgetXName != "NA" 
                            group rC by new
                            {
                                ID = rC.id,
                                WidgetXName = rC.WidgetXName,
                                FrameFile = rC.FrameFile,
                                HeightRatio = rC.HeightRatio,
                                WidthRatio = rC.WidthRatio,
                                HumanReadableName = rC.HumanReadableName
                            }
                        into roomCateGroups
                            select new
                            {
                                item = roomCateGroups.Key,
                                count = roomCateGroups.Count()
                            } into temp
                            orderby temp.count descending
                            select temp;
                List<RoomButtonCategory> l = new List<RoomButtonCategory>();
                foreach (var r in query.ToArray())
                {
                    l.Add(db.roomButtonCategory.FirstOrDefault(i => i.id == r.item.ID));
                }
                return l.Count == 0 ? this.SortByFrequency() : l;
            }
        }

        public  bool IsInTimeRange(DateTime obj, DateTime timeRangeFrom, DateTime timeRangeTo)
        {
            TimeSpan time = obj.TimeOfDay, t1From = timeRangeFrom.TimeOfDay, t1To = timeRangeTo.TimeOfDay;

            // if the time from is smaller than the time to, just filter by range
            if (t1From <= t1To)
            {
                return time >= t1From && time <= t1To;
            }

            // time from is greater than time to so two time intervals have to be created: one {timeFrom-12AM) and another one {12AM to timeTo}
            TimeSpan t2From = TimeSpan.MinValue, t2To = t1To;
            t1To = TimeSpan.MaxValue;

            return (time >= t1From && time <= t1To) || (time >= t2From && time <= t2To);
        }

        /*
         * Method retrieves the button groups to be displayed by accessing DB 
         */
        public List<ButtonGroup> GetButtonGroups()
        {

            using (var db = new UHospitalContext())
            {
                //TODO fix this  

                var query = from rC in db.roomButtonCategory
                            select rC;
                if (query.ToArray().Length == 0)
                    SeedDatabase();
                var q2 = from rB in db.roomButton
                         select rB;

                List<RoomButtonCategory> a = new List<RoomButtonCategory>();
                if (this.super.SortType == "Most Recently Clicked")
                {
                    a = SortByRecentlyUsed();
                }
                else if (this.super.SortType == "Most Frequently Used")
                {
                    a = SortByFrequency();
                }
                else if (this.super.SortType == "Time of Day Sort")
                {
                    a = SortByTimeOfDayMostUsed();
                }

                // Construct each widget in list to be injected by dynamic view 
                List<ButtonGroup> groups = new List<ButtonGroup>();
                ButtonGroup bg;
                MediaPageMaster mediaMaster = new MediaPageMaster(super);
                int labelOffset = 5;
                int margin = 20;
                foreach (var buttongroup in a)
                {
                    bg = null;
                    switch ((buttongroup.WidgetXName, buttongroup.FrameFile))
                    {
                        case ("ChannelFrame", "MediaCableLeft"):
                            var e = (Forms9Patch.Frame)new MediaCableLeft(super).FindByName(buttongroup.WidgetXName);
                            e.Margin = new Thickness(margin);
                            //e.Scale = this.super.scaleValue;
                            bg = new ButtonGroup(e, buttongroup.WidthRatio, buttongroup.HeightRatio + labelOffset);
                            break;
                        case ("SourceFrame", "MediaPageMaster"):
                            var e1 = (Forms9Patch.Frame)new MediaPageMaster(super).FindByName(buttongroup.WidgetXName);
                            e1.Margin = new Thickness(margin);
                            //e1.Scale = this.super.scaleValue;
                            bg = new ButtonGroup(e1, buttongroup.WidthRatio, buttongroup.HeightRatio + labelOffset);
                            break;
                        case ("NumberFrame", "MediaPageNumber"):
                            var e2 = (Forms9Patch.Frame)new MediaPageNumber(super, mediaMaster).FindByName(buttongroup.WidgetXName);
                            e2.Margin = new Thickness(margin);
                            //e2.Scale = this.super.scaleValue;
                            bg = new ButtonGroup(e2, buttongroup.WidthRatio, buttongroup.HeightRatio + labelOffset);
                            break;
                        case ("ControlFrame2", "OptionsPage"):
                            var e3 = (Frame)new OptionsPage(super).FindByName(buttongroup.WidgetXName);
                            e3.Margin = new Thickness(margin);
                            //e3.Scale = this.super.scaleValue;
                            bg = new ButtonGroup(e3, buttongroup.WidthRatio, buttongroup.HeightRatio + labelOffset);
                            break;
                        case ("ArrowFrame", "MediaPageArrows"):
                            Forms9Patch.Frame f_arrow = (Forms9Patch.Frame)new MediaPageArrows(super).FindByName(buttongroup.WidgetXName);
                            f_arrow.Margin = new Thickness(margin);
                            //f_arrow.Scale = this.super.scaleValue;
                            bg = new ButtonGroup(f_arrow, buttongroup.WidthRatio, buttongroup.HeightRatio + labelOffset);
                            break;
                        case ("PlayPauseFrame", "MediaAppleLeft"):
                        case ("MenuFrame", "MediaAppleLeft"):
                            Forms9Patch.Frame f = (Forms9Patch.Frame)new MediaAppleLeft(super).FindByName(buttongroup.WidgetXName);
                            f.Margin = new Thickness(margin);
                            //f.Scale = this.super.scaleValue;
                            bg = new ButtonGroup(f, buttongroup.WidthRatio, buttongroup.HeightRatio + labelOffset);
                            break;
                        case ("RoomFrame", "MediaPageAudio"):
                            var e4 = (Forms9Patch.Frame)new MediaPageAudio(super, mediaMaster).FindByName(buttongroup.WidgetXName);
                            e4.Margin = new Thickness(margin);
                            //e4.Scale = this.super.scaleValue;
                            bg = new ButtonGroup(e4, buttongroup.WidthRatio, buttongroup.HeightRatio + labelOffset);
                            break;
                        case ("VolumeFrame", "MediaSound"):
                            var e5 = (Forms9Patch.Frame)new MediaSound(super, "SBAR").FindByName(buttongroup.WidgetXName);
                            e5.Margin = new Thickness(margin);
                            //e5.Scale = this.super.scaleValue;
                            bg = new ButtonGroup(e5, buttongroup.WidthRatio, buttongroup.HeightRatio + labelOffset);
                            break;
                        case ("SceneFrame", "LightsPage"):
                        case ("AmbientFrame", "LightsPage"):
                        case ("EntranceFrame", "LightsPage"):
                        case ("ReadingFrame", "LightsPage"):
                        case ("BathFrame", "LightsPage"):
                        case ("ExamFrame", "LightsPage"):
                            var e6 = (Frame)new LightsPage(super).FindByName(buttongroup.WidgetXName);
                            e6.Margin = new Thickness(margin);
                            //e6.Margin = new Thickness(margin * this.super.scaleValue, margin * this.super.scaleValue + 15, margin * this.super.scaleValue, margin * this.super.scaleValue);
                            //e6.Scale = this.super.scaleValue;
                            bg = new ButtonGroup(e6, buttongroup.WidthRatio, buttongroup.HeightRatio + labelOffset);
                            break;
                        case ("ControlFrame", "TempPage"):
                            var e7 = (Frame)new TempPage(super).FindByName(buttongroup.WidgetXName);
                            e7.Margin = new Thickness(margin);
                            //e7.Scale = this.super.scaleValue;
                            bg = new ButtonGroup(e7, buttongroup.WidthRatio, buttongroup.HeightRatio + labelOffset);
                            break;
                        case ("ControlFrame", "BlindsPage"):
                            var e8 = (Frame)new BlindsPage(super).FindByName(buttongroup.WidgetXName);
                            e8.Margin = new Thickness(margin);
                            //e8.Scale = this.super.scaleValue;
                            bg = new ButtonGroup(e8, buttongroup.WidthRatio, buttongroup.HeightRatio + labelOffset);
                            break;
                    
                    }
                    bg.label = buttongroup.HumanReadableName;
                    groups.Add(bg);

                }
                return groups;
            }

        }

        /*
         * Testing method to generate a few button groups to be displayed 
         * 
         * Method adds Exam Light Widget, Temperature Control Widget, and Channel Control Widget to list 
         */
        public List<ButtonGroup> TestSet1()
        {
            List<ButtonGroup> groups = new List<ButtonGroup>();
            ButtonGroup bg;
            MediaPageMaster mediaMaster = new MediaPageMaster(super);

            var element = (Frame)new LightsPage(super).FindByName("ExamFrame");
            element.Margin = new Thickness(12);

            bg = new ButtonGroup(element, 20, 56);
            groups.Add(bg);

            var element2 = (Frame)new TempPage(super).FindByName("ControlFrame");
            element2.Margin = new Thickness(0);
            bg = new ButtonGroup(element2, 51, 85);
            groups.Add(bg);

            var element3 = (Forms9Patch.Frame)new MediaCableLeft(super).FindByName("ChannelFrame");
            element3.Margin = new Thickness(0);
            bg = new ButtonGroup(element3, 22, 67);
            groups.Add(bg);

            return groups;
        }

        /*
         * Testing method to generate a few button groups to be displayed 
         * 
         * Method adds Media Source Widget, Apple Remote Widget, and both Apple Play/Pause and Menu Widgets to list 
         */
        public List<ButtonGroup> TestSet2()
        {
            List<ButtonGroup> groups = new List<ButtonGroup>();
            ButtonGroup bg;
            MediaPageMaster mediaMaster = new MediaPageMaster(super);

            bg = new ButtonGroup((Frame)mediaMaster.FindByName("SourceFrame"), 100, 23);
            groups.Add(bg);

            bg = new ButtonGroup((Frame)new MediaPageArrows(super).FindByName("ArrowFrame"), 51, 67);
            groups.Add(bg);

            bg = new ButtonGroup((Frame)new MediaAppleLeft(super).FindByName("PlayPauseFrame"), 22, 22);
            groups.Add(bg);

            bg = new ButtonGroup((Frame)new MediaAppleLeft(super).FindByName("MenuFrame"), 22, 22);
            groups.Add(bg);

            return groups;
        }

        public void SeedDatabase()
        {
            using (var db = new UHospitalContext())
            {
                AddRoomButtonCategory("ChannelFrame", "MediaCableLeft", 22, 67, "TV", "Channel Up Down");
                AddRoomButtonCategory("SourceFrame", "MediaPageMaster", 100, 23, "TV", "Change TV Source");
                AddRoomButtonCategory("NumberFrame", "MediaPageNumber", 51, 67, "TV", "Channel Key Pad");
                AddRoomButtonCategory("ControlFrame2", "OptionsPage", 100, 23, "Ingo Board", "Change Options");
                AddRoomButtonCategory("ArrowFrame", "MediaPageArrows", 51, 67, "AppleTV", "Media Controls");
                AddRoomButtonCategory("PlayPauseFrame", "MediaAppleLeft", 22, 22, "AppleTV", "Play/Pause");
                AddRoomButtonCategory("MenuFrame", "MediaAppleLeft", 22, 22, "AppleTV", "Go To Menu");
                AddRoomButtonCategory("RoomFrame", "MediaPageAudio",51,67, "Soundbar", "Room Audio");
                AddRoomButtonCategory("VolumeFrame", "MediaSound", 23, 67, "Soundbar", "Volume Up Down");
                AddRoomButtonCategory("SceneFrame", "LightsPage", 100, 19, "Lighting", "Scene Light");
                AddRoomButtonCategory("AmbientFrame", "LightsPage", 20, 56, "Lighting", "Ambient Light");
                AddRoomButtonCategory("EntranceFrame", "LightsPage", 20, 56, "Lighting", "Entrance Light");
                AddRoomButtonCategory("ReadingFrame", "LightsPage", 20, 56, "Lighting", "Reading Light");
                AddRoomButtonCategory("BathFrame", "LightsPage", 20, 56, "Lighting", "Bath Light");
                AddRoomButtonCategory("ExamFrame", "LightsPage", 20, 56, "Lighting", "Exam Light");
                AddRoomButtonCategory("ControlFrame", "TempPage", 51, 85, "HVAC", "");
                AddRoomButtonCategory("ControlFrame", "TempPage", 100, 85, "Blinds", "Blind Controller");
                AddRoomButtonCategory("NA", "NA",0,0, "NA", "Null Name");

                AddRoomButton("GET_STATUS", "Requests update of room status", 18, "/Icons");
                AddRoomButton("TV_ON", "TV On", 18, "/Icons");
                AddRoomButton("TV_OFF", "TV Off", 18, "/Icons");
                AddRoomButton("TV_POWER", "TV Power Toggle", 1, "/Icons");
                AddRoomButton("TV_SOURCE_CAST", "Switches Input to AppleTV", 2, "/Icons");
                AddRoomButton("TV_SOURCE_HDMI", "Switches Input to Headwall HDMI", 2, "/Icons");
                AddRoomButton("TV_SOURCE_CABLE", "Switches Input to TV Tuner", 2, "/Icons");
                AddRoomButton("TV_SLEEP", "TV Sleep", 18, "/Icons");
                AddRoomButton("TV_CH_UP", "TV Channel Up", 1, "/Icons");
                AddRoomButton("TV_CH_DN", "TV Channel Down", 1, "/Icons");
                AddRoomButton("TV_CHx", "TV Direct # Command", 3, "/Icons");
                AddRoomButton("WB_POWER", "Info Board Power Toggle", 4, "/Icons");
                AddRoomButton("WB_BRIGHT", "Info Board Bright Mode", 4, "/Icons");
                AddRoomButton("WB_DIM", "Info Board Dim Mode", 4, "/Icons");
                AddRoomButton("ATV_RIGHT", "AppleTV Right", 5, "/Icons");
                AddRoomButton("ATV_LEFT", "AppleTV Left", 5, "/Icons");
                AddRoomButton("ATV_DOWN", "AppleTV Down", 5, "/Icons");
                AddRoomButton("ATV_UP", "AppleTV Up", 5, "/Icons");
                AddRoomButton("ATV_SELECT", "AppleTV Select", 5, "/Icons");
                AddRoomButton("ATV_PLAY", "AppleTV Play", 6, "/Icons");
                AddRoomButton("ATV_MENU", "AppleTV Menu", 7, "/Icons");
                AddRoomButton("SBAR_PAIR", "Soundbar Bluetooth Pair", 8, "/Icons");
                AddRoomButton("SBAR_STOP", "Soundbar Bluetooth Stop", 18, "/Icons");
                AddRoomButton("SBAR_PLAY", "Soundbar Bluetooth Play", 18, "/Icons");
                AddRoomButton("SBAR_PAUSE", "Soundbar Bluetooth Pause", 18, "/Icons");
                AddRoomButton("SBAR_SKIP_BW", "Soundbar Bluetooth Next", 18, "/Icons");
                AddRoomButton("SBAR_SKIP_FW", "Soundbar Bluetooth Previous", 18, "/Icons");
                AddRoomButton("SBAR_REPEAT", "Soundbar Bluetooth Repeat", 18, "/Icons");
                AddRoomButton("SBAR_WIPE", "Soundbar Remove Pairing", 18, "/Icons");
                AddRoomButton("SBAR_EQ_MOVIE", "Soundbar EQ Movie", 18, "/Icons");
                AddRoomButton("SBAR_EQ_MUSIC", "Soundbar EQ Music", 18, "/Icons");
                AddRoomButton("SBAR_EQ_NEWS", "Soundbar EQ News", 18, "/Icons");
                AddRoomButton("SBAR_VOL_UP", "Soundbar Volume Up", 9, "/Icons");
                AddRoomButton("SBAR_VOL_DN", "Soundbar Volume Down", 9, "/Icons");
                AddRoomButton("SBAR_VOL_MUTE_BP", "Soundbar Mute", 9, "/Icons");
                AddRoomButton("SBAR_VOL_MUTE VOICE", "Soundbar Mute Voice", 18, "/Icons");
                AddRoomButton("SBAR_VOL_UNMUTE_VOICE", "Soundbar Unmute Voice", 18, "/Icons");
                AddRoomButton("SBAR_VOL_BLUETOOTH", "Soundbar Source Bluetooth", 18, "/Icons");
                AddRoomButton("SBAR_VOL_ON", "Soundbar Power On", 18, "/Icons");
                AddRoomButton("SBAR_VOL_OFF", "Soundbar Power Off", 18, "/Icons");
                AddRoomButton("SBAR_VOL_x", "Soundbar Set Volume", 18, "/Icons");
                AddRoomButton("DOOR_OPEN", "Door Open", 18, "/Icons");
                AddRoomButton("DOOR_CLOSE", "Door Close", 18, "/Icons");
                AddRoomButton("DOOR_OPEN_PARTIAL", "Door Open Slightly", 18, "/Icons");
                AddRoomButton("LTG_FULL", "Lights Scene Full", 18, "/Icons");
                AddRoomButton("LTG_SOFT", "Lights Scene Soft", 10, "/Icons");
                AddRoomButton("LTG_READ", "Lights Scene Reading", 10, "/Icons");
                AddRoomButton("LTG_NURSE", "Lights Scene Nurse", 10, "/Icons");
                AddRoomButton("LTG_TV", "Lights Scene TV", 10, "/Icons");
                AddRoomButton("LTG_DARK", "Lights Scene Night", 10, "/Icons");
                AddRoomButton("LTG_OFF", "Lights Scene Off", 10, "/Icons");
                AddRoomButton("LTG_ON_0", "Lights Zone Ambient On", 11, "/Icons");
                AddRoomButton("LTG_OFF_0", "Lights Zone Ambient Off", 11, "/Icons");
                AddRoomButton("LTG_DIM_0_RAISE", "Lights Zone Ambient Up", 11, "/Icons");
                AddRoomButton("LTG_DIM_0_LOWER", "Lights Zone Ambient Down", 11, "/Icons");
                AddRoomButton("LTG_ON_1", "Lights Zone Entry On", 12, "/Icons");
                AddRoomButton("LTG_OFF_1", "Lights Zone Entry Off", 12, "/Icons");
                AddRoomButton("LTG_DIM_1_RAISE", "Lights Zone Entry Up", 12, "/Icons");
                AddRoomButton("LTG_DIM_1_LOWER", "Lights Zone Entry Down", 12, "/Icons");
                AddRoomButton("LTG_ON_2", "Lights Zone Reading On", 13, "/Icons");
                AddRoomButton("LTG_OFF_2", "Lights Zone Reading Off", 13, "/Icons");
                AddRoomButton("LTG_DIM_2_RAISE", "Lights Zone Reading Up", 13, "/Icons");
                AddRoomButton("LTG_DIM_2_LOWER", "Lights Zone Reading Down", 13, "/Icons");
                AddRoomButton("LTG_ON_3", "Lights Zone Bath On", 14, "/Icons");
                AddRoomButton("LTG_OFF_3", "Lights Zone Bath Off", 14, "/Icons");
                AddRoomButton("LTG_DIM_3_RAISE", "Lights Zone Bath Up", 14, "/Icons");
                AddRoomButton("LTG_DIM_3_LOWER", "Lights Zone Bath Down", 14, "/Icons");
                AddRoomButton("LTG_ON_4", "Lights Zone Exam On", 15, "/Icons");
                AddRoomButton("LTG_OFF_4", "Lights Zone Exam Off", 15, "/Icons");
                AddRoomButton("LTG_DIM_4_RAISE", "Lights Zone Exam Up", 15, "/Icons");
                AddRoomButton("LTG_DIM_4_LOWER", "Lights Zone Exam Down", 15, "/Icons");
                AddRoomButton("LTG_DIM_0_x", "Lights Zone Ambient 0% - 100%", 18, "/Icons");
                AddRoomButton("LTG_DIM_1_x", "Lights Zone Entry 0% - 100%", 18, "/Icons");
                AddRoomButton("LTG_DIM_2_x", "Lights Zone Reading 0% - 100%", 18, "/Icons");
                AddRoomButton("LTG_DIM_3_x", "Lights Zone Bath 0% - 100%", 18, "/Icons");
                AddRoomButton("LTG_DIM_4_x", "Lights Zone Exam 0% - 100%", 18, "/Icons");
                AddRoomButton("HVAC_TEMP_UP", "HVAC Increase Setpoint", 16, "/Icons");
                AddRoomButton("HVAC_TEMP_DN", "HVAC Decrease Setpoint", 16, "/Icons");
                AddRoomButton("HVAC_TEMP_SETPOINT_TO_x", "HVAC Change Setpoint", 18, "/Icons");
                AddRoomButton("BLINDS_ALL_OPEN", "Blinds All Open", 17, "/Icons");
                AddRoomButton("BLINDS_ALL_CLOSE", "Blinds All Close", 17, "/Icons");
                AddRoomButton("BLINDS_ALL_STOP", "Blinds All Stop", 17, "/Icons");
                AddRoomButton("BLINDS_ALL_UP_1", "Blinds All Step Up", 17, "/Icons");
                AddRoomButton("BLINDS_ALL_DOWN_1", "Blinds All Step Down", 17, "/Icons");
                AddRoomButton("BLINDS_ALL_x_OPEN", "Blinds All Set", 17, "/Icons");
                AddRoomButton("BLINDS_BLKOUT_OPEN", "Blinds Blackout Open", 17, "/Icons");
                AddRoomButton("BLINDS_BLKOUT_CLOSE", "Blinds Blackout Close", 17, "/Icons");
                AddRoomButton("BLINDS_BLKOUT_STOP", "Blinds Blackout Stop", 17, "/Icons");
                AddRoomButton("BLINDS_BLKOUT_UP_1", "Blinds Blackout Step Up", 17, "/Icons");
                AddRoomButton("BLINDS_BLKOUT_DOWN_1", "Blinds Blackout Step Down", 17, "/Icons");
                AddRoomButton("BLINDS_BLKOUT_x_OPEN", "Blinds Blackout Set", 17, "/Icons");
                AddRoomButton("BLINDS_OPAQUE_OPEN", "Blinds Opaque Open", 17, "/Icons");
                AddRoomButton("BLINDS_OPAQUE_CLOSE", "Blinds Opaque Close", 17, "/Icons");
                AddRoomButton("BLINDS_OPAQUE_STOP", "Blinds Opaque Stop", 17, "/Icons");
                AddRoomButton("BLINDS_OPAQUE_UP_1", "Blinds Opaque Step Up", 17, "/Icons");
                AddRoomButton("BLINDS_OPAQUE_DOWN_1", "Blinds Opaque Step Down", 17, "/Icons");
                AddRoomButton("BLINDS_OPAQUE_x_OPEN", "Blinds Opaque Set", 17, "/Icons");


            }
        }

        public void AddRoomButton(string APIName, string HumanReadableName, int Category, string ImagePath)
        {
            RoomButton rB = new RoomButton();
            rB.APIName = APIName;
            rB.HumanReadableName = HumanReadableName;
            rB.Category = Category;
            rB.ImagePath = ImagePath;
            using (var db = new UHospitalContext())
            {
                db.Add(rB);
                db.SaveChanges();
            }
        }
        public void AddRoomButtonCategory(string WidgetXName, string FrameFile, int WidthRatio, int HeightRatio, string Category, string HumanReadableName)
        {
            RoomButtonCategory rC = new RoomButtonCategory();
            rC.WidgetXName = WidgetXName;
            rC.FrameFile = FrameFile;
            rC.WidthRatio = WidthRatio;
            rC.HeightRatio = HeightRatio;
            rC.Category = Category;
            rC.HumanReadableName = HumanReadableName;
            using (var db = new UHospitalContext())
            {
                db.Add(rC);
                db.SaveChanges();
            }
        }
    }
}

