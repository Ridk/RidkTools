using UnityEngine;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;

public class ModelDragMove : BasicModelEvent
{
    //  [Header("距离移动的速度")]
    [Range(0, 20f)] [SerializeField] private float _speed = 10;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            UpdatePosition();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selectGameObject)
            {
                if (TriggerObject)
                {
                //    TriggerObject.GetComponent<DeviceEvent>().MountAction(true);
                }
                else
                {
               //     UIManagement.UIM.ShowContent(6);
                }

                selectGameObject.transform.position = m_OriginalPosition;
                selectGameObject = null;
            }
        }
    }


    public override void OnPointerUp(PointerEventData eventData)
    {
        if (TriggerObject == null) return;
        if (TriggerObject.name.Equals(selectGameObject.name))
        {
            Debug.Log("可以放置");
          //  TriggerObject.GetComponent<DeviceEvent>().MountAction(true);
        }
    }
}