using System.Windows;

namespace GOS.Forms
{
    /// <summary>
    /// Класс окна пользователя
    /// </summary>
    public partial class UserForm
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public UserForm()
        {
            InitializeComponent();
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
