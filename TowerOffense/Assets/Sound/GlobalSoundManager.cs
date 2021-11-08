using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSoundManager : MonoBehaviour
{
    public AudioClip sfx_OnHover;
    public AudioClip sfx_Click;

    public static GlobalSoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlayError()
    {
        GameObject.Find("ErrorSFX").GetComponent<AudioSource>().Play();
    }
}
