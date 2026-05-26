using UnityEngine;


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
        _fsm.anim.SetBool("Walk", true);
        _fsm.enemyBlock = false;
        SoundController.Instance.DecideFootstepSound();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        IniciateWayPoints();
        EnemyDetected();
    }
    public void IniciateWayPoints()
    {
        _fsm.wayPointData.wayPointList.Clear();

        GameObject[] waypoints = new GameObject[]
        {
            _fsm.waypoint1,
            _fsm.waypoint2,
            _fsm.waypoint3,
            _fsm.waypoint4,
            _fsm.waypoint5,
            _fsm.waypoint6,
            _fsm.waypoint7
        };

        foreach (GameObject wp in waypoints)
        {
            if (wp == null)
            {
                continue;
            }

            _fsm.wayPointData.AddWayPoint(wp.transform.position);
        }

        if (_fsm.wayPointData.wayPointList.Count == 0)
            return;

    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        if (_fsm.wayPointData.wayPointList.Count == 0)
            return;
            
        var wp = _fsm.wayPointData.wayPointList[0];

        Vector3 direction = wp.wayPointPosition - _fsm.transform.position;
        Quaternion objective = Quaternion.LookRotation(direction);

        _fsm.transform.rotation = Quaternion.Lerp(_fsm.transform.rotation, objective, Time.deltaTime * 1f);

        _fsm.transform.position = Vector3.MoveTowards(_fsm.transform.position, wp.wayPointPosition, _fsm.speed * Time.deltaTime);
    }


    void EnemyDetected()
    {
        if(_fsm.enemyBlock)
        {
            stateMachineFlow.ChangeState(((FSMPlayerManager)stateMachineFlow).fightState);
        }
    }  
}
