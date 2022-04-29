using System;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorCode : MonoBehaviour
{
    private bool isPlayerInRange;
    private GameObject player;
    // Start is called before the first frame update
    [SerializeField] private GameObject panelCode;
    [SerializeField] private GameObject btnClose;
    [SerializeField] private Text screen;
    private String screenNum;
    private String inputNum;
    public int codeLength;
    private string secretNum = "";
    private GameObject door;
    private GameObject numGenerator;
    //private bool acertado = false;
    void Start()
    {
        numGenerator = GameObject.FindWithTag("SecretCodeGenerator");
        isPlayerInRange = false;
        player = GameObject.FindWithTag("Player");
        //panelCode = GameObject.FindWithTag("PanelCode");
        door = GameObject.FindWithTag("Door");
        screen.text="";
        secretNum = numGenerator.GetComponent<SecretCodeGenerator>().numCompleto;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.Z))
        {
            if(player.GetComponent<PlayerController>().canMove)
            {
                //didDialogueStart = true;
                player.GetComponent<PlayerController>().canMove = false;
                player.GetComponent<Animator>().SetBool("isWalking",false);

                panelCode.SetActive(true);
                btnClose.SetActive(true);
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            //dialogMark.SetActive(true);
            Debug.Log("Zona dialogo");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            //dialogMark.SetActive(false);
            Debug.Log("No Zona dialogo");
        }
    }

    public void closePanelCode()
    {
        panelCode.SetActive(false);
        btnClose.SetActive(false);
        clearScreen();
        player.GetComponent<PlayerController>().canMove = true;

    }

    public void inputNumber()
    {
        if(screen.text.Length < codeLength)
        {
            //screenNum = screen.text;
            inputNum = gameObject.GetComponentInChildren<Text>().text;
            Debug.Log(inputNum);
            screen.text += inputNum;
            Debug.Log(screen.text);
        }
        
    }

    public void checkCode()
    {
        if(screen.text.Length == codeLength)
        {
            Debug.Log("Tiene las 4 cifras");
            //acertado = true;
            if(screen.text.Equals(secretNum))
            {
                Debug.Log("CORRECTO!!!");
                closePanelCode();
                door.SetActive(false);
            }
            else
            {
                //acertado = false;
                Debug.Log("INCORRECTOOOOOO "+screen.text+" "+secretNum);
                screen.text = "";
            }
        }
        else
        {
            Debug.Log("DEBEN SER 4 CIFRAS");
        }
        //Debug.Log(acertado.ToString());
    }

    public void clearScreen()
    {
        screen.text = "";
    }
}
