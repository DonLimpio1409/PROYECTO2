using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateStateMachineEnemies
{
    public string name;
    protected StateMachineFlowEnemies stateMachineFlow;
    public TemplateStateMachineEnemies(string name, StateMachineFlowEnemies _stateMachineFlow)
    {
        this.name = name;
        this.stateMachineFlow = _stateMachineFlow;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }
}

