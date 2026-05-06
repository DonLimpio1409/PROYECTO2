using UnityEngine;

public class TutorialSta : TemplateStateMachinePlayer
{
    private FSMPlayerManager _fsm;

    public TutorialSta(FSMPlayerManager _stateMachineFlow) : base("Tutorial", (StateMachineFlowPlayer)_stateMachineFlow)
    {
        _fsm = _stateMachineFlow;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(_fsm.tutorialControl.GetComponent<Tutorial>().tutorialDone)
        {
            _fsm.ChangeState(_fsm.walkState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        
    }
}
