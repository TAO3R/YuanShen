using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioSource audioSrc;
    public static AudioClip bgm;
    public static AudioClip win;
    public static AudioClip lose;
    public static AudioClip placeLadder;
    public static AudioClip oxygen;
    public static AudioClip fire;
    public static AudioClip outfire;
    public static AudioClip click;
    public static AudioClip climb;
    public static AudioClip drill;
    public static AudioClip run;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        bgm = Resources.Load<AudioClip>("关卡中BGM-火灾现场");
        win = Resources.Load<AudioClip>("win");
        lose = Resources.Load<AudioClip>("lose");
        placeLadder = Resources.Load<AudioClip>("放置梯子");
        oxygen = Resources.Load<AudioClip>("氧气瓶-吸氧呼吸声");
        fire = Resources.Load<AudioClip>("fire");
        outfire = Resources.Load<AudioClip>("outfire");
        click = Resources.Load<AudioClip>("click");
        climb = Resources.Load<AudioClip>("climb");
        drill = Resources.Load<AudioClip>("drill");
        run = Resources.Load<AudioClip>("run");
    }
    public static void playWin()
    {
        audioSrc.PlayOneShot(win);
    }
    public static void playLose()
    {
        audioSrc.PlayOneShot(lose);
    }
    public static void playPlaceLadder()
    {
        audioSrc.PlayOneShot(placeLadder);
    }
    public static void playOxygen()
    {
        audioSrc.PlayOneShot(oxygen);
    }
    public static void playFire()
    {
        audioSrc.PlayOneShot(fire);
    }
    public static void playOutfire()
    {
        audioSrc.PlayOneShot(outfire);
    }
    public static void playClick()
    {
        audioSrc.PlayOneShot(click);
    }
    public static void playClimb()
    {
        audioSrc.PlayOneShot(climb);
    }
    public static void playDrill()
    {
        audioSrc.PlayOneShot(drill);
    }
    public static void playRun()
    {
        audioSrc.PlayOneShot(run);
    }

}
