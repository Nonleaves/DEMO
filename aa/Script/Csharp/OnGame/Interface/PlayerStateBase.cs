using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateBase : StateBase
{
    public PlayerController player;

    public override void Init(IStateMachineOwner owner)
    {
        player = (PlayerController)owner;
    }

    public bool CheckAnimationState(string stateName, out float normalizedTime)
    {
        AnimatorStateInfo info = player.animator.GetCurrentAnimatorStateInfo(0);
        normalizedTime = info.normalizedTime;
        return info.IsName(stateName);
    }

    public bool CheckGround()
    {
        Ray ray = new Ray(player.transform.position + Vector3.up * 1.5f, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.8f, player.groundLayer))
        {
           
            return true;
        }
        Debug.Log("not on ground");
        return false;

    }
}
