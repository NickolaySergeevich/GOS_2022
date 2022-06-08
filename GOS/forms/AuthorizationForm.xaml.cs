using System;
using System.Windows;
using System.Windows.Threading;

using GOS.Forms;
using GOS.WorkWithDB;

namespace GOS.forms
{
    /// <summary>
    /// Форма авторизации
    /// </summary>
    public partial class AuthorizationForm
    {
        private ushort _wrongTry;
        private readonly DispatcherTimer _timer;
        private ushort _timeLeft;

        public AuthorizationForm()
        {
            InitializeComponent();

            _wrongTry = 0;
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            --_timeLeft;
            if (_timeLeft == 0)
            {
                _timer.Stop();
                label_timeToLoginText.Visibility = Visibility.Hidden;
                label_timeToLogin.Visibility = Visibility.Hidden;
                button_login.IsEnabled = true;
            }

            label_timeToLogin.Content = _timeLeft.ToString();
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

                ++_wrongTry;
                if (_wrongTry == 3)
                {
                    _wrongTry = 0;
                    button_login.IsEnabled = false;
                    _timeLeft = 10;
                    label_timeToLoginText.Visibility = Visibility.Visible;
                    label_timeToLogin.Visibility = Visibility.Visible;
                    label_timeToLogin.Content = _timeLeft.ToString();
                    _timer.Start();
                }
            }
            else if (answerFromDb[Constants.ActiveKey] == Constants.ActiveNo)
            {
                var warningForm = new WarningForm
                {
                    label_warningText =
                    {
                        Content = "Вы неактивны в системе!"
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
