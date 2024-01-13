using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine;
using TMPro;

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

    [HideInInspector] public TowerDefenseManager towerManager;
    [HideInInspector] public TowerDefenseLevelInfo levelInfo;

    public ObjectType objectType;

    [Header("Defender")]
    public GameObject defenderUpgradedPrefab;
    public GameObject defenderHologram;
    public int price;
    [Space(10)]

    public int damage;
    public float fireRate;
    [Space(10)]

    public float attackRadius;
    [SerializeField] SphereCollider hitArea;

    [Header("Defender Hologram")]
    
    public GameObject defenderGameObject;
    public GameObject[] collidingObjects;
    public int defenderCost;
    [Space(10)]

    [SerializeField] GameObject hitAreaGameObject;
    [SerializeField] float hitAreaTransition;

    [Header("Enemy Info")]
    
    [SerializeField] int health;
    public NavMeshAgent navMesh;
    public float speed;
    [Space(5)]

    [SerializeField] GameObject floatyText;
    public int givenScoreAtDeath;
    public int givenMoneyAtDeath;

    // ---------------------
    // Functions
    // ---------------------

    // Start Fuctions
    // ---------------------

    void Start()
    {
        // Defender
        if(objectType == ObjectType.Defender)
        {
            hitArea.radius = attackRadius;
        }

        // Defender Hologram
        if(objectType == ObjectType.DefenderHologram)
        {
            // Set Variable
            defenderCost = defenderGameObject.GetComponent<TowerDefenseObjects>().price;

            Transform areaTransform = hitAreaGameObject.transform;
            float radius = defenderGameObject.GetComponent<TowerDefenseObjects>().attackRadius * 2;
            // Animation
            areaTransform.DOScale(radius, hitAreaTransition).SetEase(Ease.InOutCirc);
        }

        // Enemy
        if(objectType == ObjectType.Enemy)
        {

        }
    }

    // Enemy Functions
    // ---------------------

    public void LoseHealth(int damage)
    {
        // Set Variables
        health -= damage;
        Debug.Log(health);

        // Display Damage
        if(floatyText != null)
        {
            var instance = Instantiate(floatyText, transform.position, floatyText.transform.rotation);
            TextMeshProUGUI text = instance.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            text.text = "- " + damage;
        }

        if(health <= 0)
        Death();
    }

    public void Death()
    {
        // Set Variables
        TowerDefensePlayer player = GameObject.Find("Player").GetComponent<TowerDefensePlayer>();
        player.currentMoney += givenMoneyAtDeath;
        player.currentScore += givenScoreAtDeath;

        // Destroy Object
        // Spawn Particles

        towerManager.CheckIfEnd();
        towerManager.RefreshUI();
        Destroy(gameObject);
    }

    // Tower Functions
    // ---------------------

    void DamageTower(int damage)
    {
        // Set Variables
        health -= damage;
        
        // Display Damage
        if(floatyText != null)
        {
            var instance = Instantiate(floatyText, transform.position, floatyText.transform.rotation);
            TextMeshProUGUI text = instance.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            text.text = "- " + damage;
        }

        if(health <= 0)
        TowerDestroyed();
    }

    void TowerDestroyed()
    {
        // Show Particle

        // Call Functions
        towerManager.MissionFailed();

        // Destroy GameObject
        Destroy(gameObject);
    }

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

        if(objectType == ObjectType.Tower)
        {
            if(other.GetComponent<TowerDefenseObjects>() != null)
            {
                TowerDefenseObjects otherObject = other.GetComponent<TowerDefenseObjects>();
                if(otherObject.objectType == ObjectType.Enemy)
                {
                    DamageTower(otherObject.damage);
                    otherObject.Death();
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
