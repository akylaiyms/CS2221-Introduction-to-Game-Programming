using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    //here we are accessing the Freeze script again 
    private Freeze freeze; 
    
    int m_CurrentWaypointIndex;

    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
        // in the very beginning of the game we are accessing the Freeze component of the freeze we set up in the beginning
        freeze = GetComponent < Freeze > ();
    }

    void Update()
    {   
        //we are checking if the enemies are frozen every frame, and if it is we are resetting the navMeshAgent path, basically stopping them from moving 
        if (freeze.isFrozen) 
        {
            navMeshAgent.ResetPath();
            return; 
        }

        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination (waypoints[m_CurrentWaypointIndex].position);
        }
    }
}
