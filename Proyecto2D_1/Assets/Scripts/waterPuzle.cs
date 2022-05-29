using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterPuzle : MonoBehaviour
{
    [SerializeField] private GameObject globalVariables;
    private bool isPlayerInRange;
    private GameObject player;
    [SerializeField] private GameObject bath;
    [SerializeField] private GameObject water;
    [SerializeField] private TextAsset inkJsonAsset;
    public string variableInk;
    private GameObject inkManager;
    public string valorVar = "-1";

    // Start is called before the first frame update
    void Start()
    {
        valorVar = "-1";
        player = GameObject.FindWithTag("Player");
        inkManager = GameObject.FindWithTag("InkManager");
    }

    // Update is called once per frame
    void Update()
    {

        if(valorVar=="-1" && globalVariables.GetComponent<GlobalVariables>().getValue("taponEncontrado") == "1")
        {
            valorVar="0";
        }

        if(isPlayerInRange && Input.GetKeyDown(KeyCode.Z))
        {
            if(player.GetComponent<PlayerController>().canMove)
            {
                //didDialogueStart = true;
                player.GetComponent<PlayerController>().canMove = false;
                player.GetComponent<Animator>().SetBool("isWalking",false);

                inkManager.GetComponent<InkManager>().StartStory(inkJsonAsset, variableInk, this.tag);
            }
        }

        if(int.Parse(valorVar)>=2)
        {
            water.SetActive(true);
        }
        else
        {
            water.SetActive(false);
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
