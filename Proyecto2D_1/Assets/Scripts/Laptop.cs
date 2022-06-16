using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour
{
    [SerializeField] public GameObject panelLaptop;
    [SerializeField] public GameObject cameraWithDb;
    [SerializeField] public GameObject confirmDoorCodeButton;
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
        if(gameObject.tag == "SecretCodeGenerator")
        {
            if (gameObject.GetComponent<checkPlayerRange>().isPlayerInRange && Input.GetKeyDown(KeyCode.Z))
            {
                if (player.GetComponent<PlayerController>().canMove)
                {
                    player.GetComponent<PlayerController>().canMove = false;
                    player.GetComponent<Animator>().SetBool("isWalking", false);

                    panelLaptop.SetActive(true);
                }
            }
        }
    }
    public void closePanel()
    {
        panelLaptop.SetActive(false);
        player.GetComponent<PlayerController>().canMove = true;

    }
}
