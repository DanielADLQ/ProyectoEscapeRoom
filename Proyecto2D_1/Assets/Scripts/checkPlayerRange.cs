using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPlayerRange : MonoBehaviour
{

    public bool isPlayerInRange;
    public string itemTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            itemTag = this.tag;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            itemTag = "";
        }
    }

}
