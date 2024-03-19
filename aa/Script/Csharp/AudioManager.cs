using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource audioSource;


    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void DoUIClick()
    {
        audioSource.clip = LoadAB.instance.uniqueClipsAB.LoadAsset<AudioClip>("UI_click"); 
        audioSource.Play();
    }

}
