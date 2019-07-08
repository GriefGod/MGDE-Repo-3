using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    public bool isFlat = true;
    private Rigidbody rb; //for the object rigidbody
    public int speed =5;
    public float tiltSpeedLimit = 0.8f;
    public float tiltLimit = 2;
    public float tiltSpeed = 5;


   

    //Setting gyro 
    private GameObject Setting;
   // public Setting_Menu isgyro = true;
    // Start is called before the first frame update
    void Start()
    {
       
            rb= GetComponent<Rigidbody>(); //for ball
        
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UseAcclerometer(); //using acclerometer to tilt table

    }

    public void UseAcclerometer()
    {
        if (new Vector2(Input.acceleration.x, Input.acceleration.z).magnitude > 0.1)
        {
            Vector3 tilt_force = Input.acceleration * speed; //using accleration input to measure tiltness in terms of tilt_force


            float tiltX = Mathf.Clamp(tilt_force.x, -tiltSpeedLimit, tiltSpeedLimit); //limits the max tiltspeed in x coordinates;
            float tiltY = Mathf.Clamp(tilt_force.y, -tiltSpeedLimit, tiltSpeedLimit); //limits the max tiltspeed in y coordinates;



            rb.AddTorque(tilt_force.y  , 0, -1 * tilt_force.x);

        }

    }
}
