using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexteVictoire : MonoBehaviour
{
    [SerializeField] private InfosGame infosGame;

    [SerializeField] private GameObject textOriginal;

    [SerializeField] private GameObject textVictoire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(infosGame.towerDefense[0].completed && infosGame.spaceInvaders[0].completed && infosGame.dataMining[0].completed){
            textOriginal.SetActive(false);
            textVictoire.SetActive(true);
        }
    }
}
