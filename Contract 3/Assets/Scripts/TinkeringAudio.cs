using UnityEngine;

public class TinkeringAudio : MonoBehaviour
{
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
}
