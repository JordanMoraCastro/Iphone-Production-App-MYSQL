using LCD_Installation.Models;
using Microsoft.EntityFrameworkCore;
using Notification.Wpf;
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for DummyView.xaml
    /// </summary>
    public partial class DummyView : UserControl
    {
        readonly NotificationManager notificationManager = new NotificationManager();
        public List<string> Failures { get; set; }
        public DummyView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cbFailure.IsEnabled = false;

            using (Iphone_Production_AppContext db = new Iphone_Production_AppContext())
            {
                var lstFailures = from d in db.AssemblyFailureCodes
                                  select d.Failure;

                Failures = lstFailures.ToList();
            }

            cbFailure.ItemsSource = Failures;
            FillDataGridView();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (txtImei.Text.Length < 15 || txtImei.Text == null)
            {


                var content = new NotificationContent
                {
                    Title = "Iphone Production App",
                    Message = "Por favor ingrese un imei valido",
                    Type = NotificationType.Error,
                    CloseOnClick = false,
                };

                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5));

            }
            else if (cbProvenance.Text == null || cbProvenance.Text == "")
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
            else if (rdPass.IsChecked == false && rdFail.IsChecked == false)
            {

                var content = new NotificationContent
                {
                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),

                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),


                    Title = "Iphone Production App",
                    Message = "Por favor seleccione la condición de la unidad",
                    Type = NotificationType.Error,
                    CloseOnClick = false,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3));
            }
            else if (rdFail.IsChecked == true && cbFailure.Text == null)
            {

                var content = new NotificationContent
                {

                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),

                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                    Title = "Iphone Production App",
                    Message = "Por favor seleccione un fallo",
                    Type = NotificationType.Error,
                    CloseOnClick = false,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3));

            }
            else
            {

                if (IsOnDB() == false)
                {
                    using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                    var data = new AssyFailure
                    {
                        Imei = txtImei.Text,
                        Condition = rdPass.IsChecked == true ? "Pass" : "Fail",

                        Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
                        DateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),

                        Failure = cbFailure.Text,

                        Lap = 1,
                        Origin = cbProvenance.Text,
                        Shift = int.Parse(DateTime.Now.ToString("HH")) >= 14 ? 2 : 1,
                        UserName = Environment.UserName,

                    };

                    db.AssyFailures.Add(data);

                    db.SaveChanges();



                    var content = new NotificationContent
                    {
                        Title = "Iphone Production App",
                        Message = "Datos registrados correctamente",
                        Type = NotificationType.Success,
                        CloseOnClick = false,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3));

                    txtImei.Text = null;
                    txtImei.Focus();
                    cbFailure.Text = null;
                    rdFail.IsChecked = false;
                    cbProvenance.Text = null;
                    rdPass.IsChecked = false;

                    FillDataGridView();
                }
                else
                {
                    if (MessageBox.Show($"La unidad ya fue registrada, ¿la desea modificar?", "Iphone Production App", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        AssyFailure prod_;

                        using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                        prod_ = db.AssyFailures.Where(b => b.Imei == txtImei.Text).FirstOrDefault();
                        if (prod_ != null)
                        {
                            prod_.Imei = txtImei.Text;
                            prod_.Condition = rdPass.IsChecked == true ? "Pass" : "Fail";

                            prod_.Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                            prod_.DateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                            prod_.Failure = cbFailure.Text;

                            prod_.Lap += 1;
                            prod_.Origin = cbProvenance.Text;
                            prod_.Shift = int.Parse(DateTime.Now.ToString("HH")) >= 14 ? 2 : 1;
                            prod_.UserName = Environment.UserName;



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
                            txtImei.Text = null;
                            txtImei.Focus();
                            cbFailure.Text = null;
                            cbProvenance.Text = null;
                            rdFail.IsChecked = false;
                            rdPass.IsChecked = false;

                            FillDataGridView();



                        }
                    }
                }
            }


        }

        private async void FillDataGridView()
        {

            await Task.Run(() =>
            {

                DateTime date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                var Query = from d in db.AssyFailures
                            where d.Date == date && d.UserName == Environment.UserName
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

        private void rdPass_Checked(object sender, RoutedEventArgs e)
        {
            cbFailure.IsEnabled = false;
            cbFailure.Text = null;
        }

        private void rdFail_Checked(object sender, RoutedEventArgs e)
        {
            cbFailure.IsEnabled = true;
            cbFailure.Text = null;
        }

        private int? GetId()
        {

            if (DataGridView1.SelectedItem == null)
            {

                return null;
            }
            else
            {
                AssyFailure classObj = DataGridView1.SelectedItem as AssyFailure;
                int? id = classObj.Id;
                return id;
            }



        }


        private bool IsOnDB()
        {
            using Iphone_Production_AppContext db = new Iphone_Production_AppContext();


            var Query = from d in db.AssyFailures
                        where d.Imei == txtImei.Text
                        select d;

            if (Query.FirstOrDefault() != null)
            {

                return true;

            }

            return false;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int? id = GetId();

            if (id != null)
            {
                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();

                AssyFailure delete = db.AssyFailures.Find(id);

                db.AssyFailures.Remove(delete);

                db.SaveChanges();


                var content = new NotificationContent
                {
                    Title = "Iphone Production App",
                    Message = "La unidad fue eliminada correctamente",
                    Type = NotificationType.Success,
                    CloseOnClick = false,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3));


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
                    Type = NotificationType.Error,
                    CloseOnClick = false,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3));


            }
        }

        private void Image_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {

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
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3));



                FillDataGridView();
            }
            else
            {
                FillDataGridViewSearch(txtImei.Text);

            }
        }

        private async void FillDataGridViewSearch(string imei)
        {


            await Task.Run(() =>
            {

                DateTime date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                var Query = from d in db.AssyFailures
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

        private void txtImei_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
