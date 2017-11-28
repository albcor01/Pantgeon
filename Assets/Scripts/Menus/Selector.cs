using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0.0f, 0.0f,Time.deltaTime * 100f));
		transform.Rotate (new Vector3 (Time.deltaTime * 100f, 0.0f,0.0f));
		transform.Rotate (new Vector3 (0.0f, Time.deltaTime * -100f,0.0f));
	}
}
