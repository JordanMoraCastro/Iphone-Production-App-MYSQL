using System;

#nullable disable

namespace LCD_Installation.Models
{
    public partial class Bpproduction
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DateTime { get; set; }
        public string Material { get; set; }
        public string PreviousLocation { get; set; }
        public string NextLocation { get; set; }
        public int? Qty { get; set; }
        public string Condition { get; set; }
        public string User { get; set; }
    }
}
