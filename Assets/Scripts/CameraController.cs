using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Animator animator;
    private bool isMainCam = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SwitchCamera();
    }

    void SwitchCamera() 
    {
        if(SceneManager.SceneManagerInstance.fillamount > 0.99f)
        {
            animator.Play("BossCam");
        }
    }
}
