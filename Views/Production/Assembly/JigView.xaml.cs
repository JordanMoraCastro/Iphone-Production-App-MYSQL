using LCD_Installation.Models;
using Microsoft.EntityFrameworkCore;
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

    public partial class JigView : UserControl
    {
        readonly NotificationManager notificationManager = new NotificationManager();

        public JigView()
        {
            InitializeComponent();
        }

        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {

            if (txtImei.Text.Length < 15)
            {



                var content = new NotificationContent
                {
                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),

                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                    Title = "Iphone Production App",
                    Message = "Por favor ingrese un imei valido",
                    Type = NotificationType.Error,
                    CloseOnClick = false,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3));


            }
            else if (cbOrigen.Text.Length == 0)
            {


                var content = new NotificationContent
                {

                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),

                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),
                    Title = "Iphone Production App",
                    Message = "Por favor seleccione el origen de la unidad",
                    Type = NotificationType.Error,
                    CloseOnClick = false,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3));


            }
            else if (cbGrade.Text.Length == 0)
            {
                var content = new NotificationContent
                {
                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),

                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                    Title = "Iphone Production App",
                    Message = "Por favor seleccione el tipo de unidad",
                    Type = NotificationType.Error,
                    CloseOnClick = false,
                };

                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3));
            }
            else if (IsOnDB())
            {

                if (MessageBox.Show($"La unidad ya fue registrada, ¿la desea modificar?", "Iphone Production App", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if ((cbReceiver.IsChecked == true || rdMic.IsChecked == true || rdSpeaker.IsChecked == true || rdUSB.IsChecked == true) && cbGrade.Text == "L2")
                    {
                        var content = new NotificationContent
                        {

                            Background = new SolidColorBrush(Colors.Red),
                            Foreground = new SolidColorBrush(Colors.White),

                            Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                            Title = "Iphone Production App",
                            Message = "Por favor seleccione otro grado, esta unidad no es candidata a L2",
                            Type = NotificationType.Error,
                            CloseOnClick = false,
                        };
                        notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5));

                        return;
                    }

                    AsyJigProduction prod_;

                    using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                    prod_ = db.AsyJigProductions.Where(b => b.Imei == txtImei.Text).FirstOrDefault();
                    if (prod_ != null)
                    {
                        prod_.Imei = txtImei.Text;
                        prod_.Condition = cbReceiver.IsChecked == false && rdMic.IsChecked == false && rdSpeaker.IsChecked == false && rdUSB.IsChecked == false ? "Pass" : "Fail";

                        prod_.Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                        prod_.DateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                        prod_.Mic = rdMic.IsChecked == true ? "Fail" : "Pass";

                        prod_.Speaker = rdSpeaker.IsChecked == true ? "Fail" : "Pass";

                        prod_.Usb = rdUSB.IsChecked == true ? "Fail" : "Pass";
                        prod_.Status = cbGrade.Text;

                        prod_.Receiver = cbReceiver.IsChecked == true ? "Fail" : "Pass";

                        prod_.Lap++;
                        prod_.Provenance = cbOrigen.Text;
                        prod_.Shift = int.Parse(DateTime.Now.ToString("HH")) >= 14 ? 2 : 1;
                        prod_.User = Environment.UserName;



                        db.Entry(prod_).State = EntityState.Modified;



                        var content = new NotificationContent
                        {
                            Title = "Iphone Production App",
                            Message = "La unidad fue modificada con exito",
                            Type = NotificationType.Success,
                            CloseOnClick = false,
                        };
                        notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3));


                        db.SaveChanges();


                        FillDataGridView();
                        txtImei.Focus();
                        txtImei.Text = null;

                        cbOrigen.Text = "";
                        cbGrade.Text = "";
                        txtImei.Text = null;
                        txtImei.Focus();

                        cbReceiver.IsChecked = false;
                        rdMic.IsChecked = false;
                        rdSpeaker.IsChecked = false;
                        rdUSB.IsChecked = false;

                        FillDataGridView();


                    }
                }
            }
            else
            {

                if ((cbReceiver.IsChecked == true || rdMic.IsChecked == true || rdSpeaker.IsChecked == true || rdUSB.IsChecked == true) && cbGrade.Text == "L2")
                {

                    var content = new NotificationContent
                    {

                        Background = new SolidColorBrush(Colors.Red),
                        Foreground = new SolidColorBrush(Colors.White),

                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                        Title = "Iphone Production App",
                        Message = "Por favor seleccione otro grado, esta unidad no es candidata a L2",
                        Type = NotificationType.Error,
                        CloseOnClick = false,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5));

                    return;
                }


                using (Iphone_Production_AppContext db = new Iphone_Production_AppContext())
                {
                    var data = new AsyJigProduction
                    {
                        Imei = txtImei.Text,
                        Condition = cbReceiver.IsChecked == false && rdMic.IsChecked == false && rdSpeaker.IsChecked == false && rdUSB.IsChecked == false ? "Pass" : "Fail",

                        Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
                        DateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),

                        Mic = rdMic.IsChecked == true ? "Fail" : "Pass",

                        Speaker = rdSpeaker.IsChecked == true ? "Fail" : "Pass",

                        Usb = rdUSB.IsChecked == true ? "Fail" : "Pass",
                        Status = cbGrade.Text,

                        Receiver = cbReceiver.IsChecked == true ? "Fail" : "Pass",


                        Lap = 1,
                        Provenance = cbOrigen.Text,
                        Shift = int.Parse(DateTime.Now.ToString("HH")) >= 14 ? 2 : 1,
                        User = Environment.UserName,

                    };

                    db.AsyJigProductions.Add(data);

                    db.SaveChanges();

                    cbOrigen.Text = "";
                    cbGrade.Text = "";
                    txtImei.Text = null;
                    txtImei.Focus();

                    rdMic.IsChecked = false;
                    rdSpeaker.IsChecked = false;
                    rdUSB.IsChecked = false;
                    cbReceiver.IsChecked = false;


                    var content = new NotificationContent
                    {
                        Title = "Iphone Production App",
                        Message = "Unidad ingresada correctamente",
                        Type = NotificationType.Success,
                        CloseOnClick = false,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5));


                    FillDataGridView();
                }
            }

        }

        private async void FillDataGridView()
        {


            await Task.Run(() =>
            {

                DateTime date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                var Query = from d in db.AsyJigProductions
                            where d.Date == date && d.User == Environment.UserName
                            orderby d.DateTime descending
                            select d;



                Dispatcher.Invoke(() =>
                {
                    try
                    {
                        DataGridView1.ItemsSource = Query.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                });
            });
        }


        private async void FillDataGridViewSearch(string imei)
        {


            await Task.Run(() =>
            {


                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                var Query = from d in db.AsyJigProductions
                            where d.Imei == imei
                            select d;



                Dispatcher.Invoke(() =>
                {
                    try
                    {
                        DataGridView1.ItemsSource = Query.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                });
            });

        }

        private int? GetId()
        {

            if (DataGridView1.SelectedItem == null)
            {
                return null;
            }
            else
            {
                AsyJigProduction classObj = DataGridView1.SelectedItem as AsyJigProduction;
                int? id = classObj.Id;
                return id;
            }



        }

        private bool IsOnDB()
        {
            using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
            var Query = from d in db.AsyJigProductions
                        where d.Imei == txtImei.Text
                        select d;

            if (Query.FirstOrDefault() != null)
            {
                return true;
            }

            return false;

        }


        private void BtnBorrar_Click(object sender, RoutedEventArgs e)
        {
            int? id = GetId();

            if (id != null)
            {
                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();

                AsyJigProduction delete = db.AsyJigProductions.Find(id);

                db.AsyJigProductions.Remove(delete);

                db.SaveChanges();


                var content = new NotificationContent
                {
                    Title = "Iphone Production App",
                    Message = "La unidad fue eliminada correctamente",
                    Type = NotificationType.Success,
                    CloseOnClick = false,
                };

                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5));

                FillDataGridView();
                txtImei.Text = null;
                txtImei.Focus();
            }
            else
            {
                var content = new NotificationContent
                {

                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),

                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                    Title = "Iphone Production App",
                    Message = "Por favor seleccione una unidad en la tabla antes de borrar",
                    Type = NotificationType.Warning,
                    CloseOnClick = false,
                };

                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5));

            }
        }

        private void Image_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (txtImei.Text.Length == 0)
            {


                var content = new NotificationContent
                {

                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),

                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                    Title = "Iphone Production App",
                    Message = "Por favor ingrese un imei valido para buscar",
                    Type = NotificationType.Error,
                    CloseOnClick = false,
                };

                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5));

                FillDataGridView();
            }
            else
            {
                FillDataGridViewSearch(txtImei.Text);
            }

        }

        private void TxtImei_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
