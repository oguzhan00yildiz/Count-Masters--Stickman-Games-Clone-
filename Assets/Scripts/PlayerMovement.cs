using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] private float playerSpeed = 1f;
    [SerializeField] private float playerHorizontalSpeed = 10f;
    /*
    [SerializeField] private Rigidbody playerRB;
    */
    [SerializeField] private GameObject Player;
    
    [SerializeField] private CharacterController controller;
    private Vector3 direction;

    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Player = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
         direction.z = playerSpeed;
         direction.x = Input.GetAxisRaw("Horizontal")*playerHorizontalSpeed;
    }

    void FixedUpdate()
    {
        controller.Move(direction*Time.fixedDeltaTime);
    }
}
