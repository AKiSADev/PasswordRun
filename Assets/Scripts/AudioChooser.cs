using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChooser : MonoBehaviour
{

    public List<AudioClip> clips;

    private int rand = 0;

    void Awake()
    {
        rand = Random.Range(0, clips.Count);
        this.gameObject.GetComponent<AudioSource>().clip = clips[rand];
        this.gameObject.GetComponent<AudioSource>().Play();
    }
}
