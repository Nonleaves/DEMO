using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDieState : NPCStateBase
{
    public override void Enter()
    {
        base.Enter();
        npc.GetComponent<CapsuleCollider>().enabled = false;
        
        npc.PlayAnimation("Die");
    }

}
