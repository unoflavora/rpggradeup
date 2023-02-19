using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    private void OnDrawGizmos() 
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Vector3 from = GetWayPoint(i);
            Gizmos.DrawSphere(from, .3f);
            
            int j = GetNextIndex(i);
            Vector3 to = GetWayPoint(j);
            Gizmos.DrawLine(from,to);
        }
    }

    public int GetNextIndex(int i)
    {
        return (i + 1) % (transform.childCount);
    }

    public Vector3 GetWayPoint(int i)
    {
        return transform.GetChild(i).transform.position;
    }
}
