using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;
    private float startIntesity;
    private float shakeTimerTotal;
    public CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    
    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    }

    public void ShakeCamera(float intesity, float time)
    { 

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intesity;
        shakeTimer = time;
        startIntesity = intesity;
        shakeTimerTotal = time;
    }

    private void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
           
              

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startIntesity, 0, (1 - (shakeTimer / shakeTimerTotal)));
                
            
        }
    }

}
