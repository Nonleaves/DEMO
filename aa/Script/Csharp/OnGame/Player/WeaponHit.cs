using LuaInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour
{

    public string targetTag;
    public AttackData curAttackData;
    public List<Collider> enemyList = new List<Collider>();
    bool isStartHit = false;


    public void StartHit(AttackData attackData)
    {
        isStartHit=true;
        curAttackData = attackData;
    }

    public void StopHit()
    {
        isStartHit = false;
        enemyList.Clear();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other!=null&&other.tag==targetTag&&!enemyList.Contains(other)&& isStartHit)
        {
            enemyList.Add(other);
            // TODO: 传递伤害数据
            other.transform.GetComponent<BaseController>().baseData.curHP -= curAttackData.damage;

            //调用lua虚拟机
            LuaState lua = GameUIManager.instance.lua;
            LuaFunction luaFunc = GameUIManager.instance.luaFunc;
            luaFunc = lua.GetFunction("GameUI.getHit");
            luaFunc.Call();
            luaFunc.Dispose();
            luaFunc = null;


            other.transform.GetComponent<BaseController>().Hit();
            if (other.transform.GetComponent<BaseController>().isAlive)
            {
                if (other.tag=="Player")
                {
                    other.transform.GetComponent<PlayerController>().GetHit();
                }
                else
                {
                    
                    CameraComtroller.instance.isShake = true;
                    other.transform.GetComponent<NPCController>().GetHit();
                }
                
                
            }
            
        }
    }
}
