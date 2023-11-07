using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Notification.Wpf;
using Spire.Xls;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using MahApps.Metro.Controls.Dialogs;
using LCD_Installation.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;


namespace LCD_Installation.Views.Production.Roxer
{
    public partial class RoxerView : UserControl
    {
        static FileSystemWatcher watcher;
        readonly NotificationManager notificationManager = new NotificationManager();

        public string Status { get; set; }

        List<double> Pressure = new List<double>();
        List<double> Distorsion = new List<double>();

        public RoxerView()
        {
            InitializeComponent();

            txtImei.Focus();
            txtImei.Text = null;

            rdFail.IsChecked = false;
            rdPass.IsChecked = false;

            cbLeak.IsEnabled = false;

            cbPart.IsEnabled = false;   

        }

        private bool IsOnDB(string Imei)
        {
            using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
            var Query = from d in db.Roxer
                        where d.Imei == Imei
                        select d;

            if (Query.FirstOrDefault() != null)
            {
                Status = Query.FirstOrDefault().Status;
                return true;
            }
            else
            {
                return false;
            }



        }

        private void StartWatcher()
        {



            watcher = new FileSystemWatcher
            {
                Path = (@"C:\Users\user\Documents\SmartRox\SmartRox_001").Replace("user", Environment.UserName),
                Filter = ConfigurationManager.AppSettings.Get("DocumentType")
            };
            watcher.Created += Watcher_Created;
            watcher.Deleted += Watcher_Deleted;
            watcher.EnableRaisingEvents = true;
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {

            Dispatcher.Invoke(() =>
            {
                RoxerChart.Series = null;
            });
            Pressure.Clear();
            Distorsion.Clear();

        }

        private async void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            try
            {

                Dispatcher.Invoke(() =>
                {

                    int roxer_value = int.Parse(ReadExcel(e.FullPath));

                    if (roxer_value <= -150)
                    {
                        var content = new NotificationContent
                        {
                            Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),
                            Title = "Roxer Test",
                            Message = $"El dato final de roxer fue de {roxer_value}",
                            Type = NotificationType.Success,
                            CloseOnClick = true,
                        };
                        notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);
                    }
                    else
                    {
                        var content = new NotificationContent
                        {
                            Background = new SolidColorBrush(Colors.Red),
                            Foreground = new SolidColorBrush(Colors.White),

                            Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),
                            Title = "Roxer Test",
                            Message = $"El dato final de roxer fue de {roxer_value}",
                            Type = NotificationType.Error,
                            CloseOnClick = false,
                        };
                        notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(90000), null, null, false, false);
                    }

                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private string ReadExcel(string path)
        {

            Workbook workbook = new Workbook();
            workbook.LoadFromFile(path, ";", 1, 1);
            Worksheet sheet = workbook.Worksheets[0];
            var roxer_value = sheet.Range["C94"].Value;



            foreach (CellRange range in sheet.Range["B22:B161"])
            {

                if (range.Value.ToString() == "")
                {
                    Pressure.Add(0);
                }
                else
                {
                    Pressure.Add(double.Parse(range.Value.ToString()));
                }


            }
            foreach (CellRange range in sheet.Range["C22:C161"])
            {
                if (range.Value.ToString() == "")
                {
                    Distorsion.Add(0);
                }
                else
                {
                    Distorsion.Add(double.Parse(range.Value.ToString()));
                }


            }

            ISeries[] Series = new ISeries[]
            {
                new LineSeries<double>
                {
                    Name = "Distorsion",
                    Values = Distorsion,
                    GeometrySize = 0,
                    LineSmoothness = 0,
                    Fill = null
                },

                new LineSeries<double>
                {
                    Name = "Pressure",
                    Values = Pressure,
                    Fill = null,
                    LineSmoothness = 0,
                    GeometrySize = 0,
                },
            };

            RoxerChart.Series = Series;

            return roxer_value.ToString();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists((@"C:\Users\user\Documents\SmartRox\SmartRox_001").Replace("user", Environment.UserName)))
            {
                StartWatcher();
            }
            else
            {
                MessageBox.Show("No se pudo conectar con el puerto de SmartRox");
            }

        }


