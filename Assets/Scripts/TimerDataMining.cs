using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerDataMining : MonoBehaviour
{
    // Start is called before the first frame update
     //Référence au texte du timer
    [SerializeField] private Text _tempsTexte;
    //Variable qui affichera le temps 
  private bool _tempsJeuFini = false;

    // Référence aux scriptables objects
    [SerializeField] private InfosDataMining _infosTempsData;
  

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculTemps(); 
    }
    void CalculTemps(){

    _infosTempsData._tempsEcoule -= Time.deltaTime; 
    if(_tempsTexte != null){
        AfficherTemps(_infosTempsData._tempsEcoule);
    }
if (_infosTempsData._tempsEcoule<= 0f){
        Debug.Log($"h");
        _tempsJeuFini = true;
        _infosTempsData._tempsEcoule =0f;
        Debug.Log("retoursceneprincipale");
         //_zoneFinScene.AllerSceneCredits();

       
    }
 
}
void AfficherTemps(float temps){

    temps +=1;

    float minutes = Mathf.FloorToInt(temps/60);
    float secondes = Mathf.FloorToInt(temps % 60);

    _tempsTexte.text = string.Format("{0:00}:{1:00}",minutes,secondes);
    
 
 
   }

}
