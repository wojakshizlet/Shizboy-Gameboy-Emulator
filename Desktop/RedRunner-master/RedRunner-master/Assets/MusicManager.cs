using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    public AudioSource[] music;
    public AudioMixer mixer;
    public Boolean music1FadeIn = true;
    public Boolean music1FadeOut = false;
    private float mute = -60f;
    private float maxVolume = 1.0f;
    private float currentVolume = -60f;
    private int ID = 0;
    private float musicTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

        mixer.SetFloat("MusicVolume", mute);
        music[0].Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (ID == 0) {
            if (music[0].time <= 0.0f) {
                music[0].Play();
            }
            musicTime = music[0].time;
        }

        if (ID == 1)
        {
            if (music[1].time <= 0.0f)
            {
                music[1].Play();
            }
            musicTime = music[1].time;
        }


        if (music1FadeIn) {
            StartCoroutine(fadeMusicIn());

            if (currentVolume >= maxVolume) {
                music1FadeIn = false;
                //currentVolume = 1.5f;
            }
        }

        if (ID == 0 && musicTime >= 32.0f) { 
            StartCoroutine(fadeMusicOut());

            if (currentVolume == mute) {
                musicTime = 0;
                music[0].time = 0;

                music[0].Stop();
                ID = 1;

                music1FadeIn = true; 
            }
        }

        if (ID == 1 && musicTime >= 96.0f)
        {
            StartCoroutine(fadeMusicOut());

            if (currentVolume == mute)
            {
                musicTime = 0;
                music[1].time = 0;

                music[1].Stop();
                ID = 0;

                music1FadeIn = true;
            }
        }

        mixer.SetFloat("MusicVolume", currentVolume);
    }

    IEnumerator fadeMusicIn()
    {
        currentVolume += 0.6f;

        if (currentVolume >= 2.0f) {
            currentVolume = 2.0f;
        }

        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator fadeMusicOut()
    {
        currentVolume -= 0.6f;

        yield return new WaitForSeconds(0.1f);
    }
}
