using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catFoodPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject fridge;
    private GameObject player;
    [SerializeField] private TextAsset inkJsonAsset;
    [SerializeField] private GameObject timelinePlayer;
    private GameObject inkManager;
    public string variableInk;

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
        if (fridge.transform.GetComponent<fridgePuzle>().valorVar == "1") //Tienes bloque de hielo
        {
            if (gameObject.GetComponent<checkPlayerRange>().isPlayerInRange && Input.GetKeyDown(KeyCode.Z))
            {
                if (player.GetComponent<PlayerController>().canMove)
                {
                    //didDialogueStart = true;
                    player.GetComponent<PlayerController>().canMove = false;
                    player.GetComponent<Animator>().SetBool("isWalking", false);

                    //Lanzar Timeline, al acabar poner variable fridge a 0
                    //Y activar dialogo gato

                    timelinePlayer.GetComponent<TimelinePlayer>().StartTimeline();
                    animLaunched = true;

                }
            }
        }

        if(animLaunched && !timelinePlayer.GetComponent<TimelinePlayer>().isplaying) //Momento en el que finaliza la ejecucion del timeline
        {
            inkManager.GetComponent<InkManager>().StartStory(inkJsonAsset, variableInk, this.tag);
            animLaunched = false;
        }

    }
}
