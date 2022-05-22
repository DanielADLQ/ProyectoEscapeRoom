using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSave : MonoBehaviour
{
    private GameObject saveVariables;
    // Start is called before the first frame update
    void Start()
    {
        saveVariables = GameObject.FindWithTag("SaveVariables");
        gameObject.GetComponent<DBManager>().guardarPartida(saveVariables.GetComponent<SaveVariables>().cod);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
