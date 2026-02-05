using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    [Tooltip("需要救援的人数")] [SerializeField] private int needNum;
    [Tooltip("已经救援的人数")] [SerializeField] private int curNum;
    public int CurNum
    {
        get { return this.curNum; }
        set { this.curNum = value; }
    }
    [Tooltip("时间")] [SerializeField] private float remainTimer = 60;
    [Tooltip("胜利面板")] [SerializeField] public GameObject winPanel;
    [Tooltip("失败面板")] [SerializeField] public GameObject losePanel;
    [Tooltip("时间文本")] [SerializeField] private TMPro.TextMeshProUGUI remainTimeText;
    [Tooltip("救援人数文本")] [SerializeField] private TMPro.TextMeshProUGUI personNumText;

    [SerializeField] private bool passed;
    [SerializeField] private bool failed;
    [SerializeField] public bool start;
    public bool Passed
    {
        get { return this.passed; }
        set { this.passed = value; }
    }
    public bool Failed
    {
        get { return this.failed; }
        set { this.failed = value; }
    }

    private void Awake()
    {
        _instance = this;
        Time.timeScale = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        // _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!start||passed || failed) return;
        remainTimeText.gameObject.SetActive(true);
        personNumText.gameObject.SetActive(true);
        remainTimer -= Time.deltaTime;
        remainTimeText.text = "00:" + remainTimer.ToString("f0");
        personNumText.text = "Rescued:" + curNum + "/" + needNum;
        if (curNum == needNum)
        {
            Win();
        }
        if (remainTimer <= 0)
        {
            Lose();
        }
    }
    private void Win()
    {
        Time.timeScale = 0;
        SoundManager.playWin();
        winPanel.SetActive(true);
        if (PlayerPrefs.GetInt("maxLevel", 1) == PlayerPrefs.GetInt("nowLevel"))
            PlayerPrefs.SetInt("maxLevel", PlayerPrefs.GetInt("nowLevel") + 1);
    }
    private void Lose()
    {
        Time.timeScale = 0;
        SoundManager.playLose();
        losePanel.SetActive(true);
    }
}
