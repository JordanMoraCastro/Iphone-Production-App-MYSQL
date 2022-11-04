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
    /// Interaction logic for SortingView.xaml
    /// </summary>
    public partial class SortingView : UserControl
    {
        readonly NotificationManager notificationManager = new NotificationManager();

        public List<string> Failures_grades { get; set; }
        public List<string> Failures_BER { get; set; }

        public List<string> Failures_Lucia { get; set; }

        public bool Transfer { get; set; }


        public SortingView()
        {
            InitializeComponent();

            txtImei.Text = null;
            cbFailure.Text = null;
            cbAA.Text = null;
            cbBD.Text = null;
            cbCondition.Text = null;

        }

        private async void FillDataGridView()
        {

            await Task.Run(() =>
            {

                DateTime date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                var Query = from d in db.SortingProductions
                            where d.Fecha == date
                            orderby d.Hora descending
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
                var Query = from d in db.SortingProductions
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
        private bool IsOnDB(string Imei)
        {
            using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
            var Query = from d in db.SortingProductions
                        where d.Imei == Imei
                        select d;

            if (Query.FirstOrDefault() != null)
            {
                return true;
            }

            return false;

        }


        private void InsertData(string Origen)
        {

            using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
            var data = new SortingProduction
            {
                Fecha = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
                Hora = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                Imei = txtImei.Text,
                Fallos = cbFailure.Text,
                Aa = cbAA.Text,
                Bd = cbBD.Text,
                Condition = cbCondition.Text,
                Origen = Origen,
                Tipo_Material = cbTipo.Text,
            };

            var content = new NotificationContent
            {
                Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),
                Title = "Sorting Production",
                Message = $"Datos registrados correctamente",
                Type = NotificationType.Success,
                CloseOnClick = true,
            };
            notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);

            db.SortingProductions.Add(data);
            db.SaveChanges();

            txtImei.Text = null;
            cbFailure.Text = null;
            cbAA.Text = null;
            cbBD.Text = null;
            cbCondition.Text = null;
            cbTipo.Text = null;
            txtImei.Focus();

            FillDataGridView();
        }

        private void UpdateData(string Origens)
        {

            using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
            SortingProduction data;

            data = db.SortingProductions.Where(b => b.Imei == txtImei.Text).FirstOrDefault();
            if (data != null)
            {
                data.Imei = txtImei.Text;
                data.MFecha = data.Fecha;
                data.Fecha = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                data.Hora = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                data.Fallos = cbFailure.Text;

                data.Condition = cbCondition.Text;

                data.Origen = Origens;

                data.Tipo_Material = cbTipo.Text;

                db.Entry(data).State = EntityState.Modified;



                var content = new NotificationContent
                {
                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),
                    Title = "Sorting Production",
                    Message = $"El imei: " + txtImei.Text + " Fue modificado con exito",
                    Type = NotificationType.Success,
                    CloseOnClick = true,
                };

                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);

                db.SaveChanges();

                txtImei.Text = null;
                cbFailure.Text = null;
                cbAA.Text = null;
                cbBD.Text = null;
                cbCondition.Text = null;
                txtImei.Focus();

                FillDataGridView();


            }
        }

        private void DeleteData()
        {


            int? id = GetId();

            if (id != null)
            {
                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();

                SortingProduction delete = db.SortingProductions.Find(id);

                db.SortingProductions.Remove(delete);

                db.SaveChanges();


                var content = new NotificationContent
                {
                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),

                    Title = "Sorting Production",
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

                    Title = "Sorting Production",
                    Message = $"Por favor seleccione una unidad en la tabla antes de borrar",
                    Type = NotificationType.Error,
                    CloseOnClick = true,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);


            }



        }
        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            if (!IsOnDB(txtImei.Text))
            {
                if (txtImei.Text.Length < 15)
                {
                    var content = new NotificationContent
                    {


                        Background = new SolidColorBrush(Colors.Red),
                        Foreground = new SolidColorBrush(Colors.White),

                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),



                        Title = "Sorting Production",
                        Message = $"Ingrese un imei valido",
                        Type = NotificationType.Error,
                        CloseOnClick = true,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);
                }
                else if (cbCondition.Text == null)
                {
                    var content = new NotificationContent
                    {

                        Background = new SolidColorBrush(Colors.Red),
                        Foreground = new SolidColorBrush(Colors.White),

                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                        Title = "Sorting Production",
                        Message = $"Por favor seleccione una condición",
                        Type = NotificationType.Error,
                        CloseOnClick = true,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);
                }
                else if (cbCondition.Text != "FG" && cbFailure.Text == null)
                {
                    var content = new NotificationContent
                    {

                        Background = new SolidColorBrush(Colors.Red),
                        Foreground = new SolidColorBrush(Colors.White),

                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                        Title = "Sorting Production",
                        Message = $"Por favor seleccione una fallo",
                        Type = NotificationType.Error,
                        CloseOnClick = true,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);
                }

                else if (cbAA.Text == null || cbBD.Text == null)
                {
                    var content = new NotificationContent
                    {

                        Background = new SolidColorBrush(Colors.Red),
                        Foreground = new SolidColorBrush(Colors.White),

                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                        Title = "Sorting Production",
                        Message = $"Por favor ingrese el nivel de las superficies",
                        Type = NotificationType.Error,
                        CloseOnClick = true,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);
                }else if(cbTipo.Text == "" || cbTipo.Text == null)
                {
                    var content = new NotificationContent
                    {

                        Background = new SolidColorBrush(Colors.Red),
                        Foreground = new SolidColorBrush(Colors.White),

                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                        Title = "Sorting Production",
                        Message = $"Por favor seleccione el tipo de unidad",
                        Type = NotificationType.Error,
                        CloseOnClick = true,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);
                }
                else
                {
                    if (Transfer == true)
                    {
                        var content = new NotificationContent
                        {

                            Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                            Title = "Disassembly Production",
                            Message = $"Por favor seleccion el orgien de la unidad",
                            Type = NotificationType.Error,
                            TrimType = NotificationTextTrimType.NoTrim,

                            LeftButtonContent = "Rework",
                            LeftButtonAction = () =>
                            {
                                InsertData("Rework");
                            },

                            RightButtonContent = "Triage",
                            RightButtonAction = () =>
                            {
                                InsertData("Triage");
                            },

                            CloseOnClick = false,
                        };
                        notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(15), null, null, false, false);
                    }
                    else
                    {
                        InsertData("Regular");
                    }
                }


            }
            else
            {

                if (txtImei.Text.Length < 15)
                {
                    var content = new NotificationContent
                    {

                        Background = new SolidColorBrush(Colors.Red),
                        Foreground = new SolidColorBrush(Colors.White),

                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                        Title = "Sorting Production",
                        Message = $"Ingrese un imei valido",
                        Type = NotificationType.Error,
                        CloseOnClick = true,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);
                }
                else if (cbCondition.Text == null)
                {
                    var content = new NotificationContent
                    {

                        Background = new SolidColorBrush(Colors.Red),
                        Foreground = new SolidColorBrush(Colors.White),

                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                        Title = "Sorting Production",
                        Message = $"Por favor seleccione una condición",
                        Type = NotificationType.Error,
                        CloseOnClick = true,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);
                }
                else if (cbCondition.Text != "FG" && cbFailure.Text == null)
                {
                    var content = new NotificationContent
                    {

                        Background = new SolidColorBrush(Colors.Red),
                        Foreground = new SolidColorBrush(Colors.White),

                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                        Title = "Sorting Production",
                        Message = $"Por favor seleccione una fallo",
                        Type = NotificationType.Error,
                        CloseOnClick = true,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);
                }
                else if (cbAA.Text == null || cbBD.Text == null)
                {
                    var content = new NotificationContent
                    {

                        Background = new SolidColorBrush(Colors.Red),
                        Foreground = new SolidColorBrush(Colors.White),

                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                        Title = "Sorting Production",
                        Message = $"Por favor ingrese el nivel de las superficies",
                        Type = NotificationType.Error,
                        CloseOnClick = true,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5), null, null, false, false);
                }
                else
                {
                    var content = new NotificationContent
                    {

                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                        Title = "Disassembly Production",
                        Message = $"Unidad duplicada,¿la desea modificar?",
                        Type = NotificationType.Error,
                        TrimType = NotificationTextTrimType.NoTrim,


                        LeftButtonContent = "Modificar",
                        LeftButtonAction = () =>
                        {

                            if (Transfer == true)
                            {
                                var content = new NotificationContent
                                {

                                    Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),

                                    Title = "Disassembly Production",
                                    Message = $"Por favor seleccion el orgien de la unidad",
                                    Type = NotificationType.Error,
                                    TrimType = NotificationTextTrimType.NoTrim,

                                    LeftButtonContent = "Rework",
                                    LeftButtonAction = () =>
                                    {
                                        UpdateData("Rework");
                                    },

                                    RightButtonContent = "Triage",
                                    RightButtonAction = () =>
                                    {
                                        UpdateData("Triage");
                                    },

                                    CloseOnClick = false,
                                };
                                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(15), null, null, false, false);
                            }
                            else
                            {
                                UpdateData("Regular");
                            }

                            
                        },


                        RightButtonContent = "Cancelar",
                        RightButtonAction = () =>
                        {
                            return;
                        },

                        CloseOnClick = false,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(15), null, null, false, false);
                }

            }
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

        private int? GetId()
        {
            if (DataGridView1.SelectedItem == null)
            {

                return null;
            }
            else
            {
                SortingProduction classObj = DataGridView1.SelectedItem as SortingProduction;
                int? id = classObj.Id;
                return id;
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (txtImei.Text.Length < 15)
            {
                var content = new NotificationContent
                {
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



        private void cbCondition_DropDownClosed(object sender, EventArgs e)
        {
            if (cbCondition.Text == "FG")
            {
                cbFailure.Text = null;
                cbFailure.IsEnabled = false;
            }
            else if (cbCondition.Text == "BER" || cbCondition.Text == "C" || cbCondition.Text == "D" || cbCondition.Text == "E")
            {
                cbFailure.ItemsSource = Failures_BER;
                cbFailure.IsEnabled = true;
            }
            else if (cbCondition.Text == "LUCIA")
            {
                cbFailure.ItemsSource = Failures_Lucia;
                cbFailure.IsEnabled = true;
            }
            else
            {
                cbFailure.ItemsSource = Failures_grades;
                cbFailure.IsEnabled = true;
            }
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {

                using Iphone_Production_AppContext context = new Iphone_Production_AppContext();
                var std_failures = from d in context.SortingFailures
                                   where d.Destino != "BER"
                                   orderby d.Defecto ascending
                                   select d.Defecto;

                var ber_failures = from d in context.SortingFailures
                                   where d.Destino == "BER"
                                   orderby d.Defecto ascending
                                   select d.Defecto;

                var lucia_failures = from d in context.SortingFailures
                                   where d.Destino == "LUCIA"
                                   orderby d.Defecto ascending
                                   select d.Defecto;

                Failures_grades = std_failures.ToList();
                Failures_BER = ber_failures.ToList();
                Failures_Lucia = lucia_failures.ToList();

            });


        }

        private void cbTipo_DropDownClosed(object sender, EventArgs e)
        {
            if (cbTipo.Text == "Transfer to Lucia")
            {
                Transfer = true;
            }
            else
            {
                Transfer = false;
            }
        }
    }
}
