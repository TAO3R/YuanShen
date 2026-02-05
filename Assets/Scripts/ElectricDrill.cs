using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricDrill : MonoBehaviour
{
    public List<GameObject> canOperableList;
    private SpriteRenderer spriteRenderer;
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
        foreach (GameObject gameObject in canOperableList)
        {
            if (gameObject.tag == "Wall")
            {
                gameObject.GetComponent<InjuredPerson>().BeHelp();
                return true;
            }
        }
        return false;
    }
}
