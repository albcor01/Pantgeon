using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ponzona : MonoBehaviour {
    public float duracion;
	Animator anim;
	// Use this for initialization
	void Start () {
        Invoke("Termina", duracion);
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D coll)
    {
//        print("holis");
        if (coll.GetComponent<Player>())
        {
//            Debug.Log("paco");
            coll.GetComponent<Player>().Ralentiza();
        }
    }


    void Termina()
    {
        
		anim.SetBool ("Desaparece", true);
		Invoke ("Destruye", 0.5f);
        
    }
	void Destruye()
	{
		Destroy(this.gameObject);
	}

}
