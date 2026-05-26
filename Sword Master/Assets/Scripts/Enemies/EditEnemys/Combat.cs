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
        _fsm.Shield.SetActive(true);

        SoundController.Instance.footstepAudioSource.Stop();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _fsm.timeDoingNacing += Time.deltaTime;
        _fsm.rdn = Random.Range(0, _fsm.hitProbably);
        if(_fsm.rdn == 0 && _fsm.canPunchAgain && !_fsm.isStuned)
        {
            Hit();
            _fsm.canPunchAgain = false;
            _fsm.StartCoroutine(WaitToPunchAgain());

        }

        if(_fsm.timeDoingNacing >= 3f && _fsm.canPunchAgain && !_fsm.isStuned)
        {
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
        Vector3 direction = _fsm.player.transform.position - _fsm.rot.transform.position;
        direction.y = 0f;
        Quaternion objective = Quaternion.LookRotation(-direction);
        _fsm.rot.transform.rotation = Quaternion.Lerp(_fsm.rot.transform.rotation, objective, Time.deltaTime * 20f);
    }

    public void Die()
    {
        if (_fsm.upEnemy <= 0)
        {
            _fsm.gameObject.GetComponent<FSMEnemysManager>().enabled = false;
            _fsm.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            _fsm.awayEnemies.SetActive(false);
            _fsm.rb.constraints = RigidbodyConstraints.None;
            _fsm.rb.AddForce(20f, 0, 0);
            _fsm.anim.SetBool("Die", true);
        }
    }

    public void Hit()
    {
        _fsm.anim.SetTrigger("Hitt"); 
        _fsm.StartCoroutine(WaitToPunch());
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

    IEnumerator WaitToPunch()
    {
        yield return new WaitForSeconds(_fsm.timeToPunch);
        if(_fsm.player.GetComponent<FSMPlayerManager>().blocking == false)
        {
            SoundController.Instance.PlaySFX(SoundController.Instance.getHitted);
            _fsm.img.GetComponent<Animator>().SetBool("Damage", true);
            _fsm.player.GetComponent<FSMPlayerManager>().hp -= 1;
            _fsm.player.GetComponent<Animator>().SetTrigger("Hit");

            _fsm.player.GetComponent<FSMPlayerManager>().lifeList.Dequeue();

        }
        else
        {
            _fsm.bloking = false;
            _fsm.isStuned = true;
            _fsm.Shield.SetActive(false);
            _fsm.Stun.SetActive(true);
            SoundController.Instance.PlaySFX(SoundController.Instance.dizzySound);
            SoundController.Instance.PlaySFX(SoundController.Instance.parryAtEnemy);
            _fsm.StartCoroutine(Stuned());
        }
    }

    IEnumerator Stuned()
    {
        yield return new WaitForSeconds(1f);
        _fsm.isStuned = false;
        _fsm.Stun.SetActive(false);
        _fsm.Shield.SetActive(true);
        _fsm.bloking = true;
    }
}
