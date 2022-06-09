using System.Linq;
using System.Windows;
using System.Windows.Controls;

using GOS.WorkWithDB;

namespace GOS.Forms
{
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
        /// Перезагружает список пользователей после закрытия дополнительных диалоговых форм
        /// </summary>
        /// <param name="sender">Кто отправил сигнал</param>
        /// <param name="e">Аргументы</param>
        private void DialogFormClosed(object sender, System.EventArgs e)
        {
            ReloadListBoxUsers();
        }

        /// <summary>
        /// Показывает форму добавления пользователя
        /// </summary>
        /// <param name="sender">Кто отправил сигнал</param>
        /// <param name="e">Аргументы</param>
        private void MenuItem_addUser_OnClick(object sender, RoutedEventArgs e)
        {
            var addUserForm = new AddUserForm();
            addUserForm.Closed += DialogFormClosed;
            addUserForm.ShowDialog();
        }

        /// <summary>
        /// Возвращение к окну авторизации
        /// </summary>
        /// <param name="sender">Кто отправил сигнал</param>
        /// <param name="e">Аргументы</param>
        private void MenuItem_exit_OnClick(object sender, RoutedEventArgs e)
        {
            var authorizationForm = new AuthorizationForm();
            authorizationForm.Show();
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
        /// Открывает окно с формой редактирования пользователя
        /// </summary>
        /// <param name="sender">Кто отправил сигнал</param>
        /// <param name="e">Аргументы</param>
        private void Button_changeRole_OnClick(object sender, RoutedEventArgs e)
        {
            if (listBox_users.SelectedItem == null)
            {
                HelpMethods.ShowWarning("Вы не выбрали пользователя!");
                return;
            }

            var editRoleForm = new EditRoleForm((UserItem)listBox_users.SelectedItem);
            editRoleForm.Closed += DialogFormClosed;
            editRoleForm.ShowDialog();
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
