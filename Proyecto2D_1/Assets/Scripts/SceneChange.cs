using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string newSceneName;
    public string actualSceneNum;

    private GameObject cam;
    private GameObject saveVariables;

    private AudioSource audS;
    public AudioClip audC;

    // Start is called before the first frame update
    void Start()
    {
        saveVariables = GameObject.FindWithTag("SaveVariables");
        cam = GameObject.FindWithTag("MainCamera");
        audS = cam.GetComponent<AudioSource>();

        checkPlayerRange check;
        if (gameObject.TryGetComponent<checkPlayerRange>(out check))
        {
            check.isPlayerInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerRange check;
        if (gameObject.TryGetComponent<checkPlayerRange>(out check))
        {
            if (check.isPlayerInRange)
            {
                cam.GetComponent<DBManager>().guardarTiempo(saveVariables.GetComponent<SaveVariables>().cod, actualSceneNum, cam.GetComponent<Timer>().timeStr);
                loadNewScene(newSceneName);
            }
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
        /*try
        {
            UnityEditor.EditorApplication.isPlaying = false; //Para simular la funcionalidad
        }
        catch
        {*/
            Application.Quit(); //Para la version final
        //}
    }

    public void openInfoPanel([SerializeField] GameObject panel)
    {
        panel.SetActive(true);
        if(audC != null)
        {
            audS.PlayOneShot(audC);
        }
    }

    public void closeInfoPanel([SerializeField] GameObject panel)
    {
        panel.SetActive(false);
    }
}
