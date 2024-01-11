using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDefensePlayer : MonoBehaviour
{
    // ---------------------
    // Class
    // ---------------------

    // ---------------------
    // Variables
    // ---------------------

    

    // ---------------------
    // Functions
    // ---------------------

    // Start Functions
    // ---------------------

    void Start()
    {
        // Set Variables
    }

    // ---------------------
    // Player Input
    // ---------------------

    public void OnStartMission()
    {
        
    }

    // ---------------------
    // Collision
    // ---------------------

    void OnTriggerEnter(Collider other)
    {
        // If Other Has TowerDefenseObjects
        if(other.GetComponent<TowerDefenseObjects>() != null)
        {
            // Defender
        }
    }
}
