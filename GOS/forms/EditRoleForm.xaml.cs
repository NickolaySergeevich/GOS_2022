using System.Windows;

using GOS.WorkWithDB;

namespace GOS.Forms
{
    /// <summary>
    /// Класс окна редактирования пользователя
    /// </summary>
    public partial class EditRoleForm
    {
        /// <summary>
        /// Переменная, хранящая информацию о пользователе
        /// </summary>
        private readonly UserItem _user;

        /// <summary>
        /// Конструктор
        /// <br>Заполняет <c>ComboBox</c> с офисами</br>
        /// <br>Заполняет данные о пользователе</br>
        /// </summary>
        public EditRoleForm(UserItem user)
        {
            InitializeComponent();

            _user = user;

            foreach (var officeName in WorkWithDb.Instance.GetAllOfficesForAdmin())
                comboBox_offices.Items.Add(officeName);
            comboBox_offices.SelectedIndex = 0;

            textBox_emailAddress.Text = user.EmailAddress;
            textBox_firstName.Text = user.Name;
            textBox_lastName.Text = user.LastName;
            comboBox_offices.SelectedIndex = comboBox_offices.Items.IndexOf(user.Office);
            switch (user.UserRole)
            {
                case "User":
                    radioButton_user.IsChecked = true;
                    break;
                case "Administrator":
                    radioButton_administrator.IsChecked = true;
                    break;
            }
        }

        /// <summary>
        /// Внесение изменений в бд, если все поля правильно заполнены
        /// </summary>
        /// <param name="sender">Кто отправил сигнал</param>
        /// <param name="e">Аргументы</param>
        private void Button_apply_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_emailAddress.Text) || string.IsNullOrEmpty(textBox_firstName.Text) ||
                string.IsNullOrEmpty(textBox_lastName.Text))
            {
                HelpMethods.ShowWarning("Не все поля заполнены!");
                return;
            }

            var roleName = string.Empty;
            if (radioButton_user.IsChecked == true)
                roleName = "User";
            else if (radioButton_administrator.IsChecked == true)
                roleName = "Administrator";

            if (WorkWithDb.Instance.UpdateUserInformation(textBox_emailAddress.Text, textBox_firstName.Text,
                    textBox_lastName.Text, comboBox_offices.SelectedItem.ToString(),
                    roleName, _user.EmailAddress))
            {
                HelpMethods.ShowMessage("Данные успешно обновлены!");
                _user.EmailAddress = textBox_emailAddress.Text;
            }
            else
                HelpMethods.ShowWarning("Пользователь с такой почтой уже есть!");
        }

        /// <summary>
        /// Закрывает окно редактирования пользователя
        /// </summary>
        /// <param name="sender">Кто отправил сигнал</param>
        /// <param name="e">Аргументы</param>
        private void Button_cancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
