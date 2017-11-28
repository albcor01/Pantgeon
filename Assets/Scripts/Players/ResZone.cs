using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResZone : MonoBehaviour {

	GameManager gm;

	// Use this for initialization
	void Start () {
		gm = FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Collider2D> ().enabled = !transform.GetComponentInParent<Player> ().dead;
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.gameObject.GetComponent<Player> ()) 
		{
//			Debug.Log ("hey nene");

			if(other.transform.GetComponentInParent<Player> ().dead)
				gm.quitaVida (1);

			other.transform.GetComponentInParent<Player> ().dead = false;

		}
	}

}
