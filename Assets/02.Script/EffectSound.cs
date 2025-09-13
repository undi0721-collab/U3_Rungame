using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSound : MonoBehaviour
{
    static AudioSource audioSource;
    public static AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // audioClip = Resources.Load<AudioClip>("Coin_Sound");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void CoinSoundPlay()
    {
        audioClip = Resources.Load<AudioClip>("Coin_Sound");
        audioSource.PlayOneShot(audioClip);
    }

    public static void AttackSoundPlay()
    {
        audioClip = Resources.Load<AudioClip>("Attack_Sound");
        audioSource.PlayOneShot(audioClip);
    }

    public static void LeftSoundPlay()
    {
        audioClip = Resources.Load<AudioClip>("Line_Sound");
        audioSource.PlayOneShot(audioClip);
    }

    public static void RightSoundPlay()
    {
        audioClip = Resources.Load<AudioClip>("Line_Sound");
        audioSource.PlayOneShot(audioClip);
    }

    public static void JumpSoundPlay()
    {
        audioClip = Resources.Load<AudioClip>("Jump_Sound");
        audioSource.PlayOneShot(audioClip);
    }
} 
