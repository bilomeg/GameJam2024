using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foi : MonoBehaviour
{
    [SerializeField] private GameObject foi1;
    [SerializeField] private GameObject foi2;
    [SerializeField] private GameObject foi3;

    [SerializeField] private InfosGame infosGame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(infosGame.dataMining[0].completed){
            foi1.SetActive(true);
        }

        if(infosGame.spaceInvaders[0].completed){
            foi2.SetActive(true);
        }

        if(infosGame.towerDefense[0].completed){
            foi3.SetActive(true);
        }
    }
}
