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
        _fsm.anim.SetBool("Combat", true);
        _fsm.anim.SetBool("Walking", false);
        _fsm.anim.SetBool("Surprise", false);

        _fsm.canPunchAgain = true;
        _fsm.bloking = true; 
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _fsm.rdn = Random.Range(0, _fsm.hitProbably);
        if(_fsm.rdn == 0 && _fsm.canPunchAgain)
        {
            Debug.Log("Te pego");
            Hit();
            _fsm.canPunchAgain = false;
            _fsm.StartCoroutine(WaitToPunchAgain());

        }
        Die();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        //Rotar
        Vector3 direction = _fsm.player.transform.position - _fsm.transform.position;
        direction.y = 0f;
        Quaternion objective = Quaternion.LookRotation(-direction);
        _fsm.transform.rotation = Quaternion.Lerp(_fsm.transform.rotation, objective, Time.deltaTime * 20f);
    }

    public void Die()
    {
        if (_fsm.upEnemy <= 0)
        {
            _fsm.gameObject.GetComponent<FSMEnemysManager>().enabled = false;
            _fsm.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            _fsm.awayEnemies.GetComponent<CapsuleCollider>().enabled = false;
            _fsm.rb.constraints = RigidbodyConstraints.None;
            _fsm.rb.AddForce(20f, 0, 0);
            _fsm.anim.SetBool("Die", true);
        }
    }

    public void Hit()
    {
        _fsm.anim.SetBool("Hit", true);
        
        if(_fsm.player.GetComponent<FSMPlayerManager>().blocking == false)
        {
            _fsm.img.GetComponent<Animator>().SetBool("Damage", true);
            _fsm.player.GetComponent<FSMPlayerManager>().hp -= 1;   
        }
        else
        {
            _fsm.bloking = false;
        }

        _fsm.StartCoroutine(WaitTilt());
    }

    IEnumerator WaitTilt()
    {
        yield return new WaitForEndOfFrame();
        _fsm.anim.SetBool("Hit", false);
        _fsm.img.GetComponent<Animator>().SetBool("Damage", false);
    }

    IEnumerator WaitToPunchAgain()
    {
        yield return new WaitForSeconds(2f);
        _fsm.canPunchAgain = true;
    }
}
