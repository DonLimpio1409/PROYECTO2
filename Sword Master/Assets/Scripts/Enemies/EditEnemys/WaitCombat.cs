using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;

public class WaitCombat : TemplateStateMachineEnemies
{
    private FSMEnemysManager _fsm;

    public WaitCombat(FSMEnemysManager _stateMachineFlow) : base("WaitCombat", (StateMachineFlowEnemies)_stateMachineFlow)
    {
        _fsm = _stateMachineFlow;
    }

    public override void Enter()
    {
        base.Enter();  //Llama al m�todo de entrada de mi clase base
        //_fsm.rend.material = _fsm.materialEstados[4];
        //Activar animacion
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        //Lógica de espera antes del combate
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        //Movimiento de espera antes del combate
    }
}
