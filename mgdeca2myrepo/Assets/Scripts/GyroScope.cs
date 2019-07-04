using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroScope : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private Rigidbody rb;
    public int strenght =  100;
    private CanvasManager gyrOffset; // check control type before game starts
    // Start is called before the first frame update
    void Start()
    {

        gyrOffset = FindObjectOfType<CanvasManager>();
       
        rb = GetComponent<Rigidbody>();
        gyroEnabled = EnableGyro(); //enables gyroscope on phone
    }


    private bool EnableGyro() //this is to ensure gyro is turned on before gameplay.
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            print("Is working");
            return true;
            
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {

        // transform.Rotate(gyro.attitude.x * strenght, gyro.attitude.y * strenght, 0, Space.Self);

        // transform.Translate(gyro.attitude.x * strenght, gyro.attitude.y * strenght, 0, Space.Self);

        //Phone is weird.
        //To get left and right motion, it uses the/our phonese up and down motion(Portait), hence gro x is our z, inverse
        //When we tilt our phone (port side left), side ways the ball move up, this is becuase the phones y is facing upwards on potrait
        // Ignore Y, it calcutaes up and down motions
        // rb.AddTorque( 0, 0, -1 * gyro.attitude.x * strenght); // this makes it move left and right correctly

        // rb.AddTorque(1 * gyro.attitude.y * strenght, 0, 0); //this makes it move up and down correctly

        //combine two codes
        
        rb.AddTorque(1 * (gyro.attitude.y - gyrOffset.offsetCalY) * strenght, 0, -1 * (gyro.attitude.x - gyrOffset.offsetCalX )* strenght); //
    }
}
