using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : TemplateStateMachine
{
    private FSMEnemysManager _fsm;

    public Patrol(FSMEnemysManager _stateMachineFlow) : base("Patrol", (StateMachineFlow)_stateMachineFlow)
    {
        _fsm = _stateMachineFlow;
    }

    public override void Enter()
    {
        base.Enter();  //Llama al m�todo de entrada de mi clase base
        _fsm.rend.material = _fsm.materialEstados[1];
        IniciateWayPoints();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _fsm.goWalk = false;
        //GoIdle();
        if (_fsm.goWalk)
        {
            stateMachineFlow.ChangeState(((FSMEnemysManager) stateMachineFlow).idleState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        if(_fsm.waypointData.wayPointList.Count > Mathf.Epsilon)
        {
            GetDestination();
            MoveToPosition();
        }
        else
        {
            Debug.LogError("No ta bien");
        }

    }

    WayPopintData.WayPoint target;
    int currentWayPointIndex = 0;
    public void IniciateWayPoints()
    {
        _fsm.waypointData.wayPointList.Clear();
        _fsm.waypointData.AddWayPoint(new Vector3(24.14f, -0.726f, -20.16f));
        _fsm.waypointData.AddWayPoint(new Vector3(25.98f, -0.726f, -0.08f));
        _fsm.waypointData.AddWayPoint(new Vector3(14.28f, -0.726f, -19.92f));
        _fsm.waypointData.AddWayPoint(new Vector3(3.83f, -0.726f, -18.13f));

        target = _fsm.waypointData.wayPointList[currentWayPointIndex];
    }

    public void GetDestination()
    {
        Vector2 size = _fsm.transform.localScale;
        float radious = size.magnitude / 2f;
        if(Vector3.Distance(_fsm.transform.position, target.wayPointPosition) < radious)
        {
            currentWayPointIndex = (currentWayPointIndex + 1) % _fsm.waypointData.wayPointList.Count;
            target = _fsm.waypointData.wayPointList[currentWayPointIndex];
        }
    }

    public void MoveToPosition()
    {
        _fsm.transform.position = Vector3.MoveTowards(_fsm.transform.position, target.wayPointPosition, _fsm.speed * Time.deltaTime);
    }

    public void GoIdle()
    {
        if(_fsm.transform.position == target.wayPointPosition)
        {
            _fsm.goWalk = true;
        }
    }
}
