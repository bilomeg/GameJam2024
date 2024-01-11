using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
                    Vector3 newLookAtRot = new Vector3(0, lookAt.rotation.y);

                    rotateYGameObject.transform.DORotateQuaternion(Quaternion.Euler(newLookAtRot), defender.fireRate).SetEase(Ease.InOutCirc);
                }

                yield return new WaitForSeconds(defender.fireRate);

                // Set Variables
                shooting = false;

                // Call Functions
                if(currentEnemy != null)
                currentEnemy.GetComponent<TowerDefenseObjects>().LoseHealth(defender.damage);
                StartCoroutine("Shoot");
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
            Debug.Log("are u even called?");
            TowerDefenseObjects otherObject = other.GetComponent<TowerDefenseObjects>();
            if(otherObject.objectType == TowerDefenseObjects.ObjectType.Enemy)
            {
                Debug.Log("are u even called? 2");
                bool alreadyImplemented = false;
                for(int i = 0;i < collidingObjects.Length;i++)
                {
                    Debug.Log("are u even called? 3");
                    if(!alreadyImplemented && collidingObjects[i] == null)
                    {
                        Debug.Log("are u even called? 4");
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
