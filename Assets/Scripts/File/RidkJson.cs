using System.IO;
using LitJson;
namespace Ridk
{
    public class RidkJson:RidkText
    {
        private JsonData _data;

        public JsonData Data
        {
            get { return _data; }
        }

        public RidkJson(string path) : base(path)
        {
            _data = JsonMapper.ToObject(Text);
        }
    }
}