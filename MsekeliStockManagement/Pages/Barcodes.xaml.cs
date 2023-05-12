using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZXing;
using Image = System.Drawing.Image;

namespace MsekeliStockManagement.Pages
{
    /// <summary>
    /// Interaction logic for Barcodes.xaml
    /// </summary>
    public partial class Barcodes : Window
    {
        public Barcodes()
        {
            InitializeComponent();
        }

        public void Display(string barcodetext)
        {
            Show();
            try
            {
                Image imageformat = null;
                BitmapImage bitImage = new BitmapImage();
                using (var memory = new MemoryStream())
                {
                    BarcodeWriter barwriter;
                    barwriter = new BarcodeWriter()
                    {
                        Format = BarcodeFormat.CODE_93,
                        Options = new ZXing.Common.EncodingOptions
                        {
                            Height = 100,
                            Width = 300,
                            PureBarcode = true
                        }
                    };

                    imageformat = barwriter.Write(barcodetext);
                    imageformat.Save(memory, System.Drawing.Imaging.ImageFormat.Jpeg);
                    memory.Position = 0;
                    bitImage.BeginInit();
                    bitImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitImage.UriSource = null;
                    bitImage.StreamSource = memory;
                    bitImage.EndInit();
                    imgbarcode.Source = bitImage;
                    productBarcode.Text = barcodetext;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
