using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerStateBase
{
    bool shutDown = false;
    public override void Enter()
    {
        base.Enter();
        player.rb.constraints = RigidbodyConstraints.FreezePositionY;
        player.GetComponent<CapsuleCollider>().enabled = false;
        player.PlayAnimation("Die");

    }

    public override void Update()
    {
        base.Update();
        if (!shutDown)
        {
            float nTime = 0;
            if (CheckAnimationState("Die", out nTime) && nTime >= 1f)
            {
                player.stateMachine.Stop();
                shutDown = true;
                GameManager.instance.GameOer();
            }
        }
        
    }


}
