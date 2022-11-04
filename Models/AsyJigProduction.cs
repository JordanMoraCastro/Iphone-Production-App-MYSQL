using System;

#nullable disable

namespace LCD_Installation.Models
{
    public partial class AsyJigProduction
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DateTime { get; set; }
        public string Imei { get; set; }
        public int? Shift { get; set; }
        public string Speaker { get; set; }
        public string Mic { get; set; }
        public string Usb { get; set; }
        public string Receiver { get; set; }
        public string Model { get; set; }
        public string Provenance { get; set; }
        public string Condition { get; set; }
        public string Status { get; set; }
        public string User { get; set; }
        public int? Lap { get; set; }
    }
}
