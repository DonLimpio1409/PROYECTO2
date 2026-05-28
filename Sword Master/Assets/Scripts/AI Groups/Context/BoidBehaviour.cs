using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBehaviour : ScriptableObject
{
    public virtual Vector3 CalculateMove(Context boid, List<Context> context, Flock flock)
    {
        return Vector3.zero;
    }
}