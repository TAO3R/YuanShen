using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //[Tooltip("ÔÝÍ£Í¼Æ¬")] [SerializeField] public Sprite pauseSprite;
    //[Tooltip("¿ªÊ¼Í¼Æ¬")] [SerializeField] public Sprite startSprite;
    //[Tooltip("ÔÝÍ£/¿ªÊ¼°´Å¥")] [SerializeField] private Button button;
    [Tooltip("ÊÇ·ñÔÝÍ£")] [SerializeField] public static bool isPause = false;

    [SerializeField] private GameObject hint;

    private void Start()
    {
        //button = transform.GetComponent<Button>();

        hint.SetActive(false);

        /*if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Time.timeScale = 0;
            Debug.Log("log message.");
        }*/
    }
    private void Update()
    {
        
    }
    public void PauseOrStart()
    {
        if (!isPause)
        {
            isPause = true;
            //button.image.sprite = startSprite;
            Time.timeScale = 0f;
            ShowHint();
        }
        else
        {
            isPause = false;
            //button.image.sprite = pauseSprite;
            Time.timeScale = 1f;
            HideHint();
        }
    }

    public void Again()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ShowHint()
    {
        hint.SetActive(true);      
    }

    public void HideHint()
    {
        hint.SetActive(false);
    }

    /*public void HideStory()
    {
        story.SetActive(false);
        Time.timeScale = 1;
    }*/
    public void NextLevel()
    {
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            PlayerPrefs.SetInt("nowLevel", PlayerPrefs.GetInt("nowLevel") + 1);
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
