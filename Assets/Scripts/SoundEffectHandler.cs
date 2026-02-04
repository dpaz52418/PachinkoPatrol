using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectHandler : MonoBehaviour
{
    public AudioClip bounce_sfx;
    public AudioSource audioSource;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        audioSource.PlayOneShot(clip); // Play the sound one time.
    }
}
