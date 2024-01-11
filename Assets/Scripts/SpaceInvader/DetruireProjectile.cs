using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetruireProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ennemis"){
            Destroy(other);
            Destroy(gameObject);
        }

        if(other.tag == "Plafond"){
            Destroy(gameObject);
        }
    }
}
