using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Checkers : MonoBehaviour
{
    //handles player obj collisions, and its effects
   private GameObject player; //get player objects


   
    int point =0; //number of coins picked up.
    bool isPoint = false; //is a point picked
    bool power_up = false; //when collecting power up
    bool reset_powerup = false; //this is use to rest power up duration
    bool isDead = false;
    // Start is called before the first frame update


    void Start()
    {
        player = GetComponent<PlayerMaster>().player; //get player object by making refrence to player master
    }


    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.CompareTag("Enemy") && power_up == false) //when player touches enemy, dies by SetActive false for now. 
        {
            isDead = true;
            player.gameObject.SetActive(false);
            

        }
        if (collider.gameObject.CompareTag("Coins")) //when player touches coins gain a point;
        {

            collider.gameObject.SetActive(false);
            point++;

            isPoint = true;
        }

        if (collider.gameObject.CompareTag("Power Up")) //when player touches power up point;
        {
            print("power_up" + power_up);
            collider.gameObject.SetActive(false);
            power_up = true;
           
        }

        if (collider.gameObject.CompareTag("Power Up") && power_up == true) //reset power up if power up is already true;
        {
            
            collider.gameObject.SetActive(false);
            reset_powerup = true;

        }

        if (collider.gameObject.CompareTag("Enemy") && power_up  == true) //when player touches enemy, dies by SetActive false for now. 
        {
            collider.gameObject.GetComponent<Spawner>().isDead = true;
            collider.gameObject.GetComponent<Spawner>().die();


        }

        
    }

    public bool getPoint() //check if player has gotten a coin
    {
        return isPoint;
    }
    public void setPoint(bool isPoint) //set point
    {
        this.isPoint = isPoint;
    }
    public bool getPowerState() //allow other class to get power up state;
    {
        return power_up;
    }
    public void setPowerStat( bool state)
    {
        power_up = state;
    }
    public bool isDeadState()
    {
      return   isDead;
    }
    public bool getResetPowerup()
    {
        return reset_powerup;
    }
    public void setResetPowerup(bool state)
    {
        reset_powerup = state;
    }


   


}
