using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace Sonta
{
    public class DrawGeometryLine : DrawGeometry
    {
        public static DrawGeometryLine geometryLine;
        private EventSystem _eventSystem;

        private void Awake()
        {
            geometryLine = this;
            _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        }

        public void BeginDraw()
        {
            GameObject drawObject = new GameObject(geometryType.ToString(), typeof(LineRenderer));
            lineRenderer = drawObject.GetComponent<LineRenderer>();
            lineRenderer.material = material;
            drawObjects.Add(lineRenderer);
            lineRenderer.SetPosition(0,
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));
            lineRenderer.SetPosition(1,
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));
        }

        public void UpdataDraw()
        {
            lineRenderer.SetPosition(1,
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_eventSystem.IsPointerOverGameObject())
                {
                    BeginDraw();
                }
                else
                {
                 
                    Debug.Log("___");
                }
            }

            if (Input.GetMouseButton(0))
            {
                UpdataDraw();
                Debug.Log("___");
            }

            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("结束绘制");
            }
        }
    }
}