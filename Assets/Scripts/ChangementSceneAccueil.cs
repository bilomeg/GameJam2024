using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangementSceneAccueil : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ChangementScene", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangementScene(){
        LevelManager.Instance.LoadAsyncScene("SceneIntro");
    }
}
