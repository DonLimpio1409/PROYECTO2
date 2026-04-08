using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateMachineFlowPlayer : MonoBehaviour
{
    TemplateStateMachinePlayer currentState;

    void Start()
    {
        GetInitialState(out currentState);
        if (currentState != null)
        {
            currentState.Enter();
        }
    }
    protected virtual void GetInitialState(out TemplateStateMachinePlayer _stateMachine)
    {
        // Asignar un valor a la variable de salida
        _stateMachine = null;
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateLogic();
        }
    }
    void LateUpdate()
    {
        if (currentState != null)
        {
            currentState.UpdatePhysics();
        }
    }

    public void ChangeState(TemplateStateMachinePlayer newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();
    }

    //Messaging:
    public TMP_Text stateName;
    //OnGui
    public void NamePrint(string name)
    {
        if (currentState == null)
            stateName.text = "Warning: no current state";
        else stateName.text = name;
    }
}
