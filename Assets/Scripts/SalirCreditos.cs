using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SalirCreditos : MonoBehaviour {

	float aumento = 0.01f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x > 1f)
			aumento = -aumento;
		else if (transform.localScale.x < 0.8f)
			aumento = -aumento;
		transform.localScale += new Vector3 (aumento, aumento); 

		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
			SceneManager.LoadScene (0);
	}
}
