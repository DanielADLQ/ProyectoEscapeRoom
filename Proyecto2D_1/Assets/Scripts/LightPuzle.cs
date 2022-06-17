using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzle : MonoBehaviour
{
    [SerializeField] private GameObject lightPanel;
    [SerializeField] private GameObject lampLight;
    [SerializeField] private GameObject roomLight;
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
        if(gameObject.tag == "Bed")
        {
            if(gameObject.GetComponent<checkPlayerRange>().isPlayerInRange && Input.GetKeyDown(KeyCode.Z))
            {
                if(roomLight.activeSelf && lampLight.activeSelf)
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
        else
        {
            if(gameObject.GetComponent<checkPlayerRange>().isPlayerInRange && Input.GetKeyDown(KeyCode.Z))
            {
                if(lightPanel.activeSelf){
                    lightPanel.SetActive(false);
                }
                else
                {
                    lightPanel.SetActive(true);
                }
                
            }
        }

    }

}
