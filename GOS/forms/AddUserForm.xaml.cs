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
        }

        /// <summary>
        /// Сохраняет пользователя в бд и очищает поля для ввода
        /// </summary>
        /// <param name="sender">Кто отправил сигнал</param>
        /// <param name="e">Аргументы</param>
        private void Button_save_OnClick(object sender, RoutedEventArgs e)
        {

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
