using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField] private TextAsset inkJsonAsset;
    private GameObject player;
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
        if(gameObject.GetComponent<checkPlayerRange>().isPlayerInRange && Input.GetKeyDown(KeyCode.Z))
        {
            if(player.GetComponent<PlayerController>().canMove)
            {
                player.GetComponent<PlayerController>().canMove = false;
                player.GetComponent<Animator>().SetBool("isWalking",false);
                inkManager.GetComponent<InkManager>().StartStory(inkJsonAsset, variableInk, gameObject.GetComponent<checkPlayerRange>().itemTag);
            }

        }
    }
}
