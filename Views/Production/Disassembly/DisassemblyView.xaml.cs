using LCD_Installation.Models;
using Microsoft.EntityFrameworkCore;
using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LCD_Installation.Views
{
    /// <summary>
    /// Interaction logic for DisassemblyView.xaml
    /// </summary>
    public partial class DisassemblyView : UserControl
    {

        public string Location { get; set; }

        int lastHour = DateTime.Now.Hour;

        readonly NotificationManager notificationManager = new NotificationManager();

        public DisassemblyView()
        {
            InitializeComponent();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            if (lastHour < DateTime.Now.Hour || (lastHour == 23 && DateTime.Now.Hour == 0))
            {
                lastHour = DateTime.Now.Hour;
                GetHourlyProduction();
            }
        }

        //Metodo a ejecutar cada hora para obtener cantidad de produccion y mostrar alertas de desempeño de los colaboradores
        private void GetHourlyProduction()
        {

            DateTime date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            int hourlyProduction;
            List<int> lasthour = new List<int>();

            using (Iphone_Production_AppContext context = new Iphone_Production_AppContext())
            {
                var query = from x in context.DisassemblyProductions
                            where x.Date == date && x.User == Environment.UserName
                            select x.DateTime;

                foreach (DateTime item in query)
                {
                    if (item.Hour == (DateTime.Now.Hour - 1))
                    {
                        lasthour.Add(item.Hour);
                    }
                }

                hourlyProduction = lasthour.Count();
            }

            if (hourlyProduction >= 25)
            {
                var content = new NotificationContent
                {
                    Title = "Produccion por Hora Disassembly",
                    Message = $"Su produccion fue de: {hourlyProduction} unidades",
                    Type = NotificationType.Success,
                    CloseOnClick = false,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);
            }
            else if (hourlyProduction < 25 && hourlyProduction > 15)
            {
                var content = new NotificationContent
                {
                    Title = "Produccion por Hora Disassembly",
                    Message = $"Su produccion fue de: {hourlyProduction} unidades",
                    Type = NotificationType.Warning,
                    CloseOnClick = false,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);
            }
            else if (hourlyProduction < 15)
            {
                var content = new NotificationContent
                {
                    Title = "Produccion por Hora Disassembly",
                    Message = $"Su produccion fue de: {hourlyProduction} unidades",
                    Type = NotificationType.Error,
                    CloseOnClick = false,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);
            }
        }

        private async void FillDataGridView()
        {

            await Task.Run(() =>
            {

                DateTime date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                var Query = from d in db.DisassemblyProductions
                            where d.Date == date && d.User == Environment.UserName
                            orderby d.DateTime ascending
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //FillDataGridView();

            CheckboxDestino.IsEnabled = false;

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1500;

            timer.Elapsed += timer_Elapsed;
            timer.Start();

        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {     

            if (txtImei.Text.Length < 15)
            {

                var content = new NotificationContent
                {
                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),

                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),
                    Title = "Disassembly Production",
                    Message = $"Por favor ingrese un imei valido",
                    Type = NotificationType.Error,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);
                return;
            }

            if (cbStep.Text == null || cbStep.Text.Length == 0)
            {

                var content = new NotificationContent
                {
                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),
                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),
                    Title = "Disassembly Production",
                    Message = $"Por favor seleccione un Step",
                    Type = NotificationType.Error,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);

                return;
            }

            if (chbPass.IsChecked == false && chbFail.IsChecked == false)
            {

                var content = new NotificationContent
                {

                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),


                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                    Title = "Disassembly Production",
                    Message = $"Por favor seleccione una condición",
                    Type = NotificationType.Error,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);

                return;
            }

            if (chbFail.IsChecked == true && cbFailure.Text == null || cbFailure.Text == "")
            {
                var content = new NotificationContent
                {
                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),


                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                    Title = "Disassembly Production",
                    Message = $"Por favor seleccione un fallo",
                    Type = NotificationType.Error,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);

                return;
            }

            if (chbFail.IsChecked == true && cbFailure_Copy.Text == null || cbFailure_Copy.Text == "")
            {

                var content = new NotificationContent
                {

                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),


                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                    Title = "Disassembly Production",
                    Message = $"Por favor seleccione un fallo",
                    Type = NotificationType.Error,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);


                return;
            }

            if (cbStep.Text == "STEP 11" && (CheckboxDestino.Text == "" || CheckboxDestino.Text == null)) 
            {

                var content = new NotificationContent
                {

                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),


                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                    Title = "Disassembly Production",
                    Message = $"Por favor seleccione el destino de la unidad",
                    Type = NotificationType.Error,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);


                return;
            }


            if (IsOnDB(txtImei.Text) == true)
            {
                if (MessageBox.Show($"La unidad ya fue registrada en el {Location}, ¿la desea modificar?", "Disassembly Production", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DisassemblyProduction prod_;

                    using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                    prod_ = db.DisassemblyProductions.Where(b => b.Imei == txtImei.Text).FirstOrDefault();
                    if (prod_ != null)
                    {
                        prod_.Imei = txtImei.Text;
                        prod_.Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                        prod_.DateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        prod_.Failure = cbFailure.Text;
                        prod_.Step = cbStep.Text;
                        prod_.FailureT = cbFailure_Copy.Text;
                        prod_.Condition = chbPass.IsChecked == true ? "Pass" : "Fail";
                        prod_.FinalLocation = CheckboxDestino.Text.Length > 1 ? CheckboxDestino.Text : null;



                        prod_.User = Environment.UserName;

                        db.Entry(prod_).State = EntityState.Modified;



                        var content = new NotificationContent
                        {
                            Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),
                            Title = "Disassembly Production",
                            Message = $"El imei: " + txtImei.Text + " Fue modificado con exito",
                            Type = NotificationType.Success,
                            CloseOnClick = true,
                        };
                        notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);

                        db.SaveChanges();

                        FillDataGridView();
                        txtImei.Focus();
                        txtImei.Text = null;
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                var data = new DisassemblyProduction
                {
                    Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
                    DateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    Imei = txtImei.Text,
                    Failure = cbFailure.Text,
                    Step = cbStep.Text,
                    User = Environment.UserName,
                    Lap = 1,
                    FailureT = cbFailure_Copy.Text,
                    Condition = chbPass.IsChecked == true ? "Pass" : "Fail",
                };

                db.DisassemblyProductions.Add(data);
                db.SaveChanges();

                var content = new NotificationContent
                {
                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),

                    Title = "Disassembly Production",
                    Message = $"El imei: " + txtImei.Text + " Fue registrada con exito",
                    Type = NotificationType.Success,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);


                FillDataGridView();
                txtImei.Focus();
                txtImei.Text = null;
            }
        }

        private bool IsOnDB(string Imei)
        {
            using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
            var Query = from d in db.DisassemblyProductions
                        where d.Imei == Imei && d.Step == cbStep.Text
                        select d;

            if (Query.FirstOrDefault() != null)
            {
                Location = Query.FirstOrDefault().Step;
                return true;
            }

            return false;

        }

        private void cbFailure_Copy_DropDownOpened(object sender, EventArgs e)
        {
            if (cbFailure.Text == "Cosmetico")
            {
                var cosmeticos = new List<string> {
                    "LCD QUEBRADA",
                    "LCD PICADURA / RAYAS",
                    "FANTASMA",
                    "DAÑO LIQUIDO / CORROSION",
                    "GENERICA",

                };

                cbFailure_Copy.ItemsSource = cosmeticos;
            }
            else
            {
                var funcionales = new List<string> {
                    "LCD LINEA INTERM",
                    "LCD NO POWER",
                    "NO TOUCH",
                    "LCD CONECTOR DAÑADO",
                    "LIGHT SENSOR",
                    "DAÑO PCB",
                    "FACE ID",
                    "FLEX WIFI",
                    "FLEX USB",
                    "FLEX FLASH",
                    "NFC",
                    "VIBRADOR",
                    "SPEAKER",

                };

                cbFailure_Copy.ItemsSource = funcionales;
            }
        }

        private void cbFailure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbFailure_Copy.Text = null;
            cbFailure_Copy.Focus();
        }

        private void chbPass_Checked(object sender, RoutedEventArgs e)
        {
            cbFailure.Text = null;
            cbFailure_Copy.Text = null;

            cbFailure.IsEnabled = false;
            cbFailure_Copy.IsEnabled = false;

            btnInsert.Focus();
        }

        private void chbFail_Checked(object sender, RoutedEventArgs e)
        {
            cbFailure.IsEnabled = true;
            cbFailure_Copy.IsEnabled = true;

            cbFailure.Focus();
        }

        private void txtImei_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtImei.Text.Length == 15)
            {
                if (cbStep.Text != null)
                {
                    btnInsert.Focus();
                    return;
                }

                cbStep.Focus();
            }
        }

        private int? GetId()
        {
            if (DataGridView1.SelectedItem == null)
            {

                return null;
            }
            else
            {
                DisassemblyProduction classObj = DataGridView1.SelectedItem as DisassemblyProduction;
                int? id = classObj.Id;
                return id;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            if (txtImei.Text.Length < 15)
            {

                var content = new NotificationContent
                {
                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),

                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),
                    Title = "Disassembly Production",
                    Message = $"Por favor ingrese un imei valido",
                    Type = NotificationType.Error,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);
                return;
            }

            if (cbStep.Text == null || cbStep.Text.Length == 0)
            {

                var content = new NotificationContent
                {
                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),
                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),
                    Title = "Disassembly Production",
                    Message = $"Por favor seleccione un Step",
                    Type = NotificationType.Error,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);

                return;
            }

            if (chbPass.IsChecked == false && chbFail.IsChecked == false)
            {

                var content = new NotificationContent
                {

                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),


                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                    Title = "Disassembly Production",
                    Message = $"Por favor seleccione una condición",
                    Type = NotificationType.Error,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);

                return;
            }

            if (chbFail.IsChecked == true && cbFailure.Text == null || cbFailure.Text == "")
            {
                var content = new NotificationContent
                {
                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),


                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                    Title = "Disassembly Production",
                    Message = $"Por favor seleccione un fallo",
                    Type = NotificationType.Error,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);

                return;
            }

            if (chbFail.IsChecked == true && cbFailure_Copy.Text == null || cbFailure_Copy.Text == "")
            {

                var content = new NotificationContent
                {

                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),


                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                    Title = "Disassembly Production",
                    Message = $"Por favor seleccione un fallo",
                    Type = NotificationType.Error,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);


                return;
            }


            if (IsOnDB(txtImei.Text) == true)
            {
                if (MessageBox.Show($"La unidad ya fue registrada en el {Location}, ¿la desea modificar?", "Disassembly Production", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DisassemblyProduction prod_;

                    using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                    prod_ = db.DisassemblyProductions.Where(b => b.Imei == txtImei.Text).FirstOrDefault();
                    if (prod_ != null)
                    {
                        prod_.Imei = txtImei.Text;
                        prod_.Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                        prod_.DateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        prod_.Failure = "ON HOLD";
                        prod_.Step = cbStep.Text;
                        prod_.FailureT = "ON HOLD";
                        prod_.Condition = "";


                        prod_.User = Environment.UserName;

                        db.Entry(prod_).State = EntityState.Modified;



                        var content = new NotificationContent
                        {
                            Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),
                            Title = "Disassembly Production",
                            Message = $"El imei: " + txtImei.Text + " Fue modificado con exito",
                            Type = NotificationType.Success,
                            CloseOnClick = true,
                        };
                        notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);

                        db.SaveChanges();

                        FillDataGridView();
                        txtImei.Focus();
                        txtImei.Text = null;
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                var data = new DisassemblyProduction
                {
                    Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
                    DateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    Imei = txtImei.Text,
                    Failure = "ON HOLD",
                    Step = cbStep.Text,
                    User = Environment.UserName,
                    Lap = 1,
                    FailureT = "ON HOLD",
                    Condition = "",
                };

                db.DisassemblyProductions.Add(data);
                db.SaveChanges();

                var content = new NotificationContent
                {
                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),

                    Title = "Disassembly Production",
                    Message = $"El imei: " + txtImei.Text + " Fue registrada con exito",
                    Type = NotificationType.Success,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);


                FillDataGridView();
                txtImei.Focus();
                txtImei.Text = null;
            }




        }


        private void DataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                int? id = GetId();

                if (id != null)
                {
                    using Iphone_Production_AppContext db = new Iphone_Production_AppContext();

                    DisassemblyProduction delete = db.DisassemblyProductions.Find(id);

                    db.DisassemblyProductions.Remove(delete);

                    db.SaveChanges();


                    var content = new NotificationContent
                    {
                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),

                        Title = "Disassembly Production",
                        Message = $"La unidad fue eliminada correctamente",
                        Type = NotificationType.Success,
                        CloseOnClick = true,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);


                    FillDataGridView();
                    txtImei.Focus();
                }
                else
                {

                    var content = new NotificationContent
                    {

                        Background = new SolidColorBrush(Colors.Red),
                        Foreground = new SolidColorBrush(Colors.White),

                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                        Title = "Disassembly Production",
                        Message = $"Por favor seleccione una unidad en la tabla antes de borrar",
                        Type = NotificationType.Error,
                        CloseOnClick = true,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);

                }
            }
        }



        private void txtImei_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cbStep.Text == "" || cbStep.Text == null)
            {
                var content = new NotificationContent
                {

                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),

                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                    Title = "Disassembly Production",
                    Message = $"Primero seleccione un paso antes de reproducir el video",
                    Type = NotificationType.Error,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);
            }
            else
            {
                if (DataGridView1.Visibility == Visibility.Visible)
                {

                    switch (cbStep.Text)
                    {
                        case "STEP 1":
                            videoPlayer.Source = new Uri(@"\\crsjwks43440\Servidor\Iphone\Disassembly Videos\1.mp4");
                            break;
                        case "STEP 2":
                            videoPlayer.Source = new Uri(@"\\crsjwks43440\Servidor\Iphone\Disassembly Videos\2.mp4");
                            break;
                        case "STEP 3":
                            videoPlayer.Source = new Uri(@"\\crsjwks43440\Servidor\Iphone\Disassembly Videos\3.mp4");
                            break;
                        case "STEP 4":
                            videoPlayer.Source = new Uri(@"\\crsjwks43440\Servidor\Iphone\Disassembly Videos\4.mp4");
                            break;
                        case "STEP 5":
                            videoPlayer.Source = new Uri(@"\\crsjwks43440\Servidor\Iphone\Disassembly Videos\5.mp4");
                            break;
                        case "STEP 6":
                            videoPlayer.Source = new Uri(@"\\crsjwks43440\Servidor\Iphone\Disassembly Videos\6.mp4");
                            break;
                        case "STEP 7":
                            videoPlayer.Source = new Uri(@"\\crsjwks43440\Servidor\Iphone\Disassembly Videos\7.mp4");
                            break;
                        case "STEP 8":
                            videoPlayer.Source = new Uri(@"\\crsjwks43440\Servidor\Iphone\Disassembly Videos\8.mp4");
                            break;
                        case "STEP 9":
                            videoPlayer.Source = new Uri(@"\\crsjwks43440\Servidor\Iphone\Disassembly Videos\9.mp4");
                            break;
                        case "STEP 10":
                            videoPlayer.Source = new Uri(@"\\crsjwks43440\Servidor\Iphone\Disassembly Videos\10.mp4");
                            break;
                        case "STEP 11":
                            videoPlayer.Source = new Uri(@"\\crsjwks43440\Servidor\Iphone\Disassembly Videos\11.mp4");
                            break;

                        default:
                            break;

                    }

                    ProgressIndicator.IsActive = true;
                    DataGridView1.Visibility = Visibility.Collapsed;

                    await Task.Run(() =>
                    {
                        Thread.Sleep(5500);
                    });

                    ProgressIndicator.IsActive = false;

                    videoPlayer.Visibility = Visibility.Visible;
                    videoPlayer.Position = new TimeSpan(0, 0, 1);
                    videoPlayer.Play();


                }
                else
                {
                    DataGridView1.Visibility = Visibility.Visible;
                    videoPlayer.Visibility = Visibility.Collapsed;
                    ProgressIndicator.IsActive = false;
                    videoPlayer.Stop();
                }
            }


        }  

        private void cbStep_DropDownClosed(object sender, EventArgs e)
        {
            if(cbStep.Text == "STEP 11")
            {
                CheckboxDestino.IsEnabled = true;
            }
            else
            {
                CheckboxDestino.IsEnabled = false;
                CheckboxDestino.Text = null;
            }
        }
    }
}
