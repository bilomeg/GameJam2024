using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeplacementJoueur : MonoBehaviour
{
    [SerializeField] private float vitesse = 3.0f;
    private float mouvementHorizontal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Bouge();
    }

    public void OnMove(InputValue value)
    {
        Vector2 touchesClavier = value.Get<Vector2>();

        mouvementHorizontal = touchesClavier.x * -1;
    }

    void Bouge(){
        transform.Translate(Vector3.forward * Time.deltaTime * vitesse * mouvementHorizontal);
        Debug.Log(mouvementHorizontal);
    }
}
