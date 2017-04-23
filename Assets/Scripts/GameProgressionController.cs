using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressionController : MonoBehaviour
{
    public int thingsSmashed = 0;
    [Space]
    public GameObject progressionPanel;
    public Text progressionText;
    public Text smashedThingsCount;

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
        
        progressionText.text = "is much amazing!!!";
        pauseC.isGamePaused = false;
        pauseC.PauseGame();
       
    }


}
