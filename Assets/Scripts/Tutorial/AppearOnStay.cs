﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearOnStay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<MeshRenderer>().enabled = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<MeshRenderer>().enabled = !true;
    }
}
