using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    //public AudioMixer unityAudioMixer;

    private GameObject SliderFunction;

    private void Start()
    {
        GameObject.Find("SliderFunction");
        SliderFunction.GetComponent<Slider>();
    }
    
    public void SetVolume(float volume)
    {
        //unityAudioMixer.SetFloat("volume", volume);
        
        //change audiolistener volume instead

        AudioListener.volume.Equals(SliderFunction);
    }
}
