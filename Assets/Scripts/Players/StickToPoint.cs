using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToPoint : MonoBehaviour {

    public Transform Point;
    
	// Update is called once per frame
	void LateUpdate () {
        transform.position = Point.position;
	}
}
