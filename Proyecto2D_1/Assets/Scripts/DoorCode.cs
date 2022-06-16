using System;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorCode : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    [SerializeField] private GameObject panelCode;
    [SerializeField] private GameObject btnClose;
    [SerializeField] private Text screen;
    private String screenNum;
    private String inputNum;
    public int codeLength;
    public string secretNum = "";
    private GameObject door;
    private GameObject numGenerator;
    //private bool acertado = false;
    void Start()
    {
        try
        {
            numGenerator = GameObject.FindWithTag("SecretCodeGenerator");
            secretNum = numGenerator.GetComponent<SecretCodeGenerator>().numCompleto;
        }
        catch
        {
            //Debug.Log("No hay generador de numeros secretos");
        }
        player = GameObject.FindWithTag("Player");
        door = GameObject.FindWithTag("Door");
        screen.text="";

    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerRange check;
        if(gameObject.TryGetComponent<checkPlayerRange>(out check))
        {
            if (gameObject.GetComponent<checkPlayerRange>().isPlayerInRange && Input.GetKeyDown(KeyCode.Z))
            {
                if (player.GetComponent<PlayerController>().canMove)
                {
                    player.GetComponent<PlayerController>().canMove = false;
                    player.GetComponent<Animator>().SetBool("isWalking", false);

                    panelCode.SetActive(true);
                    btnClose.SetActive(true);

                }
            }
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

            //acertado = true;
            if(screen.text.Equals(secretNum))
            {
                closePanelCode();
                door.SetActive(false);
            }
            else
            {
                screen.text = "";
            }
        }
    }
    public void clearScreen()
    {
        screen.text = "";
    }
}
