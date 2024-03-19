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
            //�������
            if (Input.GetMouseButtonDown(0) && player.canSwitch)
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
            // ����״̬�߼����
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
                //��ȡ�����תֵ
                float y = Camera.main.transform.rotation.eulerAngles.y;
                //��Ԫ����������ˣ���ʾ�����������������Ԫ�������ĽǶȽ�����ת����õ��µ�����
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
