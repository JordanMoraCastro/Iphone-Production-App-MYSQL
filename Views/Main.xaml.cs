using LCD_Installation.Views;
using LCD_Installation.Views.Production.Roxer;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ControlzEx.Theming;
using Notification.Wpf;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Media;
using LCD_Installation.Views.Production.LCD_Center;


namespace Navigation_Drawer_App
{
    public partial class MainWindow : MetroWindow
    {

        public bool DarkMode { get; set; }

        readonly UserControl llantera = new LLanteraVIew();
        readonly UserControl NoModule = new NoModuleView();
        readonly UserControl Jig = new JigView();
        readonly UserControl Dummy = new DummyView();
        readonly UserControl DisassemblyView = new DisassemblyView();
        readonly UserControl RoxerView = new RoxerView();
        readonly UserControl SortingView = new SortingView();
        readonly UserControl DisassemblyWip = new DisassemblyWIP();
        readonly UserControl LCDCenterView = new LCDCenterView();

        readonly NotificationManager notificationManager = new NotificationManager();

        public MainWindow()
        {
            InitializeComponent();
            DarkMode = false;
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                tt_messages.Visibility = Visibility.Collapsed;
                tt_maps.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
                tt_signout.Visibility = Visibility.Collapsed;
                
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_messages.Visibility = Visibility.Visible;
                tt_maps.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_signout.Visibility = Visibility.Visible;
            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
            => img_bg.Opacity = 1;


        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
           => img_bg.Opacity = 0.3;


        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
           => Tg_Btn.IsChecked = false;

        private void LoadUserControlInPanel(UserControl userControl)
        {
            switch (cbSettings.Content.ToString())
            {
                    case "Apple Giddy":
                       stkPanel.Children.Clear();
                       stkPanel.Children.Add(userControl);
                       img_bg.ImageSource = null;
                       BG.Visibility = Visibility.Collapsed;
                    break;

                case "ATT Giddy":
                    stkPanel.Children.Clear();
                    stkPanel.Children.Add(userControl);
                    img_bg.ImageSource = null;
                    BG.Visibility = Visibility.Collapsed;
                    break;

                default:
                    var content = new NotificationContent
                    {
                        Background = new SolidColorBrush(Colors.Red),
                        Foreground = new SolidColorBrush(Colors.White),
                        Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/error_50px.png")),
                        Title = "Iphone",
                        Message = "Por favor seleccione un modulo en la barra superior",
                        Type = NotificationType.Error,
                        CloseOnClick = true,
                    };
                    notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);
                    break;

            }

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void StackPanel_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
         => LoadUserControlInPanel(Jig);
        private void StackPanel_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
         => LoadUserControlInPanel(Dummy);
        private void StackPanel_MouseLeftButtonUp_4(object sender, MouseButtonEventArgs e)
         => LoadUserControlInPanel(DisassemblyView);
        private void StackPanel_MouseLeftButtonUp_5(object sender, MouseButtonEventArgs e)
         => LoadUserControlInPanel(SortingView);
        private void StackPanel_MouseLeftButtonUp_6(object sender, MouseButtonEventArgs e)
         => LoadUserControlInPanel(NoModule);
        private void StackPanel_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
         => LoadUserControlInPanel(NoModule);
        private void StackPanel_MouseLeftButtonUp_7(object sender, MouseButtonEventArgs e)
         => LoadUserControlInPanel(RoxerView);
        private void StackPanel_MouseLeftButtonUp_8(object sender, MouseButtonEventArgs e)
         => LoadUserControlInPanel(DisassemblyWip);
        private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
         => LoadUserControlInPanel(llantera);

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!DarkMode)
            {
                DarkMode = true;
                ThemeManager.Current.ChangeTheme(this, "Dark.Blue");
                btnDarkMode.Content = "Modo Claro";
            }
            else
            {
                DarkMode = false;
                ThemeManager.Current.ChangeTheme(this, "Light.Blue");
                btnDarkMode.Content = "Modo Oscuro";
            }
        }

        private void cbSettings_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cbSettings.Content = "Apple Giddy";

            var content = new NotificationContent
            {


                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2A2A2A")),
                Foreground = new SolidColorBrush(Colors.White),


                Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),
                Title = "Iphone",
                Message = "Modulo de Giddy Seleccionado",
                Type = NotificationType.Success,
                CloseOnClick = true,
            };
            notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            cbSettings.Content = "ATT Giddy";



            var content = new NotificationContent
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2A2A2A")),
                Icon = new BitmapImage(new Uri("pack://application:,,,/IMG/alerts/pass_50px.png")),
                Title = "Iphone",
                Message = "Modulo de Lucia Seleccionado",
                Type = NotificationType.Success,
                CloseOnClick = true,
            };
            notificationManager.Show(content, "WindowArea", TimeSpan.FromSeconds(3), null, null, false, false);
        }

        private void StackPanel_MouseLeftButtonUp_9(object sender, MouseButtonEventArgs e)
            => LoadUserControlInPanel(LCDCenterView);
    }
}
