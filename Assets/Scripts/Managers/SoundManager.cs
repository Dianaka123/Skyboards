using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public AudioSource BackgrounSource;
    public AudioSource CollisionAsteroid;
    public AudioSource UiSource;

    [SerializeField] private Slider sliderUI;
    [SerializeField] private Slider sliderBack;

    private void Start()
    {
        sliderUI.onValueChanged.AddListener(delegate { ChangeUIVolume(); });
        sliderBack.onValueChanged.AddListener(delegate { ChangeUIVolume(); });
    }

    private void ChangeUIVolume()
    {
        CollisionAsteroid.volume = sliderUI.value;
        UiSource.volume = sliderUI.value;
    }
    
    private void ChangeBackgroundVolume()
    {
        BackgrounSource.volume = sliderBack.value;
    }

    private void OnDestroy()
    {
        sliderUI.onValueChanged.RemoveAllListeners();
        sliderBack.onValueChanged.RemoveAllListeners();
    }
}
