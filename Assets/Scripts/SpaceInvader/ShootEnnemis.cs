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
        InvokeRepeating("TirerProjectile", Random.Range(1.0f, 20.0f), Random.Range(10.0f, 30.0f));
    }

    void TirerProjectile()
    {
        GameObject nouveauProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        nouveauProjectile.GetComponent<Rigidbody>().velocity = Vector3.down * vitesseProjectile;
    }
}

