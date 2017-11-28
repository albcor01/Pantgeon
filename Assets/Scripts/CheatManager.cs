using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("Bill", 1);
        PlayerPrefs.SetInt("Cosby", 1);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftControl))
            PlayerPrefs.DeleteAll();
        if(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Default settings OK");
            PlayerPrefs.SetInt("AnimP" + 0, 0);
            PlayerPrefs.SetInt("AnimP" + 1, 1);
            PlayerPrefs.SetInt("Bill", 1);
            PlayerPrefs.SetInt("Cosby", 1);
            PlayerPrefs.SetInt("Spooky", 0);
            PlayerPrefs.SetInt("Olov", 0);
            PlayerPrefs.SetInt("Izel", 0);
            PlayerPrefs.Save();
        }
	}
}
