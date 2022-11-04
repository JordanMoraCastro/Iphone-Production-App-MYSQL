using LCD_Installation.Models;
using Notification.Wpf;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LCD_Installation.Views
{
    public partial class LLanteraVIew : UserControl
    {
        public string Location { get; set; }

        readonly NotificationManager notificationManager = new NotificationManager();

        public LLanteraVIew()
        {
            InitializeComponent();
        }

        private async void FillDataGridView()
        {

            await Task.Run(() =>
            {

                DateTime date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                var Query = from d in db.Bpproductions
                            where d.Date == date && d.User == Environment.UserName
                            orderby d.DateTime descending
                            select d;

                Dispatcher.Invoke(() =>
                {
                    try
                    {
                        DataGridView1.ItemsSource = Query.ToList();
                    }
                    catch (Exception)
                    {

                        var content = new NotificationContent
                        {
                            Background = new SolidColorBrush(Colors.Red),
                            Foreground = new SolidColorBrush(Colors.White),

                            Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                            Title = "Iphone Production App",
                            Message = "Error al comunicarse con la base de datos",
                            Type = NotificationType.Error,
                            CloseOnClick = false,

                        };
                        notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3));
                        return;
                    }
                });
            });

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
             => FillDataGridView();



        private int? GetId()
        {

            if (DataGridView1.SelectedItem == null)
            {
                return null;
            }
            else
            {
                Bpproduction classObj = DataGridView1.SelectedItem as Bpproduction;
                int? id = classObj.Id;
                return id;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            if ((txtCantidad.Text.Length != 0 && cbMaterial.Text.Length > 0))
            {
                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();

                string Condition;

                if (chbIngreso.IsChecked == true)
                {
                    Condition = "Ingreso";
                }
                else if (chbSalida.IsChecked == true)
                {
                    Condition = "Salida";
                }
                else if (chbRehazo.IsChecked == true)
                {
                    Condition = "Rechazo";
                }
                else if (chbScrap.IsChecked == true)
                {
                    Condition = "Scrap";
                }
                else
                {
                    Condition = "";
                }

                var data = new Bpproduction
                {
                    Material = cbMaterial.Text,
                    Condition = Condition,
                    PreviousLocation = "",
                    NextLocation = "",
                    User = Environment.UserName,
                    Qty = int.Parse(txtCantidad.Text),
                    Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
                    DateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                };


                var content = new NotificationContent
                {
                    Title = "Iphone Production App",
                    Message = "Datos ingresados correctamente",
                    Type = NotificationType.Success,
                    CloseOnClick = false,
                };

                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3));

                db.Bpproductions.Add(data);
                db.SaveChanges();


                cbMaterial.Text = null;
                txtCantidad.Text = null;

                FillDataGridView();

                cbMaterial.Focus();
            }
            else
            {

                var content = new NotificationContent
                {
                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),

                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                    Title = "Iphone Production App",
                    Message = "Faltan campos por rellenar",
                    Type = NotificationType.Error,
                    CloseOnClick = false,
                };

                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3));

            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int? id = GetId();

            if (id != null)
            {
                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();

                Bpproduction delete = db.Bpproductions.Find(id);

                db.Bpproductions.Remove(delete);

                db.SaveChanges();

                var content = new NotificationContent
                {
                    Title = "Iphone Production App",
                    Message = "El registro fue eliminado correctamenter",
                    Type = NotificationType.Success,
                    CloseOnClick = false,
                };

                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5));

                FillDataGridView();
            }
            else
            {
                var content = new NotificationContent
                {
                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),

                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                    Title = "Iphone Production App",
                    Message = "Por favor seleccione un registro en la tabla antes de borrar",
                    Type = NotificationType.Error,
                    CloseOnClick = false,
                };

                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3));
            }
        }

        private void txtCantidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }


}
