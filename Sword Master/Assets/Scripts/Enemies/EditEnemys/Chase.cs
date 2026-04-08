using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;

public class Chase : TemplateStateMachineEnemies
{
    private FSMEnemysManager _fsm;

    public Chase(FSMEnemysManager _stateMachineFlow) : base("Chase", (StateMachineFlowEnemies)_stateMachineFlow)
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
