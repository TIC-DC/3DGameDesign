using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//a simple script for a HUD element that displays a number
//can be modified to show other datatypes
//uses TextMeshPro
//script should be a component of either the Object whose info you are trying to represent or
//a component of the TextMeshPro Object
public class HUD : MonoBehaviour
{
    [SerializeField]
    TMP_Text text; //should be assigned in the inspector

    //call either of these methods whenever the represented value changes, some other script will need a ref to this one
    public void updateText(String str)
    {
        text.SetText(str);
    }

    public void updateText(float num)
    {
        text.SetText(""+num);
    }
}
