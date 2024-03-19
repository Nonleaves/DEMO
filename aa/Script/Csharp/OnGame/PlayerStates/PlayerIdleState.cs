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
        // TODO: ����״̬�߼����
        if (GameManager.instance.curState == GameState.OnGame)
        {
            //�������
            if (Input.GetMouseButtonDown(0)&&player.canSwitch)
            {
                player.stateMachine.ChangeState(new PlayerStandAttackState());
                return;
            }

            // ��Ծ���
            if (Input.GetKeyDown(KeyCode.Space) && CheckGround())
            {
                player.stateMachine.ChangeState(new PlayerJumpState());
                return;
            }
            //�ƶ���⣨���ߣ����ܣ�
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
