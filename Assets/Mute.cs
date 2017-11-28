using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mute : MonoBehaviour {
    public Sprite Muted;
    public Sprite NoMuted;
    AudioSource[] audios;
    public bool muted;
    // Use this for initialization
    void Start () {
        muted = AudioListener.volume == 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (muted)
            GetComponent<Image>().sprite = Muted;
        else
            GetComponent<Image>().sprite = NoMuted;
    }
    public void mute()
    {
        if (muted)
            AudioListener.volume = 1;
        else
            AudioListener.volume = 0;
        muted = !muted;
    }
}
