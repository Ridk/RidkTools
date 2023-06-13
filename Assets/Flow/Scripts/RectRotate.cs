using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RectRotate : MonoBehaviour,IPointerDownHandler
{
    // Start is called before the first frame update
    void Start()
    {
        ModelPos = transform.parent.position;
       // transform.parent.localEulerAngles = localEluer;
        //transform.Rotate(Vector3.forward,5);
        //transform.RotateAround(transform.position,Vector3.forward,10);
    }

    private bool isCanRotate = false;
    private Vector2 ModelPos;
    private Vector2 premousePos;
    private Vector2 mousePos;
    private Quaternion q;  
    private float RotateAngle;
    private Vector3 localEluer;
    private float a;
    [SerializeField]
    private Text showAngle;
    void Update()
    {
        if (!isCanRotate) return;
        
        if (Input.GetMouseButtonDown(0))  premousePos = mousePos = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            //premousePos = mousePos = Input.mousePosition;
            mousePos = Input.mousePosition;
            RotateAngle = Vector2.Angle(premousePos - ModelPos, mousePos - ModelPos);
            var b=Vector2.Angle(mousePos - ModelPos, Vector2.right);
            int temp = b>90 ? -1 : 1;
            a = Vector2.Angle(mousePos - ModelPos, Vector2.up)*temp;
            //Debug.Log("RotateAngle+"+RotateAngle);
            if (RotateAngle == 0)
            {
                premousePos = mousePos;
            }
            else
            {
                q = Quaternion.FromToRotation(premousePos - ModelPos, mousePos - ModelPos);
                int k = q.z > 0 ? 1 : -1;
                localEluer.z += k * RotateAngle;
                //Debug.Log(localEluer.x);
                //angle = localEluer.x = Mathf.Clamp(localEluer.x, 0, AllowAngle); //这里是项目需要 限制一下旋转圈数
                transform.parent.localEulerAngles = localEluer;
                if ((int)a%5==0)
                {
                    showAngle.text = (int)a + "°";
                }
               
                premousePos = mousePos;
            }
        }
        
        if (Input.GetMouseButtonUp(0))  isCanRotate = false;

    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        isCanRotate = true;
    }
    
}
