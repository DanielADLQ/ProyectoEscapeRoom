using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    [SerializeField] private GameObject globalVariables;
    [SerializeField] private TextAsset inkJsonAsset;
    private GameObject player;
    private GameObject inkManager;
    public string variableInk;
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
        if(gameObject.GetComponent<checkPlayerRange>().isPlayerInRange && Input.GetKeyDown(KeyCode.Z))
        {
            if(player.GetComponent<PlayerController>().canMove)
            {
                if(globalVariables.GetComponent<GlobalVariables>().getValue(variableInk) == "0") //No cumple condicion
                {
                    player.GetComponent<PlayerController>().canMove = false;
                    player.GetComponent<Animator>().SetBool("isWalking",false);
                    inkManager.GetComponent<InkManager>().StartStory(inkJsonAsset, variableInk, gameObject.GetComponent<checkPlayerRange>().itemTag);
                }
                else //Cumple condicion
                {
                    if(gameObject.tag == "HasPuzzle")
                    {
                        player.GetComponent<PlayerController>().canMove = false;
                        panelPuzzle.SetActive(true);
                    }
                    else
                    {
                        player.GetComponent<PlayerController>().canMove = false;
                        player.GetComponent<Animator>().SetBool("isWalking", false);
                        inkManager.GetComponent<InkManager>().StartStory(inkJsonAsset, variableInk, gameObject.GetComponent<checkPlayerRange>().itemTag, "1");
                        globalVariables.GetComponent<GlobalVariables>().changeVariable("taponEncontrado","1");
                    }

                }
            }
        }
    }
}
