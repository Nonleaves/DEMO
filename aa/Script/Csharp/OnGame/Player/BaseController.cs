using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BaseController : MonoBehaviour
{

    [HideInInspector]
    public bool canSwitch = true;

    public int attackComboCount = 0;

    [HideInInspector]
    public bool isAlive = true;

    [HideInInspector]
    public Animator animator;

    public AudioSource audioSource;
    public AudioSource stepAS;


    [Header("武器列表")]
    public WeaponHit[] weaponHits;
    [Header("攻击数据")]
    public AttackData[] attackDatas;
    [Header("人物基础属性")]
    public BaseHumanData baseData;
    [Header("血条")]
    public Transform HPbar;
    [Header("攻击特效")]
    public ParticleSystem[] attackEFS;
    public void PlayAnimation(string animationName, float crossTime=0.25f)
    {
        animator.CrossFadeInFixedTime(animationName,crossTime);

    }

    public virtual void GetHit()
    {
        if (isAlive)
        {
            attackComboCount = 0;
            animator.SetTrigger("GetHit");
        }
       
    }
    public bool CheckDead()
    {
        return baseData.curHP <=0;
    }


    #region animation event

    public void CanNotSwitch()
    {
        canSwitch = false;
        
    }

    public void CanSwitch()
    {
        canSwitch = true;
    }


    public void Hit()
    {
        HitEnemy();
    }

    public virtual void StartHit(int weaponIndex)
    {
        if (attackComboCount>0)
        {
            weaponHits[weaponIndex].StartHit(attackDatas[attackComboCount - 1]);
        }
        
    }

    public void StopHit(int weaponIndex)
    {
        weaponHits[weaponIndex].StopHit();
    }
    public bool CheckAnimationState(string stateName, out float normalizedTime)
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        normalizedTime = info.normalizedTime;
        return info.IsName(stateName);
    }

    #endregion


    public void HitEnemy()
    {
        audioSource.clip = LoadAB.instance.uniqueClipsAB.LoadAsset<AudioClip>("Stab_player");
        audioSource.Play();
    }

}
