using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLightDelay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.parent.parent.position, transform.parent.position, 0.05f);
	}
}
