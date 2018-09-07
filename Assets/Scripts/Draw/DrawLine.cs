using UnityEngine;
using System.Collections.Generic;
using  UnityEngine.EventSystems;

namespace Sonta
{
    public class DrawLine : SontaDraw
    {
        public static DrawLine drawLine;
        private EventSystem _eventSystem;
        private void Awake()
        {
            drawLine = this;
            _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        }

        public void BeginDraw()
        {
            clone = Instantiate(tf);
            DrawObjects.Add(clone);
            _lr = clone.GetComponent<LineRenderer>();
            _lr.SetPosition(0,
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));
            _lr.SetPosition(1,
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));
        }

        public void UpdataDraw()
        {
            _lr.SetPosition(1,
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));
        }

        public void OverDraw()
        {
        }

        public void DestroyLine()
        {
            Destroy(clone);
        }

        void Update()
        {
            
            if (Input.GetMouseButtonUp(0))
            {

                if (_eventSystem.IsPointerOverGameObject())
                {
                    Debug.Log("有对象");
                }
                else
                {
                    Destroy(clone);
                    Debug.Log("___");
                }
            }
//            if (Input.GetMouseButtonDown(0))
//            {
//                /*    line.startColor = Color.red;
//                    line.endColor = Color.red;
//                    line.startWidth = 0.03f;
//                    line.endWidth = 0.03f;
//                    line.numCornerVertices=2; */
//
//            }
//
//            if (Input.GetMouseButton(0))
//            {
//                _lr.SetPosition(1,Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));
//            }
        }
    }
}