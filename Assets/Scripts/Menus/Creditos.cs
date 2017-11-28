using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creditos : MonoBehaviour {

	[Range(1, 100)]
	public float VelocidadCreditos = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(0.0f, VelocidadCreditos* Time.deltaTime, 0.0f));
	}
}
