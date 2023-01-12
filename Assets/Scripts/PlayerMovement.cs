using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
   public float PlayerSpeed = 1f;
    [SerializeField] private float playerHorizontalSpeed = 10f;
    /*
    [SerializeField] private Rigidbody playerRB;
    */
    [SerializeField] private GameObject Platform;
    
    [SerializeField] private CharacterController controller;
    private Vector3 direction;
    public bool Playercanmove = true;

    
    // Start is called before the first frame update
    void Start()
    {
        controller =Platform.gameObject.GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
         direction.z = PlayerSpeed;
         direction.x = Input.GetAxisRaw("Horizontal")*playerHorizontalSpeed;
    }

    void FixedUpdate()
    {      if (Playercanmove)
        {
            controller.Move(direction*Time.fixedDeltaTime);
        }
        
    }

    
}
