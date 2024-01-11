using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointMiningData : MonoBehaviour
{
    [SerializeField] private InfosDataMining _infosDataMining;
     [SerializeField] private TMP_Text _texteNbPoints;
  void Start(){
    _infosDataMining._nbPoints = 0;
   }
  void Update()
    {
        AfficherPoints();
    }

       public void AfficherPoints(){
       
       _texteNbPoints.text = _infosDataMining._nbPoints.ToString()+" points";
         
    }
}
