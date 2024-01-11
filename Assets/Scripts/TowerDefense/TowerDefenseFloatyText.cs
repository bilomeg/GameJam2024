using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class TowerDefenseFloatyText : MonoBehaviour
{
    // ---------------------
    // Variables
    // ---------------------

    [SerializeField] float moveUpYAxis;
    [SerializeField] float duration;

    // ---------------------
    // Functions
    // ---------------------

    void Start()
    {
        transform.DOMoveY(transform.position.y + moveUpYAxis, duration).SetEase(Ease.InOutCirc);
        StartCoroutine("Destroy");
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(duration *2 /3);
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOFade(0, duration/3);
        
        yield return new WaitForSeconds(duration /3);
        Destroy(gameObject);
    }
}
