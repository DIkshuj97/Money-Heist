using UnityEngine;
using System.Collections;

public class PointInTime 
{
    public Vector2 position;
    public Quaternion rotation;

    public PointInTime (Vector2 _position,Quaternion _rotation)
    {
        position = _position;
        rotation = _rotation;
    }
}
