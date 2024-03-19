using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum NPCmode
{
    STAY,
    FOLLOW
}

public class NPCController : BaseController,IStateMachineOwner
{

    public NPCmode curMode = NPCmode.STAY;
    public LayerMask playerMask;
    public LayerMask groundLayer;

    [HideInInspector]
    public StateMachine stateMachine;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public bool comboInCD = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        stepAS = transform.GetChild(0).GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        HPbar = transform.GetChild(1).GetChild(0);
    }
    void Start()
    {
        baseData = (BaseHumanData)ScriptableObject.CreateInstance("BaseHumanData");
        baseData.maxHP = 100;
        baseData.curHP = 100;
        stateMachine = new StateMachine();
        stateMachine.Init(this);
        stateMachine.ChangeState(new NPCIdleState());
    }
    private void OnEnable()
    {
        baseData.curHP = 100;
        transform.GetComponent<CapsuleCollider>().enabled = true;
        GameUIManager.instance.UpdateAllHpBar();
        isAlive = true;
        if (stateMachine!=null)
        {
            stateMachine.ChangeState(new NPCIdleState());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.curState == GameState.OnGame)
        {
            curMode = GameManager.instance.npcmode;
            animator.SetBool("Live", isAlive);
            baseData.position[0] = transform.position.x;
            baseData.position[1] = transform.position.y;
            baseData.position[2] = transform.position.z;
            if (CheckDead() && isAlive)
            {
                isAlive = false;
                
                stateMachine.ChangeState(new NPCDieState());
            }
        }
    }


    public void AnimationOver(int attackCount)
    {
        //if (attackComboCount == attackCount)
        //{
        //    ResetAutoAttack();
        //}
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime>=1)
        {
            ResetAutoAttack();
        }
    }
    public void LastAttack()
    {
        comboInCD = true;
        Invoke("ResetAutoAttack",0.5f);
    }

    public void ResetAutoAttack()
    {
        comboInCD = false;
        attackComboCount = 0;
        stateMachine.ChangeState(new NPCIdleState());
    }

    public void RegisterData(BaseHumanData data)
    {

        baseData = data;
        transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);

        GameUIManager.instance.UpdateAllHpBar();
    }
    public override void StartHit(int weaponIndex)
    {
        base.StartHit(weaponIndex);
        audioSource.clip = LoadAB.instance.uniqueClipsAB.LoadAsset<AudioClip>("Whoosh_npc");
        audioSource.Play();
    }

    public void BackToIdle()
    {
        canSwitch = true;

        stateMachine.ChangeState(new NPCIdleState());
        
    }
    public void StepSound()
    {
        AudioClip[] stepSounds = LoadAB.instance.stepClipsAB.LoadAllAssets<AudioClip>();
        int i = Random.Range(0, stepSounds.Length);
        stepAS.clip = stepSounds[i];
        stepAS.Play();
    }
    public void DestroyNPC()
    {
        GameObject newEF = Instantiate(GameManager.instance.npcDieEF);
        newEF.transform.position = transform.GetChild(0).position;
        newEF.transform.GetComponent<ParticleSystem>().Play();
        NPCGenerator.instance.ReturnObjectToPool(transform.gameObject);


    }

}
