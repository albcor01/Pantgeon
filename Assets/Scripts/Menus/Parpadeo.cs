using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Parpadeo : MonoBehaviour {

	int CurrentButton = 0;
	AudioSource button;


	// Use this for initialization
	void Start () {
		button = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {		
		CurrentInput ();
		Efecto ();
		Debug.Log (CurrentButton);
	}


	float aumento = 0.01f;

	void Efecto()
	{
		//AQUI CURRENT 0
		if (CurrentButton == 0) {
			if (transform.GetChild (0).transform.localScale.x > 1.2f)
				aumento = -aumento;
			else if (transform.GetChild (0).transform.localScale.x < 1f)
				aumento = -aumento;

			transform.GetChild (0).transform.localScale += new Vector3 (aumento, aumento); 
			transform.GetChild (1).transform.localScale = new Vector3 (1, 1); 
			transform.GetChild (2).transform.localScale = new Vector3 (1, 1);
			transform.GetChild (3).transform.localScale = new Vector3 (1, 1); 


			//AQUI CURRENT 1
		} else if (CurrentButton == 1) {
			if (transform.GetChild (1).transform.localScale.x >= 1.2f)
				aumento = -aumento;
			else if (transform.GetChild (1).transform.localScale.x < 1f)
				aumento = -aumento;

			transform.GetChild (1).transform.localScale += new Vector3 (aumento, aumento); 
			transform.GetChild (0).transform.localScale = new Vector3 (1, 1); 
			transform.GetChild (2).transform.localScale = new Vector3 (1, 1);
			transform.GetChild (3).transform.localScale = new Vector3 (1, 1); 


			//AQUI CURRENT 2
		} else if (CurrentButton == 2) {
			if (transform.GetChild (2).transform.localScale.x >= 1.2f)
				aumento = -aumento;
			else if (transform.GetChild (2).transform.localScale.x < 1f)
				aumento = -aumento;

			transform.GetChild (2).transform.localScale += new Vector3 (aumento, aumento); 
			transform.GetChild (0).transform.localScale = new Vector3 (1, 1); 
			transform.GetChild (1).transform.localScale = new Vector3 (1, 1);
			transform.GetChild (3).transform.localScale = new Vector3 (1, 1); 
		
		}
		if (CurrentButton == 3) {
			if (transform.GetChild (3).transform.localScale.x > 1.2f)
				aumento = -aumento;
			else if (transform.GetChild (3).transform.localScale.x < 1f)
				aumento = -aumento;

			transform.GetChild (3).transform.localScale += new Vector3 (aumento, aumento); 
			transform.GetChild (1).transform.localScale = new Vector3 (1, 1); 
			transform.GetChild (2).transform.localScale = new Vector3 (1, 1); 
			transform.GetChild (0).transform.localScale = new Vector3 (1, 1); 


		}

		/*if (CurrentButton == 4) {
			if (transform.GetChild (4).transform.localScale.x > 1.2f)
				aumento = -aumento;
			else if (transform.GetChild (4).transform.localScale.x < 1f)
				aumento = -aumento;

			transform.GetChild (4).transform.localScale += new Vector3 (aumento, aumento); 
			transform.GetChild (1).transform.localScale = new Vector3 (1, 1); 
			transform.GetChild (2).transform.localScale = new Vector3 (1, 1);
			transform.GetChild (3).transform.localScale = new Vector3 (1, 1); 
			transform.GetChild (0).transform.localScale = new Vector3 (1, 1); 

		}*/
	}

	void CurrentInput()
	{
		if (Input.GetKeyDown (KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow)) {
			if (CurrentButton == 0)
				CurrentButton = 3;
			else
				CurrentButton -= 1;

			button.Play ();

		} else if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
			if (CurrentButton == 3)
				CurrentButton = 0;
			else
				CurrentButton += 1;
			
			button.Play ();

		} 

		else if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Return)) {

			if (CurrentButton == 0)
				SceneManager.LoadScene(2);
			else if (CurrentButton == 1)
				SceneManager.LoadScene (1);
			else if (CurrentButton == 2)
				SceneManager.LoadScene (8);
			else if (CurrentButton == 3)
				Application.Quit();
			
				

		}

						
	}
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("TotalTime", PlayerPrefs.GetFloat("TotalTime", 0) + Time.time);
    }
}
