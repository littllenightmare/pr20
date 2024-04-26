using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using pr20.database;

namespace pr20
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InfoClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Создать многотабличную базу данных, в которой отобразятся сведения о поставках.\r\n Разработать к ней интерфейс для работы пользователя.\r\n Выполнила студентка группы ИСП-31 Кулькова Ангелина");
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void LoadDBInDataGrid()
        {
            using (Pr20Context _db = new Pr20Context())
            {
                int SelectedIndex = dg.SelectedIndex;
                _db.Zakazies.Load();
                _db.Catalogs.Load();
                _db.Clients.Load();
                dg.ItemsSource = _db.Zakazies.ToList();
                if (SelectedIndex != -1)
                {
                    if (SelectedIndex == dg.Items.Count) SelectedIndex--;
                    dg.SelectedIndex = SelectedIndex;
                    dg.ScrollIntoView(dg.SelectedItem);
                }
                dg.Focus();
            }
        }
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }
        private void FindClick(object sender, RoutedEventArgs e)
        {
            if (findtb.Text.IsNullOrEmpty() == false)
            {
                List<Zakazy> listItem2 = (List<Zakazy>)dg.ItemsSource;
                var finded2 = (listItem2.Where(p => p.NumZak.ToString().Contains(findtb.Text) ||
                    p.DateZak.ToString().Contains(findtb.Text) ||
                    p.CodeClient.ToString().Contains(findtb.Text) ||
                p.CodeObj.ToString().Contains(findtb.Text) ||
                p.CodeClientNavigation.CodeClient.ToString().Contains(findtb.Text) ||
                p.CodeClientNavigation.Name.ToString().Contains(findtb.Text) ||
                p.CodeClientNavigation.City.ToString().Contains(findtb.Text) ||
                p.CodeClientNavigation.Address.ToString().Contains(findtb.Text) ||
                p.CodeClientNavigation.Phone.ToString().Contains(findtb.Text) ||
                p.CodeObjNavigation.CodeObj.ToString().Contains(findtb.Text) ||
                p.CodeObjNavigation.Name.ToString().Contains(findtb.Text) ||
                p.CodeObjNavigation.Price.ToString().Contains(findtb.Text) ||
                p.Amount.ToString().Contains(findtb.Text)));
                if (finded2.Count() > 0)
                {
                    Zakazy item = finded2.First();
                    dg.SelectedItem = item;
                    dg.ScrollIntoView(item);
                    dg.Focus();
                }
            }
        }

        private void spravClientClick(object sender, RoutedEventArgs e)
        {
            Data.client = null;
            spravbyers f = new spravbyers();
            f.Owner = this;
            f.ShowDialog();
            LoadDBInDataGrid();
        }

        private void spravObjClick(object sender, RoutedEventArgs e)
        {
            Data.catalog = null;
            spravtovar f = new spravtovar();
            f.Owner = this;
            f.ShowDialog();
            LoadDBInDataGrid();
        }

        private void zaprosiClick(object sender, RoutedEventArgs e)
        {
            zapros f = new zapros();
            f.Owner = this;
            f.ShowDialog();
        }
    }
}