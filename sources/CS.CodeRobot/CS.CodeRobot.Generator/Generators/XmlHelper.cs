using System;
using System.Collections.Generic;
using System.Xml;
using CS.Utils;

namespace CS.CodeRobot.Generators
{
    public static class XmlHelper
    {
        /// <summary>
        /// 更改节点
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="xnsm"></param>
        /// <param name="codes"></param>
        public static void ChangeAutoItems(this XmlDocument xmlDoc, XmlNamespaceManager xnsm, List<CodeFile> codes )
        {

            var root = xmlDoc.DocumentElement;
            var node = root?.SelectSingleNode("/ns:Project/ns:ItemGroup/ns:Compile[@Include='Properties\\AssemblyInfo.cs']/..", xnsm);
            if (node == null) throw new OperationCanceledException("工程文件异常，无法定位。请检查。");

            foreach (var code in codes)
            {
                
                if (code.NoneAutoFile)
                {
                    var className = code.GetSubName(code.Name);
                    var xpath = $"ns:Compile[@Include='{className}']";
                    var csNode = node?.SelectSingleNode(xpath, xnsm);
                    if (csNode == null)
                    {
                        var classNode = xmlDoc.CreateElement("Compile", node.NamespaceURI);
                        classNode.SetAttribute("Include", className);
                        node.AppendChild(classNode);
                    }
                    else
                    {
                        DebugConsole.Debug($"{className} 已存在于项目中，跳过。");
                    }
                }
                else
                {
                    var autoClass = code.GetSubName(code.AutoName);
                    var xpath = $"ns:Compile[@Include='{autoClass}']";
                    var csNode = node?.SelectSingleNode(xpath, xnsm);
                    if (csNode == null)
                    {
                        var autoClassNode = xmlDoc.CreateElement("Compile", node.NamespaceURI);
                        autoClassNode.SetAttribute("Include", autoClass);
                        var uponNode = xmlDoc.CreateElement("DependentUpon", node.NamespaceURI);
                        uponNode.InnerText = $"{code.Name}";
                        autoClassNode.AppendChild(uponNode);
                        var classNode = xmlDoc.CreateElement("Compile", node.NamespaceURI);
                        classNode.SetAttribute("Include",code.GetSubName(code.Name));

                        node.AppendChild(autoClassNode);
                        node.AppendChild(classNode);
                    }
                    else
                    {
                        DebugConsole.Debug($"{autoClass} 已存在于项目中，跳过。");
                    }
                }
              
            }
        }

        public static void ChangeProjectInfo(this XmlDocument xmlDoc, string assemblyName, XmlNamespaceManager nsmgr)
        {
            xmlDoc.SetSingleNodeValue(nsmgr, "/ns:Project/ns:PropertyGroup/ns:ProjectGuid", $"{{{Guid.NewGuid().ToString().ToUpper()}}}",
                (x) => (x == null || x.Length < 10));
            xmlDoc.SetSingleNodeValue(nsmgr, "/ns:Project/ns:PropertyGroup/ns:RootNamespace", assemblyName);
            xmlDoc.SetSingleNodeValue(nsmgr, "/ns:Project/ns:PropertyGroup/ns:AssemblyName", assemblyName);
        }

        /// <summary>
        /// 设定XmlNode的单节点值
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="xnsm"></param>
        /// <param name="xpath"></param>
        /// <param name="value"></param>
        /// <param name="checkFunc">当不为null时只有通过检测的节点值能会被变更</param>
        static void SetSingleNodeValue(this XmlDocument xmlDoc, XmlNamespaceManager xnsm, string xpath, string value, Func<string, bool> checkFunc = null)
        {
            var root = xmlDoc.DocumentElement;
            var node = root?.SelectSingleNode(xpath, xnsm);
            //var nodes = root.SelectNodes(xpath,xnsm);
            if (node == null) return;
            if (checkFunc != null)
            {
                var val = checkFunc(node.InnerText);
                if (val) node.InnerText = value;
            }
            else
            {
                node.InnerText = value;
            }
        }
    }
}