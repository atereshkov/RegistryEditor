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

namespace lab3_registry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TestData data = new TestData();
            data.Load();
            treeView.ItemsSource = data.Groups;

            List<Item> result = new List<Item>();
            result.Add(new Item(1, "(По умолчанию)", "REG_SZ", "(значение не присвоено)"));
            result.Add(new Item(2, "gta.exe", "REG_SZ", "D:/GTA San Andreas/gta_sa.exe"));
            result.Add(new Item(3, "Player Name", "REG_SZ", "Andrew Kramer"));

            dataGrid.ItemsSource = result;
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (treeView.SelectedItem is Folder)
            {
                Folder folder = treeView.SelectedItem as Folder;
                label.Content = folder.Name;
            }
            else
            {
                File file = treeView.SelectedItem as File;
                label.Content = file.Name;
            }
        }

      
    }
}
