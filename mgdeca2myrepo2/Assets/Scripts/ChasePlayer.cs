using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
   private NavMeshAgent enemy;
   public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        

        enemy.SetDestination(player.transform.position);

    }

    private void chasePlayer()
    {
        enemy.SetDestination(player.transform.position);
    }
}
