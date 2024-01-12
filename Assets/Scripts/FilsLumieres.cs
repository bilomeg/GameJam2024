using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilsLumieres : MonoBehaviour
{
    [SerializeField] private Material notGlow1;
    [SerializeField] private Material glow1;
   
    
    [SerializeField] private InfosGame _infosGame;

   
     [SerializeField]private Renderer _dataMining;
     [SerializeField]private Renderer _tower;
     [SerializeField]private Renderer _spaceInvaders;

    void Start()
    {
   
        if (_infosGame != null)
        {
            ChangeMaterial(_infosGame.dataMining, _dataMining );
            ChangeMaterial(_infosGame.towerDefense, _tower );
            ChangeMaterial(_infosGame.spaceInvaders, _spaceInvaders );
            // Vous pouvez ajouter des appels similaires pour les autres tableaux si nécessaire
        }
    }

    void Update()
    {
       
    }

    // Fonction pour changer le matériau en fonction du tableau de niveaux
    private void ChangeMaterial(InfosGame.Levels[] levels, Renderer _renderer)
    {
        Debug.Log(levels.Length);
        // Assurez-vous que le composant Renderer est attaché
        if (_renderer != null && levels != null && levels.Length > 0)
        {
            Debug.Log(levels[0].completed);
            Debug.Log(2);
            // Exemple : Changez le matériau en fonction de la valeur de "completed" du premier niveau
            if (levels.Length > 0 && levels[0].completed)
            {
                ChangeMaterial(glow1, _renderer);
                Debug.Log("hum");
            }
            else
            {
                ChangeMaterial(notGlow1, _renderer);
            }
        }
    }

    // Fonction pour changer le matériau
    private void ChangeMaterial(Material newMaterial, Renderer _renderer)
    {
        // Assurez-vous que le composant Renderer est attaché
        if (_renderer != null)
        {
            // Changez le matériau
            _renderer.material = newMaterial;
        }
    }
}

