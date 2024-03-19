using LuaInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerController : BaseController, IStateMachineOwner
{
    [HideInInspector]
    public Rigidbody rb;

    public float rotateSpeed;
    public float jumpPower;
    public float jumpMoveSpeed;
    public float transitionSpeed;

    public LayerMask groundLayer;


    [HideInInspector]
    public StateMachine stateMachine;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        stepAS = transform.GetChild(0).GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        baseData = (BaseHumanData)ScriptableObject.CreateInstance("BaseHumanData");
        baseData.maxHP = 100;
        baseData.curHP = 100;
        //³õÊ¼»¯×´Ì¬»ú
        stateMachine = new StateMachine();
        stateMachine.Init(this);
        stateMachine.ChangeState(new PlayerIdleState());
        //PlayerChangeState(PlayerStateEnum.Idle);
        GameUIManager.instance.UpdateAllHpBar();

    }
    private void OnEnable()
    {
        baseData.curHP = 100;
        
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.curState==GameState.OnGame)
        {
            baseData.position[0] = transform.position.x;
            baseData.position[1] = transform.position.y;
            baseData.position[2] = transform.position.z;
            animator.SetBool("Live", isAlive);
            if (CheckDead()&&isAlive)
            {
                isAlive = false;
                stateMachine.ChangeState(new PlayerDieState());
            }
        }
    }

    #region animation event
    public void AnimationOver(int attackCount)
    {
        if (attackComboCount == attackCount)
        {
            ResetAttack();
        }
    }
    public void LastAttack()
    {
        ResetAttack();
        Invoke("CanSwitch", 0.4f);
    }

    public void ResetAttack()
    {

        attackComboCount = 0;
        stateMachine.ChangeState(new PlayerIdleState());
    }
    public void StepSound()
    {
        AudioClip[] stepSounds = LoadAB.instance.stepClipsAB.LoadAllAssets<AudioClip>();
        int i = Random.Range(0, stepSounds.Length);
        stepAS.clip = stepSounds[i];
        stepAS.Play();
    }
    public override void StartHit(int weaponIndex)
    {
        base.StartHit(weaponIndex);
        if (attackComboCount>0)
        {
            attackEFS[attackComboCount - 1].Play();
            audioSource.clip = LoadAB.instance.uniqueClipsAB.LoadAsset<AudioClip>("Whoosh_player"); ;
            audioSource.Play();

        }

    }


    #endregion

    public void BackToIdle()
    {
        canSwitch = true;
        stateMachine.ChangeState(new PlayerIdleState());
    }

    public void RegisterData(BaseHumanData data)
    {
        
        baseData = data;
        Vector3 newPos = new Vector3(data.position[0], data.position[1], data.position[2]);
        transform.position = newPos;
        GameUIManager.instance.UpdateAllHpBar();
    }


}
