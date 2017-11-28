using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LogicaDialogosNivel1 : MonoBehaviour {

    public string escenaSiguiente;
    public GameObject retratoProta1;
    public GameObject retratoProta2;
    public GameObject abuela;
    public GameObject vacio;
    public Text cuadroD;
    public Text cuadroN;
    Animator anim1;
    Animator anim2;
    public string[] dialogoProta1= new string[7];
    public string[] dialogoProta2 = new string[6];
    public string[] dialogoabuela = new string[1];
    public string[] ruido = new string[2];
    Personaje prota1;
    Personaje prota2;
    Personaje abuelaZombi;
    Personaje ruidos;
    int interaccion = 0;
    Personaje[] personajes;
    bool salida=false;
    
    

    
	// Use this for initialization
	void Start () {
        cuadroD.gameObject.SetActive(true);
        cuadroN.gameObject.SetActive(true);
        prota1 = new Personaje(retratoProta1, "Bill", dialogoProta1, cuadroD, cuadroN);
        prota2 = new Personaje(retratoProta2, "Cosby", dialogoProta2, cuadroD, cuadroN);
        abuelaZombi = new Personaje(abuela, "Lady Old Bones", dialogoabuela, cuadroD, cuadroN);
        ruidos = new Personaje(vacio, "", ruido, cuadroD, cuadroN);
        personajes = new Personaje[4];
        personajes[0] = prota1;
        personajes[1] = prota2;
        personajes[2] = abuelaZombi;
        personajes[3] = ruidos;
        Invoke("ConverNivel", 1f);
        anim1=retratoProta1.GetComponent<Animator>();
        anim2 = retratoProta2.GetComponent<Animator>();



    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)&&salida)
        {
            ConverNivel();
            interaccion++;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            saltarconveración();
        }
	}

    void QuitaRetratos()
    {
        for (int i = 0; i < personajes.Length; i++) {
            personajes[i].retrato.gameObject.SetActive(false);
        }
    }
    void ConverNivel()
    {
        QuitaRetratos();
        switch (interaccion)
        {
            
            case 0:
                prota1.muestraDialogo(0);
                interaccion++;
                anim1.SetBool("Sad", true);
                salida = true;
                break;
            case 1:
                prota2.muestraDialogo(0);
                break;
            case 2:
                prota2.muestraDialogo(1);
                break;
            case 3:
                prota1.muestraDialogo(1);
                anim1.SetBool("Sad", false);
                break;
            case 4:
                ruidos.muestraDialogo(0);
                break;
            case 5:
                prota2.muestraDialogo(2);
                break;
            case 6:
                prota1.muestraDialogo(2);
                anim1.SetBool("Mad", true);
                break;
            case 7:
                ruidos.muestraDialogo(1);
                break;
            case 8:
                prota1.muestraDialogo(3);
                anim1.SetBool("Mad", false);
                break;
            case 9:
                abuelaZombi.muestraDialogo(0);
                break;
            case 10:
                prota2.muestraDialogo(3);
                anim2.SetBool("Mad", true);
                break;
            case 11:
                prota1.muestraDialogo(4);
                anim1.SetBool("Mad", true);
                break;
            case 12:
                prota2.muestraDialogo(4);
                anim2.SetBool("Mad", false);
                break;
            case 13:
                prota1.muestraDialogo(5);
                anim1.SetBool("Mad", true);
                break;
            case 14:
                prota2.muestraDialogo(5);
                anim2.SetBool("Strange", true);
                break;
            case 15:
                prota1.muestraDialogo(6);
                anim1.SetBool("Mad", true);
                break;
            case 16:
                saltarconveración();
                break;
        }

               

    }
    void saltarconveración()
    {
        SceneManager.LoadScene(escenaSiguiente);
    }

}
public class Personaje : MonoBehaviour
{
    public GameObject retrato;
    public string nombre;
    public string[] dialogos;
    public Text cuadroDeDialogo;
    public Text cuadoDeNombre;
    

    public Personaje(GameObject retrat, string nombr, string[]dialogo, Text cuadroD, Text cuadroN)
    {
        retrato = retrat;
        nombre = nombr;
        dialogos= new string[dialogo.Length];
        for (int i = 0; i < dialogo.Length; i++)
        {
            dialogos[i] = dialogo[i];
        }
        cuadroDeDialogo = cuadroD;
        cuadoDeNombre = cuadroN;
        
    }
    public void muestraDialogo(int n)
    {
        retrato.gameObject.SetActive(true);
        cuadoDeNombre.text = nombre;
        cuadroDeDialogo.text = dialogos[n];
    }

}
