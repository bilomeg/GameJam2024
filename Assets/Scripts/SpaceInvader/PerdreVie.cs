using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerdreVie : MonoBehaviour
{
    [SerializeField] private AfficherVie scriptVie;

    [SerializeField] private AudioSource _sonDamage;
    [SerializeField] private AudioSource _sonGameOver;


    // Start is called before the first frame update
    void Start()
    {
        scriptVie.GetComponent<AfficherVie>();
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if(other.tag == "Player"){
            Vie(other.gameObject);
            gameObject.SetActive(false);

        }

        if(other.tag == "Plancher"){
            Destroy(gameObject);
        }
    }

    private void Vie(GameObject player){
        if(scriptVie.nombreVie > 0){
            scriptVie.nombreVie--;
            _sonDamage.Play();
        }

        if(scriptVie.nombreVie <=0){
            scriptVie.nombreVie = 0;
            Debug.Log("Vaisseau mort");
            _sonGameOver.Play();
            Destroy(player);
            Invoke("ChangementScene", 2.0f);
        }
    }

    private void ChangementScene(){
        LevelManager.Instance.LoadAsyncScene("SceneSallePrincipale");
    }
}
