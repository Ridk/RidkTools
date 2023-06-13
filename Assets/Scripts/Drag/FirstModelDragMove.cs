using System.Collections;
using System.Collections.Generic;
using Sonta;
using UnityEngine;
using UnityEngine.EventSystems;


public class FirstModelDragMove : BasicModelEvent
{
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            UpdatePosition();
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectGameObject = null;
        }
    }


    public override void OnPointerUp(PointerEventData eventData)
    {
      
        if (TriggerObject == null) return;
        if (TriggerObject.name.Equals(selectGameObject.name))
        {
            Debug.Log("可以放置");
        }
        else
        {
            Debug.Log("类型不匹配");

        }
        selectGameObject.transform.position = m_OriginalPosition;
        base.OnPointerUp(eventData);
    }
}