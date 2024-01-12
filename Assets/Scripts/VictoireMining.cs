using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoireMining : MonoBehaviour
{
    [SerializeField] private InfosDataMining infosDataMining;
    [SerializeField] private InfosGame infosGame;
    private LevelManager _levelManager;

    private bool objectifAtteint = true;

    // Start is called before the first frame update
    void Start()
    {
        _levelManager = LevelManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(infosDataMining._tempsEcoule <= 0){
            if(infosDataMining._nbPoints >= 16 && objectifAtteint){
                objectifAtteint = false;
                Debug.Log("Victoire");
                _levelManager.LoadAsyncScene("SceneSallePrincipale");
                infosGame.dataMining[0].completed = true;
            }
            else{
                Debug.Log("Defaite");
                infosGame.dataMining[0].completed = false;
                _levelManager.LoadAsyncScene("SceneSallePrincipale");
            }
        }
    }
}
