using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class TimelinePlayer : MonoBehaviour
{
    private PlayableDirector director;
    public GameObject controlPanel;
    public bool isplaying;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
        director.stopped += Director_Stopped;

        if (gameObject.tag == "InitialCutscene")
        {
            director.Play();
        }

    }
    private void Director_Stopped(PlayableDirector obj)
    {
        isplaying = false;

        if(gameObject.tag == "InitialCutscene")
        {
            player.GetComponent<PlayerController>().canMove = true;
        }

        if(gameObject.tag == "CutsceneThenText")
        {
            player.GetComponent<PlayerController>().canMove = false;
        }

        if (controlPanel != null)
        {
            controlPanel.SetActive(true);
        }
        
    }
    private void Director_Played(PlayableDirector obj)
    {
        isplaying = true;
        player.GetComponent<PlayerController>().canMove = false;
        if (controlPanel != null)
        {
            controlPanel.SetActive(false);
        }
    }
    public void StartTimeline()
    {
        director.Play();
    }
}