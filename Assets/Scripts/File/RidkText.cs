using System.IO;

namespace Ridk
{
    public class RidkText : RidkFile
    {
        protected StreamReader _streamReader;
        protected StreamWriter _streamWriter;

        private string _text;

        public RidkText(string path) : base(path)
        {
            _streamReader = new StreamReader(path);
            _streamWriter = new StreamWriter(path);
            _text = _streamReader.ReadToEnd();
        }

        public string Text
        {
            get { return _text; }
        }
        public  void Write<T>(T text) where T: struct 
        {
            _streamWriter.Write(text);
        }


        public string   Read()
        {
            return _streamReader.ReadToEnd();
        }
        public string ReadLine()
        {
            return _streamReader.ReadLine();
        }

        public void WriteLine<T>(T content) where T : struct
        {
            string text = this.ToString();
            _streamWriter.WriteLine();
        }
    }

}