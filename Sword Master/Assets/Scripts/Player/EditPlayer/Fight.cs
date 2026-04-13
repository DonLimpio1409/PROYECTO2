using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fight : TemplateStateMachinePlayer
{
    private FSMPlayerManager _fsm;

    public Fight(FSMPlayerManager _stateMachineFlow) : base("Fight", (StateMachineFlowPlayer)_stateMachineFlow)
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
        int upEnemy = _fsm.fightersList[_fsm.i].GetComponent<FSMEnemysManager>().upEnemy;
        _fsm.fightersList[_fsm.i].GetComponent<FSMEnemysManager>().greenLight = true;

        if (upEnemy > 0)
        {
            Vector3 direction = _fsm.fightersList[_fsm.i].transform.position - _fsm.transform.position;
            direction.y = 0f;
            Quaternion objective = Quaternion.LookRotation(direction);

            _fsm.transform.rotation = Quaternion.Lerp(_fsm.transform.rotation, objective, Time.deltaTime * 20f);
        }
        else
        {
            _fsm.fightersList[_fsm.i].GetComponent<FSMEnemysManager>().greenLight = false;
            _fsm.i++;

            if ( _fsm.i >= _fsm.fightersList.Count)
            {
                stateMachineFlow.ChangeState(((FSMPlayerManager)stateMachineFlow).walkState);
            }   
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}