using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningActions : MonoBehaviour
{
   RaycastHit hit;
   RaycastHit hit2;
   [SerializeField]GameObject finder;
    public LayerMask layerMask;
   
    private void Update()
    {
        if(Input.GetMouseButtonDown(0)){
             Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
               int layerMask = 1 << LayerMask.NameToLayer("Player");
              layerMask = ~layerMask;


        if (Physics.Raycast (ray, out hit, 100)) {
           // Debug.Log (hit.transform.name);
           // Debug.Log ("hit");
            finder.transform.LookAt(hit.transform.position);
            Ray ray2 = new Ray(finder.transform.position, hit.transform.position-finder.transform.position);

            
            if(Physics.Raycast (ray2, out hit2, 100,layerMask)){
                hit2.transform.GetComponent<Renderer>().material.color = Color.red;
                //Destroy(hit2.transform.gameObject);
                
            }
            
        }
       
            

        }
    }
    public void ChangerCouleurBloc(){
        
    }
     
}
