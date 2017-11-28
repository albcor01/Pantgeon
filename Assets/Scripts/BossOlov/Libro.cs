using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libro : MonoBehaviour {
    GameObject olov;
	// Use this for initialization
	void Start () {
        olov = FindObjectOfType<Olov>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
		if (col.gameObject.GetComponent<Player> ()) 
		{
			col.GetComponent<Player> ().dead = true;
		}


		if(col.GetComponent<Shoot>())
			
		if (col.GetComponent<Shoot>().mov &&  !olov.GetComponent<Olov>().tangible)
        {
            olov.GetComponent<Olov>().HitLibro();
                       
        }
    }
        }
