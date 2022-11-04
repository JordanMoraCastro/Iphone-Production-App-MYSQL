using System;

#nullable disable

namespace LCD_Installation.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public DateTime? LastConnection { get; set; }
        public string Area { get; set; }
    }
}
