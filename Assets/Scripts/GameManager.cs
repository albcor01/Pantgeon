using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject PauseMenu;
	public GameObject[] SpriteVidas;
    Player[] pers;


	int vidas = 3;


	// Use this for initialization
	void Start () {
		pers = FindObjectsOfType<Player> ();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
        }
        /*if (Input.GetKeyDown(KeyCode.Space))
            Time.timeScale = 1;*/
        calzones ();
		compruebaMuerte ();
	}

	public void quitaVida(int n)
	{
		vidas = vidas - n;
		Debug.Log ("vidas = " + vidas);
	}

	void calzones()
	{
		/*if (vidas == 3)
			for (int i; i < SpriteVidas.Length; i++) {
				SpriteVidas [i].GetComponent<SpriteRenderer> ().enabled = true;
			}*/
		
		if (vidas == 2)
			SpriteVidas [2].gameObject.SetActive (false);
		
		else if(vidas == 1)
			SpriteVidas [1].gameObject.SetActive (false);
		
		else if(vidas == 0)
			SpriteVidas [0].gameObject.SetActive (false);
		
	}


	void compruebaMuerte(){

		if(pers[0].dead == true && pers[1].dead == true)
			Invoke("vuelveArcade", 1.5f);

		if(vidas == 0 && (pers[0].dead == true || pers[1].dead == true))
			Invoke("vuelveArcade", 1.5f);

				
		}

	void vuelveArcade()
	{
        PlayerPrefs.SetInt("DeathScene", SceneManager.GetActiveScene().buildIndex);
		SceneManager.LoadScene (10);
	}



}
