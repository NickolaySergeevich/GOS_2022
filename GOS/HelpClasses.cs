namespace GOS
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
}
