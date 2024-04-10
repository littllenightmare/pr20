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
        private void AddClick(object sender, RoutedEventArgs e)
        {
            Data.zakazy = null;
            addred ar = new addred();
            ar.Owner = this;
            ar.ShowDialog();
            LoadDBInDataGrid();
        }

        private void ReClick(object sender, RoutedEventArgs e)
        {
            if (dg.SelectedItems != null)
            {
                Data.zakazy = (Zakazy)dg.SelectedItem;
                addred ar = new addred();
                ar.Owner = this;
                ar.WindowAddEdit.Title = "Редактирование записи";
                ar.btnadded.Content = "Редактировать";
                ar.ShowDialog();
                LoadDBInDataGrid();
            }
        }

        private void DelClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Zakazy row = (Zakazy)dg.SelectedItem;
                    if (row != null)
                    {
                        using (Pr20Context _db = new Pr20Context())
                        {
                            _db.Zakazies.Remove(row);
                            _db.SaveChanges();
                        }

                        LoadDBInDataGrid();
                    }
                }

                catch
                {
                    MessageBox.Show("Ошибка удаления");
                }
            }
            else dg.Focus();
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

        private void SeeClick(object sender, RoutedEventArgs e)
        {
            if (dg.SelectedItems != null)
            {
                Data.zakazy = (Zakazy)dg.SelectedItem;
                addred ar = new addred();
                ar.Owner = this;
                ar..IsEnabled = false;
                ar..IsEnabled = false;
                ar..IsEnabled = false;
                ar..IsEnabled = false;
                ar..IsEnabled = false;
                ar..IsEnabled = false;
                ar.WindowAddEdit.Title = "Просмотр записи";
                ar.btnadded.IsEnabled = false;
                ar.ShowDialog();
                LoadDBInDataGrid();
            }
        }
    }
}