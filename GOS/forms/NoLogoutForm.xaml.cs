using System;
using System.Windows;

using GOS.WorkWithDB;

namespace GOS.Forms
{
    /// <summary>
    /// Класс сообщения об ошибке при обнаружении не выхода из системы
    /// </summary>
    public partial class NoLogoutForm
    {
        /// <summary>
        /// Время входа с ошибкой
        /// </summary>
        private readonly DateTime _loginTime;

        /// <summary>
        /// Почта пользователя
        /// </summary>
        private readonly string _email;


        /// <summary>
        /// Конструктор
        /// <br>Создает сообщение о том, когда был неправильный выход</br>
        /// </summary>
        /// <param name="loginTime">Время последнего входа пользователя без выхода</param>
        /// <param name="email">Почта пользователя</param>
        public NoLogoutForm(DateTime loginTime, string email)
        {
            InitializeComponent();

            _loginTime = loginTime;
            _email = email;

            label_errorTime.Content = $"No logout detected for your last login on {loginTime.Day}/{loginTime.Month}/{loginTime.Year} at {loginTime.Hour}:{loginTime.Minute}:{loginTime.Second}";
        }

        /// <summary>
        /// Создает запись с ошибкой в бд и открывает пользовательское окно
        /// </summary>
        /// <param name="sender">Кто отправил сигнал</param>
        /// <param name="e">Аргументы</param>
        private void Button_confirm_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_reason.Text))
            {
                HelpMethods.ShowWarning(Constants.WarningNoAllFieldsFill);
                return;
            }

            WorkWithDb.Instance.InsertErrorLogForUserByEmail(_loginTime, _email, textBox_reason.Text);
            var userForm = new UserForm(_email);
            userForm.Show();
            Close();
        }
    }
}
