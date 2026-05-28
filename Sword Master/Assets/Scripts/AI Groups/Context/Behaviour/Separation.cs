using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Separation")]
public class Separation : BoidBehaviour
{
    public override Vector3 CalculateMove(Context boid, List<Context> context, Flock flock)
    {
        if (context.Count == 0)
        {
            return Vector3.zero;
        }

        Vector3 avoidanceMove = Vector3.zero;

        int nAvoid = 0;

        foreach (Context item in context)
        {

            if (Vector3.SqrMagnitude(item.transform.position - boid.transform.position) < flock.Square(flock.neighborRadius * flock.avoidanceFactor))
            {
                nAvoid++;

                avoidanceMove += boid.transform.position - item.transform.position;
            }
        }
        if (nAvoid > 0)
        {
            avoidanceMove /= nAvoid;
        } 
        return avoidanceMove;
    }
}