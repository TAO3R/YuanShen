using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yuan : MonoBehaviour
{
    private InjuredPerson injuredPersonScript;
    public InjuredPerson InjuredPersonScript
    {
        get { return this.injuredPersonScript; }
        set { this.injuredPersonScript = value; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DestroySelfAndHelpInjured()    // Used by animation frame event
    {
        // PlayerBackpack._instance.ToInteractWith.GetComponent<InjuredPerson>().BeHelp();
        injuredPersonScript.BeHelp();
        Destroy(this.gameObject);
    }
}
