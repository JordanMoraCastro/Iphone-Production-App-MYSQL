using System.Windows;
using System.Windows.Controls;

namespace LCD_Installation.Views
{
    public partial class SubAssyView : UserControl
    {
        public SubAssyView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    System.Drawing.Image img = null;
            //    BitmapImage bimg = new BitmapImage();
            //    using (var ms = new MemoryStream())
            //    {
            //        BarcodeWriter writer;
            //        writer = new BarcodeWriter() { Format = BarcodeFormat.CODE_93 };
            //        writer.Options.Height = 80;
            //        writer.Options.Width = 280;
            //        writer.Options.PureBarcode = true;
            //        img = writer.Write(this.txtCode.Text);
            //        img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //        ms.Position = 0;
            //        bimg.BeginInit();
            //        bimg.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            //        bimg.CacheOption = BitmapCacheOption.OnLoad;
            //        bimg.UriSource = null;
            //        bimg.StreamSource = ms;
            //        bimg.EndInit();
            //        this.qrImg.Source = bimg;

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //PrintDialog dialog = new PrintDialog();

            //if (dialog.ShowDialog() == true)
            //{

            //    dialog.PrintVisual(qrImg, "Impresión");
            //}
        }
    }
}
