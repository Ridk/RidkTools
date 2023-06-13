using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;


public class DrawGeometry :MonoBehaviour
{
   
   public  enum GeometryType
    {
        Line,
        Circle,
        Rect,
    }
   public Material material;
  public Camera drawCamera;
   public GeometryType geometryType;
    [HideInInspector] public LineRenderer lineRenderer;
    public List<LineRenderer> drawObjects = new List<LineRenderer>();
    
    public void ClearAllObjects()
    {
        for (int i = 0; i < drawObjects.Count; i++)
        {
            Object.Destroy(drawObjects[i]);
        }

        drawObjects.Clear();
    }
}

