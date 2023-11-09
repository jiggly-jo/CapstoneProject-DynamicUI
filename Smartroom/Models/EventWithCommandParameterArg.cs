using System;
namespace Smartroom.Models
{
    public class EventWithCommandParameterArg : EventArgs
    {
        public object CommandParameter { get; set; }
    }
}
