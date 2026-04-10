using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

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
        //Lógica de persecución
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        int upEnemy = _fsm.fightersList[_fsm.i].GetComponent<FSMEnemysManager>().upEnemy;
        _fsm.fightersList[_fsm.i].GetComponent<FSMEnemysManager>().greenLight = true;

        while (upEnemy > 0)
        {
            Vector3 direction = _fsm.fightersList[_fsm.i].transform.position - _fsm.camera.transform.position;
            Quaternion objective = Quaternion.LookRotation(direction);

            _fsm.camera.transform.rotation = Quaternion.Lerp(_fsm.camera.transform.rotation, objective, Time.deltaTime * 5f);

            if (Input.GetMouseButtonDown(0))
            {
                _fsm.fightersList[_fsm.i].GetComponent<FSMEnemysManager>().upEnemy--;
            }
        }
        _fsm.i++;
        _fsm.fightersList[_fsm.i].GetComponent<FSMEnemysManager>().greenLight = false;
        if (_fsm.fightersList.Count < _fsm.i)
        {
            stateMachineFlow.ChangeState(((FSMPlayerManager)stateMachineFlow).walkState);
        }
    }
}