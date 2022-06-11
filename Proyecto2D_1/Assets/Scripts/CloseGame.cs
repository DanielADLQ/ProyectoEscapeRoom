using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseGame : MonoBehaviour
{
    private GameObject player;
    private GameObject saveVariables;
    [SerializeField] private GameObject panelCloseGame;

    // Start is called before the first frame update
    void Start()
    {
        saveVariables = GameObject.FindWithTag("SaveVariables");
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameObject.tag == "ClosePanel")
            {
                if (player.GetComponent<PlayerController>().canMove)
                {
                    player.GetComponent<PlayerController>().canMove = false;
                    player.GetComponent<Animator>().SetBool("isWalking", false);

                    panelCloseGame.SetActive(true);

                }
            }
        }
    }

    public void returnToMainMenu()
    {
        saveVariables.GetComponent<SaveVariables>().cod = 0;
        SceneManager.LoadSceneAsync("Start");
    }

    public void closeGame()
    {
        try
        {
            UnityEditor.EditorApplication.isPlaying = false; //Para simular la funcionalidad
            
        }
        catch
        {
            Application.Quit(); //Para la version final
        }
        
    }

    public void openInfoPanel([SerializeField] GameObject panel)
    {
        panel.SetActive(true);
    }

    public void closeInfoPanel([SerializeField] GameObject panel)
    {
        panel.SetActive(false);
        player.GetComponent<PlayerController>().canMove = true;
    }

}
