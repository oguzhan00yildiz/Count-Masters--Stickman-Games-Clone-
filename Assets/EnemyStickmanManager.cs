using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStickmanManager : MonoBehaviour
{
    int enemycollidenum=0;
    Collider enemycollider;
    void Start()
    {
        enemycollider = GetComponent<Collider>();
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
}
