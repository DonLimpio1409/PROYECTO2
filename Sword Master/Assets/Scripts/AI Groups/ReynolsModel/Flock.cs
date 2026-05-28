using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public Context boid;
    List<Context> boids = new List<Context>();
    [Range(1, 100)] public int flockSize = 10;
    const float AgentDensity = 0.08f;
    [Range(0f, 1f)] public float speed = 0.5f;
    [Range(0f, 3f)] public float maxSpeed = 0.5f;
    [Range(1f, 100f)] public float driveFactor = 10f;

    [Range(1f, 50f)] public float neighborRadius = 3f;
    [Range(0f, 1f)] public float avoidanceFactor = 0.75f;

    public float Square(float _number) { _number *= _number; return _number; }

    void Start()
    {
        for (int i = 0; i < flockSize; i++)
        {
            Context newBoid = Instantiate(
                 boid,
                 transform.position + Random.insideUnitSphere * flockSize * AgentDensity,
                 Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                 transform
                 );
            newBoid.name = "Boid " + i;
            boids.Add(newBoid);
        }
    }

    public List<Context> GetNearbyObjects(Context _boid, List<Context> _contexto)
    {
        List<Context> agentFullContext = new List<Context>();
        foreach (Context _agent in _contexto)
        {
            if (_agent != _boid && (_agent.transform.position - _boid.transform.position).sqrMagnitude < Square(neighborRadius))
            {
                agentFullContext.Add(_agent);
            }
        }
        return agentFullContext;
    }

    public BoidBehaviour boidBehavior;

    void Update()
    {
        int k = 0;
        foreach (Context boidAgent in boids)
        {
            List<Context> context = GetNearbyObjects(boidAgent, boids);
            Vector3 move = boidBehavior.CalculateMove(boidAgent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > Square(maxSpeed))
            {
                move = move.normalized * maxSpeed;
            }
            boidAgent.Move(move);
            k++;
        }
    }
}