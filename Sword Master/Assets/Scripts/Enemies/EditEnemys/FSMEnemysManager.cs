using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

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
    public Animator anim = new Animator();
    public int upEnemy = 3;

    [Header("Animation")]
    public AnimatorStateInfo currentAnimation;
    public bool sen = false;

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
    //Rectos
    public Ray rayDetectorForward = new Ray();
    public Ray rayDetectorBackward = new Ray();
    public Ray rayDetectorLeft = new Ray();
    public Ray rayDetectorRight = new Ray();

    //Diagonales
    public Ray rayDetectorLeftFD = new Ray();
    public Ray rayDetectorRightBD = new Ray();
    public Ray rayDetectorRightFD = new Ray();
    public Ray rayDetectorLeftBD = new Ray();
    public bool detected;

    public RaycastHit hit;
    public float raysLength = 7f;

    [Header("Objetos")]
    public bool goWaitCombat;
    public GameObject player;
    public GameObject awayEnemies;

    [Header("WaitCombat")]
    public bool greenLight;

    [Header("Combat")]
    public bool canPunchAgain;
    public bool bloking = true;
    public Image img;
    public int rdn = 0;
    public int hitProbably = 2000;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WayPoint")
        {
            changeWayPoint = true;
            goIdle = true;
        }

        if(other.tag == "Sword" && bloking == false)
        {
            upEnemy--;
            bloking = true;
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

