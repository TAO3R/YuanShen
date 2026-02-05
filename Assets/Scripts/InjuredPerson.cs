using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuredPerson : MonoBehaviour
{
    [Tooltip("£¨Î´£©¸ßÁÁµÄÍ¼Æ¬")] [SerializeField] private Sprite[] sprites;
    private SpriteRenderer sp;
    [Tooltip("µ¯³öµÄ¾ÈÔ®Í¼")][SerializeField] public GameObject window;
    [SerializeField] private bool canInteract = false;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeHelp()
    {
        Debug.Log(name + " gets helped!");
        SoundManager.playOxygen();
        /*if (canInteract)
        {
            StartCoroutine(Anima());
        } */
        StartCoroutine(Anima());
    }

    IEnumerator Anima()
    {
        window.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        window.SetActive(false);
        //»ÓÊÖ¶¯»­
        anim.SetBool("isUp", true);
        yield return new WaitForSeconds(1.5f);
        GameManager._instance.CurNum++;
        PlayerBackpack._instance.Interactables.Remove(this.gameObject);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBackpack._instance.Interactables.Add(this.gameObject);
        //GetComponent<Outline>().enabled = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerBackpack._instance.Interactables.Remove(this.gameObject);
        // GetComponent<Outline>().enabled = false;
    }
    public void HighLight()
    {
        Debug.Log(name + " gets highlighted!");
        canInteract = true;
        anim.SetBool("isHighLight", true);
    }
    public void CancelHighLight()
    {
        Debug.Log(name + " loses highlight!");
        canInteract = false;
        anim.SetBool("isHighLight", false);

    }
}
