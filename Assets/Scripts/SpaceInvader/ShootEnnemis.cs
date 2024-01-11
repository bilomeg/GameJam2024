using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnnemis : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float vitesseProjectile = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Démarrez l'invocation répétée avec un délai initial aléatoire
        InvokeRepeating("TirerProjectile", Random.Range(1.0f, 20.0f), Random.Range(10.0f, 30.0f));
    }

    // Update is called once per frame
    void Update()
    {
        // Vous pouvez ajouter un comportement d'update ici si nécessaire
    }

    void TirerProjectile()
    {
        GameObject nouveauProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

        // Appliquez le mouvement directement au projectile au lieu d'appeler une fonction séparée
        nouveauProjectile.GetComponent<Rigidbody>().velocity = Vector3.down * vitesseProjectile;

        // Ajustez ici si le projectile nécessite un comportement supplémentaire lorsqu'il est tiré

        // Ne conservez pas la référence au projectile si vous n'en avez pas besoin pour d'autres raisons
        // Vous pouvez également ajouter ici une logique pour détruire le projectile après un certain temps ou à un certain emplacement
    }
}

