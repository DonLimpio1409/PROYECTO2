using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : TemplateStateMachineEnemies
{
    private FSMEnemysManager _fsm;

    public Patrol(FSMEnemysManager _stateMachineFlow) : base("Patrol", (StateMachineFlowEnemies)_stateMachineFlow)
    {
        _fsm = _stateMachineFlow;
    }

    public override void Enter()
    {
        base.Enter();  
        IniciateWayPoints();
        _fsm.anim.SetBool("Surprise", false);
        _fsm.anim.SetBool("Walking", true);
        _fsm.stateNameT.text = "Patrol";
        _fsm.goIdle = false;//Reafirmamos que es False para que no se vaya inmediatamente cuando entra al estado.
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        Detected();
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

    public void Detected()
    {
        //Rectos
        Vector3 RayDirectionForward = _fsm.transform.TransformDirection(0, 0, -1f);
        Vector3 RayDirectionBackward = _fsm.transform.TransformDirection(0, 0, 1f);
        Vector3 RayDirectionRight = _fsm.transform.TransformDirection(1f, 0, 0);
        Vector3 RayDirectionLeft = _fsm.transform.TransformDirection(-1f, 0, 0);

        //Diagonales
        Vector3 RayDirectionLeftFD = _fsm.transform.TransformDirection(1f, 0, -1f);
        Vector3 RayDirectionRightBD = _fsm.transform.TransformDirection(-1f, 0, 1f);
        Vector3 RayDirectionRightFD = _fsm.transform.TransformDirection(-1f, 0, -1f);
        Vector3 RayDirectionLeftBD = _fsm.transform.TransformDirection(1f, 0, 1f);

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

        _fsm.rayDetectorLeftFD = new Ray(_fsm.transform.position, RayDirectionLeftFD);
        Debug.DrawRay(_fsm.transform.position, RayDirectionLeftFD * _fsm.raysLength, Color.red);

        if (Physics.Raycast(_fsm.rayDetectorLeftFD, out _fsm.hit, _fsm.raysLength) && _fsm.hit.collider.gameObject.tag == "Player")
        {
            stateMachineFlow.ChangeState(((FSMEnemysManager)stateMachineFlow).chaseState);
        }

        _fsm.rayDetectorRightBD = new Ray(_fsm.transform.position, RayDirectionRightBD);
        Debug.DrawRay(_fsm.transform.position, RayDirectionRightBD * _fsm.raysLength, Color.red);

        if (Physics.Raycast(_fsm.rayDetectorRightBD, out _fsm.hit, _fsm.raysLength) && _fsm.hit.collider.gameObject.tag == "Player")
        {
            stateMachineFlow.ChangeState(((FSMEnemysManager)stateMachineFlow).chaseState);
        }

        _fsm.rayDetectorRightFD = new Ray(_fsm.transform.position, RayDirectionRightFD);
        Debug.DrawRay(_fsm.transform.position, RayDirectionRightFD * _fsm.raysLength, Color.red);

        if (Physics.Raycast(_fsm.rayDetectorRightFD, out _fsm.hit, _fsm.raysLength) && _fsm.hit.collider.gameObject.tag == "Player")
        {
            stateMachineFlow.ChangeState(((FSMEnemysManager)stateMachineFlow).chaseState);
        }

        _fsm.rayDetectorLeftBD = new Ray(_fsm.transform.position, RayDirectionLeftBD);
        Debug.DrawRay(_fsm.transform.position, RayDirectionLeftBD * _fsm.raysLength, Color.red);

        if (Physics.Raycast(_fsm.rayDetectorLeftBD, out _fsm.hit, _fsm.raysLength) && _fsm.hit.collider.gameObject.tag == "Player")
        {
            stateMachineFlow.ChangeState(((FSMEnemysManager)stateMachineFlow).chaseState);
        }
    }
}
