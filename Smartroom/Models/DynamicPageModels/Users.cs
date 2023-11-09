using System;
using System.ComponentModel.DataAnnotations;

namespace Smartroom.Models
{
    public class User
    {
        [Key]
        public int RoomNumber { get; set; }

    }
}
