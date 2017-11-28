using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuesoBala : MonoBehaviour {

	Boss Boss;
	Player[] objetivo;
	// Use this for initialization
	void Start () {
		Boss = FindObjectOfType<Boss> ();
		objetivo = FindObjectsOfType<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		movimiento ();
	}

	 
	[Range(1, 30)]
	public int velocity;
	Vector3 posicionLlegada;
	bool disparo = false;
	bool llegado = false;



	void movimiento()
	{
		
		if (!disparo) {
			int i = Random.Range (0, 2);
			disparo = true;
			posicionLlegada = objetivo[i].gameObject.transform.position;
		}
		if (transform.position == posicionLlegada)
			llegado = true;

		if (!llegado)
			transform.position = Vector3.MoveTowards (transform.position, posicionLlegada,
				velocity * Time.deltaTime);
		else 
		{
			transform.position = Vector3.MoveTowards (transform.position, Boss.gameObject.transform.position,
				velocity * Time.deltaTime);

		}
		if (llegado && transform.position == Boss.transform.position) {			
			Boss.Ammo++;
			Destroy (this.gameObject);
		}
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.GetComponent<Player> ()) 
		{
			collision.GetComponent<Player> ().dead = true;
		}

        if (collision.gameObject.layer == 9)
            llegado = true;
    }
		

}
