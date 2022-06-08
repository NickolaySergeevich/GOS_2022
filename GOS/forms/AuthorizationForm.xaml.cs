using System.Windows;

using GOS.Forms;
using GOS.WorkWithDB;

namespace GOS.forms
{
    /// <summary>
    /// Форма авторизации
    /// </summary>
    public partial class AuthorizationForm
    {
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработка входа в приложение
        /// </summary>
        /// <param name="sender">Кто отправил событие</param>
        /// <param name="e">Аргументы</param>
        private void Button_login_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_email.Text) || string.IsNullOrEmpty(passwordBox_password.Password))
            {
                var warningForm = new WarningForm
                {
                    label_warningText =
                    {
                        Content = "Не заполнены поля для входа!"
                    }
                };
                warningForm.ShowDialog();

                return;
            }

            var answerFromDb = WorkWithDb.Instance.Login(textBox_email.Text, passwordBox_password.Password);
            if (answerFromDb == null)
            {
                var warningForm = new WarningForm
                {
                    label_warningText =
                    {
                        Content = "Такого пользователя нет в бд!"
                    }
                };
                warningForm.ShowDialog();
            }
            else
            {
                // Тут будет открытие новой формы
            }
        }

        /// <summary>
        /// Выход из приложения
        /// </summary>
        /// <param name="sender">Кто отправил событие</param>
        /// <param name="e">Аргументы</param>
        private void Button_exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
