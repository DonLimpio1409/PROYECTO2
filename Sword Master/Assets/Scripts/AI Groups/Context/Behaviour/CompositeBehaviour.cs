using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]

public class CompositeBehaviour : BoidBehaviour
{
    public BoidBehaviour[] behaviours;
    public float[] weights;

    public override Vector3 CalculateMove(Context boid, List<Context> context, Flock flock)
    {
        if(weights.Length != behaviours.Length)
        {
            Debug.Log("Behaviours and weights don't match");
            return Vector3.zero;
        }

        Vector3 move = Vector3.zero;

        for (int i = 0; i < behaviours.Length; i++)
        {
            Vector3 partialMove = behaviours[i].CalculateMove(boid, context, flock);
            if (partialMove != Vector3.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }
                move += partialMove;
            }
        }

        return move;
    }
}