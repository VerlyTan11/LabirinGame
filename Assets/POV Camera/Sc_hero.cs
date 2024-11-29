using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_hero : MonoBehaviour
{
    float panjangRay = 10f; 
    Transform target; 
    // Start is called before the first frame update
    private void Start() 
    { 
        target = GameObject.Find("target1").transform; 
    } 
        

    void Update() 
    { 
    float v = Input.GetAxis("Vertical"); 
    float h = Input.GetAxis("Horizontal"); 
    this.transform.Translate(new Vector3(0,0,v) * 3f * Time.deltaTime); 
    this.transform.Rotate(new Vector3(0,h,0)); 
 
    Vector3 arahTarget = target.position - this.transform.position; 
    Ray ray = new Ray(this.transform.position, arahTarget); 
    Debug.DrawRay(this.transform.position, arahTarget * panjangRay, Color.red); 
        
    RaycastHit hit; 
    bool isRayHit = Physics.Raycast(ray, out hit, panjangRay); 
        if (isRayHit) { 
        Debug.Log("Ray kena : " + hit.collider.name); 
        } 
    } 
}
