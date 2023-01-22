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
    [SerializeField] private Button vibrationonbutton;
    [SerializeField] private Button vibrationoffbutton;

    
    private float endlinefirstpos;
    private float distance;

    public bool GameStarted = false;
    
    void Start()
    {
        PauseGame();

        settingsPanel.SetActive(false);
         distance = (EndLine.position.z-Player.position.z);
        endlinefirstpos = distance;


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
    vibrationonbutton.gameObject.SetActive(false);
    vibrationoffbutton.gameObject.SetActive(true);

        //titreşimi aç
    }

    public void VibrationOff()
    {
    vibrationonbutton.gameObject.SetActive(true);
    vibrationoffbutton.gameObject.SetActive(false);


        //titreşimi kapat
    }




   

    

}
