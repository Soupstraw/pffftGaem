using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    public Sprite newSprite;
    public GameObject particleEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
           gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
            particleEffect.gameObject.SetActive(true);
            //gameObject.GetComponent<Sprite>() =
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
