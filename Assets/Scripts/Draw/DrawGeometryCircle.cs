using System.Collections.Generic;
using UnityEngine;


public class DrawGeometryCircle : DrawGeometry
{
    public int positionCount = 12;

    [Tooltip("半径")] public float radius = 3f;
    public float width = .5f;
    private Vector3 centerPoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            centerPoint =drawCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            GameObject drawObject = new GameObject(geometryType.ToString(), typeof(LineRenderer));
            lineRenderer = drawObject.GetComponent<LineRenderer>();
            lineRenderer.material = material;
            drawObjects.Add(lineRenderer);
            lineRenderer.startWidth = lineRenderer.endWidth = width;
            _DrawCircle();
        }

        if (Input.GetMouseButton(0))
        {
            radius = Vector3.Distance(centerPoint,
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));

            _DrawCircle();
        }
    }


    void _DrawCircle()
    {
        lineRenderer.positionCount = positionCount + 1;
        for (int i = 0; i < positionCount; i++)
        {
            float x = radius * Mathf.Cos(i * (2 * Mathf.PI / positionCount));
            float y = radius * Mathf.Sin(i * (2 * Mathf.PI / positionCount));
            lineRenderer.SetPosition(i, centerPoint + new Vector3(x, y, 0));
        }

        lineRenderer.SetPosition(positionCount, lineRenderer.GetPosition(0));
    }
}