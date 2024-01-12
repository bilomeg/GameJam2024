using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfficherVie : MonoBehaviour
{
        public int nombreVie = 3;

        [SerializeField] private GameObject vie1;
        [SerializeField] private GameObject vie2;
        [SerializeField] private GameObject vie3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(nombreVie == 2){
            vie3.SetActive(false);
        }

        if(nombreVie == 1){
            vie2.SetActive(false);
        }

        if(nombreVie == 0){
            vie1.SetActive(false);
        }
    }
}
