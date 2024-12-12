using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------Audio Source----------")]

    [SerializeField] AudioSource bgMusic;
    [SerializeField] AudioSource sfxMusic;

    [Header("----------Audio Clips----------")]

    public AudioClip background;
    public AudioClip jump;
    public AudioClip slide;
    public AudioClip objectcollide;

    private void Start()
    {
        bgMusic.clip = background;
        bgMusic.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxMusic.PlayOneShot(clip);
    }

}
