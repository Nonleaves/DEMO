using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirDownState : PlayerStateBase
{
    bool inAir = false;
    public override void Enter()
    {
        player.PlayAnimation("AirDown");
    }

    public override void Update()
    {
        base.Update();
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(h, 0, v);
        //��ȡ�����תֵ
        float y = Camera.main.transform.rotation.eulerAngles.y;
        //��Ԫ����������ˣ���ʾ�����������������Ԫ�������ĽǶȽ�����ת����õ��µ�����
        Vector3 moveDir = Quaternion.Euler(0, y, 0) * input;
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * player.rotateSpeed);
        // �����Ծ�Ƿ񵽿���
        if (!CheckGround())
        {
            inAir = true;
            player.transform.position += moveDir * Time.deltaTime * player.jumpMoveSpeed;

        }
        else
        {

            AnimatorStateInfo info = player.animator.GetCurrentAnimatorStateInfo(0);
            if (info.IsName("Land"))
            {

                if (info.normalizedTime >= 0.8f)
                {
                    player.stateMachine.ChangeState(new PlayerIdleState());
                    //player.PlayerChangeState(PlayerStateEnum.Idle);
                }
            }
            else
            {
                player.PlayAnimation("Land", 0f);
            }
            
 

            
        }
        return;
    }

}
