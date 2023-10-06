using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Linq;
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

namespace LCD_Installation.Views.Employee.TimeOut
{
    public partial class TimeOutView : UserControl
    {
        
        readonly NotificationManager notificationManager = new NotificationManager();

        public bool Baño { get; set; }
        public bool Locker { get; set; }
        public bool Admin { get; set; }
        public bool Consultorio { get; set; }
        public bool Parqueo { get; set; }

        public bool AsoIngram { get; set; }




        public TimeOutView()
        {
            InitializeComponent();

            

        }

      

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Baño = false;
            Parqueo = false;
            Locker = false;
            Admin = false;
            Consultorio = false;
            AsoIngram = false;
            btnRegistrar.IsEnabled = false;
            pnlBadge.Visibility = Visibility.Hidden;

            checkIMG1.Visibility = Visibility.Hidden;
            checkIMG2.Visibility = Visibility.Hidden;
            checkIMG3.Visibility = Visibility.Hidden;
            checkIMG4.Visibility = Visibility.Hidden;
            checkIMG5.Visibility = Visibility.Hidden;
            checkIMG6.Visibility = Visibility.Hidden;

        }

        private void txtUserID_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.Key == Key.Enter)
            {
                if (txtUserID.Text.Length < 6 || txtUserID.Text == null)
                {
                    var content = new NotificationContent
                    {
                        Title = "Iphone Production App",
                        Message = "Por favor ingrese un numero de empleado valido",
                        Type = NotificationType.Error,
                        CloseOnClick = false,
                    };

                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5));
                }
                else if (IsOnDB())
                {
                    // Habilita paneles y botones
                 

                    //verifica que exista alguna salida, en todo caso no deja realizar el registro

                  

                        LCD_Installation.Models.TimeOut prod_;

                        using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                        prod_ = db.TimeOut.Where(b => b.UserID == txtUserID.Text && b.Status == false).FirstOrDefault();
                        if (prod_ != null)
                        {
                    
                            prod_.ReturnTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                            prod_.Status = true;



                            db.Entry(prod_).State = EntityState.Modified;


                            var content = new NotificationContent
                            {
                                Title = "Iphone Production App",
                                Message = "Regreso registrado con exito",
                                Type = NotificationType.Success,
                                CloseOnClick = false,
                            };
                            notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3));


                            db.SaveChanges();

                            txtUserID.Text = null;
                            txtUserID.IsEnabled = true;
                            txtUserID.Focus();
                            btnRegistrar.IsEnabled = false;

                        }

                  
             
                    
                }
                else
                {

                    txtUserID.IsEnabled = false;
                    pnlBadge.Visibility = Visibility.Visible;
                    btnRegistrar.IsEnabled = true;
                    btnRegistrar.Focus();

                    Baño = false;
                    Parqueo = false;
                    Locker = false;
                    Admin = false;
                    Consultorio = false;
                    AsoIngram = false;
                }
               
            }
        }

        private void btnBaño_Click(object sender, RoutedEventArgs e)
        {
            Baño = !Baño;

            if (Baño)
            {
                checkIMG1.Visibility = Visibility.Visible;
            }
            else
            {
                checkIMG1.Visibility = Visibility.Hidden;
            }

        }

        private void btnLocker_Click(object sender, RoutedEventArgs e)
        {
            Locker = !Locker;
            if (Locker)
            {
                checkIMG2.Visibility = Visibility.Visible;
            }
            else
            {
                checkIMG2.Visibility = Visibility.Hidden;
            }
        }

        private void btnConsultorio_Click(object sender, RoutedEventArgs e)
        {
            Consultorio = !Consultorio;
            
            if (Consultorio)
            {
                checkIMG3.Visibility = Visibility.Visible;
            }
            else
            {
                checkIMG3.Visibility = Visibility.Hidden;
            }
        }

        private void btnAdministrativo_Click(object sender, RoutedEventArgs e)
        {
            Admin = !Admin;
            if (Admin)
            {
                checkIMG4.Visibility = Visibility.Visible;
            }
            else
            {
                checkIMG4.Visibility = Visibility.Hidden;
            }
        }

        private void btnParqueo_Click(object sender, RoutedEventArgs e)
        {
            Parqueo = !Parqueo;
            if (Parqueo)
            {
                checkIMG5.Visibility = Visibility.Visible;
            }
            else
            {
                checkIMG5.Visibility = Visibility.Hidden;
            }
        }

        private void btnAsoIngram_Click(object sender, RoutedEventArgs e)
        {
            AsoIngram = !AsoIngram;
            if (AsoIngram)
            {
                checkIMG6.Visibility = Visibility.Visible;
            }
            else
            {
                checkIMG6.Visibility = Visibility.Hidden;
            }
        }

        private bool IsOnDB()
        {
            using Iphone_Production_AppContext db = new Iphone_Production_AppContext();


            var Query = from d in db.TimeOut
                        where d.UserID == txtUserID.Text && d.Status == false
                        select d;

            if (Query.FirstOrDefault() != null)
            {

                return true;

            }

            return false;

        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {


            if(Consultorio == true || AsoIngram == true || Baño == true || Parqueo == true || Admin == true || Locker == true)
            {
                using Iphone_Production_AppContext db = new Iphone_Production_AppContext();
                var data = new Models.TimeOut
                {
                    UserID = txtUserID.Text,


                    Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
                    ExitTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),

                    Bathroom = Baño,

                    Locker = Locker,

                    ConsultingRoom = Consultorio,

                    Parking = Parqueo,

                    AdminArea = Admin,

                    Status = false

                };

                db.TimeOut.Add(data);

                db.SaveChanges();


                var content = new NotificationContent
                {
                    Title = "Iphone Production App",
                    Message = "Datos registrados correctamente",
                    Type = NotificationType.Success,
                    CloseOnClick = false,
                };
                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3));

                txtUserID.Text = null;
                txtUserID.IsEnabled = true;
                txtUserID.Focus();
                pnlBadge.Visibility = Visibility.Hidden;
                btnRegistrar.IsEnabled = false;

                Baño = false;
                Parqueo = false;
                Locker = false;
                Admin = false;
                Consultorio = false;
                AsoIngram = false;

                checkIMG1.Visibility = Visibility.Hidden;
                checkIMG2.Visibility = Visibility.Hidden;
                checkIMG3.Visibility = Visibility.Hidden;
                checkIMG4.Visibility = Visibility.Hidden;
                checkIMG5.Visibility = Visibility.Hidden;
                checkIMG6.Visibility = Visibility.Hidden;
            }
            else
            {
                var content = new NotificationContent
                {
                    Title = "Iphone Production App",
                    Message = "Por favor seleccione los badge de salida",
                    Type = NotificationType.Error,
                    CloseOnClick = false,
                };

                notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(5));
            }
            
        }
    }
}
