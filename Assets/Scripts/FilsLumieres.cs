using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilsLumieres : MonoBehaviour
{
    [SerializeField] private Material notGlow1;
    [SerializeField] private Material glow1;
   
    
    [SerializeField] private InfosGame _infosGame;

    private Renderer _renderer; // Référence au composant Renderer de l'objet

    void Start()
    {
        // Assurez-vous que le composant Renderer est attaché à cet objet
        _renderer = GetComponent<Renderer>();
        if (_renderer == null)
        {
            Debug.LogError("Le composant Renderer est manquant sur cet objet.");
        }
    }

    void Update()
    {
        // Assurez-vous que InfosGame et les tableaux de niveaux correspondants ne sont pas nuls
        if (_infosGame != null)
        {
            ChangeMaterial(_infosGame.towerDefense);
            // Vous pouvez ajouter des appels similaires pour les autres tableaux si nécessaire
        }
    }

    // Fonction pour changer le matériau en fonction du tableau de niveaux
    private void ChangeMaterial(InfosGame.Levels[] levels)
    {
        // Assurez-vous que le composant Renderer est attaché
        if (_renderer != null && levels != null && levels.Length > 0)
        {
            // Exemple : Changez le matériau en fonction de la valeur de "completed" du premier niveau
            if (levels[0].completed)
            {
                ChangeMaterial(notGlow1);
            }
            else
            {
                ChangeMaterial(glow1);
            }
        }
    }

    // Fonction pour changer le matériau
    private void ChangeMaterial(Material newMaterial)
    {
        // Assurez-vous que le composant Renderer est attaché
        if (_renderer != null)
        {
            // Changez le matériau
            _renderer.material = newMaterial;
        }
    }
}

