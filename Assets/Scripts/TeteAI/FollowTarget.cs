using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [Tooltip("Target to follow.")]
    public Transform target;
 
    [Tooltip("True if this object should rotate to face the target.")]
    public bool lookAtTarget;

 
    // done in LateUpdate to allow the target to have the chance to move first in Update
    private void LateUpdate()
    {
        target = GameObject.Find("Joueur").transform;
        if(lookAtTarget){
            transform.LookAt(target);
        }
    }
}
