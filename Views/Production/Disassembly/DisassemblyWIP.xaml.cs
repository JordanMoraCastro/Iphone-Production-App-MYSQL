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
    public partial class DisassemblyWIP : UserControl
    {
        readonly NotificationManager notificationManager = new NotificationManager();

        public DisassemblyWIP()
        {
            InitializeComponent();
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
                    Title = "Disassembly WIP",
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
                    Message = $"Por favor seleccione la procedencia",
                    Type = NotificationType.Error,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);

                return;
            }

            if (IsOnDB(txtImei.Text) == true)
            {


                if (lbPid.Visibility == Visibility.Hidden)
                {

                    var content = new NotificationContent
                    {
                        Background = new SolidColorBrush(Colors.Red),
                        Foreground = new SolidColorBrush(Colors.White),
                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),
                        Title = "Disassembly Production",
                        Message = $"Esta unidad ya fue registrada. Esta unidad fue PID? ",
                        Type = NotificationType.Error,
                        CloseOnClick = true,

                        LeftButtonContent = "Si",
                        LeftButtonAction = () =>
                        {

                            if (lbPid.Visibility == Visibility.Hidden)
                            {
                                var content = new NotificationContent
                                {
                                    Background = new SolidColorBrush(Colors.Red),
                                    Foreground = new SolidColorBrush(Colors.White),
                                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),
                                    Title = "Disassembly Production",
                                    Message = $"Por favor selecione la informacion del PID ",
                                    Type = NotificationType.Warning,
                                    CloseOnClick = true,
                                };
                                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);

                                lbPid.Visibility = Visibility.Visible;
                                cbFailure.Visibility = Visibility.Visible;
                                lbRepair.Visibility = Visibility.Visible;
                                chbPass.Visibility = Visibility.Visible;
                                chbFail.Visibility = Visibility.Visible;

                            }

                        },

                        RightButtonContent = "No",
                        RightButtonAction = () =>
                        {
                            txtImei.Focus();
                            txtImei.Text = null;

                        },

                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(99), null, null, false, false);


                }
                else
                {

                    using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                    Models.DisassemblyWIP data;

                    data = db.DisassemblyWips.Where(b => b.Imei == txtImei.Text).FirstOrDefault();
                    if (data != null)
                    {

                        data.Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                        data.DateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                        data.Pid = true;
                        data.PidType = cbFailure.Text;



                        if (chbPass.IsChecked == true)
                        {
                            data.Repaired = true;
                        }
                        else if (chbFail.IsChecked == true)
                        {
                            data.Repaired = false;
                        }
                        else
                        {
                            var content2 = new NotificationContent
                            {
                                Background = new SolidColorBrush(Colors.Red),
                                Foreground = new SolidColorBrush(Colors.White),
                                Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),
                                Title = "Ingreso Disassembly",
                                Message = $"Por favor seleccione el estado de reparacion de la unidad",
                                Type = NotificationType.Warning,
                                CloseOnClick = true,
                            };
                            notificationManager.Show(content2, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);
                            return;
                        }


                        db.Entry(data).State = EntityState.Modified;



                        var content = new NotificationContent
                        {
                            Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),
                            Title = "Ingreso Disassembly",
                            Message = $"El imei: " + txtImei.Text + " Fue modificado con exito",
                            Type = NotificationType.Success,
                            CloseOnClick = true,
                        };

                        notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);

                        db.SaveChanges();

                        txtImei.Text = null;



                        txtImei.Focus();

                        cbFailure.Text = null;
                        cbStep.Text = null;
                        chbPass.IsChecked = false;
                        chbFail.IsChecked = false;

                        lbPid.Visibility = Visibility.Hidden;
                        cbFailure.Visibility = Visibility.Hidden;
                        lbRepair.Visibility = Visibility.Hidden;
                        chbPass.Visibility = Visibility.Hidden;
                        chbFail.Visibility = Visibility.Hidden;

                        FillDataGridView();
                    }
                }
            }
            else
            {

                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                var data = new LCD_Installation.Models.DisassemblyWIP
                {

                    Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
                    DateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    Imei = txtImei.Text,
                    User = Environment.UserName,
                    Lap = 1,
                    Provenance = cbStep.Text,
                    InProcess = true

                };

                db.DisassemblyWips.Add(data);
                db.SaveChanges();

                var content = new NotificationContent
                {
                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),

                    Title = "Ingreso Disassembly",
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

        private int? GetId()
        {
            if (DataGridView1.SelectedItem == null)
            {

                return null;
            }
            else
            {
                LCD_Installation.Models.DisassemblyWIP classObj = DataGridView1.SelectedItem as
                LCD_Installation.Models.DisassemblyWIP;
                int? id = classObj.Id;
                return id;
            }
        }
        private bool IsOnDB(string Imei)
        {
            using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
            var Query = from d in db.DisassemblyWips
                        where d.Imei == Imei
                        select d;

            if (Query.FirstOrDefault() != null)
            {
                return true;
            }

            return false;

        }

        private async void FillDataGridView()
        {
            await Task.Run(() =>
            {

                DateTime date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();

                var Query = from d in db.DisassemblyWips
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
            lbPid.Visibility = Visibility.Hidden;
            cbFailure.Visibility = Visibility.Hidden;
            lbRepair.Visibility = Visibility.Hidden;
            chbPass.Visibility = Visibility.Hidden;
            chbFail.Visibility = Visibility.Hidden;

        }

        private void txtImei_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                DeleteData();
            }
        }


        private void DeleteData()
        {
            int? id = GetId();

            if (id != null)
            {
                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();

               Models.DisassemblyWIP delete = db.DisassemblyWips.Find(id);

                db.DisassemblyWips.Remove(delete);

                db.SaveChanges();


                var content = new NotificationContent
                {
                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),

                    Title = "Disassembly Production",
                    Message = $"La unidad fue eliminada correctamente",
                    Type = NotificationType.Success,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);


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
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);


            }



        }

    }
}