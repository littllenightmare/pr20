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
    /// Логика взаимодействия для zapros.xaml
    /// </summary>
    public partial class zapros : Window
    {
        Pr20Context _context = new Pr20Context();
        public zapros()
        {
            InitializeComponent();
        }
        private void zapr1Click(object sender, RoutedEventArgs e)
        {
            if (cb1 != null && cb1.Text != "")
            {
                MessageBox.Show("Будут показаны заказы данного месяца.");
                using (Pr20Context _context = new Pr20Context())
                {
                    string month = cb1.Text;
                    int month1 = cb1.SelectedIndex + 1;
                    var summa = _context.Kakoes.FromSql($"monthzak {month1}");
                    zapr1.ItemsSource = summa.ToList();
                }
            }
        }
        private void zapr2Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Будет подсчитана сумма всех заказов.");
            using (Pr20Context _context = new Pr20Context())
            {
                var summa = _context.Nikakoes.FromSql($"sumzak");
                zapr2.ItemsSource = summa.ToList();
            }
        }
    }
}
        
