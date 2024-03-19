using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T:SingletonMono<T>
{
    public static T instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = (T)this;
        }
    }
}
