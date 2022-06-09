using System.Windows;

namespace GOS.Forms
{
    /// <summary>
    /// Форма с предупреждением.
    /// Имеет публичный Label <c>label_warningText</c> - в него надо вносить сообщение.
    /// </summary>
    public partial class MessageForm
    {
        /// <summary>
        /// Обычный конструктор
        /// </summary>
        public MessageForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Закрытие формы по кнопке
        /// </summary>
        /// <param name="sender">Кто отправил сигнал</param>
        /// <param name="e">Аргументы клика</param>
        private void Button_exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
