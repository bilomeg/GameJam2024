using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerdreVie : MonoBehaviour
{
    [SerializeField] private AfficherVie scriptVie;


    // Start is called before the first frame update
    void Start()
    {
        scriptVie.GetComponent<AfficherVie>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if(other.tag == "Player"){
            Vie(other.gameObject);
            Destroy(gameObject);

        }

        if(other.tag == "Plancher"){
            Destroy(gameObject);
        }
    }

    private void Vie(GameObject player){
        if(scriptVie.nombreVie > 0){
            scriptVie.nombreVie--;
            Debug.Log(scriptVie.nombreVie);
        }

        if(scriptVie.nombreVie <=0){
            scriptVie.nombreVie = 0;
            Debug.Log("Vaisseau mort");
            Destroy(player);
            LevelManager.Instance.LoadAsyncScene("SceneSallePrincipale");
        }
    }
}
