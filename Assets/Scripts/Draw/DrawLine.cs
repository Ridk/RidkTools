using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 挂载在Panel上
/// </summary>
public class DrawLine : MonoBehaviour
{
    public Image image; //画线的Image
    public Transform rectA; //起点
    public Transform rectB; //终点
    [SerializeField] private Transform endStar;


    public Vector3 endWorldP;

    public Vector3 endP;
    private Canvas _canvas;

    private void OnEnable()
    {
        endWorldP = Vector3.zero;
    }

    private void Start()
    {
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    /// <summary>
    /// 最短长度
    /// </summary>
    public void MiniLine()
    {
        rectB = null;
        Draw(rectA.position, rectA.position);
    }

    /// <summary>
    /// 绘制
    /// </summary>
    /// <param name="pointA"></param>
    /// <param name="pointB"></param>
    private void Draw(Vector2 pointA, Vector2 pointB) //画线方法
    {
        /*GameObject gameObject = new GameObject("line", typeof(Image));//新建一个物体包含一个Image组件
        gameObject.transform.SetParent(graphContainer, false);		 //将该图片设为graphContainer的子物体*/
        //就是在画板内画线
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>(); //获取 RectTransform 组件
        Vector2 dir = pointB - pointA; //两点间的向量i

        //同样将线段锚点设为画板左下角(原点)
        //  rectTransform.anchorMin = new Vector2(0, 0);
        //  rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(dir.magnitude / _canvas.scaleFactor, 3f); //线段的长宽，长为两点间向量的长度，就是两点间距离

        //   rectTransform.anchoredPosition = pointA + dir / 2;			//线段的中心点，为两点间的中心点
        float angle = RotateAngle(dir.x, dir.y); //线段的旋转角度
        rectTransform.localEulerAngles = new Vector3(0, 0, angle); //旋转线段
    }

    private void Update()
    {
        if (rectB != null)
        {
            if (endWorldP == Vector3.zero)
            {
                endWorldP = rectB.position;
            }

            if (Camera.main != null) endP = Camera.main.WorldToScreenPoint(endWorldP);
            endStar.transform.position = endP;
            Draw(rectA.position, endP);
        }
        else
        {
            endStar.transform.position = Input.mousePosition;
        }
    }

    /// <summary>
    /// 旋转
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private float RotateAngle(float x, float y) //旋转方法
    {
        float angle = Mathf.Atan2(y, x) * 180 / 3.14f; //Atan2返回的是弧度，需要乘以180/PI得到角度，这里PI直接用了3.14
        return angle;
    }
}