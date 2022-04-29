using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzle : MonoBehaviour
{
    //[SerializeField] private GameObject lightSwitch;
    //[SerializeField] private GameObject lamp;
    //[SerializeField] private GameObject bed;
    [SerializeField] private GameObject lightPanel;
    [SerializeField] private GameObject lampLight;
    [SerializeField] private GameObject roomLight;
    //private GameObject roomLight;
    //private GameObject lampLight;
    private bool isPlayerInRange;
    private GameObject player;
    [SerializeField] private TextAsset inkJsonAsset;
    public string variableInk;
    private GameObject inkManager;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        inkManager = GameObject.FindWithTag("InkManager");
        //roomLight = GameObject.FindWithTag("Dark");
        //lampLight = GameObject.FindWithTag("LampLight");
        //Debug.Log(roomLight.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag == "Bed")
        {
            if(isPlayerInRange && Input.GetKeyDown(KeyCode.Z))
            {
                if(roomLight.activeSelf && lampLight.activeSelf)
                {
                    Debug.Log("Enseña el numero que toque");
                    player.GetComponent<PlayerController>().canMove = false;
                    player.GetComponent<Animator>().SetBool("isWalking",false);
                    inkManager.GetComponent<InkManager>().StartStory(inkJsonAsset, variableInk, this.tag);
                    //Podria usar un panel que sea un dibujo del techo
                }
            }
        }
        else
        {
            if(isPlayerInRange && Input.GetKeyDown(KeyCode.Z))
            {
                //if(gameObject.tag == "")
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
