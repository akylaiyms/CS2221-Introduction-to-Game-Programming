using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    // I am taking the Freeze class in here 
    private Freeze freeze; 

    bool m_IsPlayerInRange;

    void Start() 
    {
        freeze = transform.parent.GetComponent < Freeze >();
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if(other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update ()
    {
        //here we check if the enemies are frozen in every frame 
        if (freeze.isFrozen)
        {
            return;
        }

        // checks if the object is frozen every frame, if yes - ignores the rest

        if(m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray (transform.position, direction);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit))
            {
                if(raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer ();
                }
            }
        }
    }

}
