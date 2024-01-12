using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using TMPro;

public class PlayerInteractor : MonoBehaviour
{
    // ---------------------
    // Class
    // ---------------------

    [System.Serializable]
    public class UIReferences
    {
        public RectTransform canvas;
        [Space(5)]

        public TextMeshProUGUI interactionText;
        public TextMeshProUGUI actionText;
        public TextMeshProUGUI informationText;
        [Space(5)]

        public float transitionTime;
        [HideInInspector] public Vector3 defaultPos;
    }

    // ---------------------
    // Variables
    // ---------------------

    [HideInInspector] public PlayerInput playerInput;
    PlayerInteraction interactingWith;

    public UIReferences uiReferences;

    // ---------------------
    // Functions
    // ---------------------

    // Start Functions
    // ---------------------

    void Start()
    {
        // Set Variables
        playerInput = GetComponent<PlayerInput>();
        uiReferences.defaultPos = uiReferences.canvas.localPosition;
    }

    // Interaction Functions
    // ---------------------

    void SetInteraction(PlayerInteraction interaction)
    {
        // Set Variables
        interactingWith = interaction;

        // Call Functions
        interactingWith.ChangeHoverUI(this);
    }

    void RemoveInteraction()
    {
        // Call Functions
        interactingWith.HideHoverUI(this);

        // Set Variables
        interactingWith = null;
    }

    // ---------------------
    // Input System
    // ---------------------

    public void OnInteract()
    {
        // Call Functions
        if(interactingWith != null)
        interactingWith.Interact();
    }

    // ---------------------
    // Collision Detection
    // ---------------------

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerInteraction>() != null)
        {
            SetInteraction(other.GetComponent<PlayerInteraction>());
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerInteraction>() != null)
        {
            RemoveInteraction();
        }
    }
}
