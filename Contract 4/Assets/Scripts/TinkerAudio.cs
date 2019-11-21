

//using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TinkerAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip failAudioClip;
    private AudioClip successAudioClip;
    private int currentFailFrequency;
    private int currentSuccessFreqency;
    public int maxFrequency;
    public TextMeshProUGUI failFrequencyText;
    public TextMeshProUGUI successFrequencyText;
    public int maxSampleRate;
    private int currentFailSampleRate;
    private int currectSuccessSampleRate;

    [SerializeField]
    private Slider failFrequencySlider;

    [SerializeField]
    private Slider successFrequencySlider;

    [SerializeField]
    private Slider successSampleRateSlider;

    [SerializeField]
    private Slider failSampleRateSlider;

    /// <summary>
    /// Sets the starting frequency to the max frequency allowed
    /// Whilst also setting the slider to be at its max value 
    /// so that when the slider is moved it represents what the current frequency is
    /// </summary>

    private void Start()
    {
        // Sets up the frequency sliders
        currentFailFrequency = maxFrequency;
        currentSuccessFreqency = maxFrequency;
        failFrequencySlider.value = 1;
        successFrequencySlider.value = 1;

        //sets up the sample rate sliders
        currentFailSampleRate = maxSampleRate;
        currectSuccessSampleRate = maxSampleRate;
        successSampleRateSlider.value = 1;
        failSampleRateSlider.value = 1;

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        FailSliderFrequency();
        SuccessSliderFrequency();
        FailSliderSampleRate();
        SuccessSliderSampleRate();
    }

    /// <summary>
    /// Uses the slider to update the frequency variable
    /// </summary>
    public void FailSliderFrequency()
    {
        currentFailFrequency = Mathf.RoundToInt(failFrequencySlider.value * maxFrequency);
        failFrequencyText.text = "Frequency: " + currentFailFrequency.ToString();
    }

    public void SuccessSliderFrequency()
    {
        currentSuccessFreqency = Mathf.RoundToInt(successFrequencySlider.value * maxFrequency);
        successFrequencyText.text = "Frequency: " + currentSuccessFreqency.ToString();
    }

    public void FailSliderSampleRate()
    {
        currentFailSampleRate = Mathf.RoundToInt(failSampleRateSlider.value * maxSampleRate);

    }

    public void SuccessSliderSampleRate()
    {
        currectSuccessSampleRate = Mathf.RoundToInt(successSampleRateSlider.value * maxSampleRate);
    }


    /// <summary>
    /// This calls the function CreateToneAudioClip, giving it a value for the frequency
    /// After the audioclip has been generated it will then play the audio clip that was preduced
    /// when the button is clicked
    /// </summary>
    public void PlayFailTone()
    {
        failAudioClip = CreateFailToneAudioClip(currentFailFrequency);
        audioSource.PlayOneShot(failAudioClip);
    }

    public void PlaySuccessTone()
    {
        successAudioClip = CreateSuccessToneAudioClip(currentSuccessFreqency);
        audioSource.PlayOneShot(successAudioClip);
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
    private AudioClip CreateFailToneAudioClip(int frequency)
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

    private AudioClip CreateSuccessToneAudioClip(int frequency)
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

}