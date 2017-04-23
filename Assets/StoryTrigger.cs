using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryTrigger : MonoBehaviour {

    public string eventText;

    private GameProgressionController gP;
    private PauseController pauseC;

    void Start ()
    {
        gP = GameObject.Find("GameController").GetComponent<GameProgressionController>();
		pauseC = GameObject.Find("GameController").GetComponent<PauseController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gP.progressionPanel.gameObject.SetActive(true);
            gP.progressionText.text = eventText;
            pauseC.isGamePaused = false;
            pauseC.PauseGame();
            this.gameObject.SetActive(false);

        }
    }


}
