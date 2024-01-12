using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="InfosGame", menuName ="SO/NouvelleGame")]
public class InfosGame : ScriptableObject
{
   [System.Serializable]
   public class Levels
{
    public string sceneName;
    public bool completed;
}

    public Levels[] towerDefense;
    public Levels[] spaceInvaders;
    public Levels[] dataMining;

    [HideInInspector] public static InfosGame instance;

    void OnEnable()
    {
        instance = this;

        spaceInvaders[0].completed = false;
        dataMining[0].completed = false;
        towerDefense[0].completed = false;
    }
}

