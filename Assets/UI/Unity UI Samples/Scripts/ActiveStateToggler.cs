using UnityEngine;
using System.Collections;

public class ActiveStateToggler : MonoBehaviour {
	[SerializeField]  GameObject _indicationsMining;

   private void Start()
    {
        // Appeler la fonction DelayedDisable après 15 secondes
        Invoke("DelayedDisable", 15f);
    }
	public void ToggleActive () {
		gameObject.SetActive (!gameObject.activeSelf);

		
	}
	    private void DelayedDisable()
    {
		if(_indicationsMining != null)  {
			  gameObject.SetActive(false);
		}
        // Désactiver le GameObject après 15 secondes
      
    }
}
