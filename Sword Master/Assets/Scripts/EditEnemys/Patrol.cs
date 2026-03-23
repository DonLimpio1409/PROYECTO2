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
        //_fsm.rend.material = _fsm.materialEstados[1];
        //Activar animacion
        IniciateWayPoints();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _fsm.goWalk = false;
        GoIdle();
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
        _fsm.waypointData.AddWayPoint(new Vector3(-55.62f, 2.99f, -121.01f), 3f);
        _fsm.waypointData.AddWayPoint(new Vector3(-55.62f, 2.824f, -112.9f), 3f);
        _fsm.waypointData.AddWayPoint(new Vector3(-62.58f, 2.82f, -121.32f), 3f);

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
