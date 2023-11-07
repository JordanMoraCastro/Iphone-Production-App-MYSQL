using Microsoft.VisualBasic;
using System;

#nullable disable

namespace LCD_Installation.Models
{
    public partial class Roxer
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DateTime { get; set; }
        public string Imei { get; set; }

        public string Firts_Pass { get; set; }
        public string Second_Pass { get; set; }

        public string Part { get; set; }

        public string Component { get; set; }

        public string Roxer_Condition { get; set; } // Pass or Fail
        public string Status { get; set; } // Pending Leak Validation Finalized
        public string User { get; set; } //Employee Name
        public int? Lap { get; set; } 
    }
}
