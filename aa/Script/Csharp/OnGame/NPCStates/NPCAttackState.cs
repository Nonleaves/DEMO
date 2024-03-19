using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NPCAttackState : NPCStateBase
{

    public override void Enter()
    {
        base.Enter();
        npc.attackComboCount = 0;
        NPCAttack();
    }



    public override void Update()
    {
        base.Update();
        npc.transform.LookAt(GameManager.instance.Player);
        if (npc.attackComboCount>0)
        {
            float nTime = 0;
            if (CheckAnimationState(npc.attackDatas[npc.attackComboCount - 1].animationName, out nTime) && nTime >= 0.9f)
            {
                NPCAttack();
            }
        }

        

    }

    private void NPCAttack()
    {
        if (!npc.comboInCD)
        {
            npc.attackComboCount++;
            npc.PlayAnimation(npc.attackDatas[npc.attackComboCount - 1].animationName);
        }
        

        
    }
}
