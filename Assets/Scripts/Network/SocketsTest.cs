using System.IO;
using System.Net;
using System.Text;
using Ridk;
using UnityEngine;
using System.Net.Sockets;

public class SocketsTest : MonoBehaviour
{
    private Socket tcpSocket;
    void Start()
    {
        //创建socket  
        tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);  
  
        //连接服务器  
        tcpSocket.Connect(IPAddress.Parse("127.0.0.1"), 4747);  
        Debug.Log("连接服务器");  
  
/*        string str = "你好,世界!";

        byte[] strByte = Encoding.UTF8.GetBytes(str);
        tcpSocket.Send(strByte);


        string str1 = "";
        byte[] strByte1 = new byte[1024];
        int strLength1 = tcpSocket.Receive(strByte1,strByte1.Length,0);
        str1 = Encoding.UTF8.GetString(strByte1,0,strByte1.Length);
        Debug.Log(str1);
        var xml = new MeRuXML(string.Format("{0}/MeRu.xml", Application.dataPath));
        xml.AddNode("wfz", "");
        MeRuText t = new MeRuText(Application.dataPath + "/1.text");
        string s = "wfz";
        byte[] d = Encoding.UTF8.GetBytes(s);
        t.Write(d, FileMode.Create);
        var bs = t.Read();
        print(Encoding.UTF8.GetString(bs));
		var e = (XmlElement)xml.Xml.DocumentElement.SelectSingleNode("wfz");
		xml.AddAttribute(e,"月色","真美");
		print(xml.ReadNode("wfz"));*/
    }

    void Update()
    {
    }
}