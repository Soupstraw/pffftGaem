using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {

    public bool isGamePaused = false;
	
	void Start () {
		
	}
	
	
	void Update () {
		
	}

    public void PauseGame()
    {
        if(!isGamePaused)
        {
            Time.timeScale = 0;
            isGamePaused = true;
        }
        else if(isGamePaused)
        {
            Time.timeScale = 1;
            isGamePaused = false;
        }

    }
}
