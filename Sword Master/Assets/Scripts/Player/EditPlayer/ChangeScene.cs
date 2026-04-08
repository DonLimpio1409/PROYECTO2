using UnityEngine;

public class ChangeScene : TemplateStateMachinePlayer
{
    private FSMPlayerManager _fsm;

    public ChangeScene(FSMPlayerManager _stateMachineFlow) : base("ChangeScene", (StateMachineFlowPlayer)_stateMachineFlow)
    {
        _fsm = _stateMachineFlow;
    }

    public override void Enter()
    {
        base.Enter();
        //_fsm.rend.material = _fsm.materialEstados[2];
        //Activar animacion
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