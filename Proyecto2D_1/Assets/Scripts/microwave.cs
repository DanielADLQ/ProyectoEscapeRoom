using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class microwave : MonoBehaviour
{
    [SerializeField] private GameObject fridge;
    private bool isPlayerInRange;
    private GameObject player;
    [SerializeField] private TextAsset inkJsonAsset;
    private GameObject inkManager;
    public string variableInk;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        inkManager = GameObject.FindWithTag("InkManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(fridge.transform.GetComponent<fridgePuzle>().valorVar == "2") //Tienes bloque de hielo
        {
            if (isPlayerInRange && Input.GetKeyDown(KeyCode.Z))
            {
                if (player.GetComponent<PlayerController>().canMove)
                {
                    //didDialogueStart = true;
                    player.GetComponent<PlayerController>().canMove = false;
                    player.GetComponent<Animator>().SetBool("isWalking", false);

                    inkManager.GetComponent<InkManager>().StartStory(inkJsonAsset, variableInk, this.tag);

                }
            }
        }

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
