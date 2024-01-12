using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastilleSpaceInvaders : MonoBehaviour
{
    [SerializeField] private GameObject _canvasToucheE;
    private LevelManager _levelManager;
    // Start is called before the first frame update
    void Start()
    {
        _levelManager = LevelManager.Instance;
    }

    // Update is called once per frame
     private void OnTriggerEnter(Collider other)
 {
    //_levelManager.LoadAsyncScene("SceneSpaceInvader");
     _canvasToucheE.SetActive(true);
 }
}
