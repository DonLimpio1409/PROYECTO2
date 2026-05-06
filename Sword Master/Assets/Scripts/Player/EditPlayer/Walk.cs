using System.Net.WebSockets;
using Mono.Cecil.Cil;
using UnityEditor.Callbacks;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Walk : TemplateStateMachinePlayer
{
    private FSMPlayerManager _fsm;

    public Walk(FSMPlayerManager _stateMachineFlow) : base("Walk", (StateMachineFlowPlayer)_stateMachineFlow)
    {
        _fsm = _stateMachineFlow;
    }

    public override void Enter()
    {
        base.Enter();
        _fsm.exit = true;  
        _fsm.anim.SetBool("Walk", true);
        _fsm.enemyBlock = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        IniciateWayPoints();
        EnemyDetected();
    }

    public WayPointDataPlayer.WayPoint target;
    public void IniciateWayPoints()
    {
        _fsm.wayPointData.wayPointList.Clear();
        _fsm.wayPointData.AddWayPoint(new Vector3(_fsm.waypoint1.transform.position.x, _fsm.waypoint1.transform.position.y, _fsm.waypoint1.transform.position.z));
        _fsm.wayPointData.AddWayPoint(new Vector3(_fsm.waypoint2.transform.position.x, _fsm.waypoint2.transform.position.y, _fsm.waypoint2.transform.position.z));
        _fsm.wayPointData.AddWayPoint(new Vector3(_fsm.waypoint3.transform.position.x, _fsm.waypoint3.transform.position.y, _fsm.waypoint3.transform.position.z));

        target = _fsm.wayPointData.wayPointList[_fsm.e];
    } 

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        Vector3 direction = _fsm.wayPointData.wayPointList[_fsm.e].wayPointPosition - _fsm.transform.position;
        Quaternion objective = Quaternion.LookRotation(direction);

        _fsm.transform.rotation = Quaternion.Lerp(_fsm.transform.rotation, objective, Time.deltaTime * 1f);

        _fsm.transform.position = Vector3.MoveTowards(_fsm.transform.position, _fsm.wayPointData.wayPointList[_fsm.e].wayPointPosition, _fsm.speed * Time.deltaTime);
    }

    void EnemyDetected()
    {
        if(_fsm.enemyBlock)
        {
            stateMachineFlow.ChangeState(((FSMPlayerManager)stateMachineFlow).fightState);
        }
    }  
    
}
