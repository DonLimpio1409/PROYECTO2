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
        _fsm.stateName.text = "Walk";
        _fsm.enemyBlock = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        EnemyDetected();

        /*
        if(Llega al final)
        {
            Cambio de escena.
        }
        */
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _fsm.transform.position = Vector3.MoveTowards(_fsm.transform.position, _fsm.destiny.transform.position, _fsm.speed * Time.deltaTime);
    }

    void EnemyDetected()
    {
        if(_fsm.enemyBlock)
        {
            stateMachineFlow.ChangeState(((FSMPlayerManager)stateMachineFlow).fightState);
        }
    }
}
