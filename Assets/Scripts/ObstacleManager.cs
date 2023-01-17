using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{

    [SerializeField]private GameObject Obstacle;
    [SerializeField]private Rigidbody ObstacleRB;
    [SerializeField]private Transform ObstacleTransform;
    // Start is called before the first frame update
    void Start()
    {
        Obstacle = gameObject;
        ObstacleRB = Obstacle.GetComponent<Rigidbody>();
        ObstacleTransform = Obstacle.GetComponent<Transform>();   
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            
            Destroy(col.gameObject);
        }
        StartCoroutine(ExecuteAfterTime(1));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        PlayerManager.PlayerManagerInstance.FormatStickman();
    }
}
