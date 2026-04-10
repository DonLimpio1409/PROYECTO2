using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Chase : TemplateStateMachineEnemies
{
    private FSMEnemysManager _fsm;

    public Chase(FSMEnemysManager _stateMachineFlow) : base("WaitCombat", (StateMachineFlowEnemies)_stateMachineFlow)
    {
        _fsm = _stateMachineFlow;
    }

    public override void Enter()
    {
        base.Enter();
        _fsm.goWaitCombat = false;
        //Activar animacion
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if(_fsm.goWaitCombat)
        {
            stateMachineFlow.ChangeState(((FSMEnemysManager)stateMachineFlow).waitCombatState);
        }
        
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _fsm.transform.position = Vector3.MoveTowards(_fsm.transform.position, _fsm.player.transform.position, _fsm.speed * Time.deltaTime);
    }
}
