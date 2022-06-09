using System.Windows;

using GOS.WorkWithDB;

namespace GOS.Forms
{
    public partial class EditRoleForm
    {
        /// <summary>
        /// Конструктор
        /// <br>Заполняет <c>ComboBox</c> с офисами</br>
        /// <br>Заполняет данные о пользователе</br>
        /// </summary>
        public EditRoleForm(UserItem user)
        {
            InitializeComponent();

            foreach (var officeName in WorkWithDb.Instance.GetAllOfficesForAdmin())
                comboBox_offices.Items.Add(officeName);
            comboBox_offices.SelectedIndex = 0;

            textBox_emailAddress.Text = user.EmailAddress;
            textBox_firstName.Text = user.Name;
            textBox_lastName.Text = user.LastName;
            comboBox_offices.SelectedIndex = comboBox_offices.Items.IndexOf(user.Office);
            switch (user.UserRole)
            {
                case "User":
                    radioButton_user.IsChecked = true;
                    break;
                case "Administrator":
                    radioButton_administrator.IsChecked = true;
                    break;
            }
        }

        private void Button_apply_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void Button_cancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
