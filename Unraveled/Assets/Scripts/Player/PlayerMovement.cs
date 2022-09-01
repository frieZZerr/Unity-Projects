using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public Animator animator;
    //public GameObject parent;
    public Transform spawnPoint;
    public TPPMovement tpp;
    public PlayerCombat combat;

    //float speed;
    public float walkSpeed = 3f;
    public float sprintSpeed = 6f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public bool isGrounded;
    //private bool isSprinting;

    private void Start()
    {
        //animator = parent.transform.GetChild(0).GetComponent<Animator>();
        animator = GetComponent<Animator>();
        transform.position = spawnPoint.position;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);     //  Ground Check

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");                                      //  Movement Inputs
        float z = Input.GetAxis("Vertical");

        //Vector3 move = transform.right * x + transform.forward * z;                 //  Movement Vector

        //controller.Move(move * speed * Time.deltaTime);                             //  Moving

        if(x == 0 && z == 0)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            animator.SetBool("Jump", false);
        }

        else
        {
            if (Input.GetKey(KeyCode.LeftShift))                                        //  Sprinting
            {
                tpp.speed = sprintSpeed;
                //isSprinting = true;

                animator.SetBool("Walk", false);
                animator.SetBool("Run", true);
                animator.SetBool("Jump", false);
            }

            else
            {
                tpp.speed = walkSpeed;
                //isSprinting = false;

                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
                animator.SetBool("Jump", false);
            }
        }

        /*if (Input.GetKey(KeyCode.Space) && isGrounded)                              //  Jumping
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            animator.SetBool("Jump", true);
        }*/

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !animator.GetBool("InAir") && !combat.isAttacking)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

                animator.SetBool("Jump", true);
                animator.SetBool("InAir", true);
            }
            else
            {
                animator.SetBool("InAir", false);
            }
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
