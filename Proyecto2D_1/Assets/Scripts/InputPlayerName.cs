using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPlayerName : MonoBehaviour
{
    [SerializeField] private GameObject confirmButton;
    [SerializeField] private GameObject inputText;

    void Update()
    {
        if(inputText.GetComponent<Text>().text.Trim().Length == 0)
        {
            confirmButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            confirmButton.GetComponent<Button>().interactable = true;
        }
    }
}
