using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="ConfigData/AttackConfig")]
[Serializable]
public class AttackData : ScriptableObject
{
    public string animationName;
    public int damage;



}
