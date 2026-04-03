using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FSMEnemysManager : StateMachineFlow
{
    //Estados
    public Idle idleState;
    public Patrol patrolState;
    public Chase chaseState;
    private void Awake()
    {
        //Declaracion de estados
        idleState = new Idle(this);
        patrolState = new Patrol(this);
        chaseState = new Chase(this);
    }
    protected override void GetInitialState(out TemplateStateMachine _stateMachine)
    {
        // Asignar un valor a la variable de salida
        _stateMachine = idleState;
        _stateMachine = patrolState;
        _stateMachine = chaseState;
    }

    [Header("Elementos de uso")]
    public Rigidbody rb = new Rigidbody();
    public TextMeshProUGUI stateNameT;

    [Header("Patrol")]
    public bool goIdle;
    public bool changeWayPoint;
    public WayPopintData waypointData;
    public float speed = 3;
    public int currentWayPointIndex = 0;
    public int waitTimeR = 1;
    public GameObject waypoint1;
    public GameObject waypoint2;
    public GameObject waypoint3;

    [Header("Detectar al jugador")]
    public Ray rayDetector = new Ray();
    public RaycastHit hit;
    public float rayLength = 2f;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WayPoint")
        {
            changeWayPoint = true;
            goIdle = true;
        }
    }
}

