using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victoire : MonoBehaviour
{
    public int nbDePoint = 0;
    private GameObject[] nbEnnemie;

    [SerializeField] private InfosGame infoGame;

    // Start is called before the first frame update
    void Start()
    {
        nbEnnemie = GameObject.FindGameObjectsWithTag("Ennemie");
    }

    // Update is called once per frame
    void Update()
    {
        nbEnnemie = GameObject.FindGameObjectsWithTag("Ennemie");

        if(nbEnnemie.Length == 0){
            Debug.Log("victoire");
            infoGame.spaceInvaders[0].completed = true;
            LevelManager.Instance.LoadAsyncScene("SceneSallePrincipale");
        }
    }
}
