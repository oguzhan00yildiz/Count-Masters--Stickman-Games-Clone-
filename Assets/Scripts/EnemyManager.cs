using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    [Header ("Enemy Settings")]
    [Range(0f,20f)] [SerializeField] private float DistanceFactor;
    [Range(0f,20f)] [SerializeField] private float Radius;
    [Header ("------------------------------------------------------------------------------------------------------------------------------------")]

    [Header ("Player Relations")]
    [SerializeField] private float enemtoplayerdistance=1.5f; 
    [SerializeField] private float enemyfighttime = 3f;
    [SerializeField] private float enemycollidespeed =1f; 
    [SerializeField] private GameObject stickman; 
    [Header ("------------------------------------------------------------------------------------------------------------------------------------")]

    [Header ("Other Properties")]
    public TextMeshPro Countertxt;
    
    public Transform enemy;
    public bool attack;

    public static EnemyManager enemyManagerInstance;
    
    

    void Start()
    {
        for (int i = 0; i < Random.Range(20,35); i++)
        {
            Instantiate(stickman,transform.position,new Quaternion(0f,180f,0f,1f),transform);
        }

        Countertxt.text =(transform.childCount ).ToString();
        FormatStickman();
        enemyManagerInstance=this;
    }

    
    void Update()
    {
        Countertxt.text =(transform.childCount ).ToString();
       if (attack && transform.childCount > 1)
       {
        
         var EnemyPos =new Vector3(enemy.position.x,transform.position.y,enemy.position.z);
         var EnemyDirection = enemy.position - transform.position;

         for (int i = 0; i < transform.childCount; i++)
         {
            transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(i).rotation, Quaternion.LookRotation(EnemyDirection,Vector3.up),
            Time.deltaTime*enemyfighttime);
            if (enemy.childCount > 1)
            {
               var distance = enemy.GetChild(1).position - transform.GetChild(i).position;

            if (distance.magnitude < enemtoplayerdistance)
            {
                transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position,
                enemy.GetChild(1).position, Time.deltaTime*enemycollidespeed);
            } 
            }
            
         }
       }

       if (transform.childCount == 0)
       {
            attack = false;
       }
    }

     private void FormatStickman()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var x =DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i*Radius);
            var z =DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i*Radius);
            var NewPos = new Vector3(x,0.28f,z);
            if ( transform.GetChild(i))
            {
                transform.GetChild(i).localPosition = NewPos;
            }
                
        }
    }

    public void AttackThem ( Transform enemyForce)
    {

        enemy = enemyForce;
        attack=true;
        
    }

    public void StopAttack()
    {
       PlayerManager.PlayerManagerInstance.gameState =attack = false;
        

    }
}
