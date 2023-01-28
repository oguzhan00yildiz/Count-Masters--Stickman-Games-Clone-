using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStickmanManager : MonoBehaviour
{
    int enemycollidenum=0;
    Collider enemycollider;
    private Animator Redanim;

    void Start()
    {
        enemycollider = GetComponent<Collider>();
        Redanim = GetComponent<Animator>();
    }

    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("blue"))
        {
            enemycollider.enabled = false;
            enemycollidenum++;
            Destroy(gameObject);
        }

    }

    private void Update() 
    {
        if ( EnemyManager.enemyManagerInstance.attack )
        {
        
             Redanim.SetBool("RedRun",true);

             

        }
        else
        {
            Redanim.SetBool("RedRun",false);
        }
        
       
        
    }
}
