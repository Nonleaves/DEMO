using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateBase : StateBase
{
    public NPCController npc;
    public float distanceFromPlayer;
    public override void Init(IStateMachineOwner owner)
    {
        npc = (NPCController)owner;
    }

    public bool CheckAnimationState(string stateName, out float normalizedTime)
    {
        AnimatorStateInfo info = npc.animator.GetCurrentAnimatorStateInfo(0);
        normalizedTime = info.normalizedTime;
        return info.IsName(stateName);
    }
    public bool CheckGround()
    {
        Ray ray = new Ray(npc.transform.position + Vector3.up * 1.5f, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.7f, npc.groundLayer))
        {
            return true;
        }
        return false;

    }
    public bool DetectPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(npc.transform.position, 7f, npc.playerMask);
        if (colliders.Length != 0)
        {
            return true;
        }
        return false;
    }
}
