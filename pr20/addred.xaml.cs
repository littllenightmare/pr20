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

namespace pr20
{
    /// <summary>
    /// Логика взаимодействия для addred.xaml
    /// </summary>
    public partial class addbyer : Window
    {
        Pr20Context _db = new Pr20Context();
        Client _client;
        public addbyer()
        {
            InitializeComponent();
        }

        private void AddEditClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (tbcode.Text.Length < 4 || tbcode.Text.Length > 5|| Int32.TryParse(tbcode.Text, out int a) == false) errors.AppendLine("Введите код (длина кода 4)");
            if (tbname.Text.Length == 0) errors.AppendLine("Введите имя");
            if (tbphone.Text.Length != 11)  errors.AppendLine("Введите корректный номер телефона (его длина 11)");
            if (BigInteger.TryParse(tbphone.Text, out BigInteger b) == false) errors.AppendLine("Введите корректный номер телефона (не пишите + или - в начале)");
            if (tbadress.Text.Length == 0) errors.AppendLine("Выберите адрес");
            if (cbcity.Text != "Орск" && cbcity.Text != "Коломна" && cbcity.Text != "Москва" && cbcity.Text != "Нижний Новгород" && cbcity.Text != "Грозный"
                && cbcity.Text != "Рязань" && cbcity.Text != "Зарайск") errors.AppendLine("Выберите город");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            //try
            //{
                if (Data.client == null)
                {
                    _db.Clients.Add(_client);
                    _db.SaveChanges();
                }
                else
                {
                    _db.SaveChanges();
                }
                MessageBox.Show("Информация сохранена");
                this.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            if (Data.client == null)
            {
                _client = new Client();
            }
            else
            {
                _client = _db.Clients.Find(Data.client.CodeClient);
            }
            WindowAddEdit.DataContext = _client;
        }
    }
}
