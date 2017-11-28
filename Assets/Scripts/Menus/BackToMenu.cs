using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	float aumento = 0.01f;
	// Update is called once per frame
	void Update () {
		
		if (transform.localScale.x > 1.55f)
			aumento = -aumento;
		else if (transform.localScale.x < 1.36f)
			aumento = -aumento;
	 transform.localScale += new Vector3 (aumento, aumento); 

		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
			SceneManager.LoadScene (0);
	}
}
