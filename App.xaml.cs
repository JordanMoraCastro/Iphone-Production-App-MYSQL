using System.Windows;
using LCD_Installation.Models;

namespace LCD_Installation
{

    public partial class App : Application
    {
        public App()
        {
            using (Iphone_Production_AppContext db = new Iphone_Production_AppContext())
            {
                if (!db.Database.CanConnect())
                {
                    MessageBox.Show("Imposible conectar con el servidor, por favor comuniquese con su lider o administrador","Iphone Production",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
        }
    }
}
