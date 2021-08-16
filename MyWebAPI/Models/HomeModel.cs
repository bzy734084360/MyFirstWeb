using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebAPI.Models
{
    public class DeviceStatus
    {
        public int ID { get; set; }
        public string Power { get; set; }
        public string Mode { get; set; }
        public string Fan { get; set; }
        public int TempSet { get; set; }
        public string UpdateTime { get; set; }
    }
}