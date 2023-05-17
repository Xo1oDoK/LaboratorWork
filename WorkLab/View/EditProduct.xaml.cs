using LabWork.ViewModel;
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
using System.Windows.Shapes;
using WorkLab.Model;

namespace WorkLab.View
{
    /// <summary>
    /// Логика взаимодействия для EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window
    {
        public EditProduct(Product productToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedProduct = productToEdit;
            DataManageVM.ProductName = productToEdit.Name;
            DataManageVM.ProductPrice = productToEdit.Price;
            DataManageVM.ProductDescription = productToEdit.Description;


        }

        private void CancelEdit_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

