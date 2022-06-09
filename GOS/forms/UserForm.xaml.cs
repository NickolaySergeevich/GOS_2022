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
        /// Почта пользователя
        /// </summary>
        private readonly string _email;

        /// <summary>
        /// Конструктор
        /// <br>Создает сообщение, приветствующее пользователя</br>
        /// <br>Заполняет таблицу логов</br>
        /// </summary>
        /// <param name="email">Почта пользователя</param>
        public UserForm(string email)
        {
            InitializeComponent();

            _email = email;

            label_hello.Content = $"Hi, {WorkWithDb.Instance.GetNameUserByEmail(_email)}, Welcome to AMONIC Airlines";

            listBox_logs.ItemsSource = WorkWithDb.Instance.GetLogsForUserByEmail(_email);
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
    }
}
