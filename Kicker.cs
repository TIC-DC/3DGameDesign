using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//A simple script for a Trigger collider attached to something that repels an object that enters the collider
public class Kicker : MonoBehaviour
{
    public float power = 10;

    void OnTriggerEnter(Collider collision)
    {
        Rigidbody ballBody = collision.GetComponent<Rigidbody>();
        Transform ballTform = collision.GetComponent<Transform>();
        //ball's trajectory will be angled based on the relative position of the GameObject the collider is attached to and the thing that entered the collider
        Vector3 direction = -(transform.position + new Vector3(0f,0.5f,0f) - ballTform.position).normalized;
        ballBody.AddForce(direction * power, ForceMode.Impulse);
    }
}
