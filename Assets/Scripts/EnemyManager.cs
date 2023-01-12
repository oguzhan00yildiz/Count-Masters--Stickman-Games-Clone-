using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro Countertxt;
    [SerializeField] private GameObject stickman;
    [Range(0f,20f)] [SerializeField] private float DistanceFactor, Radius;

    [SerializeField] private float enemtoplayerdistance=1.5f; 
    [SerializeField] private float enemyfighttime = 3f; 
    public Transform enemy;
    public bool attack;
    

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
       if (attack && transform.childCount > 1)
       {
        
         var EnemyPos =new Vector3(enemy.position.x,transform.position.y,enemy.position.z);
         var EnemyDirection = enemy.position - transform.position;

         for (int i = 0; i < transform.childCount; i++)
         {
            transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(i).rotation, Quaternion.LookRotation(EnemyDirection,Vector3.up),
            Time.deltaTime*enemyfighttime);
            
            var distance = enemy.GetChild(1).position - transform.GetChild(i).position;

            if (distance.magnitude < enemtoplayerdistance)
            {
                transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position,
                enemy.GetChild(1).position, Time.deltaTime*2f);
            }
         }
       }
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

    public void AttackThem ( Transform enemyForce)
    {

        enemy = enemyForce;
        attack=true;
        for (int i = 0; i < transform.childCount; i++)
        {
            //düşman karakterin koşma animasyonunu alıştır.
        }
    }
}
