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
        base.Enter();  
        IniciateWayPoints();
        _fsm.stateNameT.text = "Patrol";
        _fsm.goIdle = false;//Reafirmamos que es False para que no se vaya inmediatamente cuando entra al estado.
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_fsm.goIdle)
        {
            stateMachineFlow.ChangeState(((FSMEnemysManager) stateMachineFlow).idleState);
            _fsm.goIdle = false;
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
    public void IniciateWayPoints()
    {
        _fsm.waypointData.wayPointList.Clear();
        _fsm.waypointData.AddWayPoint(new Vector3(_fsm.waypoint1.transform.position.x, _fsm.waypoint1.transform.position.y, _fsm.waypoint1.transform.position.z), _fsm.waitTimeR);
        _fsm.waypointData.AddWayPoint(new Vector3(_fsm.waypoint2.transform.position.x, _fsm.waypoint2.transform.position.y, _fsm.waypoint2.transform.position.z), _fsm.waitTimeR);
        _fsm.waypointData.AddWayPoint(new Vector3(_fsm.waypoint3.transform.position.x, _fsm.waypoint3.transform.position.y, _fsm.waypoint3.transform.position.z), _fsm.waitTimeR);

        target = _fsm.waypointData.wayPointList[_fsm.currentWayPointIndex];
    }

    public void GetDestination()
    {
        if(_fsm.changeWayPoint)
        {
            //Sumo uno al indice solo si es menor que el count, de lo contrario pues a 0
            _fsm.currentWayPointIndex = (_fsm.currentWayPointIndex + 1) % _fsm.waypointData.wayPointList.Count;

            target = _fsm.waypointData.wayPointList[_fsm.currentWayPointIndex];

            _fsm.changeWayPoint = false;
        }
    }

    public void MoveToPosition()
    {
        //Mover
        _fsm.transform.position = Vector3.MoveTowards(_fsm.transform.position, target.wayPointPosition, _fsm.speed * Time.deltaTime);

        //Rotar sin que se caiga
        Vector3 direction = _fsm.rb.position - target.wayPointPosition;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);

            Vector3 euler = rotation.eulerAngles;
            euler.x = 0;
            euler.z = 0;

            rotation = Quaternion.Euler(euler);

            _fsm.transform.rotation = Quaternion.Slerp(_fsm.transform.rotation, rotation, 3 * Time.deltaTime);
        }
    }
}
