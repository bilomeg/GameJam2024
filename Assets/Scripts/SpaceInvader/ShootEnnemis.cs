using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShootEnnemis : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float vitesseProjectile = 3.0f;
    [SerializeField] private AudioSource _sonEnnemiesGunshot;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("TirerProjectile", Random.Range(3.0f, 20.0f), Random.Range(10.0f, 30.0f));

        ApparitionEnnemies();
    }

    void TirerProjectile()
    {
        GameObject nouveauProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        _sonEnnemiesGunshot.Play();
        nouveauProjectile.GetComponent<Rigidbody>().velocity = Vector3.down * vitesseProjectile;
    }

    void ApparitionEnnemies(){
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y + 10, pos.z);
        transform.DOMove(pos, 3).SetEase(Ease.InOutCirc);
    }
}

