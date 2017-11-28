using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    [HideInInspector]
    public int level = -1;
    [HideInInspector]
    public string LevelName;
    public GameObject SpaceText;
    public GameObject Screen;
    [HideInInspector]
    public TextMesh Title_Text;
    [HideInInspector]
    public Color TextColor;
    bool input = false;
    bool ready = false;

	// Use this for initialization
	void Start () {
        //SpaceText = GameObject.FindGameObjectWithTag("SpaceText");
        Title_Text = GameObject.FindGameObjectWithTag("TitleText").GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        Title_Text.text = LevelName;
        sceneloader();
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.CompareTag("Player"))
            {
                ready = level >= 0;
                if (ready)
                {
                    input = true;
                print("Cambio de target");
                    FindObjectOfType<Camera_Lock>().target = Screen;
                    //FindObjectOfType<Camera_Lock>().camSpeed = FindObjectOfType<Camera_Lock>().camSpeed / 2;
                    SpaceText.GetComponent<Text>().text = "Press space to enter";
                }
                else
                {
                    input = false;
                    SpaceText.GetComponent<Text>().text = "Select a game to play";
                }
                SpaceText.GetComponent<ParpadeoEfecto>().enabled = ready;
                SpaceText.SetActive(true);
            }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            input = false;
            SpaceText.SetActive(false);
            FindObjectOfType<Camera_Lock>().RestartTarget();
                //FindObjectOfType<Camera_Lock>().camSpeed = FindObjectOfType<Camera_Lock>().camSpeed * 2;
        }
    }
    void sceneloader()
    {
        if (input)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerPrefs.Save();
                UnityEngine.SceneManagement.SceneManager.LoadScene(level);
            }
        }
    }
}
