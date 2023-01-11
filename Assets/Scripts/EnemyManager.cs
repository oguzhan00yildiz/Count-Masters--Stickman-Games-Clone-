using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro Countertxt;
    [SerializeField] private GameObject stickman;
    [Range(0f,20f)] [SerializeField] private float DistanceFactor, Radius;

    

    void Start()
    {
        for (int i = 0; i < Random.Range(20,120); i++)
        {
            Instantiate(stickman,transform.position,new Quaternion(0f,180f,0f,1f),transform);
        }

        Countertxt.text =(transform.childCount -1).ToString();
        FormatStickman();
    }

    
    void Update()
    {
       
    }

     private void FormatStickman()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var x =DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i*Radius);
            var z =DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i*Radius);
            var NewPos = new Vector3(x,0.28f,z);

            transform.transform.GetChild(i).localPosition = NewPos;
        }
    }
}
