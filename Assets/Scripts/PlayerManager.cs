using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [Header ("Player Settings")]
    
    public float playerSpeed;
    public float roadSpeed;
    [Range(0f,20f)] [SerializeField] private float DistanceFactor, Radius;
    [SerializeField] private float fighttime=1f;
    [SerializeField] private float playercollidespeed=1f;
    [SerializeField] private bool attack=false;
    [SerializeField] private bool attackboss=false;
    [SerializeField] public bool bosszone;
    [Header ("---------------------------------------------------------------------------------------------------------------------------------------")]

    [Header ("Player Assignments")]
    public Transform player;
    private Animator anim;
    public int numberofstickman;
    [SerializeField]private GameObject PopUpTxt;
    [SerializeField]private Rigidbody rb;
    [SerializeField] private TextMeshPro Countertxt;
    [SerializeField] private GameObject stickman;
    [Header ("---------------------------------------------------------------------------------------------------------------------------------------")]
    
    [Header ("Enemy Relations")]

    [SerializeField] private Transform enemy;
    [SerializeField] private Transform boss;
    [SerializeField] private float DistancetoEnemy;
    [SerializeField] private float DistancetoBoss;
    [SerializeField] public int numbersofenemystickman;
   
    [Header ("---------------------------------------------------------------------------------------------------------------------------------------")]
    
    [Header ("Other Properties")]
    
    public bool moveByTouch;
    public bool gameState;    
    public static PlayerManager PlayerManagerInstance;
    public SceneManager sceneManager;
    public Camera camera1;
    private Vector3 mouseStartPos, playerStartPos;
    public Transform road;
    private TextMeshPro PopUpCloneText;
    
    
    

    void Start()
    {
        player = transform;
        numberofstickman = transform.childCount-1;

        Countertxt.text = numberofstickman.ToString();
        
        PlayerManagerInstance = this;
        DOTween.SetTweensCapacity(2000, 100);
        gameState = true;
        camera1 = Camera.main;

        
        
    }

    
    void Update()
    {

        if (attackboss ==false && attack==false)
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).transform.GetComponent<Rigidbody>().isKinematic=true;
            }
        }
        else
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).transform.GetComponent<Rigidbody>().isKinematic=false;
            }
        }

        numberofstickman = transform.childCount-1;
        Countertxt.text = numberofstickman.ToString();
        
        if (attack)
        {
            
            gameState = false;
            

            roadSpeed = -40;

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
                roadSpeed = -75f;
                
    
                FormatStickman();
                
                
            
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).rotation = Quaternion.identity;
                }
                    anim=enemy.transform.GetComponent<Animator>();
                    anim.SetBool("EnemyZoneanim",true);
                    
                    Destroy(enemy.gameObject,0.5f);
                    //enemy.gameObject.SetActive(false);
                
            
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
            
            
            
        }
        if (transform.childCount == 1)
        {
            gameState = false;
            transform.GetChild(0).transform.gameObject.SetActive(false);
        }
        
       
        if (gameState&&sceneManager.GameStarted == true)
        {
          road.Translate(road.forward * Time.deltaTime * roadSpeed);
        }  
//-----------------------------------------------------------------------------------------------------------------------------------------------------------
        if(bosszone)
        {
            gameState = false;
            roadSpeed = -5;

            var bossDirection = new Vector3(boss.position .x, transform.position.y,boss.position.z)- transform.position;

            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).rotation = Quaternion.Lerp(transform.GetChild(i).rotation ,Quaternion.LookRotation(bossDirection, Vector3.up),Time.deltaTime *fighttime);
            }
        }


        if (attackboss)
            {
                if(boss.childCount > 0)
                {for (int i = 1; i < transform.childCount; i++)
                {
                    var Distance = boss.GetChild(0).position - transform.GetChild(i).position;

                    if (Distance.magnitude < DistancetoBoss)
                    {
                        

                        transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position, new Vector3(boss.GetChild(0).position.x,transform.GetChild(i).position.y,
                        boss.GetChild(0).position.z),Time.deltaTime *(playercollidespeed/5));
                    }
                }
                }
                else
                {
                    attackboss=false;
                }
            }
            


    }



    void MoveThePlayer()
    {
        if(Input.GetMouseButtonDown(0)&&gameState)
        {
            moveByTouch = true;

            var plane = new Plane(Vector3.up, 0f);

            var ray = camera1.ScreenPointToRay(Input.mousePosition);

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
            var ray = camera1.ScreenPointToRay(Input.mousePosition);

            if(plane.Raycast(ray, out var distance))
            {
                var mousePos = ray.GetPoint(distance + 1f);

                var move = mousePos - mouseStartPos;
                var control = playerStartPos + move;

                    if(1 <= numberofstickman && numberofstickman < 10)
                    control.x = Mathf.Clamp(control.x , -24.5f, 60.0f);
                    else if(10 <= numberofstickman && numberofstickman < 20)
                    control.x = Mathf.Clamp(control.x , -15, 51);
                    else if(20 <= numberofstickman && numberofstickman < 30)
                    control.x = Mathf.Clamp(control.x , -11, 47);
                    else if(30 <= numberofstickman && numberofstickman < 40)
                    control.x = Mathf.Clamp(control.x , -7, 43);
                    else if(40 <= numberofstickman && numberofstickman < 50)
                    control.x = Mathf.Clamp(control.x , -6, 42);
                    else if(50 <= numberofstickman && numberofstickman < 60)
                    control.x = Mathf.Clamp(control.x , 0, 36);
                    else
                    {
                      control.x = Mathf.Clamp(control.x , 0, 36);  
                    }



                    

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
            if (player.transform.GetChild(i))
            {
                player.transform.GetChild(i).DOLocalMove(NewPos, 2f).SetEase(Ease.OutBack);
            
            }
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
                PopUpTxtHandler(gateManager.randomnumber.ToString()+"x");
            }
            else
            {
                MakeStickman((numberofstickman + gateManager.randomnumber)-1);
                PopUpTxtHandler("+"+(gateManager.randomnumber-1).ToString());
            }

        }

        if (other.CompareTag("enemy"))
        {
            enemy = other.transform;
            attack=true;
            EnemyManager.enemyManagerInstance.attack = true;
            roadSpeed = -40f;
            
            
            other.transform.GetChild(1).GetComponent<EnemyManager>().AttackThem(transform);

            StartCoroutine(UpdatetheNumbersOfPlayers());
        }

        

        if (other.CompareTag("bosszone"))
        {
            boss = other.transform;
            roadSpeed = -5f;
            bosszone=true;




            
        }
        

        if (other.CompareTag("bosszone"))
        {
            attackboss=true;
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

    public void PopUpTxtHandler(string generatedNumber)
    {
        GameObject PopUpClone = Instantiate(PopUpTxt, transform.position, Quaternion.identity);

        PopUpCloneText = PopUpClone.transform.GetChild(0).GetComponent<TextMeshPro>();
        PopUpCloneText.text = generatedNumber;

        Destroy(PopUpClone,3f);
    }
}
