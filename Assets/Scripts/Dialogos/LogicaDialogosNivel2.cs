using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LogicaDialogosNivel2 : MonoBehaviour
{

    public string escenaSiguiente;
    public GameObject retratoProta1;
    public GameObject retratoProta2;
    public GameObject retratoOlov;
    public Text cuadroD;
    public Text cuadroN;
    public string[] dialogoProta1 = new string[3];
    public string[] dialogoProta2 = new string[2];
    public string[] dialogoOlov = new string[5];
    Personaje prota1;
    Personaje prota2;
    Personaje olov;
    int interaccion = 0;
    Personaje[] personajes;
    bool salida = false;
    Animator anim1;
    Animator anim2;




    // Use this for initialization
    void Start()
    {

        cuadroD.gameObject.SetActive(true);
        cuadroN.gameObject.SetActive(true);
        prota1 = new Personaje(retratoProta1, "Bill", dialogoProta1, cuadroD, cuadroN);
        prota2 = new Personaje(retratoProta2, "Cosby", dialogoProta2, cuadroD, cuadroN);
        olov = new Personaje(retratoOlov, "Olov", dialogoOlov, cuadroD, cuadroN);
        personajes = new Personaje[4];
        personajes[0] = prota1;
        personajes[1] = prota2;
        personajes[2] = olov;
        Invoke("ConverNivel", 1f);
        anim1 = retratoProta1.GetComponent<Animator>();
        anim2 = retratoProta2.GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && salida)
        {
            ConverNivel();
            interaccion++;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            saltarconveración();
        }
    }

  /*  void QuitaRetratos()
    {
        for (int i = 0; i < personajes.Length; i++)
        {
            personajes[i].retrato.gameObject.SetActive(false);
        }
    }*/
    void ConverNivel()
    {
        //QuitaRetratos();
        switch (interaccion)
        {

            case 0:
                olov.muestraDialogo(0);
                salida = true;
                interaccion++;
                break;
            case 1:
                olov.muestraDialogo(1);
                break;
            case 2:
                olov.muestraDialogo(2);
                break;
            case 3:
                olov.muestraDialogo(3);
                break;
            case 4:
                olov.muestraDialogo(4);
                break;
            case 5:
                prota1.muestraDialogo(0);
                anim1.SetBool("Strange", true);
                break;
            case 6:
                prota1.muestraDialogo(1);
                break;
            case 7:
                prota2.muestraDialogo(0);
                anim2.SetBool("Strange", true);
                break;
            case 8:
                prota1.muestraDialogo(2);
                break;
            case 9:
                prota2.muestraDialogo(1);
                anim2.SetBool("Strange", false);
                break;
            case 10:
                saltarconveración();
                break;
        }



    }
    void saltarconveración()
    {
        SceneManager.LoadScene(escenaSiguiente);
    }


}

