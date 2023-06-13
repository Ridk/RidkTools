using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider),typeof(Rigidbody))]
public abstract class BasicModelEvent : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    protected Vector3 m_OriginalPosition;
    protected Vector3 m_ScreenPoint;
    protected Vector3 m_Offset;
    protected GameObject selectGameObject;
    private Rigidbody _rigidbody;
    
    [Header("拖动对象的相机组件")][SerializeField]
    private Camera m_Camera;

    protected GameObject TriggerObject;

    public void Awake()
    {
        m_OriginalPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.constraints =RigidbodyConstraints.FreezeAll;
/*     var i=   GetComponent<IPointerClickHandler>();
        i.OnPointerClick(new PointerEventData(EventSystem.current));*/
     //   m_Camera = UserControl.userControl.EditorCamera;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        //将目标物体的坐标转化成平面坐标  
        // Debug.Log(eventData.pointerEnter.name);
        m_ScreenPoint =m_Camera.WorldToScreenPoint(eventData.pointerCurrentRaycast.gameObject.transform.position);
        print(m_ScreenPoint.z);
        //计算鼠标的3维坐标跟物体的3维坐标的差值  
      //  m_Offset = eventData.pointerEnter.transform.position -  m_Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,m_ScreenPoint.z));
        selectGameObject = eventData.pointerEnter;
        // m_Collider.enabled = false;
    }


    public virtual void UpdatePosition()
    {
        if (selectGameObject==null) return;
        //鼠标的平面坐标  
        var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_ScreenPoint.z);
        //鼠标转移的3d空间坐标值  
        var curPosition = m_Camera.ScreenToWorldPoint(curScreenPoint);//+ m_Offset;
        //改变鼠标的3D空间坐标  
        selectGameObject.transform.position = curPosition;
    }

    private void OnCollisionEnter(Collision other)
    {       
        if (TriggerObject!=null) return;
        if (gameObject.name.Equals(other.gameObject.name))
            TriggerObject = other.gameObject;
    }

    private void OnCollisionExit(Collision other)
    {
        if (TriggerObject==null) return;
        if (gameObject.name.Equals(other.gameObject.name))
            TriggerObject = null;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        selectGameObject = null;
    }
}