#nullable disable

namespace LCD_Installation.Models
{
    public partial class PermisosUsuario
    {
        public int IdUsuario { get; set; }
        public int IdPermiso { get; set; }

        public virtual Permiso IdPermisoNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
