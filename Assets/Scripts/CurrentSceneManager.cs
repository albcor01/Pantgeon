using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CurrentSceneManager : MonoBehaviour {
    //public float OurTime;
    public GameObject WinPanel;
    float Score;
    Scene currScene;
    float startime;
    string rank;
	// Use this for initialization
	void Start () {
        currScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        startime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Exit()
    {
        Score = Time.time - startime;
        SetScore();
        WinPanel.SetActive(true);
        WinPanel.transform.GetChild(1).GetComponent<Text>().text = "Your time: " + Mathf.Round(Score);    
        WinPanel.transform.GetChild(2).GetComponent<Text>().text = "Best time: " + Mathf.Round( PlayerPrefs.GetFloat(currScene.buildIndex.ToString(), 0));
        WinPanel.transform.GetChild(3).GetComponent<Text>().text = "Rank:   " + rank;
        StartCoroutine(Win());
    }
    IEnumerator Win()
    {
        while (!Input.GetKeyDown(KeyCode.Space))
            yield return null;
        loadScene();
    }
    void loadScene()
    {
        if (PlayerPrefs.GetFloat(currScene.buildIndex.ToString(), 0) == 0)
            PlayerPrefs.SetFloat(currScene.buildIndex.ToString(), Score);
        else if ((Score) < PlayerPrefs.GetFloat(currScene.buildIndex.ToString()))
            PlayerPrefs.SetFloat(currScene.buildIndex.ToString(), Score);
        //print(SceneManager.GetActiveScene().buildIndex.ToString());
        print(Score);
        PlayerPrefs.Save();
        SceneManager.LoadScene(2);
    }
    void SetScore()
    {
        float Score = Time.time - startime;
        string ScoreLetter;
        float OurTime = PlayerPrefs.GetFloat("Maxi" + currScene.buildIndex.ToString(), 0);
        if (Score == 0)
            ScoreLetter = "";
        else if (Score > OurTime * 2.5)
            ScoreLetter = "D";
        else if (Score > OurTime * 2)
            ScoreLetter = "C";
        else if (Score > OurTime * 1.5)
            ScoreLetter = "B";
        else if (Score > OurTime)
            ScoreLetter = "A";
        else
            ScoreLetter = "S";
        rank = ScoreLetter;
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("TotalTime", PlayerPrefs.GetFloat("TotalTime", 0) + Time.time);
    }
}
