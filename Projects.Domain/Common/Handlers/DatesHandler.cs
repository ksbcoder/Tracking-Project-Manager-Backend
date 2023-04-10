using Projects.Domain.Entities;

namespace Projects.Domain.Common.Handlers
{
    public class DatesHandler
    {
        public static bool ValidateWithinTheOpenProject(DateTime? dateToCompare, Project project)
        {
            return dateToCompare?.Date >= project.OpenDate?.Date
                    && dateToCompare?.Date >= DateTime.Now.Date;
        }
        public static bool ValidateWithinTheOpenProjectDeadLine(DateTime? dateToCompare, Project project)
        {
            return dateToCompare?.Date >= project.OpenDate?.Date
                    && dateToCompare?.Date >= DateTime.Now.Date
                    && dateToCompare?.Date <= project.DeadLine?.Date;
        }
        public static bool ValidateWithinTheProjectDeadLineNotOpen(DateTime? dateToCompare, Project project)
        {
            bool isAfterOpenDate = !project.OpenDate.HasValue || dateToCompare?.Date >= project.OpenDate.Value.Date;
            bool isBeforeDeadline = !project.DeadLine.HasValue || dateToCompare?.Date <= project.DeadLine.Value.Date;
            bool isAfterCurrentDate = dateToCompare?.Date >= DateTime.Now.Date;

            return isAfterOpenDate && isBeforeDeadline && isAfterCurrentDate;
        }
        public static int CalculateDaysFromTo(DateTime? dateFrom, DateTime? dateTo)
        {
            TimeSpan dateDifference = (TimeSpan)(dateTo - dateFrom);
            int days = dateDifference.Days <= 0 ? 1 : dateDifference.Days;
            return days;
        }
    }
}