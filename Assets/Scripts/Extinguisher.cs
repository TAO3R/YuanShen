using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    public List<GameObject> canOperableList;
    private SpriteRenderer spriteRenderer;
    bool isUse = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color32(255, 255, 255, 100);
    }

    // Update is called once per frame
    void Update()
    {
        //≤‚ ‘
        /*if (Input.GetKeyDown(KeyCode.J))
        {
            Use();
        }*/
    }
    
    public bool Use()
    {
        spriteRenderer.color = new Color32(255, 255, 255, 255);
        isUse = true;
        foreach (GameObject gameObject in canOperableList)
        {
            if(gameObject.tag == "Fire")
            {
                gameObject.GetComponent<Fire>().PutOutFire();
                return true;
            }
        }
        return false;
    }
}
