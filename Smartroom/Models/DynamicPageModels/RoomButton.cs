using System;
using System.ComponentModel.DataAnnotations;
using Xamarin.Forms;

namespace Smartroom.Models
{
    public class RoomButton
    {
        public string HumanReadableName { get; set; }
        public string APIName { get; set; }
        public string ImagePath { get; set; }
        [Key]
        public int id { get; set; }
        public int Category { get; set; }
        //public virtual RoomButtonCategory RoomButtonCategory { get; set; }
    }
}
