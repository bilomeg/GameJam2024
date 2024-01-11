using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ZoneUIAccueil : MonoBehaviour
{
    
 
    private LevelManager _levelManager;
       // Start is called before the first frame update
    void Start()
    {
        _levelManager = LevelManager.Instance;
        
    }
    public void DebutGame(){
         
        _levelManager.LoadAsyncScene(LevelManager.Scene.SceneSallePrincipale);
      
    }
   public void QuiteGame()
    {
        Debug.Log($"fonctionne");
        Application.Quit();
    }


    
}
