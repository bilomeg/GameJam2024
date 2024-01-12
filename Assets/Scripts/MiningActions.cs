using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MiningActions : MonoBehaviour
{
   RaycastHit hit;
   RaycastHit hit2;
   [SerializeField]GameObject finder;
    public LayerMask layerMask;
    [SerializeField] private Material terre1;
    [SerializeField] private Material terre2;
      [SerializeField] private Material minerai1;
    [SerializeField] private Material minerai2;
     private int nombreClics = 0;
    [SerializeField] private InfosDataMining _infosDataMining;
    private int _nbPoints = 2;
        public Renderer Cube;
        void Start(){
Cursor.lockState = CursorLockMode.None; 
        }
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
                //hit2.transform.GetComponent<Renderer>().material.color = Color.red;
                //Destroy(hit2.transform.gameObject);
                
           if (hit2.transform.CompareTag("BlocTerre"))
                    {
                        Cube = hit2.transform.GetComponent<Renderer>();
                        nombreClics++;

                        if (nombreClics == 1)
                        {
                            //ChangerMaterialBloc(hit2.transform, Color.red);
                            var materials = Cube.materials;
                            // exchange one material
                            materials[1] = terre1;
                            // reassign the materials to the renderer
                            Cube.materials = materials;
                            Debug.Log(Cube.materials[0]);
                            Debug.Log(Cube.materials[1]);
                        }
                        else if (nombreClics == 2)
                        {
                            //ChangerMaterialBloc(hit2.transform, Color.red);
                            var materials = Cube.materials;
                            // exchange one material
                            materials[1] = terre2; 
                            // reassign the materials to the renderer
                            Cube.materials = materials;
                        }
                        else if (nombreClics == 3)
                        {
                            DetruireBloc(hit2.transform.gameObject);
                        }
                    }
                      if (hit2.transform.CompareTag("BlocMinerai"))
                    {
                        Cube = hit2.transform.GetComponent<Renderer>();
                        nombreClics++;

                        if (nombreClics == 1)
                        {
                             var materials = Cube.materials;
                            // exchange one material
                            materials[1] = minerai1;
                            // reassign the materials to the renderer
                            Cube.materials = materials;
                        }
                        else if (nombreClics == 2)
                        {
                             var materials = Cube.materials;
                            // exchange one material
                            materials[1] = minerai2;
                            // reassign the materials to the renderer
                            Cube.materials = materials;
                        }
                        else if (nombreClics == 3)
                        {
                            DetruireBloc(hit2.transform.gameObject);
                            _infosDataMining._nbPoints += _nbPoints;
                           
                        }
                    }
                    
            }
            
        }
       
            

        }
    }
    public void ChangerMaterialBloc(Transform blocTransform, Color nouvelleCouleur){
          Renderer blocRenderer = blocTransform.GetComponent<Renderer>();
        if (blocRenderer != null)
        {
            // Changer la couleur du matériau ici, par exemple en rouge
            blocRenderer.material.color = nouvelleCouleur;
        }
       
    }
     public void DetruireBloc(GameObject blocGameObject)
    {
        Destroy(blocGameObject);
        nombreClics = 0; // Réinitialiser le compteur après la destruction
    }
      private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Réinitialiser les points à zéro
        _infosDataMining._nbPoints = 0;
        _infosDataMining._tempsEcoule = 60f;
    } 
}
