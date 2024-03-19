using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerIdleState : PlayerStateBase
{
    public override void Enter()
    {
        player.PlayAnimation("Idle");
    }

    public override void Update()
    {
        base.Update();
        // TODO: ´ý»ú×´Ì¬Âß¼­¼ì²â
        if (GameManager.instance.curState == GameState.OnGame)
        {
            //¹¥»÷¼ì²â
            if (Input.GetMouseButtonDown(0)&&player.canSwitch)
            {
                player.stateMachine.ChangeState(new PlayerStandAttackState());
                return;
            }

            // ÌøÔ¾¼ì²â
            if (Input.GetKeyDown(KeyCode.Space) && CheckGround())
            {
                player.stateMachine.ChangeState(new PlayerJumpState());
                return;
            }
            //ÒÆ¶¯¼ì²â£¨ÐÐ×ß£¬±¼ÅÜ£©
            float rawh = Input.GetAxisRaw("Horizontal");
            float rawv = Input.GetAxisRaw("Vertical");

            if (rawh != 0 || rawv != 0)
            {
                player.stateMachine.ChangeState(new PlayerMoveState());
            }

        }
        return;
    }

}
