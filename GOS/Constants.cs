namespace GOS
{
    /// <summary>
    /// Константы для всего проекта
    /// </summary>
    public static class Constants
    {
        // Роли
        public const string RoleIdKey = "RoleID";
        public const int RoleAdminId = 1;
        public const int RoleOfficeUserId = 2;
        public const string RoleAdminText = "Administrator";
        public const string RoleUserText = "User";

        // Активен ли пользователь
        public const string ActiveKey = "Active";
        public const int ActiveNo = 0;
        public const int ActiveYes = 1;

        // Ответ от бд для запроса пользователей
        public const string NameKey = "Name";
        public const string LastNameKey = "LastName";
        public const string AgeKey = "Age";
        public const string UserRoleKey = "UserRole";
        public const string EmailAddressKey = "EmailAddress";
        public const string OfficeKey = "Office";
        public const string NoActiveUserAnswer = "False";

        // Данные для закрашивания задников у пользователей
        public const string NoActiveUserColor = "Red";
        public const string ActiveUserColor = "White";

        // Сообщения об ошибках
        public const string WarningNoAllFieldsFill = "Не все поля заполнены!";
        public const string WarningEmailExist = "Такой email уже есть в бд!";
        public const string WarningNoSelectUser = "Вы не выбрали пользователя!";
        public const string WarningCantFindUserInDb = "Такого пользователя нет в бд!";
        public const string WarningNoActiveUser = "Вы неактивны в системе!";

        // Сообщения пользователю
        public const string MessageUserInsertInDb = "Пользователь успешно добавлен в бд!";
        public const string MessageUserUpdateInDb = "Данные успешно обновлены!";

        // Для показа сообщений
        public const string JustMessageTitle = "Сообщение";
    }
}
