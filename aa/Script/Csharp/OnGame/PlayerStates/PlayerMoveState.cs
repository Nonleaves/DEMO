using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerStateBase
{

    float walk2runTransition = 0f;
    public override void Enter()
    {
        base.Enter();
        player.PlayAnimation("Move");
    }

    public override void Update()
    {
        base.Update();
        if (GameManager.instance.curState == GameState.OnGame)
        {
            //攻击检测
            if (Input.GetMouseButtonDown(0) && player.canSwitch)
            {
                player.stateMachine.ChangeState(new PlayerStandAttackState());
                return;
            }
            // 跳跃检测
            if (Input.GetKeyDown(KeyCode.Space) && CheckGround())
            {
                player.stateMachine.ChangeState(new PlayerJumpState());
                return;
            }
            // 行走状态逻辑检测
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            float rawh = Input.GetAxisRaw("Horizontal");
            float rawv = Input.GetAxisRaw("Vertical");


            if (rawh != 0 || rawv != 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    walk2runTransition = Mathf.Clamp(walk2runTransition + Time.deltaTime * player.transitionSpeed, 0, 1);
                }
                else
                {
                    walk2runTransition = Mathf.Clamp(walk2runTransition - Time.deltaTime * player.transitionSpeed, 0, 1);
                }
                player.animator.SetFloat("Move", walk2runTransition);

                Vector3 input = new Vector3(h, 0, v);
                //获取相机旋转值
                float y = Camera.main.transform.rotation.eulerAngles.y;
                //四元数与向量相乘：表示让这个向量，按照四元数所表达的角度进行旋转，后得到新的向量
                Vector3 moveDir = Quaternion.Euler(0, y, 0) * input;
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * player.rotateSpeed);


            }
            else
            {
                player.stateMachine.ChangeState(new PlayerIdleState());
            }
        }
        return;
    }




}
