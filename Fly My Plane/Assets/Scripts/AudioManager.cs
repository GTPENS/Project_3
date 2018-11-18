using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance = null;
    public AudioClip[] clips;
    
    private AudioSource sourceSFX;
    private AudioSource sourceSFX_other;
    private Slider volumeSlider;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    // Use this for initialization
    void Start()
    {
        sourceSFX = GetComponent<AudioSource>();
        sourceSFX_other = GameObject.Find("OtherSFX").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (volumeSlider == null)
        {
            volumeSlider = GameObject.Find("slider_sound").GetComponent<Slider>();
        }
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }

    public void PlayAudio(int _index)
    {
        sourceSFX.clip = clips[_index];
        sourceSFX.Play();
    }

    public void PlayOtherSFX(int _index)
    {
        sourceSFX_other.clip = clips[_index];
        sourceSFX_other.Play();
    }

    public void StopAudio()
    {
        sourceSFX.Stop();
        sourceSFX_other.Stop();
    }
}
