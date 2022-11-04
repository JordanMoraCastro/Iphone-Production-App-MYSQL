using System;



namespace LCD_Installation.Models
{
    public partial class DisassemblyWIP
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DateTime { get; set; }
        public string Imei { get; set; }
        public string Provenance { get; set; }
        public bool Pid { get; set; }

        public string PidType { get; set; }
        public bool Repaired { get; set; }
        public bool InProcess { get; set; }
        public string User { get; set; }
        public int Lap { get; set; }
    }
}
