using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        base.Enter();
        _fsm.anim.SetBool("Surprise", false);
        _fsm.anim.SetBool("Walking", false);
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
        Vector3 RayDirectionForward = _fsm.transform.TransformDirection(0, 0, -1f);
        Vector3 RayDirectionBackward = _fsm.transform.TransformDirection(0, 0, 1f);
        Vector3 RayDirectionRight = _fsm.transform.TransformDirection(1f, 0, 0);
        Vector3 RayDirectionLeft = _fsm.transform.TransformDirection(-1f, 0, 0);

        _fsm.rayDetectorForward = new Ray(_fsm.transform.position, RayDirectionForward);
        Debug.DrawRay(_fsm.transform.position, RayDirectionForward * _fsm.raysLength, Color.red);

        if (Physics.Raycast(_fsm.rayDetectorForward, out _fsm.hit, _fsm.raysLength) && _fsm.hit.collider.gameObject.tag == "Player")
        {
            stateMachineFlow.ChangeState(((FSMEnemysManager)stateMachineFlow).chaseState);
        }

        _fsm.rayDetectorBackward = new Ray(_fsm.transform.position, RayDirectionBackward);
        Debug.DrawRay(_fsm.transform.position, RayDirectionBackward * _fsm.raysLength, Color.red);

        if (Physics.Raycast(_fsm.rayDetectorBackward, out _fsm.hit, _fsm.raysLength) && _fsm.hit.collider.gameObject.tag == "Player")
        {
            stateMachineFlow.ChangeState(((FSMEnemysManager)stateMachineFlow).chaseState);
        }

        _fsm.rayDetectorRight = new Ray(_fsm.transform.position, RayDirectionRight);
        Debug.DrawRay(_fsm.transform.position, RayDirectionRight * _fsm.raysLength, Color.red);

        if (Physics.Raycast(_fsm.rayDetectorRight, out _fsm.hit, _fsm.raysLength) && _fsm.hit.collider.gameObject.tag == "Player")
        {
            stateMachineFlow.ChangeState(((FSMEnemysManager)stateMachineFlow).chaseState);
        }

        _fsm.rayDetectorLeft = new Ray(_fsm.transform.position, RayDirectionLeft);
        Debug.DrawRay(_fsm.transform.position, RayDirectionLeft * _fsm.raysLength, Color.red);

        if (Physics.Raycast(_fsm.rayDetectorLeft, out _fsm.hit, _fsm.raysLength) && _fsm.hit.collider.gameObject.tag == "Player")
        {
            stateMachineFlow.ChangeState(((FSMEnemysManager)stateMachineFlow).chaseState);
        }
    }
}
