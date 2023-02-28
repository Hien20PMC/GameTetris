using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioClip Movement;
    public AudioClip Drop;
    public AudioClip ClearLine;
    private AudioSource Sounds;
    public static Sound s_Sound;

    private void Awake()
    {
        s_Sound = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Sounds = GetComponent<AudioSource>();
    }

    public void SoundMovement()
    {
        Sounds.PlayOneShot(Movement);
    }

    public void SoundDrop()
    {
        Sounds.PlayOneShot(Drop);
    }

    public void SoundClearLine()
    {
        Sounds.PlayOneShot(ClearLine);
    }
}
