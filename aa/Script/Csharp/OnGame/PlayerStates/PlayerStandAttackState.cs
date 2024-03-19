using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandAttackState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        player.attackComboCount = 0;
        // 播放动画
        StandAttack();
    }

    private void StandAttack()
    {
        
        player.attackComboCount++;
        player.PlayAnimation(player.attackDatas[player.attackComboCount - 1].animationName);
    }

    public override void Update()
    {
        base.Update();
        float nTime = 0f;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            //处理旋转
            Vector3 input = new Vector3(h, 0, v);
            //获取相机旋转值
            float y = Camera.main.transform.rotation.eulerAngles.y;
            //四元数与向量相乘：表示让这个向量，按照四元数所表达的角度进行旋转，后得到新的向量
            Vector3 moveDir = Quaternion.Euler(0, y, 0) * input;
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * player.rotateSpeed);
        }
        if (player.attackComboCount>0)
        {
            if (player.canSwitch && Input.GetMouseButtonDown(0) && CheckAnimationState(player.attackDatas[player.attackComboCount - 1].animationName, out nTime) && nTime >= 0.8f)
            {
                StandAttack();
            }
        }


        
        
    }
}
