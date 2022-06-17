using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mirror : MonoBehaviour
{
    [SerializeField] private GameObject bath;
    [SerializeField] private GameObject panelEspejo;
    [SerializeField] private GameObject panelEmpaniado;
    [SerializeField] private GameObject btnClose;
    private GameObject player;
    private GameObject numGenerator;
    [SerializeField] public GameObject n1;
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
            //Asigna los numeros del espejo
            n1.GetComponent<Text>().text = "";
            n2.GetComponent<Text>().text = "";
            n3.GetComponent<Text>().text = "";
            n4.GetComponent<Text>().text = "";

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(n1 != null || n2 != null || n3 != null || n4 != null)
        {
            if (n1.GetComponent<Text>().text.Length == 0)
            {
                n1.GetComponent<Text>().text = numGenerator.GetComponent<SecretCodeGenerator>().n1;
            }
            if (n2.GetComponent<Text>().text.Length == 0)
            {
                n2.GetComponent<Text>().text = numGenerator.GetComponent<SecretCodeGenerator>().n2;
            }
            if (n3.GetComponent<Text>().text.Length == 0)
            {
                n3.GetComponent<Text>().text = numGenerator.GetComponent<SecretCodeGenerator>().n3;
            }
            if (n4.GetComponent<Text>().text.Length == 0)
            {
                n4.GetComponent<Text>().text = numGenerator.GetComponent<SecretCodeGenerator>().n4;
            }
        }


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
