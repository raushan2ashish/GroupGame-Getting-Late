using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------Audio Source----------")]

    [SerializeField] public AudioSource bgMusic;
    [SerializeField] public AudioSource sfxMusic;

    [Header("----------Audio Clips----------")]

    public AudioClip background;
    public AudioClip jump;
    public AudioClip objectcollide;
    public AudioClip attack;
    public AudioClip glide;
    public AudioClip damage;
    public AudioClip lostlife;
    public AudioClip gameOver;

    
    

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
