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
        stateMachineFlow.ChangeState(((FSMPlayerManager)stateMachineFlow).walkState);
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
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