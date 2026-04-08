using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public TextMeshProUGUI stateNameT;


    [Header("Walk")]
    public float speed = 3;
    public bool enemyBlock;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            enemyBlock = true;
        }
    }
}
