using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public int Ammo = 2;
    public float tiempoInvul = 1;
	public float vida = 100;
    bool invulnerable=false;
    Animator anim;
	AudioSource audio;
 
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
		Invoke ("Shoot", 2f);
        FindObjectOfType<BossLifeBar>().SetValues(vida);
        audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log (Ammo);
        HuesosEspalda();

		transform.Rotate (new Vector3 (0.0f, 0.0f,Time.deltaTime * -40f));

	}

	public GameObject Bala;

	[Range(0, 30)]
	public float ShootDelay;

	void Shoot()
	{
		if (Ammo > 0) {
			Ammo-=1;
			GameObject bala = Instantiate (Bala);
			bala.transform.position = transform.position; 
		}

		Invoke ("Shoot", ShootDelay);
	}
    public void Daño()
    {
        if (!invulnerable)
        {
            if (vida > 0)
            {
                vida -= 10;
				audio.Play ();
//                Debug.Log("Vida restada");
                FindObjectOfType<BossLifeBar>().UpdateBar(vida);
                StartCoroutine(Invulnerabilidad());
            }

            else
            {
                vida = 0;
            }
            if (vida == 0)
            {
                anim.SetTrigger("Death");
                CancelInvoke();
                transform.GetComponentInParent<BossMovimiento>().enabled = false;
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                enabled = false;
            }
        }

    }
    IEnumerator Invulnerabilidad()
    {
//        Debug.Log("Aqui he entrado");
        invulnerable = true;
        //if (gameObject.GetComponent<SpriteRenderer>())
            for (int i = 0; i < 15; i++)
            {
//                Debug.Log("Cambiando sprites");
                gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;
                yield return new WaitForSeconds(tiempoInvul / 15f);
            }
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        invulnerable = false;
        yield return null;
    }

	void HuesosEspalda()
	{
		if (Ammo == 2) {
			transform.GetChild (0).GetComponent<SpriteRenderer>().enabled = (true);
			transform.GetChild (1).GetComponent<SpriteRenderer>().enabled = (true);
		}
		else if (Ammo == 1) {
			transform.GetChild (0).GetComponent<SpriteRenderer>().enabled = (false);
			transform.GetChild (1).GetComponent<SpriteRenderer>().enabled = (true);
		}
		else if (Ammo == 0) {
			transform.GetChild (0).GetComponent<SpriteRenderer>().enabled = (false);
			transform.GetChild (1).GetComponent<SpriteRenderer>().enabled = (false);
		}
	}
    public void Finish()
    {
        PlayerPrefs.SetInt("Spooky", 1);
        FindObjectOfType<CurrentSceneManager>().Exit();
    }


}
