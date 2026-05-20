using UnityEngine;
using UnityEngine.SceneManagement;

public class Die : TemplateStateMachinePlayer
{
    private FSMPlayerManager _fsm;

    public Die(FSMPlayerManager _stateMachineFlow) : base("Die", (StateMachineFlowPlayer)_stateMachineFlow)
    {
        _fsm = _stateMachineFlow;
    }

    public override void Enter()
    {
        base.Enter();
        _fsm.dieMenu.SetActive(true);
        stateMachineFlow.ChangeState(((FSMPlayerManager)stateMachineFlow).tutorialState);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        //Lógica de persecución
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        //Movimiento de persecución
    }
}