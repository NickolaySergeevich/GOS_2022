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
        /// <summary>
        /// Количество неправильных попыток входа
        /// </summary>
        private ushort _wrongTry;
        /// <summary>
        /// Таймер для обратного отсчета в случае трех неудачных попыток
        /// </summary>
        private readonly DispatcherTimer _timer;
        /// <summary>
        /// Количество секунд - сколько осталось для до разблокировки кнопки Login
        /// </summary>
        private ushort _timeLeft;

        /// <summary>
        /// Конструктор
        /// <para>Задает начальное значение необходимым полям</para>
        /// </summary>
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

        /// <summary>
        /// Метод для таймера
        /// <br>Уменьшает кол-во оставшихся секунд, а когда приходит время - останавливается</br>
        /// </summary>
        /// <param name="sender">Кто отправил сигнал</param>
        /// <param name="e">Аргументы</param>
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
        /// <br>Есть проверки на пустые поля и на неправильные данные</br>
        /// </summary>
        /// <param name="sender">Кто отправил сигнал</param>
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
        /// <param name="sender">Кто отправил сигнал</param>
        /// <param name="e">Аргументы</param>
        private void Button_exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
