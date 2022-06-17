using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BetweenScenes : MonoBehaviour
{
    [SerializeField] private GameObject textSceneWait;
    public string newScene;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        textSceneWait.GetComponent<Text>().text = ".    ";
        yield return new WaitForSeconds(1.5f);
        textSceneWait.GetComponent<Text>().text = ". .  ";
        yield return new WaitForSeconds(1.5f);
        textSceneWait.GetComponent<Text>().text = ". . .";
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(newScene);
    }

}
