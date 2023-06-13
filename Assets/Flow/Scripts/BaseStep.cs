using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BaseStep : MonoBehaviour
{
    [SerializeField] protected Image bg;

    [SerializeField] protected Text contentText;
    
    [SerializeField] protected RectTransform flowArrowRoot;

    [SerializeField]protected List<FlowArrow> flowArrows=new List<FlowArrow>();

  //  protected FlowArrow.Direction outDirection;
    
    [SerializeField]protected List<RectTransform> outTargets=new List<RectTransform>();

    private void Start()
    {
        if (outTargets.Count==0) return;
        for (int i = 0; i < outTargets.Count; i++)
        {
         //   AddtTarget(outTargets[i]);
            flowArrows[i].mTarget=outTargets[i];
        }
    }

   public void AddTarget(RectTransform  target)
    {
        var arows=Instantiate(FlowChart.Instance.arrows,flowArrowRoot);
        var flowArrow = arows.GetComponent<FlowArrow>();
        flowArrow.mTarget = target;
       // outTargets.Add(target);
        flowArrows.Add(flowArrow);
    }
   public void RemoveTarget(FlowArrow arrow)
   {
       outTargets.Remove(arrow.mTarget);
       flowArrows.Remove(arrow);
       Destroy(arrow.gameObject);
   }

    public virtual  void StepOnClick() {
        Debug.Log(contentText);
    }
}