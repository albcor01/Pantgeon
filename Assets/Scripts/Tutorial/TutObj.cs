using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutObj : MonoBehaviour {
    public int order;
    public bool Active4Evah;
    //TutoManager tutMang;

	// Use this for initialization
	void Start () {
        //tutMang = FindObjectOfType<TutoManager>();
       // gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewWave(int waveNum)
    {
        gameObject.SetActive(waveNum == order|| Active4Evah&&waveNum>=order);
    }

}
