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
using lab3_registry.Utils;

namespace lab3_registry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TestData data = new TestData();
        private Folder selectedFolder;
        private Parameter selectedParameter;

        public MainWindow()
        {
            InitializeComponent();

            data.Load();
            treeView.ItemsSource = data.RootGroups;

            List<Parameter> result = new List<Parameter>();
            result.Add(new Parameter(1, "(По умолчанию)", "REG_SZ", "(значение не присвоено)"));
            //result.Add(new Parameter(2, "gta.exe", "REG_SZ", "D:/GTA San Andreas/gta_sa.exe"));
            //result.Add(new Parameter(3, "Player Name", "REG_SZ", "Andrew Kramer"));

            dataGrid.ItemsSource = result;
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (treeView.SelectedItem is Folder)
            {
                Folder folder = treeView.SelectedItem as Folder;
                selectedFolder = treeView.SelectedItem as Folder;
                label.Content = "Folder: " + folder.Name + " (" + folder.FullPath + ")";

                if (folder.Parameters != null)
                {
                    dataGrid.ItemsSource = folder.Parameters;
                    Console.WriteLine("Folder parameters count: " + folder.Parameters.Count());
                }
                else
                {
                    dataGrid.ItemsSource = folder.Parameters;
                    Console.WriteLine("Folder parameters null");
                }
            }
            else
            {
                //File file = treeView.SelectedItem as File;
                //label.Content = "File: " + file.Name;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            data = new TestData();
            data.Load();
            treeView.ItemsSource = data.RootGroups;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            data.Save();
        }

        private void addParamButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text.ToString() != "" && valueTextBox.Text.ToString() != "" && typeTextBox.Text.ToString() != "")
            {
                if (selectedFolder != null)
                {
                    Parameter param = 
                        new Parameter(RandomUtils.getRandomNumber(), nameTextBox.Text.ToString(), typeTextBox.Text.ToString(), valueTextBox.Text.ToString());
                    selectedFolder.Parameters.Add(param);

                    data.Save();
                    //data.Load();

                    dataGrid.ItemsSource = null; // refresh datagrid
                    dataGrid.ItemsSource = selectedFolder.Parameters;

                    MessageBox.Show("Parameter was added successfully");
                }
                else
                {
                    MessageBox.Show("No folder or subfolder selected");
                }
            }
            else
            {
                MessageBox.Show("Please, fill all fields.");
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Parameter param = (Parameter) dataGrid.SelectedItem;
                selectedParameter = param;

                nameTextBox.Text = param.Name;
                valueTextBox.Text = param.Value;
                typeTextBox.Text = param.Type;
            }
            catch { }
        }

        private void saveSelectedParamButton_Click(object sender, RoutedEventArgs e)
        {
            Parameter paramToEdit = new Parameter();

            if (selectedParameter != null)
            {
                for (int i = 0; i < selectedFolder.Parameters.Count(); i++)
                {
                    if (selectedParameter.Id == selectedFolder.Parameters[i].Id)
                    {
                        paramToEdit = selectedFolder.Parameters[i];

                        selectedFolder.Parameters[i].Name = nameTextBox.Text.ToString();
                        selectedFolder.Parameters[i].Type = typeTextBox.Text.ToString();
                        selectedFolder.Parameters[i].Value = valueTextBox.Text.ToString();

                        break;
                    }
                }

                data.Save();
                //data.Load();

                dataGrid.ItemsSource = null; // refresh datagrid
                dataGrid.ItemsSource = selectedFolder.Parameters;

                MessageBox.Show("Parameter was edited and saved successfully");
            }
            else
            {
                MessageBox.Show("Parameter is not selected");
            }
        }

        private void deleteParam_Click(object sender, RoutedEventArgs e)
        {
            if (selectedParameter != null)
            {
                for (int i = 0; i < selectedFolder.Parameters.Count(); i++)
                {
                    if (selectedParameter.Id == selectedFolder.Parameters[i].Id)
                    {
                        selectedFolder.Parameters.Remove(selectedFolder.Parameters[i]);
                        break;
                    }
                }

                data.Save();
                //data.Load();

                dataGrid.ItemsSource = null; // refresh datagrid
                dataGrid.ItemsSource = selectedFolder.Parameters;

                MessageBox.Show("Parameter was deleted successfully");
            }
            else
            {
                MessageBox.Show("Parameter is not selected");
            }
        }

        private void addSubfolderButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFolder != null)
            {
                if (newFolderTextBox.Text.ToString() != "")
                {
                    Folder newFolder = new Folder()
                    {
                        Key = RandomUtils.getRandomNumber(),
                        Name = newFolderTextBox.Text.ToString(),
                        SubFolders = new List<Folder>(),
                        Parameters = new List<Parameter>(),
                        FullPath = selectedFolder.FullPath + "\\" + newFolderTextBox.Text.ToString(),
                        Files = new List<File>()
                    };

                    selectedFolder.SubFolders.Add(newFolder);

                    treeView.ItemsSource = null;
                    treeView.ItemsSource = data.RootGroups;

                    data.Save();
                }
                else
                {
                    MessageBox.Show("Name of new folder is empty");
                }
            }
            else
            {
                MessageBox.Show("No folder or subfolder selected");
            }
        }

        private void deleteSubfolderButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFolder != null)
            {
                data.DeleteFolder(selectedFolder);

                treeView.ItemsSource = null;
                treeView.ItemsSource = data.RootGroups;

                data.Save();
            }
            else
            {
                MessageBox.Show("No folder or subfolder selected");
            }
        }

    }
}
