using System;
using UnityEngine;
using UnityEngine.UI;

public enum RayStatus
{
    None = 0,
    Enter,
    Hover,
    GazeFinish,
    Exit
}

public class InputRay :MonoBehaviour
{
    
    private RayStatus _rayStatus = RayStatus.None;
    private GameObject _hitGameObject;
    private BasicRayEventTrigger eventTrigger;
    private RaycastHit hit;
    [SerializeField] [Range(0, 10)] private int gazeTime;

    [SerializeField] private Image rig;
    private float _gazeProgress;
    private float inTime;
    private bool isGazeFinish;

    public RayStatus rayStatus
    {
        get { return _rayStatus; }
    }

    public float gazeProgress
    {
        get { return _gazeProgress; }
    }

    public GameObject hitGameObject
    {
        get { return _hitGameObject; }
    }

    private void Awake()
    {
//        FindFirstRaycast();
    }

    public virtual void Update()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward),
            out hit, 1000))
        {
            if (_hitGameObject != null)
            {
                if (_hitGameObject == hit.collider.gameObject)
                {
                    if (isGazeFinish) return;
                    StatusChangde(RayStatus.Hover);
                    _gazeProgress = (Time.time - inTime) / gazeTime;
                    rig.fillAmount = rig != null ? _gazeProgress : 0;
                    if (_gazeProgress >= 1f)
                    {
                        StatusChangde(RayStatus.GazeFinish);
                        isGazeFinish = true;
                    }
                }
                else
                {
                    StatusChangde(RayStatus.Exit);
                    isGazeFinish = false;
                    _hitGameObject = hit.collider.gameObject;
                    eventTrigger = _hitGameObject.GetComponent<BasicRayEventTrigger>();
                    StatusChangde(RayStatus.Enter);
                    inTime = Time.time;
                }
            }
            else
            {
                _hitGameObject = hit.collider.gameObject;
                eventTrigger = _hitGameObject.GetComponent<BasicRayEventTrigger>();
                StatusChangde(RayStatus.Enter);
                inTime = Time.time;
            }
        }
        else
        {
            if (_hitGameObject != null)
            {
                StatusChangde(RayStatus.Exit);
                isGazeFinish = false;
                _hitGameObject = null;
                eventTrigger = null;
            }
            else
            {
                StatusChangde(RayStatus.None);
            }
        }
    }

    private void StatusChangde(RayStatus status)
    {
        _rayStatus = status;
        if (eventTrigger == null) return;
        switch (_rayStatus)
        {
            case RayStatus.None:
                break;
            case RayStatus.Enter:
                eventTrigger.OnEnter();
                break;
            case RayStatus.Hover:
                eventTrigger.OnHover();
                break;
            case RayStatus.Exit:
                eventTrigger.OnExit();
                break;
            case RayStatus.GazeFinish:
                eventTrigger.OnGazeFinish();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}