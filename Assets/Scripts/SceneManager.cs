using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneManager : MonoBehaviour
{
    [Header("Scene Assignments")]
    [SerializeField] public float fillamount;
    public GameObject StartGamePanel;
    [SerializeField] private Transform Player;
    [SerializeField] private Transform EndLine;
    [SerializeField] private Image sliderimage;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject tryAgainPanel;
    [SerializeField] private GameObject levelCompletedPanel;
    [SerializeField] private Button vibrationOnButton;
    [SerializeField] private Button vibrationOffButton;
    [SerializeField] private Button soundOnButton;
    [SerializeField] private Button soundOffButton;
    public static SceneManager SceneManagerInstance;


   

    
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
        SceneManagerInstance = this;

    }

    // Update is called once per frame
    void Update()
    {
        distance = (EndLine.position.z-Player.position.z);

        fillamount= 1- (distance / endlinefirstpos);
        sliderimage.fillAmount =  fillamount;
        
        
        if(PlayerManager.PlayerManagerInstance.numberofstickman < 1)
        {
            StartCoroutine(TryAgainPanelUpdate());
        }

        StartCoroutine(LevelCompletedPanelUpdate());
    }



    public void ResumeGame()
    {
        Time.timeScale = 1;

        GameStarted = true;
        

        StartGamePanel.SetActive(false);
    }

    public void PauseGame()
    {
        GameStarted = false;
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



    IEnumerator TryAgainPanelUpdate()
    {
        yield return new WaitForSecondsRealtime(2);
        tryAgainPanel.gameObject.SetActive(true);
    }
   
    public void RestartGame()
    {
         UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

    }

    IEnumerator LevelCompletedPanelUpdate()
    {
        

        if ( BossManager.bossManagerInstance.currenthealth < 0)
        {
            PlayerManager.PlayerManagerInstance.roadSpeed =0f;
            PlayerManager.PlayerManagerInstance.playerSpeed=0f;

            yield return new WaitForSecondsRealtime(4f);
            levelCompletedPanel.gameObject.SetActive(true);
            
            
        }
    }

    

}
