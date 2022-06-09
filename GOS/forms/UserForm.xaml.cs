using System;
using System.Linq;
using System.Windows;

using GOS.WorkWithDB;

namespace GOS.Forms
{
    /// <summary>
    /// Класс окна пользователя
    /// </summary>
    public partial class UserForm
    {
        /// <summary>
        /// Фиксация времени входа в систему
        /// </summary>
        private readonly DateTime _loginTime;
        /// <summary>
        /// Почта пользователя
        /// </summary>
        private readonly string _email;

        /// <summary>
        /// Конструктор
        /// <br>Создает сообщение, приветствующее пользователя</br>
        /// <br>Заполняет таблицу логов</br>
        /// <br>Указывает время в системе (пока не тикает)</br>
        /// </summary>
        /// <param name="email">Почта пользователя</param>
        public UserForm(string email)
        {
            InitializeComponent();

            _loginTime = DateTime.Now;

            _email = email;

            label_hello.Content = $"Hi, {WorkWithDb.Instance.GetNameUserByEmail(_email)}, Welcome to AMONIC Airlines";

            var logs = WorkWithDb.Instance.GetLogsForUserByEmail(_email);
            listBox_logs.ItemsSource = logs;
            label_numberOfCrushes.Content = logs.Count(elem => elem.UnsuccessfulLogoutReason != "-").ToString();

            label_timeSpent.Content = TimeSpan.FromSeconds(WorkWithDb.Instance.GetTotalTimeOnSystemByEmail(_email)).ToString();
        }

        /// <summary>
        /// Обработка закрытия формы пользователя
        /// </summary>
        /// <param name="sender">Кто послал сигнал</param>
        /// <param name="e">Аргументы</param>
        private void MenuItem_exit_OnClick(object sender, RoutedEventArgs e)
        {
            var authorizationForm = new AuthorizationForm();
            authorizationForm.Show();
            Close();
        }

        /// <summary>
        /// Внесение логов при закрытии формы
        /// </summary>
        /// <param name="sender">Кто отправил сигнал</param>
        /// <param name="e">Аргументы</param>
        private void UserForm_OnClosed(object sender, EventArgs e)
        {
            WorkWithDb.Instance.InsertLogForUserByEmail(_loginTime, _email);
        }
    }
}
