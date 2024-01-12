using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ZoneUIAccueil : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    private LevelManager _levelManager;
       // Start is called before the first frame update
    void Start()
    {
        _levelManager = LevelManager.Instance;
        
    }
    public void DebutGame(){
         
        _levelManager.LoadAsyncScene("SceneSallePrincipale");
      
    }

    public void MasterVolumeChange(Slider slider)
    {
        float value = slider.value * 80;
        audioMixer.SetFloat("MasterVolume", value -80);
    }

    public void MusicVolumeChange(Slider slider)
    {
        float value = slider.value * 80;
        audioMixer.SetFloat("MusicVolume", value -80);
    }

    public void SFXVolumeChange(Slider slider)
    {
        float value = slider.value * 80;
        audioMixer.SetFloat("SFXVolume", value -80);
    }

   public void QuiteGame()
    {
        Debug.Log($"fonctionne");
        Application.Quit();
    }


    
}
