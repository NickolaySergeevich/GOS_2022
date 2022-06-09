using System.Linq;
using System.Windows;
using System.Windows.Controls;

using GOS.WorkWithDB;

namespace GOS.Forms
{
    /// <summary>
    /// Класс для отображения данных о пользователе
    /// </summary>
    public class UserItem
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Возраст пользователя
        /// </summary>
        public string Age { get; set; }
        /// <summary>
        /// Роль пользователя в системе
        /// </summary>
        public string UserRole { get; set; }
        /// <summary>
        /// Почтовый адрес пользователя
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// Офис, к которому прикреплен пользователь
        /// </summary>
        public string Office { get; set; }
        /// <summary>
        /// Задний фон пользователя
        /// </summary>
        public string BackColor { get; set; }
    }

    /// <summary>
    /// Класс окна администратора
    /// </summary>
    public partial class AdministratorForm
    {
        /// <summary>
        /// Конструктор
        /// <br>Создает список офисов</br>
        /// </summary>
        public AdministratorForm()
        {
            InitializeComponent();

            comboBox_offices.Items.Add("All offices");
            comboBox_offices.SelectedIndex = 0;
            foreach (var officeName in WorkWithDb.Instance.GetAllOfficesForAdmin())
                comboBox_offices.Items.Add(officeName);
        }

        /// <summary>
        /// Обновляет список пользователей
        /// </summary>
        private void ReloadListBoxUsers()
        {
            var usersData = WorkWithDb.Instance.GetOfficeUsersForAdmin(comboBox_offices.SelectedItem.ToString());
            if (usersData == null)
                return;
            var usersItems = usersData.Select(user => new UserItem()
            {
                Name = user["Name"],
                LastName = user["LastName"],
                Age = user["Age"],
                UserRole = user["UserRole"],
                EmailAddress = user["EmailAddress"],
                Office = user["Office"],
                BackColor = user["Active"] == "False" ? "Red" : "White"
            })
                .ToList();
            listBox_users.ItemsSource = usersItems;
        }

        /// <summary>
        /// Показывает форму добавления пользователя
        /// </summary>
        /// <param name="sender">Кто отправил сигнал</param>
        /// <param name="e">Аргументы</param>
        private void MenuItem_addUser_OnClick(object sender, RoutedEventArgs e)
        {
            var form = new AddUserForm();
            form.ShowDialog();
        }

        /// <summary>
        /// Возвращение к окну авторизации
        /// </summary>
        /// <param name="sender">Кто отправил сигнал</param>
        /// <param name="e">Аргументы</param>
        private void MenuItem_exit_OnClick(object sender, RoutedEventArgs e)
        {
            var form = new AuthorizationForm();
            form.Show();
            Close();
        }

        /// <summary>
        /// Обновление списка пользователей при выборе нового офиса
        /// </summary>
        /// <param name="sender">Кто отправил сигнал</param>
        /// <param name="e">Аргументы</param>
        private void ComboBox_offices_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReloadListBoxUsers();
        }

        /// <summary>
        /// Изменяет значение - заблокирован ли пользователь или нет
        /// </summary>
        /// <param name="sender">Кто отправил сигнал</param>
        /// <param name="e">Аргументы</param>
        private void Button_enableOrDisableLogin_OnClick(object sender, RoutedEventArgs e)
        {
            if (listBox_users.SelectedItem == null)
            {
                HelpMethods.ShowWarning("Вы не выбрали пользователя");
                return;
            }

            WorkWithDb.Instance.UpdateActiveForUser(((UserItem)listBox_users.SelectedItem).EmailAddress);
            ReloadListBoxUsers();
        }
    }
}
