using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victoire : MonoBehaviour
{
    private GameObject[] nbEnnemie;

    private bool ennemieEnVie = true;

    [SerializeField] private InfosGame infoGame;
    [SerializeField] private AudioSource _sonVictoire;

    // Start is called before the first frame update
    void Start()
    {
        nbEnnemie = GameObject.FindGameObjectsWithTag("Ennemie");
    }

    // Update is called once per frame
    void Update()
    {
        nbEnnemie = GameObject.FindGameObjectsWithTag("Ennemie");

        if(nbEnnemie.Length == 0 && ennemieEnVie){
            ennemieEnVie = false;
            Debug.Log("victoire");
            _sonVictoire.Play();
            infoGame.spaceInvaders[0].completed = true;
            Invoke("ChangementScene", 3.0f);
        }
    }

    private void ChangementScene(){
        LevelManager.Instance.LoadAsyncScene("SceneSallePrincipale");
    }
}
