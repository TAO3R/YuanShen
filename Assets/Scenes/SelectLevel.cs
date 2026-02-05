using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    //未解锁图标和已完成的图标
    public GameObject locks;
    //public GameObject checkmark;
    //是否能被选择
    private bool isSelect = false;
    //是否已通关
    private bool isFinished = false;


    private void Awake()
    {
        locks = transform.Find("Lock").gameObject;
        //checkmark = transform.Find("Checkmark").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        int curLevel = int.Parse(this.name.Substring(name.Length - 1, 1));
        int maxLevel = PlayerPrefs.GetInt("maxLevel", 1);
        if (curLevel <= maxLevel)
        {
            isSelect = true;
            locks.gameObject.SetActive(false);
        }
        if (curLevel < maxLevel)
        {
            isFinished = true;
        }


        if (isFinished)
        {
            //checkmark.SetActive(true);
        }
        else
        {
            //checkmark.SetActive(false);
        }
    }

    public void Selected()
    {
        if (isSelect)
        {
            PlayerPrefs.SetInt("nowLevel", int.Parse(this.name.Substring(name.Length - 1, 1)));
            SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("nowLevel", 1));//场景跳转
        }

    }
}
