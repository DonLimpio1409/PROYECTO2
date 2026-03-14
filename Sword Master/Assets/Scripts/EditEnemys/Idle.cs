using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Idle : TemplateStateMachine
{
    private FSMEnemysManager _fsm;

    public Idle(FSMEnemysManager _stateMachineFlow) : base("Idle", (StateMachineFlow)_stateMachineFlow)
    {
        _fsm = _stateMachineFlow;
    }

    public override void Enter()
    {
        base.Enter();  //Llama al m�todo de entrada de mi clase base
        _fsm.rend.material = _fsm.materialEstados[0];
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();  
        _fsm.goWalk = false;
        _fsm.StartCoroutine(_fsm.WaitForPatrol());
        if(_fsm.goWalk)
        {
            stateMachineFlow.ChangeState(((FSMEnemysManager)stateMachineFlow).patrolState);
        }
    }
}
