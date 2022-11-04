using System;

#nullable disable

namespace LCD_Installation.Models
{
    public partial class SortingProduction
    {
        public int Id { get; set; }
        public string Imei { get; set; }
        public string Condition { get; set; }
        public string Aa { get; set; }
        public string Bd { get; set; }
        public string PCondition { get; set; }
        public string Fallos { get; set; }
        public string Tipo_Material { get; set; }
        public string Origen { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? Hora { get; set; }
        public DateTime? MFecha { get; set; }
    }
}
