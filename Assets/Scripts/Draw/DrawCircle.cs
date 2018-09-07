using UnityEngine;

namespace Sonta
{
    public class DrawCircle : SontaDraw
    {
        public int n;
        float x;
        float y;
        private Vector3 mPoint;
        [SerializeField] private float r;

        private void Update()
        {
      /*      if (Input.GetMouseButtonDown(0))
            {
                clone = GameObject.Instantiate(tf, tf.transform.position, GameObject.transform.rotation);
                DrawObjects.Add(clone);
                _lr = clone.GetComponent<LineRenderer>();
                _lr.positionCount = n + 1;
                mPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                for (int i = 0; i < n + 1; i++)
                {
                    x = Mathf.Sin((360f * i / n) * Mathf.Deg2Rad) * r + mPoint.x; //横坐标
                    y = Mathf.Cos((360f * i / n) * Mathf.Deg2Rad) * r + mPoint.y; //纵坐标
                    _lr.SetPosition(i, new Vector3(x, y, 0));
                }
            }*/

            /*         if (Input.GetMouseButton(0))
                     {
                         //循环着取出36个点
                         for (int i = 0; i < n + 1; i++)
                         {
                             x = Mathf.Sin((360f * i / n) * Mathf.Deg2Rad) * x; //横坐标
                             y = Mathf.Cos((360f * i / n) * Mathf.Deg2Rad) * r; //纵坐标
                             _lr.SetPosition(i, new Vector3(x, y, 0));
                         }
                     }*/
        }
    }
}