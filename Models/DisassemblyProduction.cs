using System;

#nullable disable

namespace LCD_Installation.Models
{
    public partial class DisassemblyProduction
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DateTime { get; set; }
        public string Imei { get; set; }
        public string Step { get; set; }
        public string Failure { get; set; }
        public string FailureT { get; set; }
        public string Condition { get; set; }
        public string User { get; set; }
        public string? FinalLocation { get; set; }
        public int? Lap { get; set; }
    }
}
