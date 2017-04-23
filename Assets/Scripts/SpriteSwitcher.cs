using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    public Sprite newSprite;
    public GameObject bloodSplatter;
    [Space]
    public bool isSquishyMeatBag = false;
    [Space]
    public GameObject particleEffect;

    private GameProgressionController gP;

    void Start()
    {
        gP = GameObject.Find("GameController").GetComponent<GameProgressionController>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
           gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
            particleEffect.gameObject.SetActive(true);
            gP.thingsSmashed += 1;
            gP.smashedThingsCount.text = gP.thingsSmashed.ToString();
            if (isSquishyMeatBag)
            {
                bloodSplatter.gameObject.SetActive(true);
            }
            //gameObject.GetComponent<Sprite>() =
        }
    }


    
}
