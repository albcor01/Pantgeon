using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racer : MonoBehaviour
{
    AudioSource audio;
    Transform[] pos;
    public int life = 100;
    public float LaserTime = 2;
    public float MaxiLaserTime = 2;
    public float DizzTime = 5f;
    public bool landed=false;
    bool flying;
    bool NotAtk = true;
    Vector3 currPos;
    bool invulnerable = false;
    public float vida = 100;
    bool canStun = false;
    // Use this for initialization
    void Start()
    {
        try
        {
            FindObjectOfType<BossLifeBar>().SetValues(vida);
            pos = GameObject.FindGameObjectWithTag("Pos").GetComponentsInChildren<Transform>();
        }
        catch
        {
            Debug.LogWarning("Falta el conjunto de posiciones");
        }
        audio = GetComponent<AudioSource>();
        currPos = transform.parent.position;
        StartCoroutine(MainLogic());
        StartCoroutine(CambiaControles());
    }

    IEnumerator MainLogic()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        Animator anim = GetComponent<Animator>();

        //Seleccionamos posicion
        Transform target;
        do
        {
            target = pos[Random.Range(0, pos.Length)];
        } while (target.position == currPos);
        currPos = target.position;

        //Y nos tpamos
        while (transform.parent.position != target.position)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, currPos, 5 * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }


        //Y reproducimos animacion
        yield return new WaitForSeconds(0.5f);
        sprite.enabled = true;
        anim.SetTrigger("Land");

        //Y esperamos a que aterrize
        print("Aqui llega");
        yield return new WaitUntil(() => landed == true);
        canStun = true;
        print("Aqui no");
        sprite.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        flying = false;
        yield return new WaitForSeconds(1);
        //Seleccionar ataque
        //Atacar
        canStun = false;
        NotAtk = false;
        anim.SetTrigger("Laser");
        StartCoroutine(Ataque());
        yield return new WaitUntil(() => NotAtk == true);
        anim.SetTrigger("AcabaLaser");
        //Reproducir animacion
        //Eperar un rato
        yield return new WaitForSeconds(2);
        //Largarse (Desactivamos collider)

        GetComponent<Collider2D>().enabled = false;
        //Reproducir animacion
        anim.SetTrigger("Fly");
        //Y volvemos a empezar

        yield return new WaitUntil(() => flying == true);
        landed = false;
        yield return new WaitForSeconds(1);
        StartCoroutine(MainLogic());
    }
    public void Land()
    {
        landed = true;
        GetComponent<Animator>().SetTrigger("Landed");
    }
    public void Fly()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        flying = true;
    }
    IEnumerator Ataque()
    {

        GameObject laser = transform.GetChild(0).gameObject;

        int random = Random.Range(0, 2);
        if (vida < 50)
        {
            laser.transform.GetChild(random * 2).gameObject.SetActive(false);
            laser.transform.GetChild(random * 2 + 1).gameObject.SetActive(false);
            transform.GetChild(1).GetChild(random).gameObject.SetActive(false);
        }

        if (Random.Range(0, 2) == 0)
        {
            laser.transform.rotation = Quaternion.Euler(0f, 0f, 45f);
            transform.GetChild(1).gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 45f);
        }
        else
        {
            transform.GetChild(1).gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            laser.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        transform.GetChild(1).gameObject.SetActive(false);
        laser.SetActive(true);
        print(laser.transform.rotation.eulerAngles.z);
        yield return new WaitForSeconds(LaserTime);

        if (vida < 50)
        {
            print("Tengo poca vida");
            
            Quaternion fromAngle = laser.transform.rotation;
            Quaternion toAngle = Quaternion.Euler(fromAngle.eulerAngles + new Vector3(0f, 0f, 135));
            /*
                laser.transform.Rotate(Vector3.back, 90 / MaxiLaserTime * Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime);
*/
            for (float t = 0f; t < 1; t += Time.deltaTime / MaxiLaserTime)
            {
                laser.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
                yield return null;
            }
        }
        laser.SetActive(false);
        for (int i = 0; i < laser.transform.childCount; i++)
        {
            laser.transform.GetChild(i).gameObject.SetActive(true);
        }
        transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        NotAtk = true;
        
        yield return null;
    }
    IEnumerator CambiaControles()
    {


        yield return new WaitForSeconds(Random.Range(0, 30));
        int ataque = Random.Range(0, 2);
        Color newColor;
        if (ataque == 0)
            newColor = Color.red;
        else
            newColor = Color.cyan;
        yield return new WaitUntil(() => canStun == true);
        for (float i = 0; i < 1; i += Time.deltaTime / 0.5f)
        {
            GetComponent<SpriteRenderer>().color = Color.LerpUnclamped(Color.white, newColor, i);
            print("HOLA");
            yield return null;
        }
        for (float i = 0; i < 1; i += Time.deltaTime / 0.5f)
        {
            GetComponent<SpriteRenderer>().color = Color.LerpUnclamped(newColor, Color.white, i);
            print("HOLA");
            yield return null;
        }
        print("Cambiar controles");
        KeyCode[] Set = GetKeys();
        KeyCode[] DefaultKeys = GetKeys();
        //Dos modos
        if (ataque == 0)
        {
            RandomKeys(Set);
            Player[] players = FindObjectsOfType<Player>();
            players[0].Dizz(DizzTime);
            players[1].Dizz(DizzTime);
        }
        else
            SwapKeys(Set);

        ChangeKeys(Set);
        print("Acabado");
        yield return new WaitForSeconds(DizzTime);
        print("Devolviendo controles...");
        if (ataque == 0)
            ChangeKeys(DefaultKeys);
        StartCoroutine(CambiaControles());
    }
    void swap(ref KeyCode a, ref KeyCode b)
    {
        KeyCode aux = a;
        a = b;
        b = aux;
    }
    KeyCode[] GetKeys()
    {
        Player[] players = FindObjectsOfType<Player>();
        KeyCode[] Set = new KeyCode[10];
        //Arriba | Abajo | Izq | Dcha | Arriba | Abajo | Izq | Dcha |
        for (int i = 0; i < 2; i++)
        {
            Set[i * 4 + 0] = players[i].Up;
            Set[i * 4 + 1] = players[i].Down;
            Set[i * 4 + 2] = players[i].Right;
            Set[i * 4 + 3] = players[i].Left;
        }
        Set[8] = players[0].Shoot;
        Set[9] = players[1].Shoot;
        return Set;
    }
    void ChangeKeys(KeyCode[] Set)
    {
        audio.Play();
        Player[] players = FindObjectsOfType<Player>();
        for (int i = 0; i < 2; i++)
        {
            players[i].Up = Set[i * 4 + 0];
            players[i].Down = Set[i * 4 + 1];
            players[i].Right = Set[i * 4 + 2];
            players[i].Left = Set[i * 4 + 3];
        }
        players[0].Shoot = Set[8];
        players[1].Shoot = Set[9];
    }
    void RandomKeys(KeyCode[] Set)
    {
        audio.Play();
        for (int i = 0; i < 8; i += 2)
        {
            swap(ref Set[i], ref Set[i + 1]);
        }
    }
    void SwapKeys(KeyCode[] Set)
    {
        audio.Play();
        for (int i = 0; i < 4; i++)
        {
            swap(ref Set[i], ref Set[i + 4]);
        }
        swap(ref Set[8], ref Set[9]);
    }
    IEnumerator Invulnerabilidad()
    {
        //        Debug.Log("Aqui he entrado");
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color baseC = sprite.color;
        Color fadedC = sprite.color - new Color(0, 0, 0, 100);
        invulnerable = true;
        //if (gameObject.GetComponent<SpriteRenderer>())
        for (int i = 0; i < 15; i++)
        {
            if (sprite.color == baseC)
                sprite.color = fadedC;
            else
                sprite.color = baseC;
            yield return new WaitForSeconds(1f / 15f);
        }
        sprite.color = baseC;
        invulnerable = false;
        yield return null;
    }
    public void daño(int dmg)
    {
        if (!invulnerable)
        {
            if (vida > 0)
            {
                vida -= dmg;
                                Debug.Log("Vida restada");
                FindObjectOfType<BossLifeBar>().UpdateBar(vida);
                StartCoroutine(Invulnerabilidad());
            }

            else
            {
                vida = 0;
            }
            if (vida <= 0)
            {
                Animator anim = GetComponent<Animator>();
                anim.SetTrigger("Death");
                //transform.GetComponentInParent<BossMovimiento>().enabled = false;
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                enabled = false;
            }
        }
    }
    public void Finish()
    {
        PlayerPrefs.SetInt("Izel", 1);
        FindObjectOfType<CurrentSceneManager>().Exit();
    }
}
