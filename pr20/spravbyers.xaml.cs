using Microsoft.EntityFrameworkCore;
using pr20.database;
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

namespace pr20
{
    /// <summary>
    /// Логика взаимодействия для spravbyers.xaml
    /// </summary>
    public partial class spravbyers : Window
    {
        public spravbyers()
        {
            InitializeComponent();
        }
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }
        void LoadDBInDataGrid()
        {
            using (Pr20Context _dg = new Pr20Context())
            {
                int selectedIndex = databyer.SelectedIndex;
                _dg.Clients.Load();
                databyer.ItemsSource = _dg.Clients.ToList();
                if (selectedIndex != -1)
                {
                    if (selectedIndex == databyer.Items.Count) selectedIndex--;
                    databyer.SelectedIndex = selectedIndex;
                    databyer.ScrollIntoView(databyer.SelectedItem);
                }
            }
        }
        private void addclick(object sender, RoutedEventArgs e)
        {
            Data.client = null;
            addbyer ab = new addbyer();
            ab.Owner = this;
            ab.ShowDialog();
            LoadDBInDataGrid();
        }

        private void delClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Client row = (Client)databyer.SelectedItem;
                    if (row != null)
                    {
                        using (Pr20Context _dg = new Pr20Context())
                        {
                            _dg.Clients.Remove(row);
                            _dg.SaveChanges();
                        }

                        LoadDBInDataGrid();
                    }
                }

                catch
                {
                    MessageBox.Show("Ошибка удаления");
                }
            }
            else databyer.Focus();
        }
    }
}
