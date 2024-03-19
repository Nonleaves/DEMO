using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraComtroller : MonoManager
{
    public static CameraComtroller instance;


    private Transform player;
    private Vector3 offsetPosition; 
    private bool mouse1Down;


    private float currentShakeValue;
    public float shakeValue = 0.1f;

    public float distance;
    public float YdistanceOffset;
    public float scrollSpeed = 10;//视野远近变化的速度
    public float RotateSpeed = 2;//相机镜头转向的速度
    public bool isShake=false;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(player.position+Vector3.up* YdistanceOffset);
        offsetPosition = transform.position - player.position;
    }

    void Update()
    {
        if (GameManager.instance.curState == GameState.OnGame)
        {
            transform.position = offsetPosition + player.position;
            Rotate();
            Scroll();


            ShakeCam();
            
        }

    }

    public void ShakeCam()
    {
        if (isShake)
        {
            //先左右震动再上下震动
            transform.position = new Vector3(transform.position.x+(Random.Range(-currentShakeValue, currentShakeValue)), transform.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x, transform.position.y+(Random.Range(-currentShakeValue, currentShakeValue)), transform.position.z);

            currentShakeValue /= 1.02f;//震动数值减少
            if (currentShakeValue <= 0.05f)
            {
                isShake = false;
                currentShakeValue = shakeValue;
            }
        }
        else
        {
            currentShakeValue = shakeValue;
        }
    }

    void Scroll()//鼠标滑轮拉近拉远视野
    {
        distance = offsetPosition.magnitude;
        distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        distance = Mathf.Clamp(distance, 3, 10);//限制镜头缩放最远以及最近距离
        offsetPosition = offsetPosition.normalized * distance;    
    }

    void Rotate()//按下鼠标右键后移动鼠标更新视角
    {
        transform.RotateAround(player.position, player.up, RotateSpeed * Input.GetAxis("Mouse X"));

        Vector3 originalPos = transform.position;
        Quaternion originalRotation = transform.rotation;
        transform.RotateAround(player.position, transform.right, -RotateSpeed * Input.GetAxis("Mouse Y"));
                                                                                                              

        float x = transform.eulerAngles.x;

        if (x < 10 || x > 80)//限制最大、最小俯仰角度
        {
            transform.position = originalPos;
            transform.rotation = originalRotation;
        }
        //}
        offsetPosition = transform.position - player.position;
    }
}
