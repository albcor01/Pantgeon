using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public float speed;

    //[HideInInspector]
    public GameObject P1;
    //[HideInInspector]
    public GameObject P2;

    public Transform currentTarget;

    CircleCollider2D Trigger;


    public bool mov;
	// Use this for initialization
	void Start () {
        try
        {
            Player[] players = FindObjectsOfType<Player>();
            if (players[0].PlayerNum == 1)
            {
                P1 = players[0].gameObject;
                P2 = players[1].gameObject;
            }
            else
            {
                P1 = players[1].gameObject;
                P2 = players[0].gameObject;
            }
            currentTarget = P1.transform;
            transform.position = P1.transform.position;
        }
        catch
        {
            Debug.LogWarning("Falta un/unos jugadores en la escena");
        }
        try
        {
            CircleCollider2D [] colliders = GetComponents<CircleCollider2D>();
            Trigger = colliders[0];
            Trigger.enabled = false;
        }
        catch
        {
            Debug.LogWarning("Falta algun collider en la bola");
        }
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        Trigger.enabled = !mov;
        if (transform.position == currentTarget.position)
        {
            currentTarget.GetComponent<Player>().Ready = true;
            transform.rotation = Quat.LookAt(transform, GetOtherTrgt(), -90f);
        }
        else
            transform.rotation = Quat.LookAt(transform, currentTarget, -90f);

    }
    void Move()
    {
        if(mov)
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
    }
    public void swapTarget()
    {
        if (currentTarget == P1.transform)
            currentTarget = P2.transform;
        else if(currentTarget == P2.transform)
            currentTarget = P1.transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Ayyyy me han tocado");
        if (!mov && collision.GetComponent<Player>())
        {
            currentTarget = collision.transform;
            mov = true;
            Trigger.enabled = false;
        }
       
        
        Player[] players = FindObjectsOfType<Player>();
        if (mov&&!(players[0].Ready || players[1].Ready))
        {
            collision.SendMessage("Daño", SendMessageOptions.DontRequireReceiver);
        }
            if(mov && collision.GetComponent<Racer>())
            {
                Player[] player = FindObjectsOfType<Player>();
                if (!(player[0].Ready || player[1].Ready))
                {
                    Racer boss = collision.GetComponentInParent<Racer>();
                    boss.daño(5);
                }
            }
//            Debug.Log("AAAAHHHHH he colisionado
            if(collision.gameObject.layer==8|| collision.gameObject.layer ==9)
                mov = false;
        }
    public Transform GetOtherTrgt()
    {
        Transform otherTarg = currentTarget;
        if (currentTarget == P1.transform)
            otherTarg = P2.transform;
        else
            otherTarg = P1.transform;
        return otherTarg;
    }
}
