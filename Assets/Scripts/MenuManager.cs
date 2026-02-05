using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager: MonoBehaviour
{
    //public GameObject start;
    public GameObject introduce;
    //public GameObject exit;

    private void Start()
    {
        introduce.SetActive(false);
    }

    public void LoadingGame()
    {
        SceneManager.LoadScene(1);
    }

    public void EndingGame()
    {

        Application.Quit();
    }

    public void ShowHint()
    {
        introduce.SetActive(true);
    }

    public void HideHint()
    {
        introduce.SetActive(false);
    }

}
