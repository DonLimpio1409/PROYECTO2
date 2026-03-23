using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMEnemysManager : StateMachineFlow
{
    public Idle idleState;
    public Patrol patrolState;
    private void Awake()
    {
        idleState = new Idle(this);
        patrolState = new Patrol(this);
    }
    protected override void GetInitialState(out TemplateStateMachine _stateMachine)
    {
        // Asignar un valor a la variable de salida
        _stateMachine = idleState;
        _stateMachine = patrolState;
    }

    //Elements
    public List<Material> materialEstados = new List<Material>();
    public Renderer rend;
    public Rigidbody rb = new Rigidbody();


    public bool goWalk;
    public WayPopintData waypointData;
    public float speed = 3;
}

