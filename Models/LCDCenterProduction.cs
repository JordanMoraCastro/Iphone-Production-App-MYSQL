using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCD_Installation.Models
{
    public partial class LCDCenterProduction
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DateTime { get; set; }
        public string Imei { get; set; }
        public string Step { get; set; }
        public string Failure { get; set; }
        public string Condition { get; set; }
        public string User { get; set; }
        public string Location { get; set; }
        public int? Lap { get; set; }

    }
}
