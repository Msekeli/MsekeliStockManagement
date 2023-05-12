using MsekeliStockManagement.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public partial class AddDataWin : Window
    {
        StockSystemdbEntities _db = new StockSystemdbEntities();
        public AddDataWin()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a new Product entity and set its properties based on the user inputs
                Product newProduct = new Product()
                {
                    Name = TBP_name.Text,
                    CostPrice = decimal.Parse(TBC_price.Text),
                    SellingPrice = decimal.Parse(TBS_price.Text),
                    Quantity = int.Parse(TBQuality.Text),
                    Barcode = GenerateBarcode().ToString(),
                    Category = TBP_category.Text
                };

                // Add the new product to the database and save the changes
                _db.Products.Add(newProduct);
                _db.SaveChanges();

                // Display a message to the user indicating that the product was added successfully
                MessageBox.Show("Product added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Close the AddDataWin window
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding product: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
         private int GenerateBarcode()
        {
            int randomBarcode = Guid.NewGuid().GetHashCode();

            if (Math.Abs(randomBarcode) != randomBarcode)
            {
                return GenerateBarcode();
            }
            else
            {
                return randomBarcode;
            }
        }

    }
}
