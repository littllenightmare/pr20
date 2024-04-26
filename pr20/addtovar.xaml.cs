using pr20.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
using System.Xml.Linq;

namespace pr20
{
    /// <summary>
    /// Логика взаимодействия для addtovar.xaml
    /// </summary>
    public partial class addtovar : Window
    {
        Pr20Context _db = new Pr20Context();
        Catalog _catalog;
        public addtovar()
        {
            InitializeComponent();
        }
        private void AddEditClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (tbcode.Text.Length == 0 || Int32.TryParse(tbcode.Text, out int a) == false) errors.AppendLine("Введите код");
            if (tbname.Text.Length == 0) errors.AppendLine("Введите название");
            if (tbprice.Text.Length == 0 || Int32.TryParse(tbprice.Text, out a) == false) errors.AppendLine("Введите цену");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                if (Data.catalog == null)
                {
                    _db.Catalogs.Add(_catalog);
                    _db.SaveChanges();
                }
                else
                {
                    _db.SaveChanges();
                }
                MessageBox.Show("Информация сохранена");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            if (Data.catalog == null)
            {
                _catalog = new Catalog();
            }
            else
            {
                _catalog = _db.Catalogs.Find(Data.catalog.CodeObj);
            }
           windowaddtovar.DataContext = _catalog;
        }
    }
}
