using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//i have made this class on a separate script and have attached it to every enemy 
public class Freeze : MonoBehaviour
{   
    // this will check if the enemies are already frozen
    public bool isFrozen;  

    public void FreezeEnemy() 
    {
        //this will show up in the console when the enemies are frozen 
        Debug.Log("Freeze");
        isFrozen = true;
    }
    
    public void UnfreezeEnemy()
    {
        //and this will show up in the console when emnemies are not frozen 
        Debug.Log("Unfreeze");
        isFrozen = false;
    }
}
