using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NPCMoveState : NPCStateBase
{

    float walk2runTransition = 0;
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Move");
        npc.PlayAnimation("Move");
    }

    public override void Update()
    {
        base.Update();
        if (GameManager.instance.curState == GameState.OnGame && npc.curMode==NPCmode.FOLLOW)
        {
            if (DetectPlayer() && npc.agent.enabled)
            {
                npc.agent.SetDestination(npc.target.transform.position);
                distanceFromPlayer = Vector3.Distance(npc.target.transform.position, npc.transform.position);
                if (distanceFromPlayer > npc.agent.stoppingDistance)
                {
                    if (distanceFromPlayer <= 4f)
                    {
                        walk2runTransition = Mathf.Clamp(walk2runTransition - Time.deltaTime * 4, 0, 1);

                    }
                    else
                    {
                        walk2runTransition = Mathf.Clamp(walk2runTransition + Time.deltaTime * 4, 0, 1);
                    }
                    npc.animator.SetFloat("Move", walk2runTransition);
                }
                else
                {
                    npc.stateMachine.ChangeState(new NPCAttackState());
                }
            }
            else
            {
                npc.agent.SetDestination(npc.transform.position);
                npc.stateMachine.ChangeState(new NPCIdleState());
               
            }
        }
        return;
    }
}
