using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;

    // this is the ount of the protector/freeze capsules that John Lemon has
    public int protectorCount;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource>();
        // at the start of the game John Lemon has 0 protector/freeze cpsules, he needs to collect them 
        protectorCount = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool ("IsWalking", isWalking);

        if(isWalking)
        {
            if(!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play ();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);
    }
    
    void OnAnimatorMove ()
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation (m_Rotation);
    }

    // this is the collider for the freeze capsule 
    void OnTriggerEnter (Collider other)
    {   
        if(other.gameObject.CompareTag("Capsule"))
        {
            // this calls CapsuleActivation when the protectorCount goes up to 1
            other.gameObject.SetActive(false);  
            protectorCount = protectorCount + 1;
            CapsuleActivation();
        }  
    }

    // this is the function that activates the freeze of the capsule 
    void CapsuleActivation() 
    {
        // here this finds all the enemies that are tagged as Freezable and makes an array called enemies (you can do it int he prefab mode of each GameObject)
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Freezable");

        //if protectorCount is more than 0, the loop goes for every enemy in array enemies
        if(protectorCount >= 0)
        {
            foreach ( GameObject enemy in enemies) 
            {
                // then we access the Freeze component of the enemy and activate the FreezeEnemy() 
                Freeze freeze = enemy.GetComponent < Freeze > ();
                freeze.FreezeEnemy();
            }
            StartCoroutine(CapsuleDeactivation());
        }
    }

    // this is the function CapsuleDeactivation, it is IEnumerator instead of void because we need to deactivate the capsule after 10 seconds 
    IEnumerator CapsuleDeactivation()
    {   
        //this wait for 10 seconds (youcan set however many you want)
        yield return new WaitForSeconds(10);
        //then it accesses the enemy in the array again, just as in CapsuleActivation
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Freezable");

        //and here it unfreezes all the enemies that were frozen 
        foreach ( GameObject enemy in enemies) 
        {
            Freeze freeze = enemy.GetComponent < Freeze > ();
            freeze.UnfreezeEnemy();
        }
    }
}

