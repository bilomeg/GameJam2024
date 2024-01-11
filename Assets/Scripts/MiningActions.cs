using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningActions : MonoBehaviour
{
   RaycastHit hit;
   [SerializeField]GameObject finder;
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        if (Physics.Raycast (ray, out hit, 100)) {
            Debug.Log (hit.transform.name);
            Debug.Log ("hit");
            finder.transform.LookAt(hit.transform.position);
            

        }
    }
     
}
