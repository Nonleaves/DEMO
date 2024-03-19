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
        // ���Ŷ���
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
            //������ת
            Vector3 input = new Vector3(h, 0, v);
            //��ȡ�����תֵ
            float y = Camera.main.transform.rotation.eulerAngles.y;
            //��Ԫ����������ˣ���ʾ�����������������Ԫ�������ĽǶȽ�����ת����õ��µ�����
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
