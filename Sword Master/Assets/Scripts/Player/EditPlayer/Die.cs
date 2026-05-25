using UnityEngine;
using System.Collections; 
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
        Debug.Log("Die");
        SoundController.Instance.footstepAudioSource.clip = null;
        SoundController.Instance.PlaySFX(SoundController.Instance.buttonIn);
        _fsm.redDie.SetBool("Die", true);
        _fsm.tempBlackDie.GetComponent<Animator>().SetBool("Die", true);
        _fsm.dieMenu.SetActive(true);
        _fsm.tempBlackDie.SetActive(true);
        stateMachineFlow.ChangeState(((FSMPlayerManager)stateMachineFlow).tutorialState);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}