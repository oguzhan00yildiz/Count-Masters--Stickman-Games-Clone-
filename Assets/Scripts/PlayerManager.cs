using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public Transform player;
    private int numberofstickman;
    [SerializeField] private TextMeshPro Countertxt;
    [SerializeField] private GameObject stickman;
    [Range(0f,20f)] [SerializeField] private float DistanceFactor, Radius;

    [SerializeField] private Transform enemy;
    [SerializeField] private bool attack;
    [SerializeField] private float DistancetoEnemy;
    [SerializeField] private float fighttime=1f;
    public PlayerMovement PlayerMovementscript;
    
    public static PlayerManager PlayerManagerInstance;




    void Start()
    {
        player = transform;
        numberofstickman = transform.childCount-1;

        Countertxt.text = numberofstickman.ToString();
        
        PlayerManagerInstance = this;
        DOTween.SetTweensCapacity(2000, 100);
        
    }

    
    void Update()
    {
        if (attack)
        {
            //PlayerMovementscript.Playercanmove = false;
            

            var enemyDirection = new Vector3(enemy.position .x, transform.position.y,enemy.position.z)- transform.position;

            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).rotation = Quaternion.Lerp(transform.GetChild(i).rotation ,Quaternion.LookRotation(enemyDirection, Vector3.up),Time.deltaTime *fighttime);
            }

            if (enemy.GetChild(1).childCount > 1)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    var Distance = enemy.GetChild(1).GetChild(0).position - transform.GetChild(i).position;

                    if (Distance.magnitude < DistancetoEnemy)
                    {
                        transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position, new Vector3(enemy.GetChild(1).GetChild(0).position.x,transform.GetChild(i).position.y,
                        enemy.GetChild(1).GetChild(0).position.z),Time.deltaTime *3f);
                    }
                }
            }
            
        }
        else
        {
            //PlayerMovementscript.PlayerSpeed=PlayerMovementscript.PlayerSpeed*4;
            PlayerMovementscript.Playercanmove = true;
        }
    }




    public void FormatStickman()
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
        Countertxt.text = numberofstickman.ToString();

        FormatStickman();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("gate"))
        {
            other.transform.parent.GetChild(0).GetComponent<BoxCollider>().enabled=false;
            other.transform.parent.GetChild(1).GetComponent<BoxCollider>().enabled=false;

            var gateManager = other.GetComponent<GateManager>();

            if (gateManager.multiply)
        {
            MakeStickman(numberofstickman* (gateManager.randomnumber-1));
        }
        else
        {
            MakeStickman((numberofstickman + gateManager.randomnumber)-2);
        }

        }

        if (other.CompareTag("enemy"))
        {
            enemy = other.transform;
            attack=true;
            PlayerMovementscript.PlayerSpeed= -20;
            other.transform.GetChild(1).GetComponent<EnemyManager>().AttackThem(transform);
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            
            PlayerMovementscript.PlayerSpeed= -50;
            
        }
    }
}
