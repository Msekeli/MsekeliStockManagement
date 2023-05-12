using MsekeliStockManagement.EntityModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

namespace MsekeliStockManagement.Pages
{
    public partial class EditDataWin : Window
    {
        StockSystemdbEntities _db = new StockSystemdbEntities();
        public Product Product { get; set; }
        public EditDataWin()
        {
            InitializeComponent();
        }
        public void ShowProduct(Product products)
        {
            Product = products;
            TBP_category.Text = products.Category;
            TBP_name.Text = products.Name;
            TBC_price.Text = products.CostPrice.ToString();
            TBS_price.Text = products.SellingPrice.ToString();
            TBQuality.Text = products.Quantity.ToString();
            TBB_code.Text = products.Barcode.ToString();
            Show();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Product.Name = TBP_name.Text;
            Product.Category = TBP_category.Text;
            Product.CostPrice = Convert.ToDecimal(TBC_price.Text);
            Product.SellingPrice = Convert.ToDecimal(TBS_price.Text);
            Product.Quantity = Convert.ToInt32(TBQuality.Text);
            Product.Barcode = TBB_code.Text.ToString();

            _db.Products.AddOrUpdate(Product);
            _db.SaveChanges();
            Close();

            MainWindow.mainWindow.LoadData();
            MessageBox.Show("Edit Product Success!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
