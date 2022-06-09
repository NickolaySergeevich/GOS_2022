using System.Collections.Generic;

using GOS.WorkWithDB;

namespace GOS.Forms
{
    /// <summary>
    /// Класс для отображения данных о пользователе
    /// </summary>
    public class UserItem
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Возраст пользователя
        /// </summary>
        public string Age { get; set; }
        /// <summary>
        /// Роль пользователя в системе
        /// </summary>
        public string UserRole { get; set; }
        /// <summary>
        /// Почтовый адрес пользователя
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// Офис, к которому прикреплен пользователь
        /// </summary>
        public string Office { get; set; }
        /// <summary>
        /// Задний фон пользователя
        /// </summary>
        public string BackColor { get; set; }
    }

    /// <summary>
    /// Класс окна администратора
    /// </summary>
    public partial class AdministratorForm
    {
        /// <summary>
        /// Конструктор - здесь создается список пользователей и прикрепляется к <c>ListBox</c>
        /// </summary>
        public AdministratorForm()
        {
            InitializeComponent();

            var userItems = new List<UserItem>();
            foreach (var user in WorkWithDb.Instance.GetAllUsersForAdmin())
            {
                var userItem = new UserItem
                {
                    Name = user["Name"],
                    LastName = user["LastName"],
                    Age = user["Age"],
                    UserRole = user["UserRole"],
                    EmailAddress = user["EmailAddress"],
                    Office = user["Office"],
                    BackColor = user["Active"] == "False" ? "Red" : "White"
                };

                userItems.Add(userItem);
            }

            listBox_users.ItemsSource = userItems;
        }
    }
}
