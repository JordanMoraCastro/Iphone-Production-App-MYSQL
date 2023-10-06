using System;

#nullable disable

namespace LCD_Installation.Models
{
    public partial class TimeOut
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }

        public string UserID { get; set; }
        public bool Bathroom { get; set; }
        public bool Locker { get; set; }
        public bool ConsultingRoom { get; set; }
        public bool Parking { get; set; }
        public bool AdminArea { get; set; }
        public bool Status { get; set; }

        public DateTime? ExitTime { get; set; }
        public DateTime? ReturnTime { get; set; }

    }
}
