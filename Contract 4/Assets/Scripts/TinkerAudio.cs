

//using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TinkerAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip outAudioClip;
    private int currentFrequency;
    public int maxFrequency;
    public TextMeshProUGUI frequencyText;

    [SerializeField]
    private Slider frequencySlider;

    /// <summary>
    /// Sets the starting frequency to the max frequency allowed
    /// Whilst also setting the slider to be at its max value 
    /// so that when the slider is moved it represents what the current frequency is
    /// </summary>

    private void Start()
    {
        currentFrequency = maxFrequency;
        frequencySlider.value = 1;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        SliderFrequency();
    }

    /// <summary>
    /// Uses the slider to update the frequency variable
    /// </summary>
    public void SliderFrequency()
    {
        currentFrequency = Mathf.RoundToInt(frequencySlider.value * maxFrequency);
        frequencyText.text = "Frequency: " + currentFrequency.ToString();
    }


    /// <summary>
    /// This calls the function CreateToneAudioClip, giving it a value for the frequency
    /// After the audioclip has been generated it will then play the audio clip that was preduced
    /// when the button is clicked
    /// </summary>
    public void PlayOutAudio()
    {
        outAudioClip = CreateToneAudioClip(currentFrequency);
        audioSource.PlayOneShot(outAudioClip);
    }


    public void StopAudio()
    {
        audioSource.Stop();
    }


    /// <summary>
    /// Within this funtion it sets the duration of the audio clip to be 0.2 seconds
    /// It will then set the sample rate to be 44100
    /// As the duation is set to a float it needs to be rounded when * by the sample rate to get the sample length
    /// This is then used to get the levels of sound from a sine wave.
    /// </summary>
    /// <param name="frequency"></param>
    /// <returns>
    /// The audio clip to be used for the button
    /// </returns>
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

}