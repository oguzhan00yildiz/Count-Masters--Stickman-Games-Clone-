using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StickmanManager : MonoBehaviour
{

    Collider Scollider;
     void Start()
    {
        Scollider = GetComponent<Collider>();
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

        
    }
    
}
