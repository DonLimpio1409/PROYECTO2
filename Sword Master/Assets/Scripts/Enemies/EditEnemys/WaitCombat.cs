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
        base.Enter();
        //Activar animacion
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_fsm.greenLight)
        {
            stateMachineFlow.ChangeState(((FSMEnemysManager)stateMachineFlow).combatState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
