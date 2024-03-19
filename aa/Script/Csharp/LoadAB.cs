using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadAB : MonoBehaviour
{
    string stepClipsAsset = "stepsound.zrh";
    string uniqueClipsAsset = "uniquesound.zrh";

    public AssetBundle stepClipsAB;
    public AssetBundle uniqueClipsAB;

    public static LoadAB instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        stepClipsAB = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, stepClipsAsset));
        uniqueClipsAB = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, uniqueClipsAsset));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
