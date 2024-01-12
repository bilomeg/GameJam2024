using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ChangementSceneAvecE : MonoBehaviour
{
[SerializeField] LevelManager _levelManager;
[SerializeField] private GameObject _dataMining;

[SerializeField] private GameObject _tower;

[SerializeField] private GameObject _spaceInvader;


    void Start()
    {
        _levelManager = LevelManager.Instance;
    }


  

    public void OnOpen(){
        if(_dataMining){
    _levelManager.LoadAsyncScene("SceneDataMining");
    Debug.Log("PL");
 }
 if(_tower){
    _levelManager.LoadAsyncScene("TowerDefense");
 }
 else{
    _levelManager.LoadAsyncScene("SceneSpaceInvader");
 }
}
}
