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
    private GameObject cam;
    private AudioSource audS;
    public AudioClip audC;

    void Start()
    {
        try
        {
            numGenerator = GameObject.FindWithTag("SecretCodeGenerator");
            secretNum = numGenerator.GetComponent<SecretCodeGenerator>().numCompleto;
        }
        catch
        {
            
        }
        player = GameObject.FindWithTag("Player");
        door = GameObject.FindWithTag("Door");
        screen.text="";
        cam = GameObject.FindWithTag("MainCamera");
        audS = cam.GetComponent<AudioSource>();

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
            inputNum = gameObject.GetComponentInChildren<Text>().text;
            screen.text += inputNum;
        }   
    }
    public void checkCode()
    {
        if(screen.text.Length == codeLength)
        {

            if(screen.text.Equals(secretNum))
            {
                closePanelCode();
                door.SetActive(false);
            }
            else
            {
                screen.text = "";
                if (audC != null)
                {
                    audS.PlayOneShot(audC);
                }
            }
        }
    }
    public void clearScreen()
    {
        screen.text = "";
    }
}
