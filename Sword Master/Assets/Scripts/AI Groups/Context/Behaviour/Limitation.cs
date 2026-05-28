using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Limitation")]
public class Limitation : BoidBehaviour
{
    public Vector3 boxDimensions = new Vector3(100f, 50f, 100f);
    public float turnFactor = 10f;
    
    public override Vector3 CalculateMove(Context boid, List<Context> context, Flock flock)
    {
        Vector3 move = Vector3.zero;
        Vector3 offset = boid.transform.position - flock.transform.position;

        Vector3 halfSize = boxDimensions / 2f; 
        
        Vector3 margin = boxDimensions * 0.25f;

        if (offset.x < -halfSize.x + margin.x)
        {
            move.x += turnFactor;
        }
        else if (offset.x > halfSize.x - margin.x)
        {
            move.x -= turnFactor;
        }

        if (offset.y < -halfSize.y + margin.y)
        {
            move.y += turnFactor;
        }
        else if (offset.y > halfSize.y - margin.y)
        {
            move.y -= turnFactor;
        }

        if (offset.z < -halfSize.z + margin.z)
        {
            move.z += turnFactor;
        }
        else if (offset.z > halfSize.z - margin.z)
        {
            move.z -= turnFactor;
        }

        return move;
    }
}