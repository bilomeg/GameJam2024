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
    private bool canShoot = true;
    [SerializeField] private float intervalShoot;

    //Pour l'animation de tournant
    [SerializeField] private Animator animateur;
    [SerializeField] private GameObject vaisseau;
    Quaternion vaisseauRotation;

    [SerializeField] private AudioSource _sonShipGunshot;

    // Start is called before the first frame update
    void Start()
    {
        vaisseauRotation = vaisseau.transform.rotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        //BougeProjectiles();
        Bouge();

        Debug.Log(mouvementHorizontal);
    }

    public void OnMove(InputValue value)
    {
        Vector2 touchesClavier = value.Get<Vector2>();

        mouvementHorizontal = touchesClavier.x * -1;
    }

    public void OnShoot(InputValue value)
    {
        if(canShoot){
            Instantiate(projectile, transform.position, Quaternion.identity);
            _sonShipGunshot.Play();
            canShoot = false;
            Invoke("IntervalShoot", intervalShoot);
        }
    }

    void Bouge(){
        transform.Translate(Vector3.forward * Time.deltaTime * vitesse * mouvementHorizontal);
        Quaternion newRotation = Quaternion.Euler(new Vector3(vaisseauRotation.x,25 * mouvementHorizontal, -90));
        vaisseau.transform.localRotation = newRotation;
    }

    private void IntervalShoot(){
        canShoot = true;
    }

}
