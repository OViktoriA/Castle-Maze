using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum action
{
    Idle = 0, Open = 1, Close = 2, Talk = 3, Fight = 4
}
public abstract class InteractableObject : MonoBehaviour
{
    public Transform[] points;

    public Vector3 closestPoint(Vector3 position)
    {
        Vector3 p;

        if (points.Length == 0) 
        {
            p = transform.position;
        }
        else 
        {
            p = points[0].position;
        }

        for (int i = 1; i < points.Length; i++) 
        {
            if (Vector3.Distance(p, position) < Vector3.Distance(points[i].position, position))
            {
                p = points[i].position;
            }
        }

        return p;
    }

    public abstract void interact();



    public abstract action getAction();
}
