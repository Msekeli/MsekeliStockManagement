using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MsekeliStockManagement.EntityModel;
using MsekeliStockManagement.Pages;

namespace MsekeliStockManagement
{
    public partial class MainWindow : Window
    {
        private StockSystemdbEntities _db;
        public static MainWindow mainWindow;

        public MainWindow()
        {
            InitializeComponent();
            _db = new StockSystemdbEntities();
            mainWindow = this;
            LoadData();
        }

        public void LoadData()
        {
            DGridCustomer.ItemsSource = _db.Products.ToList();
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

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Product product = (Product)DGridCustomer.SelectedItem;
            var editProduct = new EditDataWin();
            editProduct.ShowProduct(product);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Product product = (Product)DGridCustomer.SelectedItem;

            if (product == null)
            {
                MessageBox.Show("Please selected a Product to Delete", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var productId = product.Id;

                var productToRemove = _db.Products.Where(x => x.Id == productId).FirstOrDefault();

                _db.Products.Remove(productToRemove);
                _db.SaveChanges();

                MessageBox.Show("Product has been succefully deleted", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddDataWin addDataWin = new AddDataWin();
            addDataWin.ShowDialog();

            // Refresh the data grid after adding a new product BarCodeGenerate_Click
            LoadData();
        }

        private void BarCodeGenerate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = (Product)DGridCustomer.SelectedItem;

                if (product == null)
                {
                    MessageBox.Show("Please select a Product to in order to Generate Barcode", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Barcodes Barcode = new Barcodes();
                    Barcode.Display(product.Barcode);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filtered = _db.Products.Where(product => product.Name.Contains(TBoxSearch.Text));
            DGridCustomer.ItemsSource = filtered.ToList();

        }
    }
}
