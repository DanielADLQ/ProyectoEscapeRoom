using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSave : MonoBehaviour
{
    private GameObject saveVariables;
    [SerializeField] private GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        saveVariables = GameObject.FindWithTag("SaveVariables");

        if(saveVariables.GetComponent<SaveVariables>().cod != 0) //Llegas al terminar el juego
        {
            gameObject.GetComponent<DBManager>().guardarPartida(saveVariables.GetComponent<SaveVariables>().cod);
            if(panel != null)
            {
                panel.SetActive(true);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
