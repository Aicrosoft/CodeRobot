using System;

namespace CS.CodeRobot.TemplateEngine
{

    /// <summary>
    /// 时间过滤器
    /// </summary>
    public class DateTimeFilter
    {
        public static string Year(DateTime dateTime)
        {
            return dateTime.Year.ToString();
        }

        public static string Format(DateTime dateTime, string formater)
        {
            return dateTime.ToString(formater);
        }

    }
}