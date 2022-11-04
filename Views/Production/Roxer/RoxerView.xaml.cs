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

namespace LCD_Installation.Views.Production.Roxer
{
    public partial class RoxerView : UserControl
    {
        static FileSystemWatcher watcher;
        readonly NotificationManager notificationManager = new NotificationManager();

        List<double> Pressure = new List<double>();
        List<double> Distorsion = new List<double>();

        public RoxerView()
        {
            InitializeComponent();
            
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
          
            Dispatcher.Invoke(() => {
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

                if(range.Value.ToString() == "")
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
    }
}
