using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    //
    [Range(0,10)]
    public float speed;

    float currSpeed;

    //Key bindings
    public KeyCode Up;
    public KeyCode Down;
    public KeyCode Left;
    public KeyCode Right;
    public KeyCode Shoot;
    public KeyCode RescueKey;

    Shoot ball;

    //int vidas = 3;
    [Range(1,2)]
    public int PlayerNum = 1;

	public bool dead = false;
    
    public int velX, velY;

    public bool Ready;

    Transform Frente;

    [HideInInspector]
	public Animator anim;

    public GameObject DizzIcon;

    bool ralent = false;

    bool CanMove = false;

	void Awake()
	{
		anim = GetComponent<Animator> ();
	}

    // Use this for initialization
    void Start () {
        Frente = transform.GetChild(0).transform;
        ball = FindObjectOfType<Shoot>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead) {
			GetInput ();
			ApuntaFrente ();
			slow ();
			Move ();
			Dispara ();

		} anim.SetBool ("Dead", dead);
        
	}



    void GetInput()
    {
        // Y Axis
		if (Input.GetKey (Up)) {
			velY = 1;
			anim.SetBool ("Up", true);
		} else if (Input.GetKey (Down)) {
			velY = -1;
			anim.SetBool ("Down", true);
		}
        else
            velY = 0;

        // X Axis
		if (Input.GetKey (Right)) {
			velX = 1;
			anim.SetBool ("Right", true);
		} else if (Input.GetKey (Left)) {
			velX = -1;
			anim.SetBool ("Left", true);
		}
        else
            velX = 0;

		if (Input.GetKeyUp (Up))
			anim.SetBool ("Up", false);
		if (Input.GetKeyUp (Down))
			anim.SetBool ("Down", false);
		if (Input.GetKeyUp (Left))
			anim.SetBool ("Left", false);
		if (Input.GetKeyUp (Right))
			anim.SetBool ("Right", false);

        if (Input.GetKeyDown(RescueKey))
        {
            if (Vector3.Distance(transform.position, GetOtherPlayer().gameObject.transform.position) < 1.5f)
                StartCoroutine(Rescue());
        }

    }
    void Move()
    {
        if (velX != 0 && velY != 0)
            this.transform.Translate(new Vector3(velX * Time.deltaTime * currSpeed / Mathf.Sqrt(2f), velY * Time.deltaTime * currSpeed / Mathf.Sqrt(2f), 0f));

        else
            this.transform.Translate(new Vector3(velX * Time.deltaTime * currSpeed, velY * Time.deltaTime * currSpeed, 0f));
    }
    void Dispara()
    {
        if (Input.GetKeyDown(Shoot))
        {
            if (Ready)
            {
                ball.swapTarget();
                Ready = false;
            }

        }
    }
    void ApuntaFrente()
    {
        if (velX != 0 && velY != 0)
            Frente.localPosition = new Vector3(velX/ Mathf.Sqrt(2f), velY / Mathf.Sqrt(2f), 0f)/2;

        else
            Frente.localPosition = new Vector3(velX, velY, 0f)/2;


        Debug.DrawLine(transform.position, transform.position + new Vector3(0f, velY) / 1.9f);

        if (Physics2D.Linecast(transform.position, transform.position + new Vector3(0f, velY)/1.9f , 1 << LayerMask.NameToLayer("Bloqueos")) &&
            Physics2D.Linecast(transform.position, transform.position + new Vector3(0f, velY)/1.9f, 1 << LayerMask.NameToLayer("Bloqueos")).collider.GetComponentInParent<PlayerStop>().PlayerToStop == PlayerNum)
            velY = 0;

        Debug.DrawLine(transform.position, transform.position + new Vector3(velX,0f) / 1.9f);
        if (Physics2D.Linecast(transform.position, transform.position + new Vector3(velX,0)/1.9f, 1 << LayerMask.NameToLayer("Bloqueos")) &&
            Physics2D.Linecast(transform.position, transform.position + new Vector3(velX,0)/1.9f, 1 << LayerMask.NameToLayer("Bloqueos")).collider.GetComponentInParent< PlayerStop >().PlayerToStop == PlayerNum)
            velX = 0;

        print("Holaaaa, colision(?)");

        if (Physics2D.Linecast(transform.position, transform.position + new Vector3(0f,velY)/1.9f,1<<LayerMask.NameToLayer("Collisiones")))
            velY = 0;
        if (Physics2D.Linecast(transform.position, transform.position + new Vector3(velX,0f)/1.9f, 1 << LayerMask.NameToLayer("Collisiones")))
            velX = 0;
        if (Physics2D.Linecast(transform.position, transform.position + new Vector3(0f, velY) / 1.9f, 1 << LayerMask.NameToLayer("Collisiones 2")))
            velY = 0;
        if (Physics2D.Linecast(transform.position, transform.position + new Vector3(velX, 0f) / 1.9f, 1 << LayerMask.NameToLayer("Collisiones 2")))
            velX = 0;
        CanMove = velX == 0 && velY == 0;
        
    }
    void slow()
    {
        if (ralent)
            currSpeed = speed / 2;
        else
            currSpeed = speed;
    }
    public void Ralentiza()
    {
        ralent = true;
        if (IsInvoking("DesRal"))
        {
            CancelInvoke("DesRal");
            
        }
        Invoke("DesRal", 1f);
    }
    void DesRal()
    {
        ralent = false;
    }

    public void Dizz(float DizzTime)
    {
        if (DizzIcon)
        {
            DizzIcon.SetActive(true);
            Invoke("StopDizz", DizzTime);
        }
    }
    void StopDizz()
    {
        DizzIcon.SetActive(false);
    }
    IEnumerator Rescue()
    {
        while (Input.GetKey(RescueKey)&& Vector3.Distance(transform.position, GetOtherPlayer().gameObject.transform.position) < 1.5f)
        {
            Player otherPlayer = GetOtherPlayer();
            otherPlayer.transform.position = Vector3.MoveTowards(otherPlayer.transform.position, transform.position, Time.deltaTime*speed/2);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }
    Player GetOtherPlayer()
    {
        Player otherplayer=this;
        Player[] players = FindObjectsOfType<Player>();
        if (players[0] == this)
            otherplayer = players[1];
        else
            otherplayer = players[0];
        Debug.Log("I am " + name + " The other is: " + otherplayer.gameObject.name);
        return otherplayer;
    }
}
