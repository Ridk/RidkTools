using System;
using UnityEngine;
public class FlowChart :MonoBehaviour
{
    public delegate void ColorChanged();

    public event ColorChanged OnColorChanged;
   public delegate void DirectionChanged();
    public event DirectionChanged OnDirectionChanged;
    private static FlowChart _instance;
    public GameObject line;
    public GameObject arrows;
    public Sprite[] stepSprites;
    public Color32 lineColor=Color.red;
    public Direction _direction =Direction.Horizontal;
    private Color32 _color32;
    private FlowChart.Direction currentDirection;
    public static FlowChart Instance
    {
        get
        {
            if (_instance != null) return _instance;
            var  flowChart=new GameObject("FlowChart");
            _instance = flowChart.AddComponent<FlowChart>();
            _instance.LoadAssets();
            return _instance;
        }
    }

    private void Update()
    {

        if (lineColor.ToString() != _color32.ToString())
        {
            _color32 = lineColor;
            OnColorChanged?.Invoke();
        }
        if (_direction != currentDirection)
        {
            currentDirection = _direction;
            OnDirectionChanged?.Invoke();
        }

       
    }

    public enum Direction
    {
        Vertical,
        Horizontal,
    }

    public enum LineType
    {
        Line,
        TurnLine,
        Arrow,
        TurnArrow
    }
    public enum StepType
    {
        None,
        Ellipse,
        Rectangle,
        Rhombus,
        Rhomboid
    }


    private void LoadAssets()
    {
        _color32 = lineColor;
        currentDirection = _direction;
        arrows=Resources.Load<GameObject>("Prefabs/FlowArrow");
        line=Resources.Load<GameObject>("Prefabs/line");
    }
}
