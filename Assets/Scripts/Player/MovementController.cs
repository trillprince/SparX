using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public CharacterController controller;

    // movement
    public float speed = 14f;
    private Vector3 velocity;
    
    // free fall
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    private bool isGrounded;

    private PhotonView photonViewRef;

    //jump
    private float jumpHeight = 3f;

    // Start is called before the first frame update
    void Start()
    {
        photonViewRef = GetComponentInParent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonViewRef.IsMine)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        
    }
}