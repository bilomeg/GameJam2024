using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class TowerDefenseHitArea : MonoBehaviour
{
    // ---------------------
    // Class
    // ---------------------

    [System.Serializable]
    public class AnimationInfo
    {
        public Animator defenderAnimator;
        [Space(5)]

        public string idle;
        public string shooting;
        [Space(10)]

        public GameObject shootParticle;
        public Transform particleTransform;
    }
    // ---------------------
    // Variables
    // ---------------------

    TowerDefenseObjects defender;
    bool shooting = false;
    

    [SerializeField] GameObject[] collidingObjects;

    [Header("Hit Area Informations")]
    [SerializeField] GameObject rotateYGameObject;
    [SerializeField] Transform lookAt;
    [Space(5)]

    [SerializeField] float xOffset;

    [SerializeField] AnimationInfo animationInfo;

    // ---------------------
    // Functions
    // ---------------------

    // Start Functions
    // ---------------------

    void Start()
    {
        // Set Variables
        /*if(animationInfo.defenderAnimator == null)
        animationInfo.defenderAnimator = GetComponent<Animator>();*/

        defender = transform.parent.GetComponent<TowerDefenseObjects>();
    }

    // Array Functions
    // ---------------------

    void SpliceCollidingAray(int index)
    {
        // Splice Entire Array
        collidingObjects[index] = null;
        for(int i = index + 1;i < collidingObjects.Length;i++)
        {
            if(collidingObjects[i] != null)
            {
                collidingObjects[i -1] = collidingObjects[i];
                collidingObjects[i] = null;
            }
        }
    }
    
    // Shooting Functions
    // ---------------------

    IEnumerator Shoot()
    {
        if(!shooting)
        {
            if(collidingObjects[0] != null)
            {
                GameObject currentEnemy = collidingObjects[0];

                // Set Variables
                shooting = true;

                // Play Animation
                if(animationInfo.defenderAnimator != null)
                animationInfo.defenderAnimator.SetTrigger(animationInfo.shooting);

                // Rotate Y Axis
                if(rotateYGameObject != null)
                {
                    lookAt.LookAt(currentEnemy.transform.position);
                    Vector3 newRot = new Vector3(lookAt.localRotation.eulerAngles.y, 90, 90);
                    rotateYGameObject.transform.DOLocalRotateQuaternion(Quaternion.Euler(newRot), defender.fireRate).SetEase(Ease.InOutCirc);
                }

                yield return new WaitForSeconds(defender.fireRate);

                // Particles
                if(animationInfo.shootParticle != null)
                {
                    var instance = Instantiate(animationInfo.shootParticle, animationInfo.particleTransform);
                    instance.name = animationInfo.shootParticle.name;
                    instance.transform.parent = null;
                }

                // Audio
                if(GetComponent<AudioSource>() != null)
                GetComponent<AudioSource>().Play();

                // Set Variables
                shooting = false;

                // Call Functions
                if(currentEnemy != null)
                currentEnemy.GetComponent<TowerDefenseObjects>().LoseHealth(defender.damage);
                StartCoroutine("Shoot");
            }

            else
            {
                bool hasOtherInside = false;
                for(int i = 0;i < collidingObjects.Length;i++)
                if(collidingObjects[i] != null)
                hasOtherInside = true;

                if(hasOtherInside)
                {
                    SpliceCollidingAray(0);
                    StartCoroutine("Shoot");
                }
            }

        }
    }
    // ---------------------
    // Collision
    // ---------------------

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TowerDefenseObjects>() != null)
        {
            TowerDefenseObjects otherObject = other.GetComponent<TowerDefenseObjects>();
            if(otherObject.objectType == TowerDefenseObjects.ObjectType.Enemy)
            {
                bool alreadyImplemented = false;
                for(int i = 0;i < collidingObjects.Length;i++)
                {
                    if(!alreadyImplemented && collidingObjects[i] == null)
                    {
                        alreadyImplemented = true;
                        collidingObjects[i] = other.gameObject;

                        StartCoroutine("Shoot");
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<TowerDefenseObjects>() != null)
        {
            TowerDefenseObjects otherObject = other.GetComponent<TowerDefenseObjects>();
            if(otherObject.objectType == TowerDefenseObjects.ObjectType.Enemy)
            {
                for(int i = 0;i < collidingObjects.Length;i++)
                {
                    if(collidingObjects[i] == other.gameObject)
                    SpliceCollidingAray(i);
                }
            }
        }
    }
}
