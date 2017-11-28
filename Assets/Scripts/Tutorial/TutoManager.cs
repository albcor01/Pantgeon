using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour {

    public int waveNum;
    public GameObject TutObjs;
    GameObject Player1, Player2;
    public Transform DefPos1, DefPos2;
    public List<GameObject> Dianas;
	// Use this for initialization
	void Start () {

        Player[] players = FindObjectsOfType<Player>();
        if (players[0].PlayerNum == 1)
        {
            Player1 = players[0].gameObject;
            Player2 = players[1].gameObject;
        }
        else
        {
            Player1 = players[1].gameObject;
            Player2 = players[0].gameObject;
        }
        SetWaveObjs();
        SendMessage("Wave" + waveNum.ToString());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void NextWave()
    {
        waveNum+=1;
        SetWaveObjs();
        SendMessage("Wave" + waveNum.ToString());
    }
    void SetWaveObjs()
    {
        foreach (TutObj obj in TutObjs.GetComponentsInChildren<TutObj>(true))
        {
            obj.NewWave(waveNum);
        }
    }
    void Wave0() //Muve al jugador 1 al punto
    {
        ResetPlayerPos();
        Player2.GetComponent<StickToPoint>().enabled = true;
    }
    void Wave1() //Jugador 2
    {
        ResetPlayerPos();
        Player2.GetComponent<StickToPoint>().enabled = !true;
        Player1.GetComponent<StickToPoint>().enabled = true;
    }
    void Wave2()
    {
        ResetPlayerPos();
        Player1.GetComponent<StickToPoint>().enabled = true;
        Player2.GetComponent<StickToPoint>().enabled = true;
        StartCoroutine(Dispara());
    }
    void Wave3()
    {
        StartCoroutine(Dispara2());
    }
    void Wave4()
    {
        ResetPlayerPos();
        Player1.GetComponent<StickToPoint>().enabled = false;
        Player2.GetComponent<StickToPoint>().enabled = false;
        StartCoroutine(Dianas1());
    }
    void Wave5()
    {
        ResetPlayerPos();
        StartCoroutine(Dianas1());
    }
    void Wave6()
    {
        Destroy(FindObjectOfType<Shoot>().gameObject);
        ResetPlayerPos();
        StartCoroutine(Rescate0());
    }
    void Wave7()
    {
        ResetPlayerPos();
        StartCoroutine(Rescate0());
    }
    void ResetPlayerPos()
    {
        Player1.transform.position = DefPos1.position;
        Player2.transform.position = DefPos2.position;
        if (FindObjectOfType<Shoot>())
            FindObjectOfType<Shoot>().transform.position = Player1.transform.position;
    }
    IEnumerator Dispara()
    {
        while (!Player2.GetComponent<Player>().Ready)
            yield return null;
        NextWave();
    }
    IEnumerator Dispara2()
    {
        while (!Player1.GetComponent<Player>().Ready)
            yield return null;
        NextWave();
    }
    IEnumerator Dianas1()
    {
        yield return new WaitForSeconds(1);
        while (Dianas.Count>0)
            yield return null;
        NextWave();
    }
    IEnumerator Rescate0()
    {
        while (Vector3.Distance(Player1.transform.position,Player2.transform.position) > .5f)
            yield return null;
        NextWave();
    }
}
