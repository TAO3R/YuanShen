using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    // private SpriteRenderer spriteRenderer;
    public GameObject ladderPre;

    // Start is called before the first frame update
    void OnEnable()
    {
        // spriteRenderer = GetComponent<SpriteRenderer>();
        // spriteRenderer.color = new Color32(255, 255, 255, 100);

        Debug.Log("Ladder shadow being activated!");
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.J))
        {
            Use();
        }*/
    }

    public void Use()
    {
        // spriteRenderer.color = new Color32(255, 255, 255, 255);
        //判断是否在地面
        Instantiate(ladderPre, transform.position, transform.rotation);
    }
}
