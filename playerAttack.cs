using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//A simple script for launching a projectile based on a prefab
public class playerAttack : MonoBehaviour
{
    //projectile will be a prefab, must have a rigidbody
    [SerializeField]
    GameObject projectile;
    //origin should probably be an empty child object of the entity launching the projectile
    //in order to make the projectile launch from desired position EX: the player's hand
    [SerializeField]
    Transform origin;

    public float launchPower = 20f;

    //configure Unity Event in player's input system
    public void OnLaunch(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            GameObject proj = Instantiate(projectile,origin.position,origin.rotation); //create a new game object, without a parent, at origin, assign to variable
            proj.GetComponent<Rigidbody>().AddForce(origin.forward*launchPower,ForceMode.Impulse); //add force to projectile in the direction the origin is facing (the blue arrow)
        }
    }
}
