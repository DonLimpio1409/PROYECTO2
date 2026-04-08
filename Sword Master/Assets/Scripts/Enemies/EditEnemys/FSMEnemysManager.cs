using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FSMEnemysManager : StateMachineFlowEnemies
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
    protected override void GetInitialState(out TemplateStateMachineEnemies _stateMachine)
    {
        // Definir el primer estado del que parte en la maquina
        _stateMachine = patrolState;
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
    public float raysLength = 6f;
    public Vector3 RayDirectionForward = new Vector3(0, 0, -6);
    public Vector3 RayDirectionBackward = new Vector3(0, 0, 6);
    public Vector3 RayDirectionRight = new Vector3(6, 0, 0);
    public Vector3 RayDirectionLeft = new Vector3(-6, 0, 0);

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WayPoint")
        {
            changeWayPoint = true;
            goIdle = true;
        }
    }
}

