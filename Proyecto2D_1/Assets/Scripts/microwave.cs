using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class microwave : MonoBehaviour
{
    [SerializeField] private GameObject fridge;
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
}
