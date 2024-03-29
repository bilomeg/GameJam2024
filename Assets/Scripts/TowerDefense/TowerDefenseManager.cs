using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine;

public class TowerDefenseManager : MonoBehaviour
{
    // ---------------------
    // Class
    // ---------------------

    // ---------------------
    // Variables
    // ---------------------

    TowerDefensePlayer player;
    TowerDefenseObjects tower;

    int numberOfEnemy;
    int enemyWaveIndex;
    [HideInInspector] public bool started;

    [Header("Level Manager")]
    [SerializeField] TowerDefenseLevelInfo levelInfo;
    [SerializeField] InfosGame infosGame;

    // ---------------------
    // Functions
    // ---------------------

    // Start Functions
    // ---------------------

    void Start()
    {
        // Set Variables
        player = GameObject.Find("Player").GetComponent<TowerDefensePlayer>();
        player.levelInfo = levelInfo;
        player.currentMoney = levelInfo.moneyAtStart;
        player.towerManager = this;

        tower = GameObject.Find("Tower").GetComponent<TowerDefenseObjects>();
        tower.levelInfo = levelInfo;
        tower.towerManager = this;
    }

    // Update Functions
    // ---------------------

    void FixedUpdate()
    {

    }

    // Mission Functions
    // ---------------------
    
    public void StartMission()
    {
        // Set Variables
        started = true;
        
        // Call Functions
        StartCoroutine("SpawnEnemyWave");
    }

    public void MissionFailed()
    {
        // Set Variables
        Invoke("ReturnToLobby", 5);
    }

    public void MissionCompleted()
    {
        // Set Variables
        for(int i = 0;i < infosGame.towerDefense.Length;i++)
        {
            if(infosGame.towerDefense[i].sceneName == SceneManager.GetActiveScene().name)
            infosGame.towerDefense[i].completed = true;
        }

        // Call Functions
        Invoke("ReturnToLobby", 5);
    }

    void ReturnToLobby()
    {
        // Load Scene
        LevelManager.Instance.LoadAsyncScene("SceneSallePrincipale");
    }

    public void CheckIfEnd()
    {
        // Set Variables
        numberOfEnemy--;

        // Check How Many Enemies Remain
        if(numberOfEnemy <= 0)
        {
            if(enemyWaveIndex >= levelInfo.enemyWaves.Length)
            MissionCompleted();
        }
    }

    IEnumerator SpawnEnemyWave()
    {
        if(enemyWaveIndex < levelInfo.enemyWaves.Length)
        {
            // Change Time UI
            yield return new WaitForSeconds(levelInfo.enemyWaves[enemyWaveIndex].timeBetweenWave);

            for(int i = 0;i < levelInfo.enemyWaves[enemyWaveIndex].numberToSpawn;i++)
            {
                // Set Variables
                numberOfEnemy++;

                // Spawn Enemy Then WaitFor...
                Vector3 spawnPos = levelInfo.enemyWaves[enemyWaveIndex].spawnLocation.position;
                Quaternion spawnRot = levelInfo.enemyWaves[enemyWaveIndex].spawnLocation.rotation;
                var instance = Instantiate(levelInfo.enemyWaves[enemyWaveIndex].enemiesPrefab, spawnPos, spawnRot);
                TowerDefenseObjects instanceScript = instance.GetComponent<TowerDefenseObjects>();

                instanceScript.towerManager = this;
                instanceScript.levelInfo = levelInfo;

                if(instanceScript.navMesh == null)
                instanceScript.navMesh = instance.GetComponent<NavMeshAgent>();
                instanceScript.navMesh.speed = instanceScript.speed;
                instanceScript.navMesh.SetDestination(levelInfo.destination);

                instance.name = "Enemy "+ i + "- Wave " + enemyWaveIndex;

                yield return new WaitForSeconds(levelInfo.enemyWaves[enemyWaveIndex].timeBetweenSpawn);
            }

            // Set Variables
            enemyWaveIndex++;

            StartCoroutine("SpawnEnemyWave");          
        }
    }
}