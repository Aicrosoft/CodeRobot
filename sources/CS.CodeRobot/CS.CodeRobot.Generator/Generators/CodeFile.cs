using CS.Extension;

namespace CS.CodeRobot.Generators
{
    /// <summary>
    /// 由模板生成而来的代码文件名称信息
    /// </summary>
    public class CodeFile
    {

        /// <summary>
        /// 子目录
        /// </summary>
        public string Sub { get; set; }

        /// <summary>
        /// 代码文件名称
        /// </summary>
        public string Name { get; set;}

        /// <summary>
        /// 自动代码文件名称，为null时没有Auto代码
        /// </summary>
        public string AutoName { get; set; }

        /// <summary>
        /// 没有自动代码段
        /// </summary>
        public bool NoneAutoFile => AutoName.IsNullOrWhiteSpace();

        /// <summary>
        /// 获取含有子目录的名称
        /// </summary>
        /// <param name="nameOrAutoName"></param>
        /// <returns></returns>
        public string GetSubName(string nameOrAutoName)
        {
            return string.IsNullOrWhiteSpace(Sub) ? nameOrAutoName : $"{Sub}\\{nameOrAutoName}";
        }
    }
}