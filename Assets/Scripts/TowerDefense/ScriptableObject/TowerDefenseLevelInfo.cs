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
    public class LevelLimit
    {
        public Vector2 minPos;
        public Vector2 maxPos;
    }

    [System.Serializable]
    public class EnemyWave
    {
        [Header("Wave Information")]
        public GameObject enemiesPrefab;
        public Vector3 spawnLocation;
        public int numberToSpawn;
        [Space(5)]

        public float timeAfterPreviousWave;
        public float timeBetweenSpawn;
    }

    // ---------------------
    // Variables 
    // ---------------------

    [Header("Level's Information")]
    public LevelLimit levelLimit;
    public float floorLevel;
    public Vector3 destination;
    [Space(5)]

    public int moneyAtStart;
    public int moneyPerSeconds;
    [Space(10)]

    public EnemyWave[] enemyWaves;
}
