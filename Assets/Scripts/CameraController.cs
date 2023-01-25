using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Animator animator;
    private bool isMainCam = true;


    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        SwitchCamera();
    }

    void SwitchCamera() 
    {
        if (PlayerManager.PlayerManagerInstance.bosszone==true)
        {
            animator.Play("BossCam");
        }
    }
}
