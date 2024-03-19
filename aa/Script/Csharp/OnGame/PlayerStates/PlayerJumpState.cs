using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerStateBase
{
    public override void Enter()
    {
        player.rb.AddForce(Vector3.up * player.jumpPower, ForceMode.Impulse);
        player.PlayAnimation("Jump",0f);

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
        float nTime = 0;
        if (CheckAnimationState("Jump", out nTime) && nTime < 0.9f)
        {
            player.transform.position += moveDir * Time.deltaTime * player.jumpMoveSpeed;

        }
        else
        {
            player.stateMachine.ChangeState(new PlayerAirDownState());
        }
        return;
    }

}
