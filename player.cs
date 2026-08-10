using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
    
    // Main
    [Header ("Main")]
    public CharacterController controller;
    public TimeManager timeManager;
    public PlayerCamera playerCamera;

    [SerializeField] float gravity = 9.81f;

    // movement
    [Header ("Movement")]
    [SerializeField] float speed;
    [SerializeField] float walk_speed = 6f;
    [SerializeField] float sprint_speed = 8f;
    Vector3 velocity;
    
    // jump
    [Header ("Jump")]
    [SerializeField] float jump_speed = 2f;
    bool was_grounded;

    // Ground Detection
    [Header ("Ground Detection")]
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask GroundLayer;
    bool is_grounded;



    
    void Update()
    {
        // ground check
        is_grounded = Physics.CheckSphere(groundCheck.position, groundDistance, GroundLayer);
        if (is_grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        Jump();
        Gravity();
        movement();
 
    }

    void movement()
    {
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprint_speed;
        }
        else
        {
            speed = walk_speed; 
        }

        controller.Move(move * speed * Time.deltaTime);



    }
    void Gravity()
    {
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    void Jump()
    {
        
        if (Input.GetButtonDown("Jump") && is_grounded)
        {
            velocity.y = Mathf.Sqrt(jump_speed * 2f * gravity);
            
        }
        


    }


    
}
