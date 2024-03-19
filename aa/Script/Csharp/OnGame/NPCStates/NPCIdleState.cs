using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleState : NPCStateBase
{
    
    public override void Enter()
    {
        base.Enter();
        npc.PlayAnimation("Idle");
    }

    public override void Update()
    {
        base.Update();
        if (GameManager.instance.curState == GameState.OnGame)
        {
            if (npc.curMode == NPCmode.FOLLOW)
            {
                if (CheckGround() && DetectPlayer() && npc.agent.enabled)
                {
                    distanceFromPlayer = Vector3.Distance(npc.target.transform.position, npc.transform.position);
                    if (distanceFromPlayer > npc.agent.stoppingDistance)
                    {
                        npc.stateMachine.ChangeState(new NPCMoveState());
                    }
                    else
                    {
                        npc.stateMachine.ChangeState(new NPCAttackState());
                    }
                }
            }
        }
        

    }

}
