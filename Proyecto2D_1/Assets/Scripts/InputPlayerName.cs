using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPlayerName : MonoBehaviour
{
    [SerializeField] private GameObject confirmButton;
    [SerializeField] private GameObject inputText;
    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        if(inputText.GetComponent<Text>().text.Trim().Length == 0)
        {
            confirmButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            confirmButton.GetComponent<Button>().interactable = true;
            //AL REGISTRAR, COGER ESE TEXTO CON EL TRIM EN LA BASE DE DATOS
        }
    }
}
