using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterPuzle : MonoBehaviour
{
    [SerializeField] private GameObject globalVariables;
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

        checkPlayerRange check;
        if (gameObject.TryGetComponent<checkPlayerRange>(out check))
        {
            if (check.isPlayerInRange && Input.GetKeyDown(KeyCode.Z))
            {
                if (player.GetComponent<PlayerController>().canMove)
                {
                    player.GetComponent<PlayerController>().canMove = false;
                    player.GetComponent<Animator>().SetBool("isWalking", false);

                    inkManager.GetComponent<InkManager>().StartStory(inkJsonAsset, variableInk, this.tag);
                }
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

}
