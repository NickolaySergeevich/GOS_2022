using System.Windows;

using GOS.WorkWithDB;

namespace GOS.Forms
{
    public partial class AddUserForm
    {
        /// <summary>
        /// Конструктор
        /// <br>Заполняет <c>ComboBox</c> с офисами</br>
        /// </summary>
        public AddUserForm()
        {
            InitializeComponent();

            foreach (var officeName in WorkWithDb.Instance.GetAllOfficesForAdmin())
                comboBox_offices.Items.Add(officeName);
            comboBox_offices.SelectedIndex = 0;
        }

        /// <summary>
        /// Сохраняет пользователя в бд и очищает поля для ввода
        /// </summary>
        /// <param name="sender">Кто отправил сигнал</param>
        /// <param name="e">Аргументы</param>
        private void Button_save_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_email.Text) || string.IsNullOrEmpty(textBox_firstName.Text) ||
                string.IsNullOrEmpty(textBox_lastName.Text) || string.IsNullOrEmpty(passwordBox_password.Password) ||
                string.IsNullOrEmpty(datePicker_birthdate.Text))
            {
                HelpMethods.ShowWarning("Не все поля заполнены!");
                return;
            }

            WorkWithDb.Instance.InsertUserIntoDb(textBox_email.Text, textBox_firstName.Text, textBox_lastName.Text,
                comboBox_offices.SelectedItem.ToString(), datePicker_birthdate.Text, passwordBox_password.Password);

            textBox_email.Text = string.Empty;
            textBox_firstName.Text = string.Empty;
            textBox_lastName.Text = string.Empty;
            datePicker_birthdate.Text = string.Empty;
            passwordBox_password.Password = string.Empty;

            HelpMethods.ShowMessage("Пользователь успешно добавлен в бд!");
        }

        /// <summary>
        /// Закрытие формы добавления пользователя
        /// </summary>
        /// <param name="sender">Кто отправил сигнал</param>
        /// <param name="e">Аргументы</param>
        private void Button_cancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
