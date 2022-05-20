using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MoveStrategy
{
    
    public Vector3 Move(float speed, Transform myTransform);
    public void OnCollisionmove(Vector3 relativePoint);
}
