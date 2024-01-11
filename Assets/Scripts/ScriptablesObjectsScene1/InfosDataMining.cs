using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="InfosDataMining", menuName ="SO/InfosNouveauMining")]
public class InfosDataMining : ScriptableObject
{
   [SerializeField] public float _tempsDeJeu;
   [SerializeField] public float _tempsEcoule;
   [SerializeField] public float _nbPoints;
   
   public void init(){
    _tempsEcoule = _tempsDeJeu;
 
   }
}
