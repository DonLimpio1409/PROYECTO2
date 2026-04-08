using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateStateMachinePlayer
{
    public string name;
    protected StateMachineFlowPlayer stateMachineFlow;
    public TemplateStateMachinePlayer(string name, StateMachineFlowPlayer _stateMachineFlowPlayer)
    {
        this.name = name;
        this.stateMachineFlow = _stateMachineFlowPlayer;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }
}

