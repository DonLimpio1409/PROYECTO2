using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Idle : TemplateStateMachineEnemies
{
    private FSMEnemysManager _fsm;

    public Idle(FSMEnemysManager _stateMachineFlow) : base("Idle", (StateMachineFlowEnemies)_stateMachineFlow)
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
        Detected();
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

    public void Detected()
    {
        _fsm.rayDetector = new Ray(_fsm.transform.position, _fsm.RayDirectionForward);
        Debug.DrawRay(_fsm.transform.position, _fsm.RayDirectionForward * _fsm.raysLength, Color.red);

        if (Physics.Raycast(_fsm.rayDetector, out _fsm.hit, _fsm.raysLength) && _fsm.hit.collider.gameObject.tag == "Player")
        {
            stateMachineFlow.ChangeState(((FSMEnemysManager)stateMachineFlow).chaseState);
        }
    }
}
