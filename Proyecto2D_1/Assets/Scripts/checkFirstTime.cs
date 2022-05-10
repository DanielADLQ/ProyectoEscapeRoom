using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkFirstTime : MonoBehaviour
{
    private string checkedItem = "0";
    private bool isPlayerInRange;
    private GameObject player;
    [SerializeField] private TextAsset inkJsonAsset;
    public string variableInk="";
    private GameObject inkManager;
    //public string valorVar = "";
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        inkManager = GameObject.FindWithTag("InkManager");
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

                inkManager.GetComponent<InkManager>().StartStory(inkJsonAsset, variableInk, this.tag, checkedItem);
                checkedItem = "1";
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
}
