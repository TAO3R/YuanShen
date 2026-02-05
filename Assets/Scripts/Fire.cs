using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire: MonoBehaviour
{
    [Tooltip("是否按下了使用键")][SerializeField] private bool isKeyDown = false;
    [Tooltip("按住使用键的时间")][SerializeField] private float timer = 0;
    [Tooltip("（未）高亮的图片")] [SerializeField] private Sprite[] sprites;
    private SpriteRenderer sp;
    [SerializeField] private bool canInteract = false;
    private Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        ani = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isKeyDown && Input.GetKey(KeyCode.K))
        {
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                PlayFireExtinguishedAnim();
            }
        }
        else
        {
            timer = 0;
            isKeyDown = false;
        }
    }

    public void PutOutFire()
    {
        Debug.Log(name + " is being put out!");
        
        if (canInteract)
        {
            SoundManager.playOutfire();
            isKeyDown = true;
        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // print("碰撞了");
        PlayerBackpack._instance.Interactables.Add(this.gameObject);

        //GetComponent<Outline>().enabled = true;
        //transform.GetComponent<UnityEngine.UI.Outline>().effectColor = Color.red;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // print("bye");
        PlayerBackpack._instance.Interactables.Remove(this.gameObject);
        // GetComponent<Outline>().enabled = false;
    }

    public void HighLight()
    {
        canInteract = true;
        ani.SetBool("isHighLight", true);
        Debug.Log(name + " gets highlighted");
    }
    public void CancelHighLight()
    {
        canInteract = false;
        ani.SetBool("isHighLight", false);
        Debug.Log(name + " loses highlight");
    }
   
    private void PlayFireExtinguishedAnim()
    {
        Debug.Log(name + "is triggering its extinguish animation");
        ani.SetTrigger("extinguish");
    }

    private void DestroyFire()   // Used by animation frame event
    {
        // Remove the current gameobject from Interactables before destroy
        PlayerBackpack._instance.Interactables.Remove(this.gameObject);
        Destroy(gameObject);
    }
}
