using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour
{
    public AudioSource footstepsSound;
    public float raycastDistance = 0.1f;
    public Transform raycastOrigin;

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W))
        {
            // Cast a ray from the origin point to the ground
            Ray ray = new Ray(raycastOrigin.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                if (hit.collider.tag == "Untagged" || hit.collider.tag == "Ground")
                {
                    footstepsSound.enabled = true;
                }
                else
                {
                    footstepsSound.enabled = false;
                }
            }
            else
            {
                footstepsSound.enabled = false;
            }
        }
        else
        {
            footstepsSound.enabled = false;
        }
    }
}
