using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [Tooltip("£¨Î´£©¸ßÁÁµÄÍ¼Æ¬")] [SerializeField] private Sprite[] sprites;
    private SpriteRenderer sp;
    [SerializeField] private bool canInteract = false;
    private Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            WallAni();
        }
    }

    public void WallAni()
    {
        Debug.Log(name + "is collapsing!");
        SoundManager.playDrill();
        if (canInteract)
        {
            ani.SetBool("isCollapse", true);
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerBackpack._instance.Interactables.Add(this.gameObject);
        //GetComponent<Outline>().enabled = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerBackpack._instance.Interactables.Remove(this.gameObject);
        // GetComponent<Outline>().enabled = false;
    }
    public void HighLight()
    {
        canInteract = true;
        ani.SetBool("highlight", true);
    }
    public void CancelHighLight()
    {
        canInteract = false;
        ani.SetBool("highlight", false);
    }
    public void DestroyWall()
    {
        this.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
