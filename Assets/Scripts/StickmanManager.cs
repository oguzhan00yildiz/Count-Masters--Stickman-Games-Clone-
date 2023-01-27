using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StickmanManager : MonoBehaviour
{

    Collider Scollider;
    public bool isBossAttacking;
    public Animator blueanim;

    private int rnd;
    [SerializeField] private GameObject smokeeffect;
    [SerializeField] private GameObject bloodsplashblue;
    

    public static StickmanManager stickmanManagerInstance;
    void Start()
    {
        Scollider = GetComponent<Collider>();
        stickmanManagerInstance = this;
        blueanim = GetComponent<Animator>();

        rnd = Random.Range(1,3);
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
             Instantiate(bloodsplashblue,transform.position + new Vector3(0,5f,0),Quaternion.identity);
            
        }
        
        if (other.CompareTag("bosshealth"))
        {
            isBossAttacking = true;

            BossManager.bossManagerInstance.BossGetDamage();
            
            Debug.Log(rnd);
            if (rnd == 1 )
            {
            Vector3 pos = transform.position;
            pos.y = pos.y +5f;

                Instantiate(smokeeffect,pos,Quaternion.identity);
                

            }
            
            transform.GetChild(0).transform.gameObject.SetActive(false);
            Destroy(gameObject,0.3f);
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
