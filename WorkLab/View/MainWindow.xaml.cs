using LabWork.ViewModel;
using System.Windows;


namespace WorkLab.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static System.Windows.Controls.ListView AllProductsView;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            AllProductsView = ViewAllProducts;
        }
    }
}
