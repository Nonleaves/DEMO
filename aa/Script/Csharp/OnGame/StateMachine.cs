using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IStateMachineOwner { }
public class StateMachine 
{

    IStateMachineOwner owner;
    public StateBase curState;

    public void Init(IStateMachineOwner owner)
    {
        this.owner = owner;
    }

    public void ChangeState (StateBase nextState, bool repeatState = false) 
    {

        if (repeatState) return;

        if (curState != null)
        {
            curState.Exit();
            MonoManager.instance.RemoveUpdateListener(curState.Update);
        }
        nextState.Init(owner);
        curState = nextState;
        curState.Enter();

        MonoManager.instance.AddUpdateListener(curState.Update);
    }

    public void Stop()
    {
        curState.Exit();
        curState = null;
    }

}
