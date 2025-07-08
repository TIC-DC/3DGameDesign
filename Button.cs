using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this class is for a gameObject that turns on/off a platform or other GameObject when it is touched by another object
public class Button : MonoBehaviour
{
    //these variables should be the Collider and MeshRenderer of the GameObject & 
    //should be assigned by dragging the Object into the proper slots in the Inspector
    [SerializeField]
    Collider collider;
    [SerializeField]
    MeshRenderer mesh;

    void Start() // setting the default values for the platform linked to the target
    {
        collider.enabled = false;
        mesh.enabled = false;
    }

    //the collider for the target object should have isTrigger checked
    void OnTriggerEnter(Collider collision)
    {
        collider.enabled = true; // makes the platform tangible
        mesh.enabled = true; // makes the platform visible
    }
}
