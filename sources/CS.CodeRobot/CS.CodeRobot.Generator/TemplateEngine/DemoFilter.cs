namespace CS.CodeRobot.TemplateEngine
{
    public class DemoFilter
    {
        public static string Substr(string value, int startIndex, int length = -1)
        {
            if (length >= 0)
                return value.Substring(startIndex, length);
            return value.Substring(startIndex);
        }
    }
}