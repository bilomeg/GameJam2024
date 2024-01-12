using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerdreVie : MonoBehaviour
{
    [SerializeField] private AfficherVie scriptVie;
    [SerializeField] private AudioSource _sonDamage;
    [SerializeField] private AudioSource _sonDefaite;



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
            _sonDefaite.Play();
            scriptVie.nombreVie = 0;
            Debug.Log("Vaisseau mort");
            Destroy(player);
            Invoke("ChangementScene", 2.0f);
        }
    }

    void ChangementScene(){
        LevelManager.Instance.LoadAsyncScene("SceneSallePrincipale");
    }
}