        private void cbLeak_DropDownOpened(object sender, EventArgs e)
        {


            switch (cbPart.Text)
            {
                case "Botones":

                    var botones = new List<string> {
                    "Mute",
                    "Vol +/-",
                    "Power",

                };

                    cbLeak.ItemsSource = botones;
                    break;

                case "Componentes":
                    var Componentes = new List<string> {
                    "Receiver",
                    "Speaker",
                    "Cam Mic",
                    "SPK Mic",

                };

                    cbLeak.ItemsSource = Componentes;

                    break;

                case "Superficies":
                    var Superficies = new List<string> {
                    "LCD",
                    "BD",
                    "Camera Lens",
                    "5G",

                };

                    cbLeak.ItemsSource = Superficies;

                    break;



            }

        }

        private void rdFail_Checked(object sender, RoutedEventArgs e)
        {
         

                cbPart.Text = null;
                cbLeak.Text = null;

                cbLeak.IsEnabled = true;
                cbPart.IsEnabled = true;

         


            
        }

        private void rdFail_Unchecked(object sender, RoutedEventArgs e)
        {

            cbPart.Text = null;
            cbLeak.Text = null;

            cbLeak.IsEnabled = false;
            cbPart.IsEnabled = false;
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {

            IsOnDB(txtImei.Text);

            if (txtImei.Text.Length < 15)
            {

                var content = new NotificationContent
                {
                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),

                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),
                    Title = "Roxer Production",
                    Message = $"Por favor ingrese un imei valido",
                    Type = NotificationType.Error,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);
                return;
            }


            if(rdPass.IsChecked == false && rdFail.IsChecked == false)
            {
                var content = new NotificationContent
                {
                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),
                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),
                    Title = "Roxer Production",
                    Message = $"Por favor seleccione una condición",
                    Type = NotificationType.Error,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);

                return;
            }

            if (Status == "Pending" && (rdFail.IsChecked == true && cbLeak.Text == null))
            {

                var content = new NotificationContent
                {
                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),
                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),
                    Title = "Roxer Production",
                    Message = $"Por favor seleccione el componente en el que se encuentra la fuga",
                    Type = NotificationType.Error,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);

                return;
            }

            if (IsOnDB(txtImei.Text) == true && Status == "Pending")
            {
                if (MessageBox.Show($"La unidad ya fue registrada ¿la desea modificar?", "Roxer Production", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Models.Roxer prod_;

                    using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                    prod_ = db.Roxer.Where(b => b.Imei == txtImei.Text).FirstOrDefault();
                    if (prod_ != null)
                    {
                        prod_.Imei = txtImei.Text;
                        prod_.Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                        prod_.DateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                        if(rdPass.IsChecked == true)
                        {
                            prod_.Roxer_Condition = "PASS";
                            prod_.Second_Pass = "PASS";
                        }
                        else if (rdFail.IsChecked == true)
                        {
                            prod_.Roxer_Condition = "FAIL";
                            prod_.Second_Pass = "FAIL";

                            prod_.Part = cbPart.Text;
                            prod_.Component = cbLeak.Text;

                        }

                        prod_.User = Environment.UserName;

                        prod_.Status = "Complete";



                        db.Entry(prod_).State = EntityState.Modified;



                        var content = new NotificationContent
                        {
                            Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),
                            Title = "Roxer Production",
                            Message = $"El imei: " + txtImei.Text + " Fue modificado con exito",
                            Type = NotificationType.Success,
                            CloseOnClick = true,
                        };
                        notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);

                        db.SaveChanges();

                        txtImei.Focus();
                        txtImei.Text = null;

                        rdFail.IsChecked = false;
                        rdPass.IsChecked = false;

                        cbLeak.Text = null;

                        cbPart.Text = null;
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
                var data = new Models.Roxer
                {
                    Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
                    DateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    Imei = txtImei.Text,
          
                    User = Environment.UserName,
                    Lap = 1,

                    Firts_Pass = rdPass.IsChecked == true ? "PASS" : "FAIL",

                    Status = rdPass.IsChecked == true ? "Complete" : "Pending",


                };

                db.Roxer.Add(data);
                db.SaveChanges();

                var content = new NotificationContent
                {
                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),

                    Title = "Roxer Production",
                    Message = $"El imei: " + txtImei.Text + " Fue registrada con exito",
                    Type = NotificationType.Success,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);


           
                txtImei.Focus();
                txtImei.Text = null;

                rdFail.IsChecked = false;
                rdPass.IsChecked = false;

                cbLeak.Text = null;

                cbPart.Text = null;
            }

        }

      
    }

    
}

