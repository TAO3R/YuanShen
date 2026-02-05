using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region Variables

    [Tooltip("玩家移动速度")][SerializeField] private float speed = 5f;
    public float Speed
    {
        get { return this.speed; }
        set { this.speed = value; }
    }

    [Tooltip("玩家当前是否着地")][SerializeField] private bool isGrounded = false;
    public bool IsGrounded
    {
        get { return this.isGrounded; }
        set { this.isGrounded = value; }
    }

    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask groundCheckMask;
    [SerializeField] private Rigidbody2D rbComponent;
    [SerializeField] private KeyCode leftward, rightward;
    [SerializeField] private float horizontalInput = 0f;    // A float value between -1 and 1

    private Animator ani;

    private AudioSource audioSource;

    #endregion

    #region MonoBehavior Callbacks

    // Start is called before the first frame update
    void Start()
    {
        rbComponent = transform.GetComponent<Rigidbody2D>();
        ani = transform.Find("Firefighter").GetComponent<Animator>();
        audioSource = transform.GetComponent<AudioSource>();

        Debug.Log("Current time scale = " + Time.timeScale);
    }

    private void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckMask) != null)
        {
            rbComponent.velocity = new Vector2(horizontalInput * speed, rbComponent.velocity.y);

            if (GameManager._instance.start)    // Tool shadow actiavtion will not be considered during the tool selection phase
            {
                if (!PlayerBackpack._instance.ToolShadows.GetChild((int)PlayerBackpack._instance.CurrentTool).gameObject.activeSelf)
                {   // To activate the current tool shdaow in case its deavtivated while the player is off the ground
                    PlayerBackpack._instance.ToolShadows.GetChild((int)PlayerBackpack._instance.CurrentTool).gameObject.SetActive(true);
                }
            }
            
        }
        else
        {
            Debug.Log("Not on ground!");
            PlayerBackpack._instance.ToolShadows.GetChild((int)PlayerBackpack._instance.CurrentTool).gameObject.SetActive(false);
        }
            
        //判断是否移动，添加动画
        if (Mathf.Abs(horizontalInput)> 0.1f)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();            
            SetRun(true);
        }
        else
        {
            audioSource.Stop();
            SetRun(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Movement-related
        horizontalInput = Input.GetAxis("Horizontal");
        // Debug.Log(horizontalInput);

        // Flip player horizontally
        if (horizontalInput > 0 & transform.localScale.x < 0 || horizontalInput < 0 & transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    #endregion

    public void SetClimb(bool climb)
    {
        ani.SetBool("isClimb", climb);

        PlayerBackpack._instance.ToolShadows.GetChild((int)tools.ladder).gameObject.SetActive(false);

        // Debug.Log("正在爬");

    }
    public void SetStop(bool stop)
    {
        ani.SetBool("isStop", stop);
        // Debug.Log("停在了梯子上");
    }
    public void SetRun(bool run)
    {
        ani.SetBool("isRun", run);

        if (PlayerBackpack._instance.CurrentTool == tools.ladder)
        {
            // PlayerBackpack._instance.LadderShadow.SetActive(true);
        }

        // Debug.Log("正在跑");
    }
    public void SetTurn(bool turn)
    {
        ani.SetBool("isTurn", turn);

        PlayerBackpack._instance.ToolShadows.GetChild((int)tools.ladder).gameObject.SetActive(false);

        // Debug.Log("转身");
    }
}
