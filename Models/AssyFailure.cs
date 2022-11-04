using System;

#nullable disable

namespace LCD_Installation.Models
{
    public partial class AssyFailure
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DateTime { get; set; }
        public string Imei { get; set; }
        public string Failure { get; set; }
        public string UserName { get; set; }
        public string Condition { get; set; }
        public string Origin { get; set; }
        public int? Lap { get; set; }
        public int? Shift { get; set; }
    }
}
