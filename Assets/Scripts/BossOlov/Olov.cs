using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Olov : MonoBehaviour
{
    [HideInInspector]
    public bool tangible = false;
    AudioSource audio; 
    public float ventana;
    public float enfriamientoAceite;
   // public float delta;
   // public float speed;
    bool targetP1= true;
    //private Vector3 startPos;
    Animator anim;
    public GameObject libro;
    public GameObject PJ1;
    public GameObject PJ2;
    public GameObject ponzoña;
    public GameObject posEscudo;
    public GameObject[] posVulnerable;
    public int vida;
    public int damage;
    Vector3 PosEs;
    Vector3 PosNEs;
    [Range(0,100f)]
    public float velD;
    Vector3 target;
    bool movement;
    bool poniendoMugre;
    bool poniendoLibro;
    bool haylibro;
	public float tiempoInvul = 1;
	bool invulnerable=false;
    // Use this for initialization
    void Start()
    {

        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        PosEs = posEscudo.transform.position;
        transform.position = PosEs;
        Invoke("Aceite", enfriamientoAceite);
        FindObjectOfType<BossLifeBar>().SetValues(vida);

    }

    // Update is called once per frame
    void Update()
    {
        if (vida <= 0)
        {
            anim.SetTrigger("Death");
            CancelInvoke();
            if (FindObjectOfType<Libro>())
            {
                Destroy(FindObjectOfType<Libro>().gameObject);
            }
            enabled = false;
        }
        if (FindObjectOfType<Libro>())
        {
            haylibro = true;
        }
        else if (!FindObjectOfType<Libro>() && !tangible)
        {
            haylibro = false;
        }
        if (movement)
        {
            EnMovimiento();
        }
        PoneMugre();
        if (!haylibro)
        {
            PoneLibro();
        }
        ControlDeInvocaciones();
        
        //Vector3 v = startPos;
        //v.y += delta * Mathf.Sin(Time.time * speed);
        //transform.position = v;
    }
    public void HitLibro()
    {

        tangible = true;
        movement = true;
        audio.Play();
        target = posVulnerable[Random.Range(0,posVulnerable.Length)].transform.position;
        anim.SetBool("Vulnerable", true);
        //startPos = PosNEs;
        Invoke("Reinicio", ventana);



    }
    void Reinicio()
    {
        anim.SetBool("Vulnerable", false);
        poniendoLibro = true;
        tangible = false;
        //startPos = PosEs;
        movement = true;
        target = PosEs;
        Invoke("Aceite", enfriamientoAceite);

    }
    void OnTriggerEnter2D(Collider2D col)
    {
		if(col.GetComponent<Shoot>())

        if ((col.GetComponent<Shoot>().mov) && tangible)
        {
			StartCoroutine (Invulnerabilidad());
            vida -= damage;
                FindObjectOfType<BossLifeBar>().UpdateBar(vida);
                Reinicio();
        }
    }

    void Aceite()
    {

        if (!tangible)
        {
            
            if (targetP1)
            {
                //startPos = PJ1.transform.position;
                movement = true;
                target = PJ1.transform.position;
                targetP1 = false;

            }
            else
            {
                //startPos = PJ2.transform.position;

                movement = true;
                target = PJ2.transform.position;
                targetP1 = true;

            }
            poniendoMugre = true;
            //delta = 0;
            /*if (!movement)
            {
                GameObject charco = Instantiate(ponzoña);
                charco.transform.position = transform.position;
                Invoke("Vuelve", 1.5f);
                Sieg Heil
            }*/

        }
        else
        {
            Invoke("Aceite", enfriamientoAceite);
        }
    }
    void Vuelve()
    {
        //startPos = PosEs;
        movement = true;
        target = PosEs;
        Invoke("Aceite", enfriamientoAceite);
    }

    void EnMovimiento()
    {

        if (transform.position != target && movement)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, velD * Time.deltaTime);
        }
        else movement = false;
    }

    void PoneMugre()
    {
        if (!movement && poniendoMugre)
        {
            GameObject charco = Instantiate(ponzoña);
            charco.transform.position = transform.position;
            Invoke("Vuelve", 1f);
            poniendoMugre = false;
        }
    }
    void PoneLibro()
    {
        if (!movement && poniendoLibro&& !haylibro)
        {
            GameObject bala = Instantiate(libro);
            bala.transform.position = transform.position;

            poniendoLibro = false;
        }
    }
    void ControlDeInvocaciones()
    {
        if (IsInvoking("Reinicio") || IsInvoking("Vuelve"))
        {
            CancelInvoke("Aceite");
        }
        
    }

	IEnumerator Invulnerabilidad()
	{
		Debug.Log("Aqui he entrado");
		invulnerable = true;
		//if (gameObject.GetComponent<SpriteRenderer>())
		for (int i = 0; i < 15; i++)
		{
			Debug.Log("Cambiando sprites");
			gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;
			yield return new WaitForSeconds(tiempoInvul / 15f);
		}
		gameObject.GetComponent<SpriteRenderer>().enabled = true;
		invulnerable = false;
		yield return null;
	}
    public void Finish()
    {
        PlayerPrefs.SetInt("Olov", 1);
        FindObjectOfType<CurrentSceneManager>().Exit();
    }

}
