using System.IO;
using CS.Utils;
using DotLiquid;
using DotLiquid.FileSystems;

namespace CS.CodeRobot.TemplateEngine
{
    public class DotliquidTemplateFileSystem : IFileSystem
    {
        public string ReadTemplateFile(Context context, string templateName)
        {
            var path = context[templateName] as string;
            if (string.IsNullOrEmpty(path))
                return path;
            var fullPath = FileHelper.GetFullPath(path);
            //var fullPath = HttpContext.Current.Server.MapPath(path);
            return File.ReadAllText(fullPath);
        }
    }
}