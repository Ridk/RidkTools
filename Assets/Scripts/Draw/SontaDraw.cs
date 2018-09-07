using UnityEngine;
using System.Collections.Generic;

namespace Sonta
{
    public class SontaDraw :MonoBehaviour
    {
        [HideInInspector] public GameObject clone;
        [HideInInspector] public LineRenderer _lr;
        public List<GameObject> DrawObjects = new List<GameObject>();
        public GameObject tf;
        
        public void ClearAllObjects()
        {
            for (int i = 0; i < DrawObjects.Count; i++)
            {
                Object.Destroy(DrawObjects[i]);
            }

            DrawObjects.Clear();
        }
    }
}
