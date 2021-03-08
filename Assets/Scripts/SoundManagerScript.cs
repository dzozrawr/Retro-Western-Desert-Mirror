using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip gunShot;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        gunShot = Resources.Load<AudioClip>("gunShot");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "gunShot":
                audioSrc.PlayOneShot(gunShot);
                break;
        }
    }
}
