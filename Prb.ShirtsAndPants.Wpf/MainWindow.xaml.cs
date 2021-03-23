using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Prb.ShirtsAndPants.Core;

namespace Prb.ShirtsAndPants.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // 47:00
        public MainWindow()
        {
            InitializeComponent();
        }

        Shop shop; // alle functionaliteiten verlopen via deze service klasse!


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            shop = new Shop();
            ShowCustomers();
            PopulateComboBoxes();
            grpCustomer.IsEnabled = true;
            grpSales.IsEnabled = false;
            InitCustomerControls();
            InitSalesControls();
        }

        private void ShowCustomers()
        {
            lstCustomers.ItemsSource = null;
            lstCustomers.ItemsSource = shop.Customers;
        }

        private void ShowCustomersSales()
        {
            lstSalesHistory.ItemsSource = null;
            lstSalesHistory.ItemsSource = shop.CustomerHistory((Customer)lstCustomers.SelectedItem, out decimal total);
            lblTotalCustomerSales.Content = total.ToString("#,##0.00");
        }

        private void ShowRegister()
        {
            lstSales.ItemsSource = null;
            lstSales.ItemsSource = shop.Register;
        }

        private void PopulateComboBoxes() // merken en maten toevoegen in comboboxes
        {
            cmbPantsSize.ItemsSource = shop.Sizes;
            cmbShirtSize.ItemsSource = shop.Sizes;
            cmbPantsBrand.ItemsSource = shop.Brands;
            cmbShirtBrand.ItemsSource = shop.Brands;
        }

        private void InitCustomerControls()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtCity.Text = "";
            rdbFeMale.IsChecked = false;
            rdbMale.IsChecked = false;
            cmbShirtSize.SelectedIndex = -1;
            cmbPantsSize.SelectedIndex = -1;
            lblCustomerPantsSize.Content = "";
            lblCustomerShirtSize.Content = "";
            lstSalesHistory.ItemsSource = null;
        }

        private void InitSalesControls()
        {
            cmbPantsBrand.SelectedIndex = -1;
            cmbShirtBrand.SelectedIndex = -1;
            txtSellingPrice.Text = "0";
        }

        private void MakeRegisterStats()
        {
            lblRegisterTotal.Content = shop.CurrentRegisterTotal().ToString("#,##0.00");
        }
        private void btnUpdateSizes_Click(object sender, RoutedEventArgs e) // maten van de klant aanpassen
        {
            if (lstCustomers.SelectedItem == null)
            {
                MessageBox.Show("Selecteer eerst een klant uit de lijst!", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                lstCustomers.Focus();
                return;
            }
            Customer customer = (Customer)lstCustomers.SelectedItem;
            shop.UpdateCustomerSizes(customer, cmbShirtSize.SelectedItem.ToString(), cmbPantsSize.SelectedItem.ToString());
            lblCustomerPantsSize.Content = customer.PantsSize;
            lblCustomerShirtSize.Content = customer.ShirtSize;
            
            
        }

        private void btnSellShirt_Click(object sender, RoutedEventArgs e)
        {
            if (lstCustomers.SelectedItem == null)
            {
                MessageBox.Show("Selecteer eerst een klant uit de lijst!", "Fout!", MessageBoxButton.OK, MessageBoxImage.Error);
                lstCustomers.Focus();
                return;
            }
            if (cmbShirtBrand.SelectedItem == null)
            {
                MessageBox.Show("Kies een merk van hemden!", "Fout!", MessageBoxButton.OK, MessageBoxImage.Error);
                cmbShirtBrand.Focus();
                return;
            }

            string brand = cmbShirtBrand.SelectedItem.ToString();
            string strPrice = txtSellingPrice.Text.Trim().Replace(".", ","); // vervangt de punten door komma's 
            decimal price = 0M;
            decimal.TryParse(strPrice, out price);
            string size = lblCustomerShirtSize.Content.ToString();
            string response = shop.RegisterSale((Customer)lstCustomers.SelectedItem, ProductType.Shirt, brand, size, price);
            if (response != "")
            {
                MessageBox.Show(response, "Fout!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MakeRegisterStats();
            ShowRegister();
            InitSalesControls();
        }

        private void btnSellPants_Click(object sender, RoutedEventArgs e)
        {
            if (lstCustomers.SelectedItem == null)
            {
                MessageBox.Show("Selecteer eerst een klant uit de lijst!", "Fout!", MessageBoxButton.OK, MessageBoxImage.Error);
                lstCustomers.Focus();
                return;
            }
            if (cmbPantsBrand.SelectedItem == null)
            {
                MessageBox.Show("Kies een merk van broek", "Fout!", MessageBoxButton.OK, MessageBoxImage.Error);
                cmbPantsBrand.Focus();
                return;
            }

            string brand = cmbPantsBrand.SelectedItem.ToString();
            decimal price = 0M;
            decimal.TryParse(txtSellingPrice.Text, out price);
            string size = lblCustomerPantsSize.Content.ToString();
            string response = shop.RegisterSale((Customer)lstCustomers.SelectedItem, ProductType.Pants, brand, size, price);
            if (response != "")
            {
                MessageBox.Show(response, "Fout!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MakeRegisterStats();
            ShowRegister();
            InitSalesControls();
        }

        private void btnNewSale_Click(object sender, RoutedEventArgs e)
        {
            if (lstCustomers.SelectedItem == null)
            {
                MessageBox.Show("Selecteer eerst een klant uit de lijst!", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                lstCustomers.Focus();
                return;
            }
            InitSalesControls();
            grpCustomer.IsEnabled = false;
            grpSales.IsEnabled = true;
        }

        private void btnCloseSale_Click(object sender, RoutedEventArgs e)
        {
            shop.ConvertRegisterToSales(); // de verkoop naar verkoopgeschiedenis brengen
            ShowCustomersSales();
            ShowRegister();
            MakeRegisterStats();
            grpCustomer.IsEnabled = true;
            grpSales.IsEnabled = false;
        }

        private void btnCancelSale_Click(object sender, RoutedEventArgs e)
        {
            grpCustomer.IsEnabled = true;
            grpSales.IsEnabled = false;
            shop.Register.Clear();
            lblRegisterTotal.Content = "0";
            ShowRegister();
            grpCustomer.IsEnabled = true;
            grpSales.IsEnabled = false;
        }

        private void lstCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e) // klant details vullen!
        {
            InitCustomerControls();
            if (lstCustomers.SelectedItem == null)
            {
                return;
            }

            Customer customer = (Customer)lstCustomers.SelectedItem;
            txtFirstName.Text = customer.Firstname;
            txtLastName.Text = customer.Lastname;
            txtCity.Text = customer.City;
            if (customer.Gender == Gender.Male)
            {
                rdbMale.IsChecked = true;
            }
            else
            {
                rdbFeMale.IsChecked = true;
            }

            cmbShirtSize.SelectedItem = customer.ShirtSize;
            cmbPantsSize.SelectedItem = customer.PantsSize;
            lblCustomerPantsSize.Content = customer.PantsSize;
            lblCustomerShirtSize.Content = customer.ShirtSize;
            ShowCustomersSales();
        }
    }
}
