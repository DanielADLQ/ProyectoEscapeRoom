using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mirror : MonoBehaviour
{
    [SerializeField] private GameObject bath;
    //[SerializeField] private GameObject canvasMirror;
    [SerializeField] private GameObject panelEspejo;
    [SerializeField] private GameObject panelEmpaniado;
    [SerializeField] private GameObject btnClose;
    private GameObject player;
    private GameObject numGenerator;
    [SerializeField] private GameObject n1;
    [SerializeField] private GameObject n2;
    [SerializeField] private GameObject n3;
    [SerializeField] private GameObject n4;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        checkPlayerRange check;
        if (gameObject.TryGetComponent<checkPlayerRange>(out check))
        {
            numGenerator = GameObject.FindWithTag("SecretCodeGenerator");
            n1.GetComponent<Text>().text = numGenerator.GetComponent<SecretCodeGenerator>().n1;
            n2.GetComponent<Text>().text = numGenerator.GetComponent<SecretCodeGenerator>().n2;
            n3.GetComponent<Text>().text = numGenerator.GetComponent<SecretCodeGenerator>().n3;
            n4.GetComponent<Text>().text = numGenerator.GetComponent<SecretCodeGenerator>().n4;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerRange check;
        if (gameObject.TryGetComponent<checkPlayerRange>(out check))
        {
            if (check.isPlayerInRange && Input.GetKeyDown(KeyCode.Z))
            {
                if (player.GetComponent<PlayerController>().canMove)
                {
                    player.GetComponent<PlayerController>().canMove = false;
                    player.GetComponent<Animator>().SetBool("isWalking", false);

                    btnClose.SetActive(true);
                    if (bath.GetComponent<waterPuzle>().valorVar == "4")
                    {
                        panelEspejo.SetActive(false);
                        panelEmpaniado.SetActive(true);
                    }
                    else
                    {
                        panelEspejo.SetActive(true);
                        panelEmpaniado.SetActive(false);
                    }
                }
            }
        }
    }

    public void closePanel()
    {
        panelEspejo.SetActive(false);
        panelEmpaniado.SetActive(false);
        btnClose.SetActive(false);
        player.GetComponent<PlayerController>().canMove = true;

    }

}
