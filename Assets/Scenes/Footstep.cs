using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    public AudioSource Forest_ground_step1Sound;

    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
            Forest_ground_step1Sound.enabled = true;
        }
        else
        {
            Forest_ground_step1Sound.enabled = false;
        }
    }
}