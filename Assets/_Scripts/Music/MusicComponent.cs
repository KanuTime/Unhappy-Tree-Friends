using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MusicComponent : MonoBehaviour
{
    
     
    [SerializeField] private Slider _natureIntensity;
    [SerializeField] private Slider _humanIntensity;


    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public AudioClip audioClip3;
    AudioSource myAudioSource1;
    AudioSource myAudioSource2;
    AudioSource myAudioSource3;
 

    void Start()
    {
        myAudioSource1 = AddAudio(true, true, 0.3f);
        myAudioSource2 = AddAudio(true, true, 0.3f);
        myAudioSource3 = AddAudio(true, true, 0.3f);
        StartPlayingSounds();
    }


    public AudioSource AddAudio(bool loop, bool playAwake, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        //newAudio.clip = clip; 
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        return newAudio;
    }


    void StartPlayingSounds()
    {
        myAudioSource1.clip = audioClip1;
        myAudioSource1.Play();
        myAudioSource2.clip = audioClip2;
        myAudioSource2.Play();
        myAudioSource3.clip = audioClip3;
        myAudioSource3.Play();    
    }


    // Update is called once per frame
    void Update()
    {
        if (_natureIntensity.value > _humanIntensity.value)
        {
            myAudioSource2.volume = (_natureIntensity.value);
            myAudioSource3.volume = (_humanIntensity.value / 4);
        }

        if (_natureIntensity.value <= _humanIntensity.value)
        {
            myAudioSource2.volume = (_natureIntensity.value / 4);
            myAudioSource3.volume = (_humanIntensity.value);
        }

       // myAudioSource2.volume = (_natureIntensity.value / 2);
       // myAudioSource3.volume = (_humanIntensity.value / 2);

    }

}
