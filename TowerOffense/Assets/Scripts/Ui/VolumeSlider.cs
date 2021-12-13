using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public GameObject volumeSlider;
    public Slider Slider;
    
    private void Awake()
    {
        volumeSlider = GameObject.Find("VolumeSlider"); //gameobject with Slider on it
        Slider = volumeSlider.GetComponent<Slider>();
    }

    public void SetVolume()
    {
        AudioListener.volume = Slider.value;
    }
}
