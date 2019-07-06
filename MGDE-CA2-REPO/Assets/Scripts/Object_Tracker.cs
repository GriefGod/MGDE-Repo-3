using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Tracker : MonoBehaviour
{//This to to keep certain gameobjects to follow other game objects
    public GameObject target;
    
    public Collider tracking_object;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = target.transform.position - tracking_object.transform.position; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tracking_object.transform.position = offset + target.transform.position;
    //    print("tracking position of " + tracking_object.transform.position);
    }
}
