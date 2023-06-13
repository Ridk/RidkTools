using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class FreeView : MonoBehaviour
{
    public static FreeView Instance;
    [SerializeField] private Transform sceneTransform; //主场景
    public static Vector3 TargetPoint; //查看目标
    public static Transform Target; //查看目标
    private static Vector3 _originalPosition;
    private static Vector3 _originalAngle;
    public Camera cam;

    public bool isMove;

    public static bool LookOne;
    //观察目标  
    //   public Transform Target;  

    //观察距离  
    public float distance = 20;

    //旋转速度  
    private float _speedX = 240;
    private float _speedY = 120;

    //角度限制  
    private float _minLimitY = 0;
    private float _maxLimitY = 90;

    //旋转角度 
    private float mX = 0.0F;

    private float mY = 0.0F;

    [Header("缩放距离限制")] [SerializeField]
    //鼠标缩放距离最大值  
    private float maxDistance = 30;

    [SerializeField]
    //鼠标缩放距离最小值  
    private float minDistance = 1.5F;

    //鼠标缩放速率  
    [SerializeField] [Header("鼠标缩放速率")] private float zoomSpeed = 2F;

    //是否启用差值  
    public bool isNeedDamping = true;

    //速度  
    public float damping = 30F;

    //存储角度的四元数  
    private Quaternion _mRotation;

    //定义鼠标按键枚举  
    private enum MouseButton
    {
        //鼠标左键  
        MouseButtonLeft = 0,

        //鼠标右键  
        MouseButtonRight = 1,

        //鼠标中键  
        MouseButtonMiddle = 2
    }

    //相机移动速度  
    private float _moveSpeed = 5.0F;

    //屏幕坐标  
    private Vector3 _mScreenPoint;

    //坐标偏移  
    private Vector3 _mOffset;

    private void Awake()
    {
        Instance = this;
    }


    public void Initialized(Transform scenetarget)
    {
        sceneTransform = scenetarget;
        TargetPoint = sceneTransform.position;
        //初始化旋转角度  
        var eulerAngles = transform.eulerAngles;
        mX = eulerAngles.y;
        mY = eulerAngles.x;
        Rotate();
        _originalAngle = eulerAngles;
        _originalPosition = transform.position;
    }


    void LateUpdate()
    {
        if (sceneTransform == null) return;
        //鼠标右键旋转  
        if (Input.GetMouseButton((int)MouseButton.MouseButtonRight))
        {
            //获取鼠标输入  
            mX += Input.GetAxis("Mouse X") * _speedX * 0.02F;
            mY -= Input.GetAxis("Mouse Y") * _speedY * 0.02F;
            Rotate();
        }

        //鼠标中键平移  

        /*if (Input.GetAxis("Mouse ScrollWheel")>0||Input.GetAxis("Mouse ScrollWheel")<0)
        {
            
        }*/

        if (!isMove)
        {
            //鼠标滚轮缩放  
            distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
            //重新计算位置  
            var mPosition = _mRotation * new Vector3(0.0F, 0F, -distance) + TargetPoint;

            //设置相机的位置  
            if (isNeedDamping)
            {
                transform.position = Vector3.Lerp(transform.position, mPosition, Time.deltaTime * damping);
            }
            else
            {
                transform.position = mPosition;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && LookOne)
        {
            Restore();
        }
    }

    public void Rotate()
    {
        //范围限制  
        mY = ClampAngle(mY, _minLimitY, _maxLimitY);
        //计算旋转  
        _mRotation = Quaternion.Euler(mY, mX, 0);
        //根据是否插值采取不同的角度计算方式  
        if (isNeedDamping)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _mRotation, Time.deltaTime * damping);
        }
        else
        {
            transform.rotation = _mRotation;
        }
    }

    //角度限制  
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    public void Restore()
    {
        transform.DOLocalRotate(_originalAngle, 0.3f);
        isMove = true;
        var t = transform.DOMove(_originalPosition, 0.3f);
        t.onComplete += () =>
        {
            isMove = false;
            distance = 20f;
            maxDistance = 30;
            var transform1 = transform;
            var rotation = transform1.rotation;
            mY = rotation.eulerAngles.x;
            mX = rotation.eulerAngles.y;
            Rotate();
        };
        TargetPoint = sceneTransform.position;
        LookOne = false;
    }

    public void ViewTarget(Transform target)
    {
      
            maxDistance = 10;
            distance = 2f;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
            //重新计算位置  
            var mPosition = _mRotation * new Vector3(0.0F, 0F, -distance) +
                            (target.position + new Vector3(0, 1f, 0));
            isMove = true;
            var t = transform.DOMove(mPosition, 0.5f);
            t.onComplete += () =>
            {
                LookOne = true;
                isMove = false;
            };
            TargetPoint = TargetPoint + new Vector3(0, 1f, 0);
       
    }

    public static Vector3 GetPoint(Transform transform, Vector3 target, float distance)
    {
        var rotation = transform.rotation;
        var mX = rotation.eulerAngles.y;
        var mY = rotation.eulerAngles.x;
        //计算旋转  
        var mRotation = Quaternion.Euler(mY, mX, 0);
        var mPosition = mRotation * new Vector3(0.0F, 0F, -distance) + target;
        return mPosition;
    }

    /// <summary>
    /// 获取两点之间的一个点,在方法中进行了向量的减法，以及乘法运算
    /// </summary>
    /// <param name="start">起始点</param>
    /// <param name="end">结束点</param>
    /// <param name="distance">距离</param>
    /// <returns></returns>
    public static Vector3 GetBetweenPoint(Vector3 start, Vector3 end, float distance)
    {
        var normal = (end - start).normalized;
        normal = normal * distance + start;
        return normal;
    }
}