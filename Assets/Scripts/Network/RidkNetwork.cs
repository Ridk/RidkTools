using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Ridk
{
    public delegate void WebRequest();

    public class RidkNetwork : MonoBehaviour
    {
        public event WebRequest WebRequestEvent;

        private UnityWebRequest _wwwRequest;

        //string url="https://api.stschool.cn/course/get_resource_detail?resId=7090";
        private string requestData;

        public string url = "https://danbooru.donmai.us/posts.json";

        public string RequestData
        {
            get { return requestData; }
        }

        private IEnumerator Ienumerator;

        private void Start()
        {
//            url = "https://api.stschool.cn/course/get_resource_detail";
//            var f=new WWWForm();
//            f.AddField("resId","7090");
//            Post(f);
//            url = "https://danbooru.donmai.us/posts.json";
//            Get();
        }

        /// <summary>
        /// 发送Post请求获取数据
        /// </summary>
        /// <param name="form">数据表单</param>
        public void Post(WWWForm form)
        {
            if (Ienumerator != null) return;

            Ienumerator = StartWww(form);
            StartCoroutine(Ienumerator);
        }

        /// <summary>
        /// 发送Get请求获取数据
        /// </summary>
        public void Get()
        {
            if (Ienumerator != null) return;

            Ienumerator = StartWww();
            StartCoroutine(Ienumerator);
        }

        /// <summary>
        /// 获取到的数据保存到实例成员
        /// </summary>
        /// <param name="www">请求对象实例</param>
        private void SetData(UnityWebRequest www)
        {
            if (www.isDone && !www.isNetworkError)
            {
                Debug.Log(www.downloadHandler.text);
                requestData = www.downloadHandler.text;
                if (WebRequestEvent != null)
                {
                    WebRequestEvent();
                }

                StopCoroutine(Ienumerator);
                www = null;
            }
        }

        /// <summary>
        /// 协程异步发送Post请求
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartWww()
        {
            _wwwRequest = UnityWebRequest.Get(url);
            yield return _wwwRequest.SendWebRequest();
            SetData(_wwwRequest);
        }

        /// <summary>
        /// 协程异步发送Post请求
        /// </summary>
        /// <param name="form">需要传输的数据表单</param>
        /// <returns></returns>
        private IEnumerator StartWww(WWWForm form)
        {
            _wwwRequest = UnityWebRequest.Post(url, form);
            yield return _wwwRequest.SendWebRequest();
            SetData(_wwwRequest);
        }
    }
}