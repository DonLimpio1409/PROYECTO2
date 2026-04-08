using UnityEditor.Callbacks;
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
        _fsm.stateName.text = "Walk";
        _fsm.enemyBlock = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        EnemyDetected();     
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _fsm.rb.linearVelocity = 
        new Vector3(_fsm.gameObject.transform.position.x * _fsm.speed * Time.deltaTime, _fsm.gameObject.transform.position.y, _fsm.gameObject.transform.position.z);
    }

    void EnemyDetected()
    {
        if(_fsm.enemyBlock)
        {
            stateMachineFlow.ChangeState(((FSMPlayerManager)stateMachineFlow).fightState);
        }
    }
}
