

//using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TinkerAudio : MonoBehaviour
{
    private AudioSource audioSource;
    // The Success and Fail audio clips
    private AudioClip failAudioClip;
    private AudioClip successAudioClip;
    // Variables for frequency
    public int maxFrequency;
    private int currentFailFrequency;
    private int currentSuccessFreqency;
    public TextMeshProUGUI failFrequencyText;
    public TextMeshProUGUI successFrequencyText;
    // Variables for samplerate
    public int maxSampleRate;
    private int currentFailSampleRate;
    private int currectSuccessSampleRate;
    public TextMeshProUGUI failSampleRateText;
    public TextMeshProUGUI successSampleRateText;
    // Variables for duration
    public float maxDurationSecs;
    private float currentSuccessSampleDurationSecs;
    private float currentFailSampleDurationSecs;
    public TextMeshProUGUI failDurationText;
    public TextMeshProUGUI successDurationText;

    // Frequency sliders
    [SerializeField]
    private Slider failFrequencySlider;

    [SerializeField]
    private Slider successFrequencySlider;

    // Sample rate sliders
    [SerializeField]
    private Slider successSampleRateSlider;

    [SerializeField]
    private Slider failSampleRateSlider;

    // Duration sliders
    [SerializeField]
    private Slider successDurationSlider;

    [SerializeField]
    private Slider failDurationSlider;

    /// <summary>
    /// Sets all the changing variables to their max value
    /// Whilst also setting the slider to be at its max value 
    /// so that when the slider is moved it represents what the variable actually is
    /// </summary>

    private void Start()
    {
        // Sets up the frequency sliders
        currentFailFrequency = maxFrequency;
        currentSuccessFreqency = maxFrequency;
        failFrequencySlider.value = 1;
        successFrequencySlider.value = 1;

        // Sets up the sample rate sliders
        currentFailSampleRate = maxSampleRate;
        currectSuccessSampleRate = maxSampleRate;
        successSampleRateSlider.value = 1;
        failSampleRateSlider.value = 1;

        // Sets up the duartion sliders
        currentFailSampleDurationSecs = maxDurationSecs;
        currentSuccessSampleDurationSecs = maxDurationSecs;
        failDurationSlider.value = 1;
        successDurationSlider.value = 1;

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Checks for updates to the frequency sliders
        FailSliderFrequency();
        SuccessSliderFrequency();

        // Checks for updates to the samplerate sliders
        FailSliderSampleRate();
        SuccessSliderSampleRate();

        // Checks for updates to the duration sliders
        FailSliderDuration();
        SuccessSliderDuration();

    }

    /// <summary>
    /// Uses the slider to update the frequency of the sound
    /// Also updates the text field to include the current frequency
    /// </summary>
    public void FailSliderFrequency()
    {
        currentFailFrequency = Mathf.RoundToInt(failFrequencySlider.value * maxFrequency);
        failFrequencyText.text = "Frequency: " + currentFailFrequency.ToString();
    }

    /// <summary>
    /// Uses the slider to update the frequency of the sound
    /// Also updates the text field to include the current frequency
    /// </summary>
    public void SuccessSliderFrequency()
    {
        currentSuccessFreqency = Mathf.RoundToInt(successFrequencySlider.value * maxFrequency);
        successFrequencyText.text = "Frequency: " + currentSuccessFreqency.ToString();
    }

    /// <summary>
    /// Uses the slider to update the sample rate
    /// Also updates the text field for the sample rate to include the current sample rate
    /// </summary>
    public void FailSliderSampleRate()
    {
        currentFailSampleRate = Mathf.RoundToInt(failSampleRateSlider.value * maxSampleRate);
        failSampleRateText.text = "Sample rate: " + currentFailSampleRate.ToString();
    }

    /// <summary>
    /// Uses the slider to update the sample rate
    /// Also updates the text field for the sample rate to include the current sample rate
    /// </summary>
    public void SuccessSliderSampleRate()
    {
        currectSuccessSampleRate = Mathf.RoundToInt(successSampleRateSlider.value * maxSampleRate);
        successSampleRateText.text = "Sample rate: " + currectSuccessSampleRate.ToString();
    }

    /// <summary>
    /// Uses the slider to change the duration of the sound
    /// Also updates the text field to show the current duration
    /// </summary>
    public void FailSliderDuration()
    {
        currentFailSampleDurationSecs = failDurationSlider.value * maxDurationSecs;
        currentFailSampleDurationSecs = Mathf.Round(currentFailSampleDurationSecs * 100) / 100;
        failDurationText.text = "Duration: " + currentFailSampleDurationSecs.ToString();
    }

    /// <summary>
    /// Uses the slider to change the duration of the sound
    /// Also updates the text field to show the current duration
    /// </summary>
    public void SuccessSliderDuration()
    {
        currentSuccessSampleDurationSecs = successDurationSlider.value * maxDurationSecs;
        currentSuccessSampleDurationSecs = Mathf.Round(currentSuccessSampleDurationSecs * 100) / 100;
        successDurationText.text = "Duration: " + currentSuccessSampleDurationSecs.ToString();
    }




    /// <summary>
    /// This calls the function CreateFailToneAudioClip, giving it the required values that are needed to generate the sound
    /// After the audioclip has been generated it will then play the audio clip that was preduced
    /// when the fail button is clicked
    /// </summary>
    public void PlayFailTone()
    {
        failAudioClip = CreateFailToneAudioClip(currentFailFrequency, currentFailSampleDurationSecs, currentFailSampleRate);
        audioSource.PlayOneShot(failAudioClip);
    }

    /// <summary>
    /// This calls the function CreateSuccessToneAudioClip, giving it the required values that are needed to generate the sound
    /// After the audioclip has been generated it will then play the audio clip that was preduced
    /// when the success button is clicked
    /// </summary>
    public void PlaySuccessTone()
    {
        successAudioClip = CreateSuccessToneAudioClip(currentSuccessFreqency, currentSuccessSampleDurationSecs, currectSuccessSampleRate);
        audioSource.PlayOneShot(successAudioClip);
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }

    /// <summary>
    /// Generates a sound from a sine wave by using different values
    /// </summary>
    /// <param name="frequency"></param>
    /// <param name="duration"></param>
    /// <param name="sampleRate"></param>
    /// <returns>
    /// The audio clip to be used for the fail button
    /// </returns>
    private AudioClip CreateFailToneAudioClip(int frequency, float duration, int sampleRate)
    {
        int sampleLength = Mathf.FloorToInt(sampleRate * duration);
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
    /// Generates a sound from a sine wave by using different values
    /// </summary>
    /// <param name="frequency"></param>
    /// <param name="duration"></param>
    /// <param name="sampleRate"></param>
    /// <returns>
    /// The audio clip to be used for the success button
    /// </returns>
    private AudioClip CreateSuccessToneAudioClip(int frequency, float duration, int sampleRate)
    {
        int sampleLength = Mathf.FloorToInt(sampleRate * duration);
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