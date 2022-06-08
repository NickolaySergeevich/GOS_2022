using GOS.Forms;

namespace GOS
{
    /// <summary>
    /// Вспомогательный класс для методов, которые будут использоваться между экранами
    /// </summary>
    public static class HelpMethods
    {
        /// <summary>
        /// Показывает сообщение с предупреждением
        /// </summary>
        /// <param name="warningText">Текст предупреждения</param>
        public static void ShowWarning(string warningText)
        {
            var warningForm = new WarningForm { label_warningText = { Content = warningText } };
            warningForm.ShowDialog();
        }
    }
}
