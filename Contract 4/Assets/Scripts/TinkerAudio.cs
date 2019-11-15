//using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class TinkerAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip outAudioClip;
    private int currentFrequency;
    public int maxFrequency;

    [SerializeField]
    private Slider frequencySlider;


    // Start is called before the first frame update
    void Start()
    {
        currentFrequency = maxFrequency;
        frequencySlider.value = 1;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        SliderFrequency();
    }

    public void SliderFrequency()
    {
        currentFrequency = Mathf.RoundToInt(frequencySlider.value * maxFrequency); 
    }


    // Public APIs
    public void PlayOutAudio()
    {
        outAudioClip = CreateToneAudioClip(currentFrequency);
        audioSource.PlayOneShot(outAudioClip);
    }


    public void StopAudio()
    {
        audioSource.Stop();
    }


    // Private 
    private AudioClip CreateToneAudioClip(int frequency)
    {
        float sampleDurationSecs = 0.2f;
        int sampleRate = 44100;
        int sampleLength = Mathf.FloorToInt(sampleRate * sampleDurationSecs);
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

    private void ChangeVolume()
    {

    }


#if UNITY_EDITOR
    //[Button("Save Wav file")]
    private void SaveWavFile()
    {
        string path = EditorUtility.SaveFilePanel("Where do you want the wav file to go?", "", "", "wav");
        var audioClip = CreateToneAudioClip(1500);
        SaveWavUtil.Save(path, audioClip);
    }
#endif
}