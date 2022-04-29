using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPush : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y < -7)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x,-7,gameObject.transform.position.z);
        }
    }
}
