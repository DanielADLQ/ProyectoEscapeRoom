using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairPuzle : MonoBehaviour
{

    private bool isPlayerInRange;
    private bool chairNearShelf;
    private GameObject player;
    [SerializeField] private TextAsset inkJsonAsset;
    public string variableInk;
    private GameObject inkManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        inkManager = GameObject.FindWithTag("InkManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerInRange && chairNearShelf && Input.GetKeyDown(KeyCode.Z))
        {
            if(player.GetComponent<PlayerController>().canMove)
            {
                //didDialogueStart = true;
                player.GetComponent<PlayerController>().canMove = false;
                player.GetComponent<Animator>().SetBool("isWalking",false);
                Debug.Log("Me subo a la silla");
                inkManager.GetComponent<InkManager>().StartStory(inkJsonAsset, variableInk, this.tag);
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

        if(collision.gameObject.CompareTag("Shelf"))
        {
            chairNearShelf = true;
            //dialogMark.SetActive(true);
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
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

        if(collision.gameObject.CompareTag("Shelf"))
        {
            chairNearShelf = false;
            //dialogMark.SetActive(true);
        }
    }
}
