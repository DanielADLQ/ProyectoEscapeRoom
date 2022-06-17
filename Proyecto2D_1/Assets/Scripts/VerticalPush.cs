using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPush : MonoBehaviour
{
    int minY = -7;
    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y < minY)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, minY, gameObject.transform.position.z);
        }
    }
}
