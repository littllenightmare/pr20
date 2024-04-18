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
            LoadDBInListBox();
        }
        void LoadDBInListBox()
        {
            using (Pr20Context _lb = new Pr20Context())
            {
                int selectedIndex = ListByer.SelectedIndex;
                _lb.Clients.Load();
                //ListByer.ItemsSource = _lb.Clients.Surname;
                if (selectedIndex != -1)
                {
                    if (selectedIndex == ListByer.Items.Count) selectedIndex--;
                    ListByer.SelectedIndex = selectedIndex;
                    ListByer.ScrollIntoView(ListByer.SelectedItem);
                }
            }
        }
    }
}
