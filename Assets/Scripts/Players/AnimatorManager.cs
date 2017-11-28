using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour {
    public AnimatorOverrideController[] pjs;
    GameObject Player1, Player2;
    private void Start()
    {
        Player[] players;
        players = FindObjectsOfType<Player>();
        if (players[0].gameObject.CompareTag("P2"))
        {
            Player2 = players[0].gameObject;
            Player1 = players[1].gameObject;
        }
        else
        {
            Player2 = players[1].gameObject;
            Player1 = players[0].gameObject;
        }
        SetAnims();
    }
    public void SetAnims()
    {
        print("Player 1 indx: " + PlayerPrefs.GetInt("AnimP" + 0));
        Player1.GetComponent<Animator>().runtimeAnimatorController = pjs[PlayerPrefs.GetInt("AnimP" + 0, 0)];
        Player2.GetComponent<Animator>().runtimeAnimatorController = pjs[PlayerPrefs.GetInt("AnimP" + 1, 1)];
    }

}
