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
        transform.Translate(Vector3.up * Time.deltaTime * 3.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
       Debug.Log("Ennemie détruit");
        if(other.tag == "Ennemie"){
            
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if(other.tag == "Plafond"){
            Destroy(gameObject);
        }
    }
}
