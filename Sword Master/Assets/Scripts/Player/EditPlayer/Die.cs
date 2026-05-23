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
        SoundController.Instance.PlaySFX(SoundController.Instance.buttonIn);
        SoundController.Instance.footstepAudioSource.Stop();
        _fsm.redDie.SetBool("Die", true);
        _fsm.StartCoroutine(StopTime());
        _fsm.dieMenu.SetActive(true);
        stateMachineFlow.ChangeState(((FSMPlayerManager)stateMachineFlow).tutorialState);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator StopTime()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
    }
}