using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
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
        _fsm.anim.SetBool("Surprise", true);
        _fsm.anim.SetBool("Chase", true);
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
        Vector3 direction = _fsm.player.transform.position - _fsm.transform.position;
        direction.y = 0f;
        Quaternion objective = Quaternion.LookRotation(-direction);

        _fsm.transform.rotation = Quaternion.Lerp(_fsm.transform.rotation, objective, Time.deltaTime * 20f);

        _fsm.currentAnimation = _fsm.anim.GetCurrentAnimatorStateInfo(0);
        //Se espera a que termine la animacion de sorpresa y lo persigue.
        if(_fsm.currentAnimation.IsName("Surprise") && _fsm.currentAnimation.normalizedTime >= 0.9f)
        {
            _fsm.sen = true;
        }
        if (_fsm.sen)
        {
            _fsm.transform.position = Vector3.MoveTowards(_fsm.transform.position, _fsm.player.transform.position, _fsm.speed * Time.deltaTime);
        }
    }
}
