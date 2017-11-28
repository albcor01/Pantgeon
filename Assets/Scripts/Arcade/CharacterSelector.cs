using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour {
    bool GetInput1 = false;
    bool GetInput2 = false;
    public GameObject SpaceText;
    public GameObject ChangeMenu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GetInput2 && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            ChangeMenu.SetActive(false);
            GetInput1 = false;
            GetInput2 = false;
            PlayerPrefs.Save();
            FindObjectOfType<AnimatorManager>().SetAnims();
        }
        if (GetInput1 && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 0;
            ChangeMenu.SetActive(true);
            GetInput1 = false;
            GetInput2 = true;
        }
        
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetInput1 = true;
        SpaceText.SetActive(true);
        SpaceText.GetComponent<Text>().text = "Press space to enter";
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GetInput1 = false;
        SpaceText.SetActive(false);
    }
}
