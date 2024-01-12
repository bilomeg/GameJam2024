using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // ---------------------
    // Class
    // ---------------------

    public enum LevelSelect
    {
        SpaceInvader,
        DataMiner,
        TowerDefense,
    }

    public enum InteractionType
    {
        Dialogue,
        SceneLoader,
        Pet,
    }

    [System.Serializable]
    public class UIText
    {
        public string interactionText;
        public string actionText;
        public string informationText;
    }

    // ---------------------
    // Variables
    // ---------------------

    [Header("Player Interaction")]
    public InteractionType interactionType;
    public GameObject hoverCamera;
    public UIText uiText;
    [Space(10)]

    [Header("Scene Loader")]
    InfosGame infosGame;
    [SerializeField] LevelSelect levelSelect;

    [Header("Pet")]
    [SerializeField] GameObject particle;
    [SerializeField] AudioSource celebrate;

    // ---------------------
    // Functions
    // ---------------------

    private void Start() {
        infosGame = InfosGame.instance;
    }

    public void ChangeHoverUI(PlayerInteractor interactor)
    {
        if(hoverCamera != null && interactionType == InteractionType.SceneLoader)
        {
            // Set Variables
            hoverCamera.SetActive(true);

            string inputString = InputControlPath.ToHumanReadableString(interactor.playerInput.actions.FindAction("Interact").bindings[0].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);

            // Change All Text
            interactor.uiReferences.interactionText.text = uiText.interactionText + "'" + inputString + "'";
            interactor.uiReferences.actionText.text = uiText.actionText;
            
            interactor.uiReferences.informationText.transform.gameObject.SetActive(false);
            
            if(interactionType == InteractionType.SceneLoader)
            {
                int completed = 0;
                int total = 0;

                if(levelSelect == LevelSelect.SpaceInvader)
                {
                    for(int i  = 0;i < infosGame.spaceInvaders.Length;i++)
                    {
                        // Set Variables
                        total++;

                        if(infosGame.spaceInvaders[i].completed)
                        completed++;
                    }
                }

                if(levelSelect == LevelSelect.DataMiner)
                {
                    for(int i  = 0;i < infosGame.dataMining.Length;i++)
                    {
                        // Set Variables
                        total++;

                        if(infosGame.dataMining[i].completed)
                        completed++;
                    }
                }

                if(levelSelect == LevelSelect.TowerDefense)
                {
                    for(int i  = 0;i < infosGame.towerDefense.Length;i++)
                    {
                        // Set Variables
                        total++;

                        if(infosGame.towerDefense[i].completed)
                        completed++;
                    }
                }

                interactor.uiReferences.informationText.transform.gameObject.SetActive(true);
                interactor.uiReferences.informationText.text = completed + "/" + total + uiText.informationText;
            }

            else if(uiText.informationText != "")
            {
                interactor.uiReferences.informationText.transform.gameObject.SetActive(true);
                interactor.uiReferences.informationText.text = uiText.informationText;
            }

            // Set Canvas Settings

            RectTransform canvas = interactor.uiReferences.canvas;
            canvas.localPosition = new Vector2(0, interactor.uiReferences.canvas.localPosition.y);
            canvas.DOLocalMove(interactor.uiReferences.defaultPos, interactor.uiReferences.transitionTime).SetEase(Ease.InOutCirc);

            canvas.GetComponent<CanvasGroup>().alpha = 0;
            canvas.GetComponent<CanvasGroup>().DOFade(1, interactor.uiReferences.transitionTime).SetEase(Ease.InOutCirc);
        }

        if(interactionType == InteractionType.Pet)
        {
            // Set Variables
            string inputString = InputControlPath.ToHumanReadableString(interactor.playerInput.actions.FindAction("Interact").bindings[0].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);

            // Change All Text
            interactor.uiReferences.interactionText.text = uiText.interactionText + "'" + inputString + "'";
            interactor.uiReferences.actionText.text = uiText.actionText;
            
            interactor.uiReferences.informationText.transform.gameObject.SetActive(false);

            // Set Canvas Settings
            RectTransform canvas = interactor.uiReferences.canvas;
            canvas.localPosition = new Vector2(0, interactor.uiReferences.canvas.localPosition.y);
            canvas.DOLocalMove(interactor.uiReferences.defaultPos, interactor.uiReferences.transitionTime).SetEase(Ease.InOutCirc);

            canvas.GetComponent<CanvasGroup>().alpha = 0;
            canvas.GetComponent<CanvasGroup>().DOFade(1, interactor.uiReferences.transitionTime).SetEase(Ease.InOutCirc);
        }

    }

    public void HideHoverUI(PlayerInteractor interactor)
    {
        if(interactionType == InteractionType.Pet || interactionType == InteractionType.SceneLoader && hoverCamera != null)
        {
            // Set Variables
            hoverCamera.SetActive(false);
            RectTransform canvas = interactor.uiReferences.canvas;

            // Set Canvas Settings
            Vector3 newPos = new Vector2(0, interactor.uiReferences.canvas.localPosition.y);
            canvas.DOLocalMove(newPos, interactor.uiReferences.transitionTime).SetEase(Ease.InOutCirc);
            canvas.GetComponent<CanvasGroup>().DOFade(0, interactor.uiReferences.transitionTime).SetEase(Ease.InOutCirc);
        }
    }

    public void Interact()
    {
        // Scene Loader
        if(interactionType == InteractionType.SceneLoader)
        {
            bool alreadyLoaded = false;
            if(levelSelect == LevelSelect.SpaceInvader)
            {
                for(int i = 0;i < infosGame.spaceInvaders.Length;i++)
                {
                    if(!infosGame.spaceInvaders[i].completed)
                    {
                        alreadyLoaded = true;
                        LevelManager.Instance.LoadAsyncScene(infosGame.spaceInvaders[i].sceneName);
                    }
                }

                if(!alreadyLoaded)
                LevelManager.Instance.LoadAsyncScene(infosGame.spaceInvaders[infosGame.spaceInvaders.Length -1].sceneName);
            }

            if(levelSelect == LevelSelect.TowerDefense)
            {
                for(int i = 0;i < infosGame.towerDefense.Length;i++)
                {
                    if(!infosGame.towerDefense[i].completed)
                    {
                        alreadyLoaded = true;
                        LevelManager.Instance.LoadAsyncScene(infosGame.towerDefense[i].sceneName);
                    }
                }

                if(!alreadyLoaded)
                LevelManager.Instance.LoadAsyncScene(infosGame.towerDefense[infosGame.towerDefense.Length -1].sceneName);
            }

            if(levelSelect == LevelSelect.DataMiner)
            {
                for(int i = 0;i < infosGame.dataMining.Length;i++)
                {
                    if(!infosGame.dataMining[i].completed)
                    {
                        alreadyLoaded = true;
                        LevelManager.Instance.LoadAsyncScene(infosGame.dataMining[i].sceneName);
                    }
                }

                if(!alreadyLoaded)
                LevelManager.Instance.LoadAsyncScene(infosGame.dataMining[infosGame.dataMining.Length -1].sceneName);
            }
        }

        if(interactionType == InteractionType.Pet)
        {
            // Create Particle
            var instance = Instantiate(particle, transform);
            instance.name = "YOOPIII";

            // Play Audio
            celebrate.Play();
        }
    }
}
