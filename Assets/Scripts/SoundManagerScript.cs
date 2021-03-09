using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip gunShot,rifleShot,jumpSound,playerHurtSound, threeBulletPowUpSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        gunShot = Resources.Load<AudioClip>("gunShot");
        rifleShot = Resources.Load<AudioClip>("rifleShot");
        jumpSound= Resources.Load<AudioClip>("jumpSound");
        playerHurtSound= Resources.Load<AudioClip>("playerHurtSound");
        threeBulletPowUpSound = Resources.Load<AudioClip>("threeBulletPowUpSound");

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
            case "rifleShot":
                audioSrc.PlayOneShot(rifleShot,0.25f);
                break;
            case "jumpSound":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "playerHurtSound":
                audioSrc.PlayOneShot(playerHurtSound);
                break;
            case "threeBulletPowUpSound":
                audioSrc.PlayOneShot(threeBulletPowUpSound);
                break;
        }
    }
}
