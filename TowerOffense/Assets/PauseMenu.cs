using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject VolumeSlider;
    public Slider Slider;
    
    private void Start()
    {
        VolumeSlider = GameObject.Find("VolumeSlider"); //gameobject with Slider on it
        Slider = VolumeSlider.GetComponent<Slider>();
    }

    public void SetVolume()
    {
        AudioListener.volume = Slider.value;
    }
}
