using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryTrigger : MonoBehaviour {

	public bool heals = false;
    public bool isItEndGameAlready = false;
    [Space]
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
			if(heals)
				other.GetComponent<Damagable> ().health = other.GetComponent<Damagable> ().healthMax;
            gP.progressionPanel.gameObject.SetActive(true);
            gP.progressionText.text = eventText;
            pauseC.isGamePaused = false;
            pauseC.PauseGame();
            this.gameObject.SetActive(false);
            if(isItEndGameAlready)
            {
                gP.progressionText.text = ("Game over! You did very well, only smashed " + gP.thingsSmashed.ToString() + " things and/or pppl :)");
            }

        }
    }


}
