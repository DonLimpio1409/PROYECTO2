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

    WayPopintData.WayPoint target;
    public override void Enter()
    {
        base.Enter();  //Llama al m�todo de entrada de mi clase base
        _fsm.stateNameT.text = "Idle";

        target = _fsm.waypointData.wayPointList[_fsm.currentWayPointIndex];
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        WaitTime();
        if(_fsm.goIdle)
        {
            stateMachineFlow.ChangeState(((FSMEnemysManager)stateMachineFlow).patrolState);
        }
    }

    public void WaitTime()
    {
        target.waitTime -= Time.deltaTime;

        if (target.waitTime < 0)
        {
            _fsm.goIdle = true;
        }
    }
}
