using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressionController : MonoBehaviour
{
    public GameObject progressionPanel;
    public Text progressionText;
    private string pauseC;
    //public GameObject pauseController;

    private void Start()
    {
        EnableProgressionPanel();
    }

    public void EnableProgressionPanel()
    {
        progressionPanel.gameObject.SetActive(true);
        
        progressionText.text = "hahaha test";
        gameObject.GetComponent<PauseController>().isGamePaused = false;
        gameObject.GetComponent<PauseController>().PauseGame();
    }


}
