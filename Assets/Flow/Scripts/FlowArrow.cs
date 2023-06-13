using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class FlowArrow : MonoBehaviour
{

    delegate void TurnNumberChanged();

    private event TurnNumberChanged OnTurnNumberChanged;

    //  [SerializeField] private Image line1;
    [SerializeField] private Image arrows;
    [SerializeField] [Range(0, 4)] private int turnNumbers;
    [SerializeField] private Image arrowhead;
    [SerializeField] private FlowChart.Direction _direction;
    [SerializeField]public RectTransform mTarget;
    public bool isShowHead=true;

    private int currentTurnNumbers;
    private Image[] lines;
    private Vector3[] lineEndPos;

    public int TurnNumbers
    {
        get { return turnNumbers; }
        set { turnNumbers = Mathf.Clamp(value, 0, 4); }
    }

    // Start is called before the first frame update
    void Start()
    {
        FlowChart.Instance.OnDirectionChanged += SetDirection;
        _direction = FlowChart.Instance._direction;
        OnTurnNumberChanged += AddLine;
        SetColor();
        FlowChart.Instance.OnColorChanged += SetColor;
        if (turnNumbers > 0)
        {
            AddLine();
        }
        else
        {
            SetDirection();
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        if (mTarget == null) return;
   

        if (turnNumbers != currentTurnNumbers)
        {
            OnTurnNumberChanged?.Invoke();
        }


        switch (turnNumbers)
        {
            case 1:
                SingletLineEndPos();
                break;
            case 2:
                DoubleLineEndPos();
                break;
            case 3:
                ThreeLineEndPos();
                break;
            case 4:
                FourLineEndPos();
                break;
        }

        //   if (lines==null) return;
        Vector3 start = lines != null ? DrawLines() : transform.position;
        DrawArrows(start, mTarget.position);
    }

    private void SetColor()
    {
        if (lines!=null)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i].color = FlowChart.Instance.lineColor;
            }
        }
        arrowhead.color=FlowChart.Instance.lineColor;
        arrows.color=FlowChart.Instance.lineColor;
    }

    /// <summary>
    /// 绘制所有线条(包含转折点)
    /// </summary>
    /// <returns></returns>
    public Vector3 DrawLines()
    {
        Vector3 endPos = transform.position;
        for (int i = 0; i < turnNumbers; i++)
        {
            if (i == 0)
            {
                endPos = DrawlLine(lines[i], transform.position, lineEndPos[i]);
            }
            else
            {
                lines[i].transform.position = lineEndPos[i - 1];
                endPos = DrawlLine(lines[i], lineEndPos[i - 1], lineEndPos[i]);
            }
        }

        return endPos;
    }

    /// <summary>
    /// 增减线条实例对象
    /// </summary>
    public void AddLine()
    {
        if (lines != null)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                Destroy(lines[i].gameObject);
            }
        }

        lines = new Image[turnNumbers];
        for (int i = 0; i < turnNumbers; i++)
        {
            var line = Instantiate(FlowChart.Instance.line, transform);
            line.name = $"line{i}";
            lines[i] = line.GetComponent<Image>();
            lines[i].color = FlowChart.Instance.lineColor;
        }

        currentTurnNumbers = turnNumbers;

        SetDirection();
    }

    /// <summary>
    /// 设置单个转折点
    /// </summary>
    public void SingletLineEndPos()
    {
        lineEndPos = new Vector3[turnNumbers];
        switch (_direction)
        {
            case FlowChart.Direction.Vertical:
                lineEndPos[0] = new Vector3(mTarget.position.x, transform.position.y);
                break;
            case FlowChart.Direction.Horizontal:
                lineEndPos[0] = new Vector3(transform.position.x, mTarget.position.y);
                break;
        }
    }

    /// <summary>
    /// 设置2个转折点
    /// </summary>
    public void DoubleLineEndPos()
    {
        lineEndPos = new Vector3[turnNumbers];
        switch (_direction)
        {
            case FlowChart.Direction.Vertical:
                lineEndPos[0] = new Vector3(transform.position.x,
                    transform.position.y + (mTarget.position.y - transform.position.y) / 2);
                lineEndPos[1] = new Vector3(mTarget.position.x, lineEndPos[0].y);
                break;
            case FlowChart.Direction.Horizontal:
                lineEndPos[0] = new Vector3(transform.position.x + (mTarget.position.x - transform.position.x) / 2,
                    transform.position.y);
                lineEndPos[1] = new Vector3(lineEndPos[0].x, mTarget.position.y);
                break;
        }
    }

    /// <summary>
    /// 设置3个转折点
    /// </summary>
    public void ThreeLineEndPos()
    {
        lineEndPos = new Vector3[turnNumbers];
        lineEndPos[0] = new Vector3(transform.position.x + (mTarget.position.x - transform.position.x) / 2,
            transform.position.y);
        lineEndPos[1] = new Vector3(lineEndPos[0].x, mTarget.position.y);
        //   lineEndPos[0]=new Vector3(transform.position.x+(transform.position.x-mTarget.position.x)/2,transform.position.y);
        //  lineEndPos[0]=new Vector3(transform.position.x+(transform.position.x-mTarget.position.x)/2,transform.position.y);
    }

    /// <summary>
    /// 设置4个转折点
    /// </summary>
    public void FourLineEndPos()
    {
        lineEndPos = new Vector3[turnNumbers];
        lineEndPos[0] = new Vector3(transform.position.x + (mTarget.position.x - transform.position.x) / 2,
            transform.position.y);
        lineEndPos[1] = new Vector3(lineEndPos[0].x, mTarget.position.y);
        //   lineEndPos[0]=new Vector3(transform.position.x+(transform.position.x-mTarget.position.x)/2,transform.position.y);
        //  lineEndPos[0]=new Vector3(transform.position.x+(transform.position.x-mTarget.position.x)/2,transform.position.y);
    }

    /// <summary>
    /// 设置线条绘制方向,垂直或者水平
    /// </summary>
    public void SetDirection()
    {
        _direction = FlowChart.Instance._direction;
        switch (_direction)
        {
            case FlowChart.Direction.Vertical:
                if (turnNumbers > 0)
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        lines[i].rectTransform.pivot = new Vector2(1, 0f);
                    }
                }

                arrows.rectTransform.pivot = new Vector2(0.5f, 0f);
                break;
            case FlowChart.Direction.Horizontal:
                if (turnNumbers > 0)
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        lines[i].rectTransform.pivot = new Vector2(0.5f, 0f);
                    }
                }

                arrows.rectTransform.pivot = new Vector2(0.5f, 0f);
                break;
        }
    }

    /// <summary>
    /// 绘制带箭头的线指向目标
    /// </summary>
    /// <param name="start">开始点</param>
    /// <param name="end">结束点</param>
    public void DrawArrows(Vector3 start, Vector3 end)
    {
        arrows.transform.position = start;
        var slope = Mathf.Atan((mTarget.rect.height) / (mTarget.rect.width))* Mathf.Rad2Deg; //算出坡度
        float angle = Vector3.Angle(new Vector3(start.x,end.y)-end, start-end);  //计算到目标与水平线的夹角
        float diffe;
        if (angle==0)
        {
            diffe =start.y==end.y ? mTarget.rect.width / 2  : mTarget.rect.height / 2 ;
        }
        else
        {
            diffe = angle < slope ? mTarget.rect.width / 2 / Mathf.Cos(angle * Mathf.Deg2Rad) : mTarget.rect.height / 2 / Mathf.Cos((90-angle) * Mathf.Deg2Rad);
        }
        
        if (!isShowHead)
        {
            diffe = 0;
            arrowhead.enabled = false;
        }
        else
        {
            arrowhead.enabled = true;
        }

        var length = Vector3.Distance(arrows.transform.position, mTarget.position) - diffe-arrowhead.rectTransform.rect.height;
        arrows.rectTransform.sizeDelta = new Vector2(4, length);
        double angle1 = Mathf.Atan2(end.y - start.y, end.x - start.x) * Mathf.Rad2Deg;
        arrows.transform.rotation = Quaternion.Euler(0, 0, (float) angle1 -90);
    }

    /// <summary>
    /// 两个点之间绘制一条线
    /// </summary>
    /// <param name="line">绘制的线条对象</param>
    /// <param name="start">开始点</param>
    /// <param name="end">结束点</param>
    /// <returns>返回最后结束的点</returns>
    public Vector3 DrawlLine(Image line, Vector3 start, Vector3 end)
    {
        line.rectTransform.sizeDelta = new Vector2(4, Vector3.Distance(start, end));
        double angle1 = Mathf.Atan2(end.y - start.y, end.x - start.x) * 180 / Mathf.PI;
        line.transform.rotation = Quaternion.Euler(0, 0, (float) angle1 + 270);
        return end;
    }
}