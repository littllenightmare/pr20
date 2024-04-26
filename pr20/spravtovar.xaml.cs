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
    /// Логика взаимодействия для spravka.xaml
    /// </summary>
    public partial class spravtovar : Window
    {
        public spravtovar()
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
                int selectedIndex = gridtovar.SelectedIndex;
                _dg.Catalogs.Load();
                gridtovar.ItemsSource = _dg.Catalogs.ToList();
                if (selectedIndex != -1)
                {
                    if (selectedIndex == gridtovar.Items.Count) selectedIndex--;
                    gridtovar.SelectedIndex = selectedIndex;
                    gridtovar.ScrollIntoView(gridtovar.SelectedItem);
                }
            }
        }

        private void addclick(object sender, RoutedEventArgs e)
        {
            Data.catalog = null;
            addtovar at = new addtovar();
            at.Owner = this;
            at.ShowDialog();
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
                    Catalog row = (Catalog)gridtovar.SelectedItem;
                    if (row != null)
                    {
                        using (Pr20Context _dg = new Pr20Context())
                        {
                            _dg.Catalogs.Remove(row);
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
            else gridtovar.Focus();
        }
    }
}
