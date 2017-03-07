using System.IO;
using System.Text.RegularExpressions;
using DotLiquid;
using DotLiquid.Util;

namespace CS.CodeRobot.TemplateEngine
{
    public class TemplateComment : DotLiquid.Block
    {
        private static readonly Regex ShortHandRegex = R.C(@"/\*.*\*/");

        public static string FromShortHand(string @string)
        {
            if (@string == null)
                return @string;

            Match match = ShortHandRegex.Match(@string);
            return match.Success ? $@"/*{match.Groups[1].Value}*/" : @string;
        }

        public override void Render(Context context, TextWriter result)
        {
        }
    }
}
