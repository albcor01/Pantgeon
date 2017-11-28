using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dianas : MonoBehaviour {
    public Sprite Off;
	// Use this for initialization
	void OnEnable () {
        FindObjectOfType<TutoManager>().Dianas.Add(this.gameObject);
        print("Añadiendo diana a la lista...");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Daño()
    {
        GetComponent<SpriteRenderer>().sprite = Off;
        FindObjectOfType<TutoManager>().Dianas.Remove(this.gameObject);
    }
}
