using System;
using System.Collections.Generic;
using System.Diagnostics;
using Smartroom.Models;
using Smartroom.ViewModels;
using Xamarin.Forms;
using System.Linq;

namespace Smartroom.Views
{
    public partial class DynamicUI : ContentView
    {
        private MasterPage super;
        public DynamicUI(MasterPage context)
        {
            InitializeComponent();
            this.super = context;

            Grid G = (Grid)FindByName("DynamicGrid");
            int GSize = 100;

            //Set up 100 x 100 grid
            for (int i = 0; i < GSize; i++)
            {
                //Define new row
                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(1.0, GridUnitType.Star);

                //Define new col
                ColumnDefinition c = new ColumnDefinition();
                c.Width = new GridLength(1.0, GridUnitType.Star);

                //Add to grid 
                G.RowDefinitions.Add(r);
                G.ColumnDefinitions.Add(c);
            }

            DynamicPageViewModel vm = new DynamicPageViewModel(super);
            List<ButtonGroup> groups = vm.GetButtonGroups();
            //List<ButtonGroup> groups = vm.TestSet1();
            //List<ButtonGroup> groups = vm.TestSet2();

            PackDisplay(G, groups, GSize);

        }

        /*
         * Method will take a uniform size Xamarin Grid of size x size dimensions
         * and pack as many button groups as possible onto the grid 
         * 
         * Method searches cell by cell to find open space for button group
         */
        private void PackDisplay(Grid G, List<ButtonGroup> groups, int size)
        {
            int labelOffset = 5;

            //Check if there is no user data
            if (groups.Count == 0)
            {
                Label l = new Label();
                l.Text = "No Existing User Data";
                G.Children.Add(l, 10, 10);
                Grid.SetColumnSpan(l, 50);
                Grid.SetRowSpan(l, 20);
            }

            //States whether each cell is occupied or not 
            bool[,] grid = new bool[size, size];

            foreach (ButtonGroup bg in groups)
            {
                bg.rowSpan = (int)Math.Round(bg.rowSpan * this.super.scaleValue);
                bg.colSpan = (int)Math.Round(bg.colSpan * this.super.scaleValue);

                //Loop over grid cells
                for (int y = 0; y < size && !bg.placed; y++)
                {
                    for (int x = 0; x < size && !bg.placed; x++)
                    {
                        //Check to see if button group will overlap borders
                        if ((y + bg.rowSpan - 1) >= size || (x + bg.colSpan - 1) >= size)
                        {
                            continue;
                        }

                        //Check if top left and bottom right corners are free
                        if (!grid[y, x] && !grid[y + bg.rowSpan - 1, x + bg.colSpan - 1])
                        {
                            bool validLocation = true;

                            //Check all cells within possible box location
                            for (int yy = y; yy < y + bg.rowSpan - 1; yy++)
                            {
                                for (int xx = x; xx < x + bg.colSpan - 1; xx++)
                                {
                                    if (grid[yy, xx])
                                    {
                                        validLocation = false;
                                        break;
                                    }
                                }
                            }

                            //Place button group if valid location
                            if (validLocation)
                            {
                                // add label above
                                Label l = new Label();
                                l.Text = bg.label;
                                l.VerticalOptions = LayoutOptions.FillAndExpand;
                                l.HorizontalOptions = LayoutOptions.FillAndExpand;
                                l.HorizontalTextAlignment = TextAlignment.Center;
                                l.FontSize = 30;

                                G.Children.Add(l, x, y);
                                Grid.SetColumnSpan(l, bg.colSpan);
                                Grid.SetRowSpan(l, labelOffset);

                                // add button group
                                G.Children.Add(bg.GetFrame(), x, y + labelOffset);
                                Grid.SetColumnSpan(bg.GetFrame() , bg.colSpan); //second number can make the widgit thiner
                                Grid.SetRowSpan(bg.GetFrame(), bg.rowSpan - labelOffset);

                                bg.placed = true;

                                //Set all used cells to true
                                for (int yy = y; yy < y + bg.rowSpan - 1; yy++)
                                {
                                    for (int xx = x; xx < x + bg.colSpan - 1; xx++)
                                    {
                                        grid[yy, xx] = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }    
}
