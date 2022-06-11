using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private bool isPlayerInRange;
    public string newSceneName;
    public string actualSceneNum;

    private GameObject cam;
    private GameObject saveVariables;

    // Start is called before the first frame update
    void Start()
    {
        saveVariables = GameObject.FindWithTag("SaveVariables");
        cam = GameObject.FindWithTag("MainCamera");
        isPlayerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerInRange)
        {
            cam.GetComponent<DBManager>().guardarTiempo(saveVariables.GetComponent<SaveVariables>().cod, actualSceneNum, cam.GetComponent<Timer>().timeStr);
            loadNewScene(newSceneName);
        }
    }

    public void loadNewScene(string sceneN)
    {
        SceneManager.LoadSceneAsync(sceneN);
    }

    public void loadNewScene()
    {
        SceneManager.LoadSceneAsync(newSceneName);
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
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            //dialogMark.SetActive(true);
            Debug.Log("Zona cambio de escena");
            //itemTag = this.tag;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            //dialogMark.SetActive(false);
            //Debug.Log("No Zona dialogo");
            //itemTag = "";
        }
    }

}
