using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{//This scripts handles death and respawn of enemy objecst
   
    public GameObject Spawn_Location;
    public float Respawn_Time;

    public bool isDead;

    private float timetoSpawn = 0;

    public MeshRenderer render;
    public SphereCollider enemyCollider;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if ( isDead == true   )
        {
           
            Spawn_Countdown();
            
        }
        //print("Dead Variable" + isDead);

    }

    private void Spawn_Countdown()
    {
      
        timetoSpawn +=  Time.deltaTime;

        if (timetoSpawn >= Respawn_Time)
        {
            Respawn();
            isDead = false;
            timetoSpawn = 0;
        }

        

    }

    private void Respawn()
    {
        print("Respawn!!!!!");
        render.enabled  = true;
        enemyCollider.enabled = true;
        gameObject.transform.position = Spawn_Location.transform.position;
        
    }

    public void die()
    {
        print("dies");
        Spawn_Countdown();
        render.enabled = false;
        enemyCollider.enabled = false;

    }
}
