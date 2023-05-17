using WorkLab.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using WorkLab.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Documents;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Threading;

namespace LabWork.ViewModel
{
    public class DataManageVM : INotifyPropertyChanged
    {

        //все продукты
        private List<Product> allProducts = DataWorker.GetAllProducts();
        public List<Product> AllProducts
        {
            get { return allProducts; }
            set { allProducts = value; NotifyPropertyChanged("AllProducts"); }
        }




        #region OpenWindows metodhs
        //открытие окна добавки
        private void OpenAddProductWindow()
        {

            AddNewProducts addProduct = new AddNewProducts();
            SetCenterPosAndOpen(addProduct);

        }
        // открытие окна эдита
        private void OpenEditProductWindow(Product product)
        {

            EditProduct editProduct = new EditProduct(product);
            SetCenterPosAndOpen(editProduct);

        }
        private void SetCenterPosAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        #endregion
        #region Add Commands
        public static string ProductName { get; set; }
        public static double? ProductPrice { get; set; }
        public static string ProductDescription { get; set; }

        private RelayCommand addNewProduct;
        public RelayCommand AddNewProduct
        {
            get
            {
                return addNewProduct ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    if (!RegExpr.IsNameVld(ProductName))
                    {
                        SetRedBlockControl(wnd, "NameBlock");
                        return;
                    }
                    if (!RegExpr.IsPriceVld(ProductPrice))
                    {
                        SetRedBlockControl(wnd, "PriceBlock");
                        return;
                    }
                    if (!RegExpr.IsDescVld(ProductDescription))
                    {
                        SetRedBlockControl(wnd, "DescriptionBlock");
                        return;
                    }
                    else
                    {
                        DataWorker.AddProduct(ProductName, ProductPrice, ProductDescription);
                        UpdateAllProductsView();
                        OpenMessageWindow("Готово");
                        SetNullProductsProps();
                        wnd.Close();
                    }
                }
                    );
            }
        }
        #endregion  
        public static Product SelectedProduct { get; set; }
        #region Delete Commands
        private RelayCommand deleteSelectedProduct;
        public RelayCommand DeleteSelectedProduct
        {
            get
            {
                return deleteSelectedProduct ?? new RelayCommand(obj =>
                {

                    if (SelectedProduct == null)
                    {
                        SetNullProductsProps();
                        OpenMessageWindow("Ничего не выбрано");
                    }
                    else
                    {
                        DataWorker.DeleteProduct(SelectedProduct);
                        UpdateAllProductsView();
                    }
                });
            }
        }
        #endregion
        #region Edit Commands
        private RelayCommand editSelectedProduct;
        public RelayCommand EditSelectedProduct
        {
            get
            {
                return editSelectedProduct ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    if (SelectedProduct != null)
                    {
                        if (!RegExpr.IsNameVld(ProductName) || !RegExpr.IsDescVld(ProductDescription) || !RegExpr.IsPriceVld(ProductPrice))
                        {
                            OpenMessageWindow("Не корректные данные, поле не может быть пустым или превышать кол-во в 20 символов");
                        }
                        else
                        {
                            DataWorker.EditProduct(SelectedProduct, ProductName, ProductDescription, ProductPrice);
                            UpdateAllProductsView();
                            OpenMessageWindow("Готово!");
                            SetNullProductsProps();
                            wnd.Close();
                        }
                    }
                    else
                    {
                        OpenMessageWindow("Ничего не выбрано");
                    }
                }
                );
            }
        }
        #endregion
        private RelayCommand openAddNewProductWindow;
        public RelayCommand OpenAddNewProductWindow
        {
            get
            {
                return openAddNewProductWindow ?? new RelayCommand(obj =>
                {
                    OpenAddProductWindow();
                }
                );
            }
        }
        private RelayCommand editProductWindow;
        public RelayCommand EditProductWindow
        {
            get
            {
                return editProductWindow ?? new RelayCommand(obj =>
                {
                    OpenEditProductWindow(SelectedProduct);
                }
                );
            }
        }
        private void OpenMessageWindow(string message)
        {
            MessageView messageWindow = new MessageView(message);
            SetCenterPositionAndOwner(messageWindow);
        }
        private void SetRedBlockControl(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }
        private void SetNullProductsProps()
        {
            ProductName = null;
            ProductPrice = 0;
            ProductDescription = null;
        }
        private void UpdateAllProductsView()
        {
            AllProducts = DataWorker.GetAllProducts();
            MainWindow.AllProductsView.ItemsSource = null;
            MainWindow.AllProductsView.Items.Clear();
            MainWindow.AllProductsView.ItemsSource = AllProducts;
            MainWindow.AllProductsView.Items.Refresh();
        }
        private void SetCenterPositionAndOwner(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}