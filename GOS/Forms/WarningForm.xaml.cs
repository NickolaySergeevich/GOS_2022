using System.Windows;

namespace GOS.Forms
{
    /// <summary>
    /// Форма с предупреждением.
    /// Имеет публичный Label <c>label_warningText</c> - в него надо вносить сообщение.
    /// </summary>
    public partial class WarningForm
    {
        /// <summary>
        /// Обычный конструктор
        /// </summary>
        public WarningForm()
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
