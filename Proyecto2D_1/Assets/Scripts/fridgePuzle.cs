using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fridgePuzle : MonoBehaviour
{
    [SerializeField] private GameObject globalVariables;
    private GameObject player;
    [SerializeField] private GameObject fridge;
    [SerializeField] private TextAsset inkJsonAsset;
    public string variableInk;
    private GameObject inkManager;
    public string valorVar = "0";

    // Start is called before the first frame update
    void Start()
    {
        valorVar = "0";
        player = GameObject.FindWithTag("Player");
        inkManager = GameObject.FindWithTag("InkManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<checkPlayerRange>().isPlayerInRange && Input.GetKeyDown(KeyCode.Z))
        {
            if (player.GetComponent<PlayerController>().canMove)
            {
                player.GetComponent<PlayerController>().canMove = false;
                player.GetComponent<Animator>().SetBool("isWalking", false);

                inkManager.GetComponent<InkManager>().StartStory(inkJsonAsset, variableInk, this.tag);
            }
        }
    }

}
