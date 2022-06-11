using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    //[SerializeField] private GameObject dialogPanel;
    //[SerializeField] private TMP_Text dialogueText;
    //[SerializeField, TextArea(4,6)] private String[] dialogueLines;
    [SerializeField] private TextAsset inkJsonAsset;
    //private float typingTime = 0.05f;
    private bool isPlayerInRange;
    private bool didDialogueStart;
    //private int lineIndex;
    private GameObject player;
    private GameObject inkManager;
    public string variableInk;
    private string itemTag;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        inkManager = GameObject.FindWithTag("InkManager");
        //dialogueImage = GameObject.FindWithTag("Portrait");
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.Z))
        {
            if(player.GetComponent<PlayerController>().canMove)
            {
                player.GetComponent<PlayerController>().canMove = false;
                player.GetComponent<Animator>().SetBool("isWalking",false);
                inkManager.GetComponent<InkManager>().StartStory(inkJsonAsset, variableInk, itemTag);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            itemTag = this.tag;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            itemTag = "";
        }
    }
}
