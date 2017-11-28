using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectorManager : MonoBehaviour {
    public GameObject P1;
    public GameObject P2;
    public Image P1Im;
    public Image P2Im;
    public int P1Indx, P2Indx;
    public Character[] PJs;

    public int max;
    [System.Serializable]
    public struct Character
    {
        public string Name;
        public Sprite Charact;
        //public AnimatorOverrideController Animator;
        public bool unlocked;
    }
    private void Start()
    {
        for (int i = 0; i < PJs.Length; i++)
        {
            PJs[i].unlocked = PlayerPrefs.GetInt(PJs[i].Name, 0) == 1;
        }
        P1Indx = PlayerPrefs.GetInt("AnimP" + 0, 0);
        P2Indx = PlayerPrefs.GetInt("AnimP" + 1, 1);
        setImgs();
    }
	
	// Update is called once per frame
	void Update () {
        GetInput();
        setImgs();
        setAnims();
	}
    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
            P1Indx = nextChar(P1Indx, P2Indx);
        else if (Input.GetKeyDown(KeyCode.D))
            P1Indx = prevChar(P1Indx, P2Indx);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            P2Indx = nextChar(P2Indx, P1Indx);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            P2Indx = prevChar(P2Indx, P1Indx);
    }
    void setImgs()
    {
        P1Im.sprite = PJs[P1Indx].Charact;
        P2Im.sprite = PJs[P2Indx].Charact;
    }
    int nextChar(int indx,int otherPJ)
    {
        int next = indx;
        bool valido = false;
        while (!valido)
        {
            next+= 1;
            if (next >= PJs.Length)
                next = 0;
            valido = next != otherPJ && PJs[next].unlocked||indx==next;
        }
        return next;
    }
    int prevChar(int indx, int otherPJ)
    {
        int next = indx;
        bool valido = false;
        while (!valido)
        {
            next -= 1;
            if (next <0)
                next = PJs.Length-1;
            valido = next != otherPJ && PJs[next].unlocked || indx == next;
        }
        return next;
    }
    void setAnims()
    {
        PlayerPrefs.SetInt("AnimP" + 0, P1Indx);
        PlayerPrefs.SetInt("AnimP" + 1, P2Indx);
        print("Player 1 anim indx = " + PlayerPrefs.GetInt("AnimP" + 0));
        print("Player 2 anim indx = " + PlayerPrefs.GetInt("AnimP" + 1));
    }
}
