using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Smartroom.Views
{
    public partial class ElevatorPage : ContentView
    {
        MasterPage Master;
        public ElevatorPage(MasterPage context)
        {
            InitializeComponent();
            Master = context;
        }

        void ButtonPressed(object sender, EventArgs args)
        {
            //  Button sent = (Button)sender;
            //  sent.BackgroundColor = Color.DarkSlateGray;
        }
        void ButtonReleased(object sender, EventArgs args)
        {
            //  Button sent = (Button)sender;
            //  sent.BackgroundColor = Color.Transparent;
        }

        void DFloorDown(object sender, EventArgs args)
        {
            Master.Controller.DFloorDownCommand.Execute(null);
            int floor;

            if (DFloorLabel.Text.Equals("LL1"))
            {
                floor = 0;
            }
            else
            {
                floor = int.Parse(DFloorLabel.Text);
            }

            switch (floor)
            {
                case 0:
                    LevelLabel.Text = "LL1";
                    LevelInfo.Text = "Fabrication Lab\nMobility Garage\nSummit Cafe\nNeuro Prosthestics Lab";
                    break;

                case 1:
                    LevelLabel.Text = "L1";
                    LevelInfo.Text = "Administration\nAdmissions\nMiners Hospital\nPhysical Medicine and Rehab Clinic\nTherapy Excersise Gym";
                    break;

                case 2:
                    LevelLabel.Text = "L2";
                    LevelInfo.Text = "Assistive Technology\nDigital Innovation Lab\nPsychology\nTherapy Patio\nTherapy and Rehabilitation Services\nWheelchair Clinic";
                    break;

                case 3:
                    LevelLabel.Text = "L3";
                    LevelInfo.Text = "Rehab Inpatient 3";
                    break;

                case 4:
                    LevelLabel.Text = "L4";
                    LevelInfo.Text = "Rehab Inpatient 4";
                    break;

                case 5:
                    LevelLabel.Text = "L5";
                    LevelInfo.Text = "Rehab Inpatient 5";
                    break;
            }
        }

        void DFloorUp(object sender, EventArgs args)
        {
            Master.Controller.DFloorUpCommand.Execute(null);
            int floor;




            floor = int.Parse(DFloorLabel.Text);


            switch (floor)
            {
                case 1:
                    LevelLabel.Text = "L1";
                    LevelInfo.Text = "Administration\nAdmissions\nMiners Hospital\nPhysical Medicine and Rehab Clinic\nTherapy Excersise Gym";
                    break;

                case 2:
                    LevelLabel.Text = "L2";
                    LevelInfo.Text = "Assistive Technology\nDigital Innovation Lab\nPsychology\nTherapy Patio\nTherapy and Rehabilitation Services\nWheelchair Clinic";
                    break;

                case 3:
                    LevelLabel.Text = "L3";
                    LevelInfo.Text = "Rehab Inpatient 3";
                    break;

                case 4:
                    LevelLabel.Text = "L4";
                    LevelInfo.Text = "Rehab Inpatient 4";
                    break;

                case 5:
                    LevelLabel.Text = "L5";
                    LevelInfo.Text = "Rehab Inpatient 5";
                    break;

                case 6:
                    LevelLabel.Text = "L6";
                    LevelInfo.Text = "Future Expansion";
                    break;
            }
        }
    }
}