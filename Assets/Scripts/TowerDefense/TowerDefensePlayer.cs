using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine;
using Unity.VisualScripting;


public class TowerDefensePlayer : MonoBehaviour
{
    // ---------------------
    // Class
    // ---------------------

    [System.Serializable]
    public class Sound
    {
        public AudioSource confirmed;
        public AudioSource denied;
        public AudioSource purchased;
        public AudioSource hover;
        public AudioSource hologram;
    }

    [System.Serializable]
    public class AnimationInfo
    {
        public Animator animator;
        [Space(5)]

        public string idle;
        public string hover;
    }

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

    [HideInInspector] public TowerDefenseLevelInfo levelInfo;
    [HideInInspector] public TowerDefenseManager towerManager;
    [SerializeField] GameObject defaultDefender;
    [SerializeField] Sound sound;

    [Header("Player's Info")]
    public int currentMoney;
    public int currentScore;
    [Space(5)]
    
    [SerializeField] CameraInfo cameraInfo;
    [Space(5)]

    [SerializeField] Color hoverColor;
    [SerializeField] float hoverTransitionTime;
    Color defaultColor;

    [Header("Movement's Information")]
    [SerializeField] float moveSpeed;
    [SerializeField] AnimationInfo animationInfo;

    // Player Input
    
    PlayerInput playerInput;
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
        playerInput = GetComponent<PlayerInput>();
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
        // Move Camera To Player with DOTween
        Vector3 newPos = transform.position + cameraInfo.cameraOffset;
        cameraInfo.cameraObject.transform.DOMove(newPos, cameraInfo.transitionTime).SetEase(Ease.InOutCirc);
    }

    // Defender Functions
    // ---------------------

    void StartHovering(TowerDefenseObjects defenseObject)
    {
        // Set Variables
        hovering = defenseObject;
        animationInfo.animator.SetTrigger(animationInfo.hover);
        sound.hover.Play();
    }

    void StopHovering()
    {
        // Set Variables
        hovering = null;
        animationInfo.animator.SetTrigger(animationInfo.idle);
    }

    void SelectCurrentObject()
    {
        if(hovering != null && !selectingObject)
        {
            // Call Functions
            MoveCameraToPlayer();

            // Instantiate
            var instance = Instantiate(hovering.defenderHologram, transform.position, hovering.defenderHologram.transform.rotation);
            instance.name = "Hologram";
            instance.transform.SetParent(transform);

            hologram = instance.GetComponent<TowerDefenseObjects>();
            hologram.defenderGameObject = hovering.transform.gameObject;

            // Set Camera To Player
            cameraInfo.cameraObject.transform.SetParent(gameObject.transform);
            cameraInfo.cameraObject.transform.DOLocalMove(cameraInfo.cameraOffset, cameraInfo.transitionTime);

            // Set Variables
            selectingObject = true;
            StopHovering();

            sound.hologram.Play();
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

                // If Defender isn't in Hierachy Purchase Defender
                if(hologram.defenderGameObject.activeInHierarchy)
                {
                    sound.confirmed.Play();
                    Destroy(hologram.defenderGameObject);
                    Destroy(hologram.transform.gameObject);
                }
                else
                {
                    if(currentMoney < hologram.defenderCost)
                    {
                        sound.denied.Play();
                        Destroy(instance);
                        Destroy(hologram.transform.gameObject);
                    }

                    else
                    {
                        currentMoney -= hologram.defenderCost;
                        towerManager.RefreshUI();
                        sound.purchased.Play();

                        // Destroy Object
                        Destroy(hologram.transform.gameObject);
                    }
                }

                // Set Variables
                selectingObject = false;

                // Remove Camera from Player
                cameraInfo.cameraObject.transform.parent = null;
            }

            else
            {
                Debug.Log("Can't be placed");
            }
        }
    }

    void CancelCurrentObject()
    {
        if(selectingObject)
        {
            // Set Variables
            selectingObject = false;

            // Destroy Object
            Destroy(hologram.transform.gameObject);

            // Play Audio
            sound.denied.Play();

            // Remove Camera from Player
            cameraInfo.cameraObject.transform.parent = null;
        }

        else
        Debug.Log("Nothing is currently selected");
    }

    public void PurchaseDefender(GameObject defenderPrefab)
    {
        if(hovering != null)
        StopHovering();

        hovering = defenderPrefab.GetComponent<TowerDefenseObjects>();
        SelectCurrentObject();
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
        if(!selectingObject)
        MoveCameraToPlayer();
    }

    public void OnStartMission()
    {
        // Call Functions
        if(!towerManager.started)
        towerManager.StartMission();
    }

    public void OnSelect()
    {
        // Call Functions
        SelectCurrentObject();
    }

    public void OnOpenMenu()
    {
        if(!selectingObject && hovering == null)
        PurchaseDefender(defaultDefender);
    }

    public void OnCancel()
    {
        CancelCurrentObject();
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
