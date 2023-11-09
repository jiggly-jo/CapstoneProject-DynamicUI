using System;
using Xamarin.Forms;

namespace Smartroom.Models
{
    /*
     * Class represents one button group (Widget)
     * Class is used by dynamic view to inject frames onto a grid 
     */
    public class ButtonGroup
    {
        private Frame frame { get; set; }
        private Forms9Patch.Frame f9pFrame { get; set; }
        public int colSpan { get; set; }
        public int rowSpan { get; set; }
        public bool placed { get; set; }
        public string label { get; set; }

        public ButtonGroup()
        {

        }

        public ButtonGroup(Frame f, int width, int height)
        {
            this.label = label;
            frame = f;
            //frame.Margin = 4;
            colSpan = width;
            rowSpan = height;
            placed = false;
        }

        public ButtonGroup(Forms9Patch.Frame f, int width, int height)
        {
            this.label = label;
            f9pFrame = f;
            //f9pFrame.Margin = 4;
            colSpan = width;
            rowSpan = height;
            placed = false;
        }

        public View GetFrame()
        {
            if (frame != null)
                return frame;
            return f9pFrame;
        }

    }
}
