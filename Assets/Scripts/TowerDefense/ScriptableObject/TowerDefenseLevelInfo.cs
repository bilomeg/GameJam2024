using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName ="TowerDefenseLevel_00", menuName ="SO/Tower Defense/Level Info")]
public class TowerDefenseLevelInfo : ScriptableObject
{
    // ---------------------
    // Class
    // ---------------------

    [System.Serializable]
    public class EnemyWave
    {
        [Header("Wave Information")]
        public GameObject enemiesPrefab;
        public int numberToSpawn;
        [Space(5)]

        public float timeAfterPreviousWave;
        public float timeBetweenSpawn;
    }

    // ---------------------
    // Variables 
    // ---------------------

    [Header("Level's Information")]
    public int maxTower;
    [Space(5)]

    public int moneyAtStart;
    public int moneyPerSeconds;
    [Space(10)]

    public EnemyWave[] enemyWaves;
}
