using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine;
using TreeEditor;

public class TowerDefensePlayer : MonoBehaviour
{
    // ---------------------
    // Class
    // ---------------------

    [System.Serializable]
    public class CameraInfo
    {
        public GameObject cameraObject;
        public Vector3 cameraOffset;
        [Space(5)]

        public float transitionTime;
    }

    // ---------------------
    // Variables
    // ---------------------

    public TowerDefenseLevelInfo levelInfo;

    [Header("Player's Info")]
    [SerializeField] CameraInfo cameraInfo;
    [Space(5)]

    [SerializeField] Color hoverColor;
    [SerializeField] float hoverTransitionTime;
    Color defaultColor;

    [Header("Movement's Information")]
    [SerializeField] float moveSpeed;

    // Player Input
    Vector2 moveInput;
    bool selectingObject;
    bool isUIOpen;

    // Defender
    TowerDefenseObjects hovering;
    TowerDefenseObjects hologram;
    

    // ---------------------
    // Functions
    // ---------------------

    // Start Functions
    // ---------------------

    void Start()
    {
        // Set Variables
        if(cameraInfo.cameraObject == null) cameraInfo.cameraObject = GameObject.Find("Main Camera");
    }

    // Update Functions
    // ---------------------

    void FixedUpdate()
    {
        // Call Functions
        Move();
        RestrainMovement();
    }

    // Movement Functions
    // ---------------------

    void RestrainMovement()
    {
        Vector3 newPos = transform.position;

        // X Axis
        if(transform.position.x < levelInfo.levelLimit.minPos.x)
        newPos.x = levelInfo.levelLimit.minPos.x;

        if(transform.position.x > levelInfo.levelLimit.maxPos.x)
        newPos.x = levelInfo.levelLimit.maxPos.x;

        // Z Axis
        if(transform.position.z < levelInfo.levelLimit.minPos.y)
        newPos.z = levelInfo.levelLimit.minPos.y;

        if(transform.position.z > levelInfo.levelLimit.maxPos.y)
        newPos.z = levelInfo.levelLimit.maxPos.y;

        transform.position = newPos;
    }

    void Move()
    {
        // Translate Player
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    void MoveCameraToPlayer()
    {
        if(!selectingObject)
        {
            // Move Camera To Player with DOTween
            Vector3 newPos = transform.position + cameraInfo.cameraOffset;
            cameraInfo.cameraObject.transform.DOMove(newPos, cameraInfo.transitionTime).SetEase(Ease.InOutCirc);
        }
        
    }

    // Defender Functions
    // ---------------------

    void StartHovering(TowerDefenseObjects defenseObject)
    {
        // Set Variables
        hovering = defenseObject;
        Debug.Log(hovering);

        // --------------------------------------------
        // Add Hover Effect or Display Information Text 
        // --------------------------------------------

        Renderer objectRenderer = defenseObject.transform.gameObject.GetComponent<Renderer>();
        defaultColor = objectRenderer.material.color;

        objectRenderer.material.DOColor(hoverColor, hoverTransitionTime).SetEase(Ease.InOutCirc);
    }

    void StopHovering()
    {
        // --------------------------------------
        // Remove Hover Effect or Informaion Text
        // --------------------------------------

        Renderer objectRenderer = hovering.transform.gameObject.GetComponent<Renderer>();
        objectRenderer.material.DOColor(defaultColor, hoverTransitionTime).SetEase(Ease.InOutCirc);

        // Set Variables
        hovering = null;
        Debug.Log(hovering);
    }

    void SelectCurrentObject()
    {
        if(hovering != null && !selectingObject)
        {
            // Instantiate
            var instance = Instantiate(hovering.defenderHologram, transform.position, hovering.defenderHologram.transform.rotation);
            instance.name = "Hologram";
            instance.transform.SetParent(transform);

            hologram = instance.GetComponent<TowerDefenseObjects>();
            hologram.defenderGameObject = hovering.transform.gameObject;

            // Set Variables
            selectingObject = true;
            StopHovering();
        }

        else if(selectingObject)
        {
            // Check if Colliding With other Objects
            bool canPlace = true;
            for(int i = 0;i < hologram.collidingObjects.Length;i++)
            {
                if(hologram.collidingObjects[i] != null)
                canPlace = false;
            }

            if(canPlace)
            {
                Vector3 newPos = new Vector3(transform.position.x, levelInfo.floorLevel, transform.position.z);
                var instance = Instantiate(hologram.defenderGameObject, newPos, hologram.defenderGameObject.transform.rotation);
                instance.name = hologram.defenderGameObject.name;

                instance.GetComponent<Renderer>().material.color = defaultColor;

                Destroy(hologram.defenderGameObject);
                Destroy(hologram.transform.gameObject);

                // Set Variables
                selectingObject = false;
            }

            else
            {
                Debug.Log("Can't be placed");
            }
        }
    }

    // ---------------------
    // Player Input
    // ---------------------

    public void OnMove(InputValue value)
    {
        // Set Variables
        moveInput = value.Get<Vector2>();
    }

    public void OnMoveCamera()
    {
        // Call Functions
        MoveCameraToPlayer();
    }

    public void OnStartMission()
    {

    }

    public void OnSelect()
    {
        // Call Functions
        SelectCurrentObject();
    }

    public void OnSell()
    {
        
    }

    public void OnOpenMenu()
    {

    }

    public void OnCancel()
    {

    }

    // ---------------------
    // Collision
    // ---------------------

    void OnTriggerEnter(Collider other)
    {
        // If Other Has TowerDefenseObjects
        if(other.GetComponent<TowerDefenseObjects>() != null)
        {
            TowerDefenseObjects otherObject = other.GetComponent<TowerDefenseObjects>();

            // Defender
            if(otherObject.objectType == TowerDefenseObjects.ObjectType.Defender && !selectingObject)
            {
                if(hovering != null)
                StopHovering();

                StartHovering(otherObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // If Other Has TowerDefenseObjects
        if(other.GetComponent<TowerDefenseObjects>() != null)
        {
            TowerDefenseObjects otherObject = other.GetComponent<TowerDefenseObjects>();

            // Defender
            if(otherObject.objectType == TowerDefenseObjects.ObjectType.Defender)
            {
                if(hovering == otherObject)
                StopHovering();
            }
        }
    }
}
