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
                //didDialogueStart = true;
                player.GetComponent<PlayerController>().canMove = false;
                player.GetComponent<Animator>().SetBool("isWalking",false);
                inkManager.GetComponent<InkManager>().StartStory(inkJsonAsset, variableInk, itemTag);
                //player.GetComponent<PlayerController>().canMove = true;
                //didDialogueStart = false;
                //StartDialogue();
            }
            /*else if(dialogueText.text == dialogueLines[lineIndex])
            {
                nextDialogueLine();
            }*/
        }
    }

    /*private void StartDialogue()
    {
        didDialogueStart = true;

        dialogueImage.sprite = characterPortrait; 

        player.GetComponent<PlayerController>().canMove = false;
        dialogPanel.SetActive(true);
        //dialogueMark.SetActive(false);
        //lineIndex = 0;
        //StartCoroutine(ShowLine());
    }

    private void nextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialogPanel.SetActive(false);
            player.GetComponent<PlayerController>().canMove = true;
            //dialogMark.SetActive(true);
        }

    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = String.Empty;

        foreach(char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }*/

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
