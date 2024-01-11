using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDefenseObjects : MonoBehaviour
{
    // ---------------------
    // Class
    // ---------------------

    public enum ObjectType
    {
        Obstacle,
        Road,
        Defender,
        DefenderHologram,
        Enemy,
        Tower,
    }

    // ---------------------
    // Variables
    // ---------------------

    public ObjectType objectType;

    [Header("Defender")]
    public GameObject defenderUpgradedPrefab;
    public GameObject defenderHologram;
    [Space(10)]

    [SerializeField] float damage;
    [SerializeField] float fireRate;
    [SerializeField] GameObject[] enemies;

    [Header("Defender Hologram")]
    
    public GameObject defenderGameObject;
    public GameObject[] collidingObjects;

    // ---------------------
    // Functions
    // ---------------------


    // ---------------------

    // ---------------------
    // Collision Detection
    // ---------------------

    void OnTriggerEnter(Collider other)
    {
        if(objectType == ObjectType.DefenderHologram)
        {
            if(other.GetComponent<TowerDefenseObjects>() != null)
            {
                bool alreadyColliding = false;

                for(int i = 0;i < collidingObjects.Length;i++)
                {
                    if(collidingObjects[i] == null && !alreadyColliding)
                    {
                        collidingObjects[i] = other.gameObject;
                        alreadyColliding = true;
                    }
                }
            }
        }        
    }

    void OnTriggerExit(Collider other)
    {
        if(objectType == ObjectType.DefenderHologram)
        {
            if(other.GetComponent<TowerDefenseObjects>() != null)
            {
                for(int i = 0;i < collidingObjects.Length;i++)
                {
                    if(collidingObjects[i] == other.gameObject)
                    {
                        collidingObjects[i] = null;
                    }
                }
            }
        }    
    }
}
