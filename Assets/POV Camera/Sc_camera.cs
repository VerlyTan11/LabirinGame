using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_camera : MonoBehaviour
{
    // Start is called before the first frame update
    float panjangRay = 100f; 
    Transform target; 
    void Start()
    {
        target = GameObject.Find("target3").transform;
    }

    // Update is called once per frame
    void Update() 
    { 
        //Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); 
        Vector3 arahTarget = target.position - this.transform.position; 
        Ray ray = new Ray(this.transform.position, arahTarget); 
        Debug.DrawLine(ray.origin, arahTarget * panjangRay, Color.red); 
        RaycastHit hit; 
        bool isRayHit = Physics.Raycast(ray,out hit); 
            if (isRayHit) { 
             Debug.Log("Ray Cam kena: " + hit.collider.name); 
            } 
    }
}
