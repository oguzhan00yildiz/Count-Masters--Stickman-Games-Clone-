using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    public Transform player;
    private int numberofstickman;
    [SerializeField] private GameObject stickman;
    [Range(0f,20f)] [SerializeField] private float DistanceFactor, Radius;

    void Start()
    {
        player = transform;
        numberofstickman = transform.childCount;
        MakeStickman(30);

        
    }

    
    void Update()
    {
        
    }
    private void FormatStickman()
    {
        for (int i = 0; i < player.childCount; i++)
        {
            var x =DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i*Radius);
            var z =DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i*Radius);
            var NewPos = new Vector3(x,0.28f,z);

            player.transform.GetChild(i).DOLocalMove(NewPos, 1f).SetEase(Ease.OutBack);
        }
    }

    private void MakeStickman(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(stickman,transform.position,Quaternion.identity,transform);

        }

        numberofstickman = transform.childCount-1;

        FormatStickman();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("gate"))
        {
            other.transform.parent.GetChild(0).GetComponent<BoxCollider>().enabled=false;
            other.transform.parent.GetChild(1).GetComponent<BoxCollider>().enabled=false;

            Debug.Log(other);
            var gateManager = other.GetComponent<GateManager>();

            if (gateManager.multiply)
        {
            MakeStickman(numberofstickman* gateManager.randomnumber);
        }
        else
        {
            MakeStickman(numberofstickman + gateManager.randomnumber);
        }

        }
        
    }
}
