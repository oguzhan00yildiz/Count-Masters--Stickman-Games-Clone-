using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StickmanManager : MonoBehaviour
{

    Collider Scollider;
    public bool isBossAttacking;
    public Animator blueanim;
    

    public static StickmanManager stickmanManagerInstance;
    void Start()
    {
        Scollider = GetComponent<Collider>();
        stickmanManagerInstance = this;
        blueanim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("ramp"))
        {
            
            transform.DOJump(transform.position, 25f, 1, 1f).SetEase(Ease.Flash).OnComplete(PlayerManager.PlayerManagerInstance.FormatStickman);

        }

        if (other.CompareTag("red") && other.transform.parent.childCount > 0)
        {
             Scollider.enabled = false;
             Destroy(gameObject);
            
        }
        
        if (other.CompareTag("bosshealth"))
        {
            isBossAttacking = true;

            BossManager.bossManagerInstance.BossGetDamage();
            
            
            
            Destroy(gameObject);
        }

    
          
        
    }

    void Update()
    {
        if ( SceneManager.SceneManagerInstance.GameStarted & BossManager.bossManagerInstance.isbossdead== false)
        {
            blueanim.SetBool("PlayerMoving",true);
        }
        else 
        {
            blueanim.SetBool("PlayerMoving",false);
        }

        
    }


    
}
