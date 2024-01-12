using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastilleDataMining : MonoBehaviour
{
    private LevelManager _levelManager;

    // Start is called before the first frame update
    [SerializeField] private GameObject _canvasToucheE;
    void Start()
    {
        _levelManager = LevelManager.Instance;
    }

    // Update is called once per frame
   private void OnTriggerEnter(Collider other)
 {
    //_levelManager.LoadAsyncScene("SceneDataMining");
    _canvasToucheE.SetActive(true);
 }

}
