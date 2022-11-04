
using LCD_Installation.Models;
using Microsoft.EntityFrameworkCore;
using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LCD_Installation.Views.Production.LCD_Center
{
    /// <summary>
    /// Interaction logic for LCDCenterView.xaml
    /// </summary>
    public partial class LCDCenterView : UserControl
    {

        public string Location { get; set; }

        readonly NotificationManager notificationManager = new NotificationManager();

        public LCDCenterView()
        {
            InitializeComponent();

            var failures_list = new List<string> {
                    "Face ID",
                    "Ligth Sensor",
                    "Desprendimiento en LCD",
                    "Ligth Sensor Rasgado",
                    "Pantalla Fantasma",
                    "Pixel LCD",
                    "Receiver no Suena",
                    "Burbuja en LCD",
                    "Línea intermintente",
                    "Microfono Fail",
                    "Bezel Fail",
                    "Prueba de Video Fail"
            };



            var ReceiverSealingSteps = new List<string> {
                    "Componente Receiver",
                    "Sellado de Malla",
                    "Test Malla",
                    "Sellado de Bracket",
                    "Test de Bracket",
                    "Adhesivo Light Sensor",
                    "Ensamble Light Sensor",
                    "Test Final Fuga",
                    "Funcional & Cosmetico Test",

            };

            cbStep.ItemsSource = ReceiverSealingSteps;


            cbFailure_Copy.ItemsSource = failures_list;

        }

        private void DataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                int? id = GetId();

                if (id != null)
                {
                    using Iphone_Production_AppContext db = new Iphone_Production_AppContext();

                    LCDCenterProduction delete = db.LCDCenterProductions.Find(id);

                    db.LCDCenterProductions.Remove(delete);

                    db.SaveChanges();


                    var content = new NotificationContent
                    {
                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),

                        Title = "LCD Center Production",
                        Message = $"La unidad fue eliminada correctamente",
                        Type = NotificationType.Success,
                        CloseOnClick = true,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);


                    FillDataGrid();
                    txtImei.Focus();
                }
                else
                {

                    var content = new NotificationContent
                    {

                        Background = new SolidColorBrush(Colors.Red),
                        Foreground = new SolidColorBrush(Colors.White),

                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                        Title = "LCD Center Production",
                        Message = $"Por favor seleccione una unidad en la tabla antes de borrar",
                        Type = NotificationType.Error,
                        CloseOnClick = true,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);

                }
            }
        }

        private void txtImei_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtImei_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void cbStep_DropDownClosed(object sender, EventArgs e)
        {

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
                    Title = "LCD Center Production",
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
                    Title = "LCD Center Production",
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

                    Title = "LCD Center Production",
                    Message = $"Por favor seleccione una condición",
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

                    Title = "LCD Center Production",
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

                    Title = "LCD Center Production",
                    Message = $"Por favor seleccione un fallo",
                    Type = NotificationType.Error,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);


                return;
            }


            if (IsOnDB(txtImei.Text) != null)
            {
                LCDCenterWIP wip;

                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                wip = db.LCDCenterWips.Where(b => b.Imei == txtImei.Text).FirstOrDefault();

                if (wip.CurrentLocation == cbStep.Text)
                {
                    UpdateWip(IsOnDB(txtImei.Text));

                    var data = new LCDCenterProduction
                    {
                        Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
                        DateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                        Imei = txtImei.Text,
                        Failure = cbFailure_Copy.Text,
                        Step = cbStep.Text,
                        User = Environment.UserName,


                        Condition = chbPass.IsChecked == true ? "Pass" : "Fail",

                        Location = chbPass.IsChecked == true ? "Ensamble" : "Componente Receiver",
                    };
                    db.LCDCenterProductions.Add(data);
                    db.SaveChanges();

                    FillDataGrid();
                    txtImei.Focus();
                    txtImei.Text = null;
                }
                else
                {
                    var content = new NotificationContent
                    {

                        Background = new SolidColorBrush(Colors.Red),
                        Foreground = new SolidColorBrush(Colors.White),


                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                        Title = "LCD Center Production",
                        Message = $"La unidad se encuentra en {wip.CurrentLocation}",
                        Type = NotificationType.Warning,
                        CloseOnClick = true,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);
                    return;
                }

            }
            else //Insert Data on Production and WIP
            {


                if (cbStep.Text == "Componente Receiver")
                {
                    InsertWIP();

                    using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                    var data = new LCDCenterProduction
                    {
                        Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
                        DateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                        Imei = txtImei.Text,
                        Failure = cbFailure_Copy.Text,
                        Step = cbStep.Text,
                        User = Environment.UserName,


                        Condition = chbPass.IsChecked == true ? "Pass" : "Fail",

                        Location = chbPass.IsChecked == true ? "Ensamble" : "Componente Receiver",
                    };
                    db.LCDCenterProductions.Add(data);
                    db.SaveChanges();

                    var content = new NotificationContent
                    {
                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),

                        Title = "LCD Center Production",
                        Message = $"El imei: " + txtImei.Text + " Fue registrada con exito",
                        Type = NotificationType.Success,
                        CloseOnClick = true,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);


                    FillDataGrid();
                    txtImei.Focus();
                    txtImei.Text = null;
                }
                else
                {
                    var content = new NotificationContent
                    {
                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),

                        Title = "LCD Center Production",
                        Message = $"La unidad aun no es registrada en el proceso, por favor seleccione el step Componente Receiver",
                        Type = NotificationType.Error,
                        CloseOnClick = true,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);
                    return;
                }


            }
        }

        private void txtImei_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void chbPass_Checked(object sender, RoutedEventArgs e)
        {
            cbFailure_Copy.Text = null;
            cbFailure_Copy.IsEnabled = false;
            btnInsert.Focus();
        }

        private void chbFail_Checked(object sender, RoutedEventArgs e)
        {
            cbFailure_Copy.Text = null;
            cbFailure_Copy.IsEnabled = true;
            cbFailure_Copy.Focus();
        }

        private void cbFailure_Copy_DropDownOpened(object sender, EventArgs e)
        {


        }

        private void CheckboxDestino_DropDownOpened(object sender, EventArgs e)
        {

        }

        private async void FillDataGrid()
        {
            await Task.Run(() =>
            {
                DateTime date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                var Query = from d in db.LCDCenterProductions
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
        private string? IsOnDB(string Imei)
        {
            using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
            var Query = from d in db.LCDCenterWips
                        where d.Imei == Imei
                        select d;

            if (Query.FirstOrDefault() != null)
            {
                return Query.FirstOrDefault().CurrentLocation;

            }

            return null;

        }
        private int? GetId()
        {
            if (DataGridView1.SelectedItem == null)
            {

                return null;
            }
            else
            {
                LCDCenterProduction classObj = DataGridView1.SelectedItem as LCDCenterProduction;
                int? id = classObj.Id;
                return id;
            }
        }

        private void InsertWIP()
        {


            
                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                var data_wip = new LCDCenterWIP
                {
                    ReceiptDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
                    ReceiptDateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    LastScanDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
                    LastScanDateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),

                    Imei = txtImei.Text,

                    Process = "",
                    UserName = Environment.UserName,
                    Lap = 1,
                    CurrentLocation = SetLocation(cbStep.Text),
                    PreviousLocation = "Rework",
                    Status = "Peding"

                };

                db.LCDCenterWips.Add(data_wip);
                db.SaveChanges();

           
        }


        private void UpdateWip(string step)
        {
            LCDCenterWIP wip;

            using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
            wip = db.LCDCenterWips.Where(b => b.Imei == txtImei.Text).FirstOrDefault();
            if (wip != null)
            {

                if (SetLocation(step) == "Ensamble" || SetLocation(step) == "RUR" || SetLocation(step) == "Rework")
                {
                    wip.Status = "Finalized";
                }

                wip.Imei = txtImei.Text;
                wip.LastScanDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                wip.LastScanDateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                wip.PreviousLocation = wip.PreviousLocation;
                wip.CurrentLocation = SetLocation(step);
                wip.UserName = Environment.UserName;



                db.Entry(wip).State = EntityState.Modified;



                var content = new NotificationContent
                {
                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),
                    Title = "LCD Center Production",
                    Message = $"El imei: " + txtImei.Text + " Fue modificado con exito",
                    Type = NotificationType.Success,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);

                db.SaveChanges();

                FillDataGrid();
                txtImei.Focus();
                txtImei.Text = null;
            }
        }



        private string? SetLocation(string step)
        {
            switch (step)
            {

                case "Componente Receiver":
                    if (chbPass.IsChecked == true)
                    {
                        return "Sellado de Malla";
                    }
                    else
                    {
                        return "RUR";
                    }


                case "Sellado de Malla":
                    if (chbPass.IsChecked == true)
                    {
                        return "Test Malla";
                    }
                    else
                    {
                        return "Sellado de Malla";
                    }


                case "Test Malla":
                    if (chbPass.IsChecked == true)
                    {
                        return "Sellado de Malla";
                    }
                    else
                    {
                        return "Sellado de Bracket";
                    }


                case "Sellado de Bracket":
                    if (chbPass.IsChecked == true)
                    {
                        return "Test de Bracket";
                    }
                    else
                    {
                        return null;
                    }



                case "Test de Bracket":
                    if (chbPass.IsChecked == true)
                    {
                        return "Adhesivo Light Sensor";
                    }
                    else
                    {

                        return null;
                    }



                case "Adhesivo Light Sensor":
                    if (chbPass.IsChecked == true)
                    {
                        return "Ensamble Light Sensor";
                    }
                    else
                    {

                        return null;
                    }



                case "Ensamble Light Sensor":
                    if (chbPass.IsChecked == true)
                    {
                        return "Test Final Fuga";
                    }
                    else
                    {
                        return null;
                    }


                case "Test Final Fuga":
                    if (chbPass.IsChecked == true)
                    {
                        return "Funcional & Cosmetico Test";
                    }
                    else
                    {
                        return "Sellado de Malla";
                    }



                case "Funcional & Cosmetico Test":
                    if (chbPass.IsChecked == true)
                    {
                        return "Ensamble";
                    }
                    else
                    {
                        return "Rework";

                    }

                default:
                    return null;


            }
        }




    }

}

