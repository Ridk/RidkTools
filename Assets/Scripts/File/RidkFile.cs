using System;
using System.IO;

namespace Ridk
{
    public abstract class RidkFile
    {
        private string filePath;
        private string name = string.Empty;

        protected RidkFile(string path)
        {
            filePath = path;
            try
            {
                if (File.Exists(filePath))
                {
                    var f = new FileInfo(filePath);
                    name = f.Name;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        ///获取文件所在路劲
        /// </summary>
        public string FilePath
        {
            get { return filePath; }
        }

        /// <summary>
        /// 获取文件名字
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// 获取文件字节流
        /// </summary>
        /// <returns></returns>
        public virtual byte[] Read()
        {
            var f = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] b = new byte[f.Length];
            f.Read(b, 0, (int) f.Length);
            f.Close();
            return b;
        }

        /// <summary>
        /// 对文件写入字节流
        /// </summary>
        /// <param name="bytes">要写入的字节数据</param>
        /// <param name="mode">写入的方式</param>
        /// <exception cref="ArgumentOutOfRangeException">抛出的异常信息</exception>
        public virtual void Write(byte[] bytes, FileMode mode = FileMode.Append)
        {
            var f = new FileStream(filePath, mode, FileAccess.Write);
            f.Write(bytes, 0, (int) bytes.Length);
            f.Close();
        }
    }
}