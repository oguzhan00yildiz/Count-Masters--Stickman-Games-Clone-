using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public Transform player;
    private int numberofstickman;
    private int numbersofenemystickman;
    [SerializeField] private TextMeshPro Countertxt;
    [SerializeField] private GameObject stickman;
    [Range(0f,20f)] [SerializeField] private float DistanceFactor, Radius;

    [SerializeField] private Transform enemy;
    [SerializeField] private bool attack;
    [SerializeField] private float DistancetoEnemy;
    [SerializeField] private float fighttime=1f;
    [SerializeField] private float playercollidespeed=1f;
    public bool moveByTouch, gameState;
    public PlayerMovement PlayerMovementscript;
    
    public static PlayerManager PlayerManagerInstance;

    public SceneManager sceneManager;

    public Camera camera;
    private Vector3 mouseStartPos, playerStartPos;
    public float playerSpeed, roadSpeed;
    public Transform road;


    void Start()
    {
        player = transform;
        numberofstickman = transform.childCount-1;

        Countertxt.text = numberofstickman.ToString();
        
        PlayerManagerInstance = this;
        DOTween.SetTweensCapacity(2000, 100);
        gameState = true;
        camera = Camera.main;
    }

    
    void Update()
    {
        numberofstickman = transform.childCount-1;
        Countertxt.text = numberofstickman.ToString();
        

        //gameState=PlayerMovementscript.Playercanmove;
        if (attack)
        {
            //PlayerMovementscript.Playercanmove = false;
            gameState = false;
            PlayerMovementscript.PlayerSpeed= -20;

            roadSpeed = -20;

            var enemyDirection = new Vector3(enemy.position .x, transform.position.y,enemy.position.z)- transform.position;

            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).rotation = Quaternion.Lerp(transform.GetChild(i).rotation ,Quaternion.LookRotation(enemyDirection, Vector3.up),Time.deltaTime *fighttime);
            }

            if (enemy.GetChild(1).childCount > 1)
            {
                for (int i = 1; i < transform.childCount; i++)
                {
                    var Distance = enemy.GetChild(1).GetChild(0).position - transform.GetChild(i).position;

                    if (Distance.magnitude < DistancetoEnemy)
                    {
                        transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position, new Vector3(enemy.GetChild(1).GetChild(0).position.x,transform.GetChild(i).position.y,
                        enemy.GetChild(1).GetChild(0).position.z),Time.deltaTime *playercollidespeed);
                    }
                }
            }
            else
            {   
                attack = false;
                roadSpeed = -50f;
                PlayerMovementscript.PlayerSpeed= -50;
    
                FormatStickman();
                
            
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).rotation = Quaternion.identity;
                }
                
                
                    enemy.gameObject.SetActive(false);
                
            
            }

            if (transform.childCount==1)
            {
                enemy.transform.GetChild(1).GetComponent<EnemyManager>().StopAttack();
                gameObject.SetActive(false);
            }
            
        }
        else
        {
            
            MoveThePlayer();
            gameState=true;
            PlayerMovementscript.Playercanmove = true;
            
            
        }
        if (transform.childCount == 1)
        {
            gameState = false;
        }
        
       
        if (gameState&&sceneManager.GameStarted == true)
        {
          road.Translate(road.forward * Time.deltaTime * roadSpeed);
        }  
    }



    void MoveThePlayer()
    {
        if(Input.GetMouseButtonDown(0)&&gameState)
        {
            moveByTouch = true;

            var plane = new Plane(Vector3.up, 0f);

            var ray = camera.ScreenPointToRay(Input.mousePosition);

            if(plane.Raycast(ray, out var distance))
            {
                mouseStartPos = ray.GetPoint(distance+1f);
                playerStartPos = transform.position;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            moveByTouch = false;
        }

        if(moveByTouch)
        {
            var plane = new Plane(Vector3.up, 0f);
            var ray = camera.ScreenPointToRay(Input.mousePosition);

            if(plane.Raycast(ray, out var distance))
            {
                var mousePos = ray.GetPoint(distance + 1f);

                var move = mousePos - mouseStartPos;
                var control = playerStartPos + move;

                if(numberofstickman > 50)
                    control.x = Mathf.Clamp(control.x , -45.0f, 45.0f);
                else
                    control.x = Mathf.Clamp(control.x , -48.0f, 48.0f);

                transform.position = new Vector3(Mathf.Lerp(transform.position.x,control.x,Time.deltaTime * playerSpeed)
                    ,transform.position.y,transform.position.z);
            }
        }
    }


    public void FormatStickman()
    {
        for (int i = 1; i < player.childCount; i++)
        {
            var x =DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i*Radius);
            var z =DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i*Radius);
            var NewPos = new Vector3(x,0.28f,z);

            player.transform.GetChild(i).DOLocalMove(NewPos, 2f).SetEase(Ease.OutBack);
            Countertxt.text = numberofstickman.ToString();
        }
    }

    private void MakeStickman(int number)
    {
        for (int i = numberofstickman; i < number; i++)
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
            MakeStickman(numberofstickman* (gateManager.randomnumber));
        }
        else
        {
            MakeStickman((numberofstickman + gateManager.randomnumber)-1);
        }

        }

        if (other.CompareTag("enemy"))
        {
            enemy = other.transform;
            attack=true;
            roadSpeed = -20f;
            
            
            other.transform.GetChild(1).GetComponent<EnemyManager>().AttackThem(transform);

            StartCoroutine(UpdatetheNumbersOfPlayers());
        }
        
        
    }

    IEnumerator UpdatetheNumbersOfPlayers()
    {
        numbersofenemystickman =enemy.transform.GetChild(1).childCount -1;
        numberofstickman = transform.childCount-1;
        

        while (numbersofenemystickman >0 && numberofstickman > 0)
        {
            numbersofenemystickman --;
            numberofstickman--;

            enemy.transform.GetChild(1).GetComponent<EnemyManager>().Countertxt.text = numbersofenemystickman.ToString();
            Countertxt.text = numberofstickman.ToString();

            yield return null;
        }

        if (numbersofenemystickman ==0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).rotation = Quaternion.identity;
            }
        }
    }
}
