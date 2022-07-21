using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
   // public static CinemachineSwitcher instance;
    public bool playerCam;

    private Animator animator;
    [SerializeField]private CinemachineVirtualCamera vcam;//playercam
    [SerializeField]private CinemachineVirtualCamera vcam2;//wallcam



    private void Start()
    {
      //  if (instance == null)
        //    instance = this;
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    public void SwitchState()
    {
        if (playerCam)
        {
            animator.Play("WallCamera");
        }
        else
        {
            animator.Play("PlayerCamera");
        }
        playerCam = !playerCam;
    }

    public void SwitchPriority()
    {
        if (playerCam)
        {
            vcam.Priority = 0;
            vcam2.Priority = 1;
        }
        else
        {
            vcam.Priority = 1;
            vcam2.Priority = 0;
        }
        playerCam = !playerCam;
    }
}
