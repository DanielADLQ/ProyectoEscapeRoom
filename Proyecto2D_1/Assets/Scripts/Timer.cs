using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    private bool cronoActive;
    private float currentTime;
    public string timeStr;

    //public Text timerText;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        StartCrono();
    }

    // Update is called once per frame
    void Update()
    {
        if(cronoActive)
        {
            if(currentTime >= 3600) //Fuerza un tiempo máximo (no influye en el juego, pero ya no se registra más)
            {
                StopCrono();
                currentTime = 3599.999f;
            }
            else
            {
                currentTime = currentTime + Time.deltaTime;
            }
            
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            timeStr = time.ToString(@"mm\:ss\:fff");
            
        }

    }

    public void StartCrono()
    {
        cronoActive = true;
    }

    public void StopCrono()
    {
        cronoActive = false;
    }

}
