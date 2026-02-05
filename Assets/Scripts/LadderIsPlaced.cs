using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderIsPlaced : MonoBehaviour
{
    [Tooltip("爬楼梯速度")] [SerializeField] public float climbSpeed = 2f;
    [Tooltip("是否在梯子上")] [SerializeField] private bool inLadder;
    [Tooltip("地板的碰撞体")] [SerializeField] public GameObject[] ground;
    [Tooltip("（未）高亮的图片")] [SerializeField] private Sprite[] sprites;
    [SerializeField] private bool canInteract = false;
    [SerializeField] private bool isTurn = false;
    private SpriteRenderer sp;
    Collider2D collisionCur;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
        ground = GameObject.FindGameObjectsWithTag("Ground");
        sp = GetComponent<SpriteRenderer>();
        audioSource = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inLadder)
        {
            Climb();
        }
    }
    public void Recycle()
    {
        Debug.Log(name + " gets recycled!");

        if (canInteract)
        {
            PlayerBackpack._instance.Interactables.Remove(this.gameObject);
            Debug.Log(name + "has been removed from the interactables list");
            
            Destroy(this.gameObject);
            //收起梯子
        }
    }
    private void Climb()
    {
        if(collisionCur.tag == "Player")
        {
            if (Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W))
            {
                if(!audioSource.isPlaying)
                    audioSource.Play();
                if (!isTurn)
                {
                    collisionCur.SendMessage("SetTurn", true);
                    isTurn = true;
                }               
                collisionCur.SendMessage("SetClimb", true);
                collisionCur.SendMessage("SetStop", false);
                inLadder = true;
                collisionCur.GetComponent<Rigidbody2D>().gravityScale = 0;
                for(int i = 0; i < ground.Length; i++)
                {
                    Physics2D.IgnoreCollision(collisionCur.GetComponents<CapsuleCollider2D>()[0], ground[i].GetComponent<Collider2D>());
                }
                collisionCur.GetComponent<Rigidbody2D>().velocity = new Vector2(0, climbSpeed);
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                if (!isTurn)
                {
                    collisionCur.SendMessage("SetTurn", true);
                    isTurn = true;
                }
                if (!audioSource.isPlaying)
                    audioSource.Play();
                collisionCur.SendMessage("SetClimb", true);
                collisionCur.SendMessage("SetStop", false);
                inLadder = true;
                collisionCur.GetComponent<Rigidbody2D>().gravityScale = 0;
                for (int i = 0; i < ground.Length; i++)
                {
                    Physics2D.IgnoreCollision(collisionCur.GetComponents<CapsuleCollider2D>()[0], ground[i].GetComponent<Collider2D>());
                }
                collisionCur.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -climbSpeed);
            }
            else
            {
                audioSource.Stop();
                collisionCur.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                collisionCur.SendMessage("SetClimb", false);
                collisionCur.SendMessage("SetStop", true);
            }

        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inLadder = true;
            collisionCur = collision;
        }          
        PlayerBackpack._instance.Interactables.Add(this.gameObject);
        //GetComponent<Outline>().enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerBackpack._instance.Interactables.Remove(this.gameObject);
        // GetComponent<Outline>().enabled = false;

        //离开梯子后
        audioSource.Stop();
        collision.GetComponent<Rigidbody2D>().gravityScale = 1;
        collision.SendMessage("SetClimb", false);
        collision.SendMessage("SetStop", false);
        inLadder = false;
        isTurn = false;
        for (int i = 0; i < ground.Length; i++)
        {
            Physics2D.IgnoreCollision(collision.GetComponents<CapsuleCollider2D>()[0], ground[i].GetComponent<Collider2D>(), false);
        }
    }
    public void HighLight()
    {
        canInteract = true;
        sp.sprite = sprites[0];
    }
    public void CancelHighLight()
    {
        canInteract = false;
        sp.sprite = sprites[1];
    }
}
