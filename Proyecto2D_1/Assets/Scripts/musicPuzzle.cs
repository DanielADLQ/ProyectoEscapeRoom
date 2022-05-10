using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject globalVariables;
    [SerializeField] private GameObject musicGamePanel;
    [SerializeField] private GameObject musicExample;
    //[SerializeField] private GameObject btnClose;
    private GameObject player;
    private string numCompleto;
    private string numPrueba;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        //gameObject.transform.GetChild(0).GetComponent<Button>().enabled = false;
        for(int i=0;i<6;i++)
        {
            gameObject.transform.GetChild(i).GetComponent<Button>().enabled = false;
        }
        numCompleto = "14213042"; //La solucion es fija
        numPrueba = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(musicExample.GetComponent<TimelinePlayer>().isplaying)
        {
            for(int i=0;i<6;i++)
            {
                gameObject.transform.GetChild(i).GetComponent<Button>().enabled = false;
            }
            numPrueba = "";
        }
        else
        {
            for(int i=0;i<6;i++)
            {
                gameObject.transform.GetChild(i).GetComponent<Button>().enabled = true;
            }
        }   
    }

    public void btnMusicClick(string cod)
    {
        if(numPrueba.Length < 8)
        {
            Debug.Log("Registra");
            numPrueba += cod;
        }

        if(numPrueba.Length >= 8)
        {
            checkCode();
        }

    }

    private void checkCode()
    {
        Debug.Log("Ya no Registra");
        if(numPrueba == numCompleto)
        {
            Debug.Log("CORRECTO");
            globalVariables.GetComponent<GlobalVariables>().changeVariable("gotKey2","1");
            closeMusicPanel();

        }
        else
        {
            Debug.Log("INCORRECTO"); //AÑADIR SONIDO DE INCORRECTO
        }
        numPrueba = "";    
    }

    public void closeMusicPanel()
    {
        musicGamePanel.SetActive(false);
        //btnClose.SetActive(false);
        numPrueba = "";
        player.GetComponent<PlayerController>().canMove = true;
    }

}
