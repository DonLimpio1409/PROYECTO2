using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Combat : TemplateStateMachineEnemies
{
    private FSMEnemysManager _fsm;

    public Combat(FSMEnemysManager _stateMachineFlow) : base("Combat", (StateMachineFlowEnemies)_stateMachineFlow)
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
        if (_fsm.upEnemy <= 0)
        {
            _fsm.gameObject.GetComponent<FSMEnemysManager>().enabled = false;
            _fsm.rb.constraints = RigidbodyConstraints.None;
            _fsm.rb.AddForce(3f, 0, 0);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
