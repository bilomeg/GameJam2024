using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ZoneUIAccueil : MonoBehaviour
{
    public void DebutGame(){
          //int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene("SceneSallePrincipale");
      
    }
   public void QuiteGame()
    {
        Debug.Log($"fonctionne");
        Application.Quit();
    }
}
