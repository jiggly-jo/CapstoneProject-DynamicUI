using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Smartroom.Views
{
    public partial class SidebarView : ContentView
    {
        public MasterPage Master;

        public Frame ShadowFrame;
        public Frame ArrowShadowFrame;

        private SidebarItem Selected { get; set; }
        public SidebarView(MasterPage super)
        {
            InitializeComponent();
            Master = super;

            //start up, select a screen
            OptionsRow.IsSelected = true;
            Selected = OptionsRow;
            Master.ChangePage("Options");

            //TODO for tutorial
            //ShadowFrame = new Frame();
            //ShadowFrame.BackgroundColor = new Color(0, 0, 0, .5);

            //ArrowShadowFrame = new Frame();
            //ArrowShadowFrame.BackgroundColor = new Color(0, 0, 0, .5);


            //SidebarGrid.Children.Add(ShadowFrame, 0, 3, 0, 15);
            //SidebarGrid.Children.Add(ArrowShadowFrame, 3, 4, 0, 15);

            //ArrowShadowFrame.CornerRadius = 0;

            //ShadowFrame.IsVisible = false;
            //ArrowShadowFrame.IsVisible = false;
        }

        void SidebarItem_ItemClicked(System.Object sender, System.Object parameter)
        {
            if (!(sender is SidebarItem sidebarItem) || sidebarItem == Selected)
            {
                return;
            }

            //Change the selected status.
            //Can change this to use bindings in the future instead of directly modifying.
            if (!(Selected is null))
            {
                Selected.IsSelected = false;
            }
            sidebarItem.IsSelected = true;
            Selected = sidebarItem;

            //Change the main view.
            Master.ChangePage((sender as SidebarItem).Text);            
        }

        public void ChangePageFromVoiceCommand(string pageName)
        {
            //match the page selected 
            foreach (SidebarItem sidebarItem in SidebarFlexLayout.Children)
            {
                if(pageName == sidebarItem.Text)
                {
                    //match
                    Selected.IsSelected = false;
                    Selected = sidebarItem;
                    Selected.IsSelected = true;
                    //Change the main view.
                    Master.ChangePage(Selected.Text);
                    break;
                }
            }
        }
    }
}