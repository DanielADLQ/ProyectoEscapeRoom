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

    [SerializeField] private GameObject timelinePlayer;
    private bool animLaunched = false;

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
                player.GetComponent<PlayerController>().canMove = false;
                player.GetComponent<Animator>().SetBool("isWalking",false);

                timelinePlayer.GetComponent<TimelinePlayer>().StartTimeline();
                animLaunched = true;

            }
        }

        if (animLaunched && !timelinePlayer.GetComponent<TimelinePlayer>().isplaying) //Momento en el que finaliza la ejecucion del timeline
        {
            inkManager.GetComponent<InkManager>().StartStory(inkJsonAsset, variableInk, this.tag);
            animLaunched = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }

        if(collision.gameObject.CompareTag("Shelf"))
        {
            chairNearShelf = true;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }

        if(collision.gameObject.CompareTag("Shelf"))
        {
            chairNearShelf = false;
        }
    }
}
