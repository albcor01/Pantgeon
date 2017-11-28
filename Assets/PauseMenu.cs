using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject Continue;
    public GameObject Exit;
    public GameObject Selector;
    public int ExitScene = 2;
    int indx = 0;
	// Use this for initialization
	void Start () {
        SetSelector();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow))
        {
            indx = 0;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            indx = 1;
        SetSelector();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            Act();
	}
    void SetSelector()
    {
        if(indx==0)
            Selector.transform.position = Continue.transform.position;
        else if(indx==1)
            Selector.transform.position = Exit.transform.position;
    }
    void Act()
    {

        if (indx == 0)
            Time.timeScale = 1;
        else if (indx == 1)
        {
            Time.timeScale = 1;
            PlayerPrefs.Save();
            if (ExitScene >=0)
                UnityEngine.SceneManagement.SceneManager.LoadScene(ExitScene);
            else
                Application.Quit();
        }
        gameObject.SetActive(false);
    }
}
