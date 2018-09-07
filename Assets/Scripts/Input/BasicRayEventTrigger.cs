using UnityEngine;
using UnityEngine.UI;

public delegate void DelegateOnEnter();

public delegate void DelegateOnHover();

public delegate void DelegateOnExit();

public delegate void DelegateOnGazeFinish();

public class BasicRayEventTrigger : MonoBehaviour, IReyEvent
{
    public event DelegateOnEnter onEnter;
    public event DelegateOnHover onHover;
    public event DelegateOnExit onExit;
    public event DelegateOnGazeFinish onGazeFinish;

    public virtual void OnEnter()
    {
        if (onEnter != null) onEnter();
        Debug.Log("进入");
    }

    public virtual void OnHover()
    {
        if (onHover != null) onHover();
      //  Debug.Log("停留");
    }

    public virtual void OnExit()
    {
        if (onExit != null) onExit();
        Debug.Log("离开");
    }

    public virtual void OnGazeFinish()
    {
        if (onGazeFinish != null) onGazeFinish();
        Debug.Log("凝视完成");
    }
}