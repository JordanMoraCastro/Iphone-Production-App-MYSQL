using System;



namespace LCD_Installation.Models
{
    public partial class LCDCenterWIP
    {
        public int Id { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public DateTime? ReceiptDateTime { get; set; }
        public DateTime? LastScanDate { get; set; }
        public DateTime? LastScanDateTime { get; set; }
        public string Imei { get; set; }
        public string Process { get; set; }
        public string UserName { get; set; }
        public string CurrentLocation { get; set; }
        public string PreviousLocation { get; set; }
        public int DaysInLocation { get; set; }
        public string Status { get; set; }
        public string Failure { get; set; }

        public int Lap { get; set; }
    }
}
