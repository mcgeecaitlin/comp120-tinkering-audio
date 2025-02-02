﻿using UnityEngine;
// Link to github: https://github.com/mcgeecaitlin/comp120-tinkering-audio
public class TinkeringAudio : MonoBehaviour
{
    [SerializeField]
    private int frequency;
    private AudioClip currentClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // (freq, dur(optional))
    public AudioClip CreateToneAudioClip(int frequency, int sampleDurationSecs = 1)
    {
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDurationSecs; // How long the samples array will be.
        float maxValue = 1f / 4f;
        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);
        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
        }
        audioClip.SetData(samples, 0);
        return audioClip;
    }
    /// <summary>
    /// Converts any audio clip waves into a square wave.
    /// </summary>
    /// <param name="clip"></param>
    /// <returns> Returns an audio clip that is now a square wave.</returns>
    private AudioClip ConvertToSquare(AudioClip clip)
    {
        float[] samples = new float[clip.samples * clip.channels];
        clip.GetData(samples, 0);

        for (int i = 0; i < samples.Length; i++)
        {
            if (samples[i] > 0) samples[i] = 1f;
            else if (samples[i] < 0) samples[i] = -1f;
        }

        clip.SetData(samples, 0);
        return clip;
    }

    private void Update()
    {
        ChangeWaveType();
    }
    /// <summary>
    /// Receives player input from the keyboard and plays a sound based on the input. 
    /// Switches between sine waves and square waves.
    /// </summary>
    private void ChangeWaveType()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentClip = CreateToneAudioClip(frequency, 1);
            audioSource.PlayOneShot(currentClip);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentClip = CreateToneAudioClip(frequency, 1);
            currentClip = ConvertToSquare(currentClip);
            audioSource.PlayOneShot(currentClip);
        }
    }
}
