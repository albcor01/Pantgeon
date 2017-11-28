using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class MenuGameOver : MonoBehaviour {

	int CurrentButton = 0;
	AudioSource button;

	// Use this for initialization
	void Start () {
		button = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		parpadeo ();
		CurrentInput ();
	}

	float aumento = 0.01f;

	void parpadeo(){

		if (CurrentButton == 0) {
			if (transform.GetChild (0).transform.localScale.x > 1.2f)
				aumento = -aumento;
			else if (transform.GetChild (0).transform.localScale.x < 1f)
				aumento = -aumento;

			transform.GetChild (0).transform.localScale += new Vector3 (aumento, aumento); 
			transform.GetChild (1).transform.localScale = new Vector3 (1, 1); 
	


			//AQUI CURRENT 1
		} else if (CurrentButton == 1) {
			if (transform.GetChild (1).transform.localScale.x >= 1.2f)
				aumento = -aumento;
			else if (transform.GetChild (1).transform.localScale.x < 1f)
				aumento = -aumento;

			transform.GetChild (1).transform.localScale += new Vector3 (aumento, aumento); 
			transform.GetChild (0).transform.localScale = new Vector3 (1, 1); 
	
		}
	}

	void CurrentInput()
	{
		if (Input.GetKeyDown (KeyCode.W)) {
			if (CurrentButton == 0)
				CurrentButton = 1;
			else
				CurrentButton -= 1;

			button.Play ();

		} else if (Input.GetKeyDown (KeyCode.S)) {
			if (CurrentButton == 1)
				CurrentButton = 0;
			else
				CurrentButton += 1;

			button.Play ();

		} 

		else if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Return)) {

			if (CurrentButton == 0)
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt("DeathScene"),0);
            }
			else if (CurrentButton == 1)
				SceneManager.LoadScene (2);
		



		}

	}

}
