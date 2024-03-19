using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase 
{
    public virtual void Init(IStateMachineOwner owner) { }
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
