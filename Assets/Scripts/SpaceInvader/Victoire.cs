using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victoire : MonoBehaviour
{
    public int nbDePoint = 0;
    private GameObject[] nbEnnemie;

    [SerializeField] private InfosGame infoGame;
    [SerializeField] private AudioSource _sonWin;

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
            _sonWin.Play();
            Debug.Log("victoire");
            infoGame.spaceInvaders[0].completed = true;
            
            Invoke("ChangementScene", 3.0f);
        }
    }

    void ChangementScene(){
        LevelManager.Instance.LoadAsyncScene("SceneSallePrincipale");
    }
}
