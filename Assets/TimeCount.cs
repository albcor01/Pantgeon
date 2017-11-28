using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour {

    public GameObject TimeText;
    bool Total;
    bool show = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (show)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Total = !Total;
            if (TimeText)
            {
                if (!Total)
                    TimeText.GetComponent<Text>().text = "Current: " + Mathf.Round(Time.time / 60) + " mins";
                else
                    TimeText.GetComponent<Text>().text = "Total: " + Mathf.Round((PlayerPrefs.GetFloat("TotalTime", 0)+Time.time) / 60) + " mins";
            }
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        show = true;
        TimeText.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        show = false;
        TimeText.SetActive(false);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("TotalTime", PlayerPrefs.GetFloat("TotalTime", 0) + Time.time);
    }
}
