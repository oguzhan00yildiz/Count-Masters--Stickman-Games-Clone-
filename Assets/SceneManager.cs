using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public GameObject StartGamePanel;
    [SerializeField] Transform Player;
    [SerializeField] Transform EndLine;
    [SerializeField] Image sliderimage;
    private float scaledfloat;
     private float i ;
    
    

    public bool GameStarted = false;
    
    void Start()
    {

        PauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        scaledfloat = (Player.position.z) / (EndLine.position.z - Player.position.z);
       if (sliderimage.fillAmount < 1)
       {
    
       
            sliderimage.fillAmount = -scaledfloat; 
       }

       
       
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

   

    

}
