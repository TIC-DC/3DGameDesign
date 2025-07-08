using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

//A script for player movement, includes based WASD, raycast ground detection, and double jump
//NOTE! this uses the OLD input system. To fix any potential errors, go to project settings -> Player -> Other Settings -> Active Input Handling -> Both
public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;

    Rigidbody rb;//variable to directly effect the physics body of the player

    Vector2 moveInput;//Vector used to keep track of the WASD inputs of the palyer

    public Transform orientation;

    public bool isGrounded;
    bool airJumpReady = true;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        orientation = GetComponent<Transform>();  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.angularVelocity = new Vector3(0f,0f,0f); //manually setting rotational velocity to zero to prevent player spinning out
        MovePlayer();
        isGrounded = Physics.Raycast(transform.position, -transform.up, 0.1f);//adjust final value as needed, current value is set assuming the position of the transform in just above the floor
        if(isGrounded)
        {
            airJumpReady = true;
        }
    }

    private void MovePlayer()
    {
        Vector3 playerVelocity = new Vector3(moveInput.x * moveSpeed, rb.velocity.y,moveInput.y*moveSpeed);//create a vector for the player's velocity in X and Z based on WASD input
        //NOTE! because we are manually setting the player's X & Z velocity, external effects on the Player's Rigidbody, like an AddForce call, will only have effect on velocity on the Y-axis
        //To have external forces act on the player, do something like this
        //playerVelocity += new Vector3(10f,0f,10f);
        rb.velocity = orientation.TransformDirection(playerVelocity); //set the Rigidbody's velocity to the Vector we created, based on how the player is facing
    }

    //A note on Input System and Events
    /*
    So, these methods need to be assigned via the input manager of the player, SO......
    1. add a Player Input component to the player
    2. change the Behavior from Send Messages to Invoke Unity Events
    3. For any methods you want to set up, find the corresponding Event (Move for OnMove, Jump for OnJump, etc) and click the +
        then, drag the Script COMPONENT in the Inspector into the spot that says None (Object)
        then, click the box that says "no function," hover over the script containing the method, then select the correct method from the list
        the method should appear in a seperate list under the heading "Dynamic CallbackContext." if it does not, something is wrong
    */

    public void OnMove(InputAction.CallbackContext context)//NOTE! this method is called when the player presses W,A,S, or D OR releases WASD
    {
        moveInput = context.ReadValue<Vector2>();//assign moveInput to what the InputSystem read (when a key is released, the Input System reads (0,0))
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started)//this if statement ensures that the player will only jump when the correct button is pressed, not when it is released
        {
            if(isGrounded || airJumpReady)
            {
                if(!isGrounded)//if the player is using an air jump
                {
                    airJumpReady = false;
                }
                rb.AddForce(transform.up * 10, ForceMode.Impulse);
            }
        }
    }
}
