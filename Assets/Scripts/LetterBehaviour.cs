using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBehaviour : MonoBehaviour
{

    public List<Sprite> letterSprites = new List<Sprite>();

    private int rand = 0;
    private int rand2 = 0;


    void Start()
    {

        rand2 = Random.Range(0, 4);
        if (rand2 == 0) 
        {
            rand = Random.Range(0, letterSprites.Count);

            SpriteRenderer sr = this.gameObject.AddComponent<SpriteRenderer>();
            sr.sprite = letterSprites[rand];

        }else
        {
            Destroy(this.gameObject);
        }
    }


}
