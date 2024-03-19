using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ConfigData/BaseHumanDataConfig")]
[Serializable]
public class BaseHumanData : ScriptableObject
{
    public int maxHP;
    public int curHP;

    public float[] position = new float[3];

}

