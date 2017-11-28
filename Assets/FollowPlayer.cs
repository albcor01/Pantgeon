using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    GameObject Player;
    Animator anim;
	// Use this for initialization
	void Start () {
        Player[] players = FindObjectsOfType<Player>();
        if (players[0].gameObject == gameObject)
            Player = players[1].gameObject;
        else
            Player = players[0].gameObject;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        move();
        setAnim();
	}
    void move()
    {
        transform.position = Vector3.Lerp(transform.position, Player.transform.position, 5 * Time.deltaTime);
    }
    void setAnim()
    {
        anim.SetBool("Left", transform.position.x > Player.transform.position.x+.3);
        anim.SetBool("Right", transform.position.x < Player.transform.position.x-.3);
        anim.SetBool("Down", transform.position.y > Player.transform.position.y+.3);
        anim.SetBool("Up", transform.position.y < Player.transform.position.y-.3);
        if(Vector3.Distance(transform.position, Player.transform.position) < .2)
        {
            anim.SetBool("Down",true);
            anim.SetBool("Left", false);
            anim.SetBool("Right", false);
            anim.SetBool("Up", false);
        }
        
    }
}
