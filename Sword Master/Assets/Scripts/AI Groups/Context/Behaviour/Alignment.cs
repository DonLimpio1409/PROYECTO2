using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class Alignment : BoidBehaviour
{
    public override Vector3 CalculateMove(Context boid, List<Context> context, Flock flock)
    {
        if (context.Count == 0)
        {
            return boid.ObjectRb.linearVelocity;
        }
        Vector3 alignmentMove = Vector3.zero;

        foreach (Context agent in context)
        {
            alignmentMove += agent.ObjectRb.linearVelocity;

        }
        alignmentMove /= context.Count;
        return alignmentMove;
    }
}