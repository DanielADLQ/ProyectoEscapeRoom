using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    [SerializeField] private GameObject globalVariables;
    [SerializeField] private TextAsset inkJsonAsset;
    //private float typingTime = 0.05f;
    private bool isPlayerInRange;
    private bool didDialogueStart;
    //private int lineIndex;
    private GameObject player;
    private GameObject inkManager;
    public string variableInk;
    private string itemTag;
    [SerializeField] private GameObject panelPuzzle;
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
                //player.GetComponent<PlayerController>().canMove = false;
                //player.GetComponent<Animator>().SetBool("isWalking",false);
                //inkManager.GetComponent<InkManager>().StartStory(inkJsonAsset, variableInk, itemTag);
                if(globalVariables.GetComponent<GlobalVariables>().getValue(variableInk) == "0") //No cumple condicion
                {
                    Debug.Log("Cerrado");
                    player.GetComponent<PlayerController>().canMove = false;
                    player.GetComponent<Animator>().SetBool("isWalking",false);
                    inkManager.GetComponent<InkManager>().StartStory(inkJsonAsset, variableInk, itemTag);
                }
                else //Cumple condicion (Por ejemplo tener una llave)
                {
                    if(gameObject.tag == "HasPuzzle")
                    {
                        Debug.Log("Abierto, muestra puzle");
                        player.GetComponent<PlayerController>().canMove = false;
                        panelPuzzle.SetActive(true);
                        //globalVariables.GetComponent<GlobalVariables>().changeVariable("gotKey2","1");
                        
                    }
                    else
                    {
                        inkManager.GetComponent<InkManager>().StartStory(inkJsonAsset, variableInk, itemTag,"1");
                        Debug.Log("Abierto, consigues tapon");
                        globalVariables.GetComponent<GlobalVariables>().changeVariable("taponEncontrado","1");
                    }

                }
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
            itemTag = this.tag;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            //dialogMark.SetActive(false);
            Debug.Log("No Zona dialogo");
            itemTag = "";
        }
    }
}
