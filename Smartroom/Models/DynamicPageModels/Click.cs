using System;
namespace Smartroom.Models
{
    public class Click
    {
        public int id { get; set; }
        public int RoomNumber { get; set; }
        public int ButtonID { get; set; }
        public DateTime TimePressed { get; set; }

    }
}
