using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BossAttack()
    {
        

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("blue"))
        {
            
        }
    }
}
