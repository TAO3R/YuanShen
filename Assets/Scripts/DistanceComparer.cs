using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceComparer : IComparer<GameObject>
{
    private Vector2 targetPos;

    public DistanceComparer(Vector2 _pos)
    {
        this.targetPos = _pos;
    }

    public int Compare(GameObject x, GameObject y)
    {
        float distanceToX = Vector2.Distance(targetPos, x.transform.position);
        float distanceToY = Vector2.Distance(targetPos, y.transform.position);
        
        // if (distanceToX == distanceToY) { return 0; }
        return distanceToX.CompareTo(distanceToY);
        
        // throw new System.NotImplementedException();
    }
}
