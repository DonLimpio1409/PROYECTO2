using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class Cohesion : BoidBehaviour
{
    public override Vector3 CalculateMove(Context boid, List<Context> context, Flock flock)
    {

        if (context.Count == 0)
            return Vector3.zero;

        Vector3 cohesionMove = Vector3.zero;
        foreach (Context item in context)
        {
            cohesionMove += item.transform.position;
        }
        cohesionMove /= context.Count;

        cohesionMove -= boid.transform.position;
        return cohesionMove;
    }

}