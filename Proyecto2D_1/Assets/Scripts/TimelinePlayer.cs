using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class TimelinePlayer : MonoBehaviour
{
    private PlayableDirector director;
    public GameObject controlPanel;
    public bool isplaying;
    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
        director.stopped += Director_Stopped;
        //isplaying = true;
    }
    private void Director_Stopped(PlayableDirector obj)
    {
        isplaying = false;
        if(controlPanel != null)
        {
            controlPanel.SetActive(true);
        }
        
    }
    private void Director_Played(PlayableDirector obj)
    {
        isplaying = true;
        if(controlPanel != null)
        {
            controlPanel.SetActive(false);
        }
    }
    public void StartTimeline()
    {
        director.Play();
    }
}