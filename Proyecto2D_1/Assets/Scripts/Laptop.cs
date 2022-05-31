using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour
{
    [SerializeField] public GameObject panelLaptop;
    [SerializeField] public GameObject cameraWithDb;
    [SerializeField] public GameObject confirmDoorCodeButton;
    //[SerializeField] public GameObject btnClose;
    private bool isPlayerInRange;
    private GameObject player;
    public string menuCode;

    public string ing1;
    public string ing2;
    public string ing3;
    public string ing4;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if(gameObject.tag == "SecretCodeGenerator")
        {
            menuCode = cameraWithDb.GetComponent<DBManager>().getRandomMenuCode();
            confirmDoorCodeButton.GetComponent<DoorCode>().secretNum = menuCode;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Z))
        {
            if (player.GetComponent<PlayerController>().canMove)
            {
                //didDialogueStart = true;
                player.GetComponent<PlayerController>().canMove = false;
                player.GetComponent<Animator>().SetBool("isWalking", false);

                //btnClose.SetActive(true);
                panelLaptop.SetActive(true);
            }
        }
    }
    public void closePanel()
    {
        panelLaptop.SetActive(false);
        player.GetComponent<PlayerController>().canMove = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            //dialogMark.SetActive(true);
            Debug.Log("Zona dialogo");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            //dialogMark.SetActive(false);
            Debug.Log("No Zona dialogo");
        }
    }
}
