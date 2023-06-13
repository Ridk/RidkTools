using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidkGeometry 
{
    Vector3 B;
    Vector3 C;
    private Vector3 VC;


    public static void PlaneNormal()
    {
        /*Vector3.Angle()
        Plane plane =new Plane()
        plane.normal
            
            VC = Vector3.Cross(Va, VB);*/
    }


    public static float Angle(Vector3 a,Vector3 b)
    {
        var angle = Mathf.Acos(Vector3.Dot(a, b) / (a.magnitude * b.magnitude));
        return angle*Mathf.Rad2Deg;
    }


}