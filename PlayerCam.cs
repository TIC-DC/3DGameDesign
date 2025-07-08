using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//generic camera script, usable for first and third person cameras
//NOTE: this uses the OLD input system. To fix any potential errors, go to project settings -> Player -> Other Settings -> Active Input Handling -> Both
//NOTE: if the camera begins jittering and freaking out when the player touches on object in the world, be sure to lock all rotation of the Player's Rigidbody
    //Rigidbody -> Constraints - > Rotation, click all three boxes
//this script should be a component of a child object of the player, the camera should be a child of that child
//Like So:
//Player
    //mount
        //camera
//For first person, put camera mount in the player's head, or slightly in front, 0,0,0 rotation
//For third person, put camera mount behind and above the player (negative z, position y) with a negative X rotation around 15
public class PlayerCam : MonoBehaviour
{
    //these are the values for look sensitivity. they should be set in the inspector
    //[SerializeField]
    public float sensiX;
    //[SerializeField]
    public float sensiY;

    public Transform playerOrientation;
    public Transform cameraOrientation;

    //[SerializeField]
    public float xRotation;
    //[SerializeField]
    public float yRotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()//in case of camera freaking out, change to FixedUpdate
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * sensiX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensiY;

        //update values for rotation
        yRotation += mouseX; //the mouse moving horizontally corresponds with the rotation around the y-axis
        xRotation -= mouseY; //the mouse moving vertically corresponds with the rotation around the x-axis
        xRotation = Mathf.Clamp(xRotation,-90f,90f);//prevent camera from looping under or over the player

        //now we apply rotation numbers to camera and player, player is only rotated about the y-axis
        cameraOrientation.rotation = Quaternion.Euler(xRotation,yRotation,0);
        playerOrientation.rotation = Quaternion.Euler(0,yRotation,0);
    }
}
