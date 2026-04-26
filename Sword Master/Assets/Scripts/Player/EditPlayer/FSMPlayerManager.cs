using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Mono.Cecil.Cil;
using System;
using Unity.VisualScripting;

public class FSMPlayerManager : StateMachineFlowPlayer
{
    //Estados
    public Walk walkState;
    public Fight fightState;
    public ChangeScene changeSceneState;
    public Die dieState;

    private void Awake()
    {
        walkState = new Walk(this);
        fightState = new Fight(this);
        changeSceneState = new ChangeScene(this);
        dieState = new Die(this);
    }
    protected override void GetInitialState(out TemplateStateMachinePlayer _stateMachine)
    {
        // Definir el primer estado del que parte en la maquina
        _stateMachine = walkState;
    }

    [Header("Elementos de uso")]
    public Rigidbody rb = new Rigidbody();
    public Animator anim = new Animator();
    public TextMeshProUGUI stateNameT;


    [Header("Walk")]
    public float speed = 1f;
    public bool enemyBlock;
    public GameObject destiny;

    [Header("Fight")]
    public List<GameObject> fightersList = new List<GameObject>();
    public GameObject cameraR;
    public int i = 0; 
    public bool blocking;
    public int hp = 3;

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            enemyBlock = true;
            fightersList.Add(other.gameObject);
        }
    }
}
