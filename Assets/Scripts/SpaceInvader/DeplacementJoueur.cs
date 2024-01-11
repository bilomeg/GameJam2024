using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeplacementJoueur : MonoBehaviour
{
    //Pour le déplacement du joueur
    [SerializeField] private float vitesse = 3.0f;
    private float mouvementHorizontal;

    //Pour le shoot
    [SerializeField] private GameObject projectile;
    [SerializeField] private float vitesseProjectile = 3.0f;
     private List<GameObject> projectilesEnMouvement = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BougeProjectiles();
        Bouge();
    }

    public void OnMove(InputValue value)
    {
        Vector2 touchesClavier = value.Get<Vector2>();

        mouvementHorizontal = touchesClavier.x * -1;
    }

    public void OnShoot(InputValue value)
    {
        GameObject nouveauProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        projectilesEnMouvement.Add(nouveauProjectile);
    }

    void Bouge(){
        transform.Translate(Vector3.forward * Time.deltaTime * vitesse * mouvementHorizontal);
    }

    void BougeProjectiles()
    {
        for (int i = 0; i < projectilesEnMouvement.Count; i++)
        {
            if (projectilesEnMouvement[i] != null)
            {
                projectilesEnMouvement[i].transform.Translate(Vector3.up * Time.deltaTime * vitesseProjectile);
            }
            else
            {
                // Retirer le projectile s'il a été détruit
                projectilesEnMouvement.RemoveAt(i);
                i--;
            }
        }
    }
}
