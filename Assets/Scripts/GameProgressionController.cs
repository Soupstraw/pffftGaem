using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressionController : MonoBehaviour
{
    public GameObject progressionPanel;
    public Text progressionText;
    private PauseController pauseC;
    //public GameObject pauseController;

    private void Start()
    {
        pauseC = gameObject.GetComponent<PauseController>();
        EnableProgressionPanel();
    }

    public void EnableProgressionPanel()
    {
        progressionPanel.gameObject.SetActive(true);
        
        progressionText.text = "hahaha test";
        pauseC.isGamePaused = false;
        pauseC.PauseGame();
       
    }


}
