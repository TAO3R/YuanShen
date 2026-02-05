using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public static AudioSource audioSrc;
    public static AudioClip click;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        click = Resources.Load<AudioClip>("click");
    }

    public static void playClick()
    {
        audioSrc.PlayOneShot(click);
    }
}
