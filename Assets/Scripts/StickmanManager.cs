using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StickmanManager : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("ramp"))
        {
            Debug.Log("collided with ramp");
            transform.DOJump(transform.position, 25f, 1, 1f).SetEase(Ease.Flash).OnComplete(PlayerManager.PlayerManagerInstance.FormatStickman);

        }
    }
    
}
