using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioSource musicaFundo;
    public AudioClip[] Musicaclip;

    [SerializeField]
    Slider volumeSlider;


    void Start()
    {
        AudioClip musicDessaFase = Musicaclip[0];
        musicaFundo.clip = musicDessaFase;
        musicaFundo.Play();
    }
    
    void Update()
    { 
        
    }

    // Método para parar a música de fundo
    public void StopSound()
    {
        musicaFundo.Stop();
    }
    
    //criando slider que que aumentara o volume com tambem diminui 
    public void ChangeVolume(){
        
        AudioListener.volume = volumeSlider.value;

        
    }
}
