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
    public WaitCombat waitCombatState;
    public Combat combatState;
    private void Awake()
    {
        //Declaracion de estados
        idleState = new Idle(this);
        patrolState = new Patrol(this);
        chaseState = new Chase(this);
        waitCombatState = new WaitCombat(this);
        combatState = new Combat(this);
    }

    protected override void GetInitialState(out TemplateStateMachineEnemies _stateMachine)
    {
        // Definir el primer estado del que parte en la maquina
        _stateMachine = patrolState;
    }

    [Header("Elementos de uso")]
    public Rigidbody rb = new Rigidbody();
    public TextMeshProUGUI stateNameT;
    public int upEnemy = 3;

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
    public Ray rayDetectorForward = new Ray();
    public Ray rayDetectorBackward = new Ray();
    public Ray rayDetectorLeft = new Ray();
    public Ray rayDetectorRight = new Ray();
    public RaycastHit hit;
    public float raysLength = 7f;

    //[Header("Detectar al jugador")]
    public bool goWaitCombat;
    public GameObject player;

    //WaitCombat
    public bool greenLight;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WayPoint")
        {
            changeWayPoint = true;
            goIdle = true;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            goWaitCombat = true;
        }
    }
}

