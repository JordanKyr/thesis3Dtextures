using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;

    [Header("Ground Check")]
    public float playerHeight;
    public float groundDrag;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;


    float horizontalInput;
    float verticalInput;



    Vector3 moveDirection;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        rb=GetComponent<Rigidbody>();
        rb.freezeRotation=true;
        
    }

private void MyInput(){
    horizontalInput= Input.GetAxisRaw("Horizontal");
    verticalInput=Input.GetAxisRaw("Vertical");


}

    private void MovePlayer(){

        //calculate movement direction

        moveDirection= orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force); 
    }

    private void speedControl(){
        //manage speed , no overspeeding
        Vector3 flatVel= new Vector3(rb.linearVelocity.x,0f,rb.linearVelocity.z);

        if(flatVel.magnitude > moveSpeed){
            Vector3 limitedVel=flatVel.normalized*moveSpeed;
            rb.linearVelocity= new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }

    }


    // Update is called once per frame
    void Update()
    {
        //ground check
        MyInput();
        speedControl();
        grounded=Physics.Raycast(transform.position,Vector3.down,playerHeight*0.5f+0.2f,whatIsGround);

   Vector3 down = transform.TransformDirection(Vector3.down) * 1;

        Debug.DrawRay(transform.position,down,Color.green,60);

        //handle drag

        if(grounded)
            rb.linearDamping=groundDrag;
        else 
            rb.linearDamping=0;

    }

    private void FixedUpdate(){

        MovePlayer();
    }
}
