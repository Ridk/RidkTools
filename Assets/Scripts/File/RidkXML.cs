using System;
using System.IO;
using System.Xml;

namespace Ridk
{
    public class RidkXML : RidkFile
    {
        private XmlDocument _xml;

        public XmlDocument Xml
        {
            get
            {
                if (_xml == null)
                {
                    _xml = new XmlDocument();
                    if (File.Exists(FilePath))
                        _xml.Load(FilePath);
                    else
                        _xml = CreateXml();
                }

                return _xml;
            }
        }

        public RidkXML(string path) : base(path)
        {
        }

        /// <summary>
        /// 在内存创建新的XML
        /// </summary>
        /// <returns></returns>
        private XmlDocument CreateXml()
        {
            var declaration = _xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            _xml.AppendChild(declaration);
            var root = _xml.CreateElement("MeRu");
            root.InnerText = "";
            _xml.AppendChild(root);
            _xml.Save(FilePath);
            return _xml;
        }

        /// <summary>
        /// 读取节点内容字符串
        /// </summary>
        /// <param name="nodeName">节点名字</param>
        /// <returns></returns>
        public string ReadNode(string nodeName)
        {
            return Xml.DocumentElement.SelectSingleNode(nodeName).InnerText;
        }

        /// <summary>
        /// 增加新的节点并保持到本地
        /// </summary>
        /// <param name="eName">新节点名字</param>
        /// <param name="text">新节点内容</param>
        public void AddNode(string eName, string text = "")
        {
            var xmlElement = Xml.CreateElement(eName);
            xmlElement.InnerText = text;
            Xml.DocumentElement.AppendChild(xmlElement);
            Xml.Save(FilePath);
        }

        /// <summary>
        /// 更改节点内容
        /// </summary>
        /// <param name="eName">节点名字</param>
        /// <param name="text">新的内容</param>
        public void WriteNode(string eName, string text)
        {
            var xmlElement = Xml.DocumentElement.SelectSingleNode(eName);
            xmlElement.InnerText = text;
            Xml.Save(FilePath);
        }

        /// <summary>
        /// 给节点添加属性并保存到本地
        /// </summary>
        /// <param name="xmlElement">节点对象</param>
        /// <param name="key">属性名</param>
        /// <param name="text">属性内容</param>
        public void AddAttribute(XmlElement xmlElement, string key, string text = "")
        {
            try
            {
                xmlElement.SetAttribute(key, text);
                Xml.Save(FilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 更改节点属性内容
        /// </summary>
        /// <param name="xmlElement">节点对象</param>
        /// <param name="key">属性名</param>
        /// <param name="text">新的属性内容</param>
        public void WriteAttribute(XmlElement xmlElement, string key, string text)
        {
            try
            {
                xmlElement.Attributes[key].Value = text;
                Xml.Save(FilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 读取属性值
        /// </summary>
        /// <param name="xmlElement">节点对象</param>
        /// <param name="key">属性名</param>
        /// <returns></returns>
        public string ReadAttribute(XmlElement xmlElement, string key)
        {
            return xmlElement.GetAttribute(key);
        }
    }
}