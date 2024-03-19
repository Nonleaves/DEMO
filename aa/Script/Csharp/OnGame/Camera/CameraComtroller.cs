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
    public float scrollSpeed = 10;//��ҰԶ���仯���ٶ�
    public float RotateSpeed = 2;//�����ͷת����ٶ�
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
            //����������������
            transform.position = new Vector3(transform.position.x+(Random.Range(-currentShakeValue, currentShakeValue)), transform.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x, transform.position.y+(Random.Range(-currentShakeValue, currentShakeValue)), transform.position.z);

            currentShakeValue /= 1.02f;//����ֵ����
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

    void Scroll()//��껬��������Զ��Ұ
    {
        distance = offsetPosition.magnitude;
        distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        distance = Mathf.Clamp(distance, 3, 10);//���ƾ�ͷ������Զ�Լ��������
        offsetPosition = offsetPosition.normalized * distance;    
    }

    void Rotate()//��������Ҽ����ƶ��������ӽ�
    {
        transform.RotateAround(player.position, player.up, RotateSpeed * Input.GetAxis("Mouse X"));

        Vector3 originalPos = transform.position;
        Quaternion originalRotation = transform.rotation;
        transform.RotateAround(player.position, transform.right, -RotateSpeed * Input.GetAxis("Mouse Y"));
                                                                                                              

        float x = transform.eulerAngles.x;

        if (x < 10 || x > 80)//���������С�����Ƕ�
        {
            transform.position = originalPos;
            transform.rotation = originalRotation;
        }
        //}
        offsetPosition = transform.position - player.position;
    }
}
