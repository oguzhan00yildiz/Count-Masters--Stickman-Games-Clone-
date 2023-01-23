using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneManager : MonoBehaviour
{
    public GameObject StartGamePanel;
    [SerializeField] private Transform Player;
    [SerializeField] private Transform EndLine;
    [SerializeField] private Image sliderimage;

    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button vibrationOnButton;
    [SerializeField] private Button vibrationOffButton;
    [SerializeField] private Button soundOnButton;
    [SerializeField] private Button soundOffButton;

    
    private float endlinefirstpos;
    private float distance;

    public bool GameStarted = false;
    
    void Start()
    {
        PauseGame();

        settingsPanel.SetActive(false);
        distance = (EndLine.position.z-Player.position.z);
        endlinefirstpos = distance;
        soundOffButton.gameObject.SetActive(false);
        vibrationOffButton.gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        distance = (EndLine.position.z-Player.position.z);

        sliderimage.fillAmount = 1-(distance / endlinefirstpos);
        
    }



    public void ResumeGame()
    {
        Time.timeScale =1;

        GameStarted = true;
        

        StartGamePanel.SetActive(false);
    }

    public void PauseGame()
    {
        GameStarted =false;
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void VibrationOn()
    {
        vibrationOnButton.gameObject.SetActive(false);
        vibrationOffButton.gameObject.SetActive(true);

        //titreşimi aç
    }

    public void VibrationOff()
    {
        vibrationOnButton.gameObject.SetActive(true);
        vibrationOffButton.gameObject.SetActive(false);


        //titreşimi kapat
    }

    public void SoundOn()
    {
        soundOnButton.gameObject.SetActive(false);
        soundOffButton.gameObject.SetActive(true);

        //ses aç
    }

    public void SoundOff()
    {
        soundOnButton.gameObject.SetActive(true);
        soundOffButton.gameObject.SetActive(false);

        //ses kapat
    }




   

    

}
