using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform player;
    private int numberofstickman;
    [SerializeField] private GameObject stickman;

    void Start()
    {
        player = transform;
        numberofstickman = transform.childCount;
    }

    
    void Update()
    {
        
    }

    private void MakeStickman(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(stickman,transform.position,Quaternion.identity,transform);

        }

        numberofstickman = transform.childCount-1;

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
