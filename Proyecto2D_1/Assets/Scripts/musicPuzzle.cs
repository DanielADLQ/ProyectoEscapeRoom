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
    private GameObject player;
    private GameObject cam;
    private string numCompleto;
    private string numPrueba;
    private AudioSource audS;
    public AudioClip audC;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        for(int i=0;i<6;i++)
        {
            gameObject.transform.GetChild(i).GetComponent<Button>().enabled = false;
        }
        numCompleto = "14213042"; //La solucion
        numPrueba = "";

        cam = GameObject.FindWithTag("MainCamera");
        audS = cam.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Activa o desactiva los botones del puzle
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
            numPrueba += cod;
        }

        if(numPrueba.Length >= 8)
        {
            checkCode();
        }

    }

    private void checkCode()
    {
        if(numPrueba == numCompleto)
        {
            globalVariables.GetComponent<GlobalVariables>().changeVariable("gotKey2","1");
            closeMusicPanel();
        }
        else
        {
            if (audC != null)
            {
                audS.PlayOneShot(audC);
            }
        }
        numPrueba = "";    
    }

    public void closeMusicPanel()
    {
        musicGamePanel.SetActive(false);
        numPrueba = "";
        player.GetComponent<PlayerController>().canMove = true;
    }

}
