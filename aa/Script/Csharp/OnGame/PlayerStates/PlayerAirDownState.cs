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
        //获取相机旋转值
        float y = Camera.main.transform.rotation.eulerAngles.y;
        //四元数与向量相乘：表示让这个向量，按照四元数所表达的角度进行旋转，后得到新的向量
        Vector3 moveDir = Quaternion.Euler(0, y, 0) * input;
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * player.rotateSpeed);
        // 检测跳跃是否到空中
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
