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
    private Vector3 tilt_force = Vector3.zero; //get tilt info



    //Setting acclero
    private GameObject Setting;
    private CanvasManager accelroOffset;
    // public Setting_Menu isgyro = true;
    // Start is called before the first frame update
    void Start()
    {
        accelroOffset = FindObjectOfType<CanvasManager>();
        rb = GetComponent<Rigidbody>(); //for ball
        
        

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
            Vector3 tilt_force = Vector3.zero;
            tilt_force.x = Input.acceleration.x - accelroOffset.calibratedtilt.x; //using accleration input to measure tiltness in terms of tilt_force
            tilt_force.y = Input.acceleration.y - accelroOffset.calibratedtilt.y ; //using accleration input to measure tiltness in terms of tilt_force


            float tiltX = Mathf.Clamp(tilt_force.x, -tiltSpeedLimit, tiltSpeedLimit); //limits the max tiltspeed in x coordinates;
            float tiltY = Mathf.Clamp(tilt_force.y, -tiltSpeedLimit, tiltSpeedLimit); //limits the max tiltspeed in y coordinates;
            tilt_force = Quaternion.Euler(90, 90, 0) * tilt_force;

            rb.AddTorque(tilt_force * speed ); //add force to the ball via torque

        }

    }
}
