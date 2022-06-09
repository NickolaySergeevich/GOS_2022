using System;

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

    /// <summary>
    /// Класс для получения ответа от сервера по логам
    /// </summary>
    public class UserLog
    {
        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Время входа пользователя в систему
        /// </summary>
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// Время выхода пользователя из системы
        /// </summary>
        public DateTime LogoutTime { get; set; }

        /// <summary>
        /// Время, потраченное пользователем в системе, в секундах
        /// </summary>
        public string TimeSpent
        {
            get
            {
                var allSeconds = Convert.ToInt64(TimeSpent);
                var seconds = allSeconds % 60;
                var minutes = allSeconds / 60 % 60;
                var hours = allSeconds / 3600;

                return String.Format("{0:00}:{0:00}:{0:00}", hours, minutes, seconds);
            }
            set => TimeSpent = value;
        }
        /// <summary>
        /// Причина неудачного выхода из системы
        /// </summary>
        public string UnsuccessfulLogoutReason { get; set; }
        /// <summary>
        /// Переменная цвета заднего фона у ячейки
        /// </summary>
        public string BackColor { get; set; }
    }
}
