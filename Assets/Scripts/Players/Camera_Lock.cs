using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Lock : MonoBehaviour {
    public GameObject target;
    public GameObject Player;
    public float offset;
    public float camSpeed=20f;
	// Use this for initialization
	void Start () {
        target = Player;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, camSpeed*Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, offset);
	}
    public void RestartTarget()
    {
        target = Player;
    }
}
