using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : MonoBehaviour
{
    public static NPCGenerator instance;


    [Header("∂‘œÛ≥ÿ")]
    public GameObject NPC_prefab;
    public int poolCapacity = 3;
    public int poolSize =0;

    private List<GameObject> objPool = new List<GameObject>();

    [Header("ºÏ≤‚≤„º∂")]
    public LayerMask playerMask;
    public LayerMask groundLayer;

    float nTime = 0f;
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        for (int i=0;i< poolCapacity; i++)
        {
            GameObject obj = Instantiate(NPC_prefab,transform);
            poolSize++;
            obj.transform.position = GetGeneratePos();
            objPool.Add(obj);
        }
    }
    private void Update()
    {
        
        if (poolSize<poolCapacity)
        {
            nTime += Time.deltaTime;
            if (nTime>=3f)
            {
                GameObject backLive = GetObjectFromPool();
                nTime = 0;
            }
        }
    }

    private Vector3 GetGeneratePos()
    {
        while (true)
        {
            float x = Random.Range(MapBoarder.XNEG, MapBoarder.XPOS);
            float z = Random.Range(MapBoarder.ZNEG, MapBoarder.ZPOS);
            float h = 10f;
            Ray ray = new Ray(new Vector3(x, h, z), Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 15, groundLayer))
            {
                return new Vector3(hit.point.x,5f,hit.point.z);
            }
        }

    }

    public GameObject GetObjectFromPool()
    {
        foreach (GameObject obj in objPool)
        {
            if (!obj.activeInHierarchy)
            {
                poolSize++;
                Vector3 pos = GetGeneratePos();
                obj.SetActive(true);
                obj.transform.position = pos;
                return obj;
            }
        }

        GameObject newObj = Instantiate(NPC_prefab);
        objPool.Add(newObj);
        return newObj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        poolSize--;
        obj.SetActive(false);
    }
}
